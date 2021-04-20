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
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            //WindowState = FormWindowState.Minimized;
            ClientSize = new Size(700, 700);
            
        }

        public MainForm()
        {
            //InitializeComponent();
            var levels = LoadLevels().ToArray();
            var scenePainter = new ScenePainter(levels);
            var terrainControl = new TerrainControl(scenePainter);
            Controls.Add(terrainControl);
            //var gameControl = new GameControl();
            //Controls.Add(gameControl);
        }

        private static IEnumerable<Map> LoadLevels()
        {
            yield return Map.FromText(Properties.Resources.TestMap);
        }
    }
}
