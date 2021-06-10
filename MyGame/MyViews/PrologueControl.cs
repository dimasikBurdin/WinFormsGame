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

            var button = new Button();
            button.Size = new Size(60, 30);
            button.Left = label.Right / 2 + button.Width / 2;
            button.Top = label.Bottom + 20;
            button.BackColor = Color.White;
            button.Text = "Далее";

            button.Click += Button_Click;
            Controls.Add(button);
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
                MainForm.PrologueControl.Hide();
                MainForm.TerrainControl.Show();
                MainForm.TerrainControl.Timer.Start();
                MainForm.TerrainControl.Focus();
                IsFinished = true;
                return;
            }
            label.Text = messages.First();
            messages.RemoveAt(0);
        }
    }
}
