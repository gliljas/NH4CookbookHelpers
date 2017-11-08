using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace NH4CookbookHelpers
{
    public partial class WindowsFormsRunner : Form
    {
        public WindowsFormsRunner()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            var recipeTypes = RecipeLoader.GetRecipeTypes();
            listBox1.DataSource = recipeTypes;
            listBox1.DisplayMember = "Name";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RunSelected();
        }

        private void RunSelected()
        {
            var recipeType = listBox1.SelectedItem as RecipeDescriptor;
            //tabControl1.TabPages.Clear();
            var form = new RecipeForm(recipeType);
            form.ShowDialog();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            RunSelected();
        }
    }
}
