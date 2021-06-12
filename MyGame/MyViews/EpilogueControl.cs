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
    public partial class EpilogueControl : UserControl
    {
        private Label label;
        private readonly List<string> messages = new List<string>();
        public bool IsFinished { get; set; }

        public EpilogueControl()
        {
            messages = LoadEpilogueMessage().ToList();

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

            nextButton.Click += NextButton_Click;
            Controls.Add(nextButton);
        }
          
        private void NextButton_Click(object sender, EventArgs e)
        {
            if(messages.Count == 0)
            {
                Application.Restart();
                return;
            }

            label.Text = messages.First();
            messages.RemoveAt(0);
        }

        private static IEnumerable<string> LoadEpilogueMessage()
        {
            yield return Properties.Resources.Epilogue1;
            yield return Properties.Resources.Epilogue2;
            yield return Properties.Resources.Epilogue3;
            yield return Properties.Resources.Epilogue4;
        }
    }
}
