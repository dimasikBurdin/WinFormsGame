using MyGameModel.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGameModel.Views
{
    public partial class MessageBoxControl : UserControl//а зачем ты нужен...
    {
        public string Quest { get; set; }
        public DialogResult Result { get; set; }

        public MessageBoxControl()
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
            label.Text = "              Вы проиграли. \nНажмите любую клавишу для \n            продолжения...";
            Controls.Add(label);

            //var yesButton = new Button();
            //yesButton.BackColor = Color.RosyBrown;
            //yesButton.Size = new Size(80, 40);
            //yesButton.Left = ClientSize.Width / 2 - yesButton.Width / 2;
            //yesButton.Top = label.Bottom + 5;
            //yesButton.Font = new Font(FontFamily.GenericSerif, 13, FontStyle.Regular);
            //yesButton.ForeColor = Color.GhostWhite;
            //yesButton.Text = "Да";
            //Controls.Add(yesButton);

            //var noButton = new Button();
            //noButton.BackColor = Color.RosyBrown;
            //noButton.Size = new Size(80, 40);
            //noButton.Left = ClientSize.Width / 2 - noButton.Width / 2 + noButton.Width;
            //noButton.Top = label.Bottom + 5;
            //noButton.Font = new Font(FontFamily.GenericSerif, 13, FontStyle.Regular);
            //noButton.ForeColor = Color.GhostWhite;
            //noButton.Text = "Нет";
            //Controls.Add(noButton);

            //yesButton.Click += YesButton_Click;
            Click += MessageBoxControl_Click;
            KeyPress += MessageBoxControl_KeyPress;
            //noButton.Click += NoButton_Click;
        }

        private void MessageBoxControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            Application.Restart();
        }

        private void MessageBoxControl_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        //private void NoButton_Click(object sender, EventArgs e)
        //{
        //    Result = DialogResult.No;
        //    MainForm.MessageBox.Hide();
        //}

        //private void YesButton_Click(object sender, EventArgs e)
        //{
        //    //Result = DialogResult.Yes;
        //    if (MainForm.Game.CurrentGameStage == GameStage.GameOver)
        //    {
        //        Application.Restart();
        //    }
        //    //MainForm.MessageBox.Hide();
        //    //MainForm.TerrainControl.Hide();
        //    //MainForm.MainMenu.Show();

        //}
    }
}
