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
        //index / zIndex
        //в форме есть событие, принимающее нажатие клафиш. Для управления написать методы в моделе (в игроке например), которые будут реагировать на эти нажатия
        public static Label label;
        private TerrainControl terrainControl;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            ClientSize = new Size(terrainControl.ClientSize.Width + 100, terrainControl.ClientSize.Height);
        }

        public MainForm()
        {
            BackColor = Color.LightGray;
            //InitializeComponent();
            label = new Label()
            {
                Size = new Size(100, 40),
                Top = 0,
                Left = 710,
                Font = new Font(FontFamily.GenericSerif, 30, FontStyle.Bold),
                ForeColor = Color.Purple
                };

            var levels = LoadLevels().ToArray();
            var scenePainter = new ScenePainter(levels);
            terrainControl = new TerrainControl(scenePainter);
            Controls.Add(terrainControl);
            Controls.Add(label);
        }

        private static IEnumerable<Map> LoadLevels()
        {
            yield return Map.FromText(Properties.Resources.TestMap);
        }
    }
}
