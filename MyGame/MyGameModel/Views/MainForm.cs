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
        public MainForm()
        {
            InitializeComponent();
            var levels = LoadLevels().ToArray();

            //var gameControl = new GameControl();
            //Controls.Add(gameControl);
        }

        private static IEnumerable<Map> LoadLevels()
        {
            yield return Map.FromText(Properties.Resources.TestMap);
        }
    }
}
