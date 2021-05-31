//using MyGameModel.Views;
//using MyViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameModelNew.Domain
{
    public class Game
    {
        public Map CurrentMap;
        public List<Map> AllMaps;
        public  GameStage CurrentGameStage { get; set; }

        //public static void Over()
        //{
        //    CurrentGameStage = GameStage.GameOver;
        //    //MainForm.TerrainControl.Timer.Stop();
        //    //MainForm.MessageBox.Show();
        //    //MainForm.MessageBox.Focus();            
        //    //MainForm.TerrainControl.SendToBack();//-> в mainForm
        //}
    }
}
