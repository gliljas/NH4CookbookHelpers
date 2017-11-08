using System;
using System.IO;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace NH4CookbookHelpers
{
    public class ControlWriter : TextWriter
    {
        private TextBox textbox;
        private Timer _timer;
        private object lockObj=new object();
        private StringBuilder _buffer;

        public ControlWriter(TextBox textbox)
        {
            _timer=new Timer();
            _timer.Interval = 10;
            _timer.Elapsed += _timer_Elapsed;
            _buffer =new StringBuilder();
            this.textbox = textbox;
            _timer.Enabled = true;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (lockObj)
            {
                FlushBuffer();
            }
        }

        private void FlushBuffer()
        {
            if (textbox.InvokeRequired)
            {
                textbox.Invoke(new Action(() => FlushBuffer()));
            }
            else
            {
                textbox.AppendText(_buffer.ToString());
                _buffer.Clear();
            }
        }

        public override void Write(char value)
        {
            lock (lockObj)
            {
                _buffer.Append(value);
            }
        }

        public override void Write(string value)
        {
            lock (lockObj)
            {
                _buffer.Append(value);
            }
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
}