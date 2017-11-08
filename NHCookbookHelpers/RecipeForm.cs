using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace NH4CookbookHelpers
{
    public partial class RecipeForm : Form, IRecipeLogger
    {
        private RecipeDescriptor _recipeType;



        public RecipeForm(RecipeDescriptor recipeType)
        {
            this._recipeType = recipeType;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Text = _recipeType.Name;
            Console.SetOut(new ControlWriter(textBox1));
            Console.SetError(new ControlWriter(textBox1));
            var thread = new Thread(() => RecipeLoader.RunRecipe(_recipeType.Type, this));
            thread.Start();
        }

        private void AddToLog(string log)
        {
            if (listView1.InvokeRequired)
            {
                listView1.Invoke(new Action(() => AddToLog(log)));
                return;
            }
            listView1.Items.Add(log);
        }

        

        public void ShowTable(string name, DataTable schema, DataTable dataTable)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ShowTable(name,schema,dataTable)));
                return;
            }
            var tabPage = new TabPage(name);
            var grid = new DataGridView
            {
                DataSource = dataTable,
                EditMode = DataGridViewEditMode.EditProgrammatically,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                Dock = DockStyle.Fill
            };
            tabPage.Controls.Add(grid);
            tabControl2.TabPages.Add(tabPage);
        }

        public void LogMessage(string message)
        {
            AddToLog(message);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                textBox2.Text= listView1.SelectedItems[0].Text;
            }
        }
    }

}
