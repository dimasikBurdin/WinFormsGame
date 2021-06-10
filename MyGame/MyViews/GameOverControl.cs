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
    public partial class GameOverControl : UserControl
    {
        public string Quest { get; set; }
        public DialogResult Result { get; set; }

        public GameOverControl()
        {
            DoubleBuffered = true;

            ClientSize = new Size(300, 130);
            BackColor = Color.SandyBrown;

            var label = new Label();
            label.BackColor = Color.Transparent;
            label.Size = new Size(280, 80);
            label.Left = ClientSize.Width / 2 - label.Width / 2;
            label.Top = ClientSize.Height / 2 - 40;
            label.Font = new Font(FontFamily.GenericSerif, 15, FontStyle.Regular);
            label.ForeColor = Color.GhostWhite;
            label.Text = "Вы проиграли. \nНажмите любую клавишу для \nпродолжения...";
            label.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(label);

            KeyPress += MessageBoxControl_KeyPress;
        }

        private void MessageBoxControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            Application.Restart();
        }
    }
}
