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
        private readonly Button startButton;
        private readonly Button exitButton;
        private bool restartButtonIsAdd;


        public MenuControl()
        {
            DoubleBuffered = true;

            var terrainControl = MainForm.TerrainControl;
            ClientSize = terrainControl.ClientSize;
            BackgroundImage = Properties.Resources.Tree1;

            var label = new Label();
            label.BackColor = Color.Transparent;
            label.Size = new Size(400, 50);
            label.Left = ClientSize.Width / 2 - label.Width / 2;
            label.Top = ClientSize.Height / 2 - 110;
            label.Font = new Font(FontFamily.GenericSerif, 30, FontStyle.Regular);
            label.ForeColor = Color.GhostWhite;
            //label.Text = "The best game";
            label.Text = "A way to uncharted";
            label.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(label);

            startButton = new Button();
            startButton.BackgroundImage = Properties.Resources.newGrass1;
            startButton.BackgroundImageLayout = ImageLayout.Stretch;
            startButton.Width = 150;
            startButton.Height = 35;
            startButton.Left = ClientSize.Width / 2 - startButton.Width / 2;
            startButton.Top = ClientSize.Height / 2 - startButton.Height;
            startButton.Font = new Font(FontFamily.GenericSerif, 16, FontStyle.Regular);
            startButton.ForeColor = Color.White;
            startButton.Text = "Start game";
            Controls.Add(startButton);

            exitButton = new Button();
            exitButton.BackgroundImage = Properties.Resources.newGrass1;
            exitButton.BackgroundImageLayout = ImageLayout.Stretch;
            exitButton.Width = 150;
            exitButton.Height = 35;
            exitButton.Left = ClientSize.Width / 2 - exitButton.Width / 2;
            exitButton.Top = ClientSize.Height / 2 - exitButton.Height + 50;
            exitButton.Font = new Font(FontFamily.GenericSerif, 16, FontStyle.Regular);
            exitButton.ForeColor = Color.White;
            exitButton.Text = "Exit game";
            Controls.Add(exitButton);

            startButton.Click += StartButton_Click;
            exitButton.Click += ExitButton_Click;
        }

        public void GamePause()
        {
            MainForm.TerrainControl.Hide();
            if (!restartButtonIsAdd)
            {
                restartButtonIsAdd = true;
                var restartButton = new Button();
                restartButton.Size = new Size(150, 35);
                restartButton.BackgroundImage = Properties.Resources.newGrass1;
                restartButton.BackgroundImageLayout = ImageLayout.Stretch;
                restartButton.Left = startButton.Left;
                restartButton.Top = startButton.Top + 50;
                restartButton.Text = "Restart game";
                restartButton.Font = new Font(FontFamily.GenericSerif, 16, FontStyle.Regular);
                restartButton.ForeColor = Color.White;
                restartButton.Click += RestartButton_Click;
                Controls.Add(restartButton);

                startButton.Text = "Continue";
                exitButton.Top += 50;
            }
            MainForm.MainMenu.Show();
            MainForm.TerrainControl.Timer.Stop();
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
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
