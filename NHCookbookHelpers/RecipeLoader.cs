using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using log4net;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;

namespace NH4CookbookHelpers
{
    public class RecipeLoader
    {
        public static ReadOnlyCollection<RecipeDescriptor> GetRecipeTypes()
        {
            var assembly = Assembly.GetEntryAssembly();

            var recipeTypes = assembly.GetReferencedAssemblies().Select(x => Assembly.Load((AssemblyName)x)).Union(new[] { assembly }).SelectMany(x => x.GetExportedTypes()).Where(x => typeof(IRecipe).IsAssignableFrom(x) && !x.IsAbstract).ToList().AsReadOnly();
            return recipeTypes.Select(x => new RecipeDescriptor { Type = x, Name = x.Namespace.Split('.').Last() }).OrderBy(x => x.Name).ToList().AsReadOnly();
        }

        public static RecipeResult RunRecipe(Type recipeType, IRecipeLogger logger)
        {
            using (var choosenRecipe = Activator.CreateInstance(recipeType) as IRecipe)
            {
                if (choosenRecipe != null)
                {
                    ConfigureLog4Net(x =>
            {
                x = ParseSql(x).Trim();
                if (choosenRecipe.LogEnabled)
                {
                    logger.LogMessage(x);
                }
            });

                    choosenRecipe.Initialize(logger);
                    choosenRecipe.Run();
                }
            }
            return null;
        }

        private static string ParseSql(string o)
        {
            var match = Regex.Match(o.Trim(), @",?(.p\d+) = (.+?)\[Type:([^\]]+)\]$", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
            while (match.Success)
            {
                var paramName = match.Groups[1].Value.Trim();
                var value = match.Groups[2].Value.Trim();
                var typeName = match.Groups[3].Value.Trim();
                o = o.Replace(match.Value, "").Trim();
                o = o.TrimEnd(',');
               // o = o.Replace(paramName, FormatParameter(value,typeName));
                match = Regex.Match(o.Trim(), @",?(.p\d+) = (.+?)\[Type:([^\]]+)\]$", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
            }

            return o.Trim();
        }

        //private static string FormatParameter(string value, string typeName)
        //{
        //    switch (typeName.Substring(0,typeName.IndexOf("(")-1))
        //    {
        //        case "Guid":
        //            return $"'{value.Replace("'","''")}'";
        //        default:
        //            return value;
        //    }
        //}

        public static Func<Configuration> DefaultConfiguration { get; set; }

        private static void ConfigureLog4Net(Action<string> action)
        {
            var hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.RemoveAllAppenders();
            var rootLogger = hierarchy.Root;
            rootLogger.Level = Level.Debug;

            var appenderNh = new DelegateAppender(action)
            {
                Name = "DelegateAppender",
                Layout = new PatternLayout("%message%newline")
            };

            appenderNh.ActivateOptions();

            var loggerNh = hierarchy.GetLogger("NHibernate.SQL") as Logger;
            //var loggerNhCache = hierarchy.GetLogger("NHibernate.Cache") as Logger;

            if (loggerNh != null)
            {
                loggerNh.RemoveAllAppenders();
                loggerNh.Level = Level.Debug;
                loggerNh.AddAppender(appenderNh);
            }

            //if (loggerNhCache != null)
            //{
            //    loggerNhCache.RemoveAllAppenders();
            //    loggerNhCache.Level = Level.Debug;
            //    loggerNhCache.AddAppender(appenderNh);
            //}

            // rootLogger.AddAppender(appenderNh);
            hierarchy.Configured = true;
        }
    }

    public class RecipeResult
    {
        public IList<string> Queries { get; set; }

    }
}