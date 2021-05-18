using MyGameModel.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGameModel.Domain
{
    public partial class MainForm : Form
    {
        //size +
        //index / zIndex +
        //в форме есть событие, принимающее нажатие клафиш. Для управления написать методы в моделе (в игроке например), которые будут реагировать на эти нажатия +
        public static Label label;
        public static TerrainControl TerrainControl { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            ClientSize = new Size(TerrainControl.ClientSize.Width + 100, TerrainControl.ClientSize.Height);
        }

        public MainForm()
        {
            var levels = LoadLevels().ToArray();
            var scenePainter = new ScenePainter(levels);
            TerrainControl = new TerrainControl(scenePainter);

            BackColor = Color.LightGray;
            label = new Label()
            {
                Size = new Size(100, 40),
                Top = 0,
                Left = TerrainControl.TerrainClientSize.Width + 10,
                Font = new Font(FontFamily.GenericSerif, 30, FontStyle.Bold),
                ForeColor = Color.Purple
            };
            
            Controls.Add(TerrainControl);
            Controls.Add(label);
        }

        private static IEnumerable<Map> LoadLevels()
        {
            yield return Map.FromText(Properties.Resources.TestMap);
            yield return Map.FromText(Properties.Resources.TestMap3);
            //yield return Map.FromText(Properties.Resources.TestMap_2);
        }
    }
}
