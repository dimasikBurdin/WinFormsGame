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
    public partial class MenuControl : UserControl
    {
        public MenuControl()
        {
            DoubleBuffered = true;

            var terrainControl = MainForm.TerrainControl;
            ClientSize = terrainControl.ClientSize;
            BackgroundImage = Properties.Resources.Tree1;

            var label = new Label();
            label.BackColor = Color.Transparent;
            label.Size = new Size(250, 50);
            label.Left = ClientSize.Width / 2 - label.Width / 2;
            label.Top = ClientSize.Height / 2 - 110;
            label.Font = new Font(FontFamily.GenericSerif, 30, FontStyle.Regular);
            label.ForeColor = Color.GhostWhite;
            label.Text = "The best game";
            Controls.Add(label);

            var startButton = new Button();
            startButton.BackgroundImage = Properties.Resources.newGrass1;
            startButton.BackgroundImageLayout = ImageLayout.Stretch;
            startButton.Width = 150;
            startButton.Height = 35;
            startButton.Left = ClientSize.Width / 2 - startButton.Width / 2;
            startButton.Top = ClientSize.Height / 2 - 40;
            startButton.Font = new Font(FontFamily.GenericSerif, 16, FontStyle.Regular);
            startButton.ForeColor = Color.White;
            startButton.Text = "Start game";
            Controls.Add(startButton);

            var exitButton = new Button();
            exitButton.BackgroundImage = Properties.Resources.newGrass1;
            exitButton.BackgroundImageLayout = ImageLayout.Stretch;
            exitButton.Width = 150;
            exitButton.Height = 35;
            exitButton.Left = ClientSize.Width / 2 - exitButton.Width / 2;
            exitButton.Top = ClientSize.Height / 2 + 20;
            exitButton.Font = new Font(FontFamily.GenericSerif, 16, FontStyle.Regular);
            exitButton.ForeColor = Color.White;
            exitButton.Text = "Exit game";
            Controls.Add(exitButton);

            startButton.Click += StartButton_Click;
            exitButton.Click += ExitButton_Click;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            MainForm.MainMenu.Hide();
            if (MainForm.PrologueControl.IsFinished)
            {
                MainForm.TerrainControl.Show();
                MainForm.TerrainControl.Timer.Start();
                MainForm.TerrainControl.Focus();
            }
            MainForm.PrologueControl.Show();            
        }
    }
}
