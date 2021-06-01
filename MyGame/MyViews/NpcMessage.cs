using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyViews
{
    public partial class NpcMessage : UserControl
    {
        public Label Label { get; set; }
        public List<string> Messages { get; set; }

        public NpcMessage()
        {
            Messages = new List<string>();
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            BackgroundImage = Properties.Resources.BackMessage;
            BackgroundImageLayout = ImageLayout.Stretch;
            Size = new Size(400, 150);            

            Label = new Label()
            {
                Size = new Size(350, 100),
                BackColor = Color.Transparent,
            };
            Label.Left = (ClientSize.Width - Label.Width) / 2;
            Label.Top = (ClientSize.Height - Label.Height) / 2;
            Label.Font = new Font(FontFamily.GenericSerif, 15, FontStyle.Regular);
            Label.ForeColor = Color.Black;            

            var button = new Button();
            button.BackColor = Color.Transparent;
            button.Size = new Size(50, 20);
            button.Left = Label.Right - button.Width;
            button.Top = Label.Bottom - button.Height;
            button.Text = "->";

            Controls.Add(button);
            Controls.Add(Label);

            button.Click += Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Messages.RemoveAt(0);            
            MainForm.ShowNpcMessages(Messages);
        }
    }
}
