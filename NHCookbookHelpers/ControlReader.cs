using System.IO;
using System.Windows.Forms;

namespace NH4CookbookHelpers
{
    public class ControlReader : TextReader
    {
        private Control textbox;
        public ControlReader(Control textbox)
        {
            this.textbox = textbox;
        }

        public override int Read()
        {

            return base.Read();
        }
    }
}