using MyGameModel.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameModel.Domain
{
    public class Game
    {
        public Map CurrentMap;
        public List<Map> AllMaps;
        public GameStage CurrentGameStage { get; set; }

        public void Over()
        {
            CurrentGameStage = GameStage.GameOver;
            MainForm.TerrainControl.Timer.Stop();
            MainForm.MessageBox.Show();
            MainForm.MessageBox.Focus();            
            MainForm.TerrainControl.SendToBack();
        }
    }
}
