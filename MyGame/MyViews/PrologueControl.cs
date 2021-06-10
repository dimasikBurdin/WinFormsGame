using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyViews
{
    public partial class PrologueControl : UserControl
    {
        private Label label;
        private readonly List<string> messages = new List<string>();
        public bool IsFinished { get; set; }
          
        public PrologueControl()
        {
            messages = LoadPrologueMessage().ToList();

            BackColor = Color.Black;
            label = new Label();
            label.Size = new Size(600, 400);
            label.BackColor = Color.Transparent; 
            label.Left = MainForm.TerrainControl.ClientSize.Width / 2 - label.Width / 2;
            label.Top = MainForm.TerrainControl.ClientSize.Height / 2 - label.Height / 2;
            label.Text = messages.First();
            messages.RemoveAt(0);
            label.TextAlign = ContentAlignment.MiddleCenter;

            label.Font = new Font(FontFamily.GenericSerif, 25, FontStyle.Regular);
            label.ForeColor = Color.White;

            Controls.Add(label);

            var nextButton = new Button();
            nextButton.Size = new Size(60, 30);
            nextButton.Left = label.Right / 2 + nextButton.Width / 2;
            nextButton.Top = label.Bottom + 20;
            nextButton.BackColor = Color.LightGray;
            nextButton.Text = "Далее";

            nextButton.Click += Button_Click;
            Controls.Add(nextButton);

            var skipButton = new Button();
            skipButton.Size = new Size(80, 30);
            skipButton.Left = label.Right - skipButton.Width;
            skipButton.Top = label.Bottom + 20;
            skipButton.BackColor = Color.LightGray;
            skipButton.Text = "Пропустить";

            skipButton.Click += SkipButton_Click;
            Controls.Add(skipButton);
        }

        private static IEnumerable<string> LoadPrologueMessage()
        {
            yield return Properties.Resources.Prologue1;
            yield return Properties.Resources.Prologue2;
            yield return Properties.Resources.Prologue3;
            yield return Properties.Resources.Prologue4;
            yield return Properties.Resources.Prologue5;
            yield return Properties.Resources.Prologue6;
            yield return Properties.Resources.Prologue7;
            yield return Properties.Resources.Prologue8;
            yield return Properties.Resources.Prologue9;
            yield return Properties.Resources.Training;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (messages.Count == 0)
            {
                StartGame();
                return;
            }

            label.Text = messages.First();
            messages.RemoveAt(0);
        }

        private void SkipButton_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void StartGame()
        {
            MainForm.PrologueControl.Hide();
            MainForm.TerrainControl.Show();
            MainForm.TerrainControl.Timer.Start();
            MainForm.TerrainControl.Focus();
            IsFinished = true;
        }
    }
}
