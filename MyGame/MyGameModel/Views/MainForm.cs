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

        private TerrainControl terrainControl;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            ClientSize = terrainControl.ClientSize;
        }

        public MainForm()
        {
            //InitializeComponent();
            var levels = LoadLevels().ToArray();
            var scenePainter = new ScenePainter(levels);
            terrainControl = new TerrainControl(scenePainter);
            Controls.Add(terrainControl);
        }

        private static IEnumerable<Map> LoadLevels()
        {
            yield return Map.FromText(Properties.Resources.TestMap);
        }
    }
}
