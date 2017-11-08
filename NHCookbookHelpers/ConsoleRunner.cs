using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NH4CookbookHelpers
{
    public class ConsoleRunner
    {
        public void Run()
        {
            var recipeTypes = RecipeLoader.GetRecipeTypes();
            if (!recipeTypes.Any())
            {
                Console.WriteLine("No recipes have been added to the project");
                return;
            }

            Console.WriteLine("Choose a recipe:");

            int choosenRecipe = 0;
            while (choosenRecipe == 0)
            {
                for (int i = 1; i <= recipeTypes.Count; i++)
                {
                    var recipeType = recipeTypes[i - 1];
                    Console.WriteLine(i + ". " + recipeType.Name);
                }
                if (!int.TryParse(Console.ReadLine(), out choosenRecipe))
                {
                    choosenRecipe = 0;
                }
            }

            var choosenRecipeType = recipeTypes[choosenRecipe - 1];

            RecipeLoader.RunRecipe(choosenRecipeType.Type,new NullLogger());

        }



        

        
    }

    public static class Helper
    {
        public static void Start()
        {
            var form = new WindowsFormsRunner();

            var t = new Thread(() => form.ShowDialog());
            t.Start();
            //form.ShowDialog();  
        }


    }
}
