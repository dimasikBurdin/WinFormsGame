using MyGameModel.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
//using Point = MyGameModel.Domain.Point;

namespace MyGameModel.Views
{
    public class ScenePainter
    {
        private int mapNumber;
        private Map[] maps = null;
        private Bitmap bitmap;
        private bool open = false;
        public SizeF Size => new SizeF(currentMap.Terrain.GetLength(0), currentMap.Terrain.GetLength(1));
        public Size LevelSize => new Size(currentMap.Terrain.GetLength(0), currentMap.Terrain.GetLength(1));

        public static Map currentMap = default;
        
        public static Player Player { get; set; }

        public ScenePainter(Map[] maps)
        {
            this.maps = maps;
            currentMap = maps[0];        
            Player = currentMap.Player;
            CreateMap();
        }

        public void NextMap()
        {
            if (mapNumber >= 0 && mapNumber < maps.Length)
                currentMap = maps[mapNumber];
            currentMap.Player.Health = Player.Health;
            Player = currentMap.Player;            
            CreateMap();
        }

        private void CreateMap()
        {
            var cellWidth = Properties.Resources.Rock.Width;
            var cellHeight = Properties.Resources.Rock.Height;
            bitmap = new Bitmap(LevelSize.Width * cellWidth, LevelSize.Height * cellHeight);
            using(var graphics = Graphics.FromImage(bitmap))
            {
                for(int x = 0; x < currentMap.Terrain.GetLength(0); x++)
                {
                    for(int y = 0; y < currentMap.Terrain.GetLength(1); y++)
                    {
                        Bitmap image = default;
                        switch(currentMap.Terrain[x, y])
                        {
                            //case MapCell.Forest:
                            //    image = Properties.Resources...
                            case MapCell.Grass:
                                image = Properties.Resources.MyGrass;
                                break;
                            case MapCell.Land:
                                image = Properties.Resources.Land;
                                break;
                            case MapCell.Rock:
                                image = Properties.Resources.Rock;
                                break;
                            case MapCell.Trail:
                                image = Properties.Resources.MyTrail;
                                break;
                            case MapCell.Water:
                                image = Properties.Resources.Water;
                                break;
                        }
                        if(image != null)
                            graphics.DrawImage(image, new Rectangle(x * cellWidth, y * cellHeight, cellWidth, cellHeight));
                        else throw new ArgumentException($"{image} с таким названием не существует");
                    }
                }
            }
        }

        public void Paint(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            DrawLevel(g);
            DrawPlayer(g);
            UpdateInterface();
        }

        private void DrawLevel(Graphics graphics)
        {
            graphics.DrawImage(bitmap, 0, 0, LevelSize.Width, LevelSize.Height);
            foreach(var enemy in currentMap.Enemies)
            {
                graphics.DrawImage(Properties.Resources.TestPng3, new Rectangle(enemy.Position.X, enemy.Position.Y, 1, 1));
            }
            foreach(var e in currentMap.Objects)
            {

            }
            foreach(var e in currentMap.Puzzles)
            {

            }
            foreach(var e in currentMap.Npcs)
            {

            }
        }        

        private void DrawPlayer(Graphics graphics)
        {
            if (currentMap.ExitPosition == Player?.Position && open)
            {
                mapNumber++;
                NextMap();
                open = false;
                Player.Delta = Point.Empty;
            }

            if (currentMap.InitialPosition == Player.Position && currentMap != maps[0] && open)
            {
                mapNumber--;
                NextMap();
                open = false;
                Player.Delta = Point.Empty;
            }

            if (Player != null)
            {
                graphics.DrawImage(Properties.Resources.TestPng2, new Rectangle(Player.Position.X, Player.Position.Y, 1, 1));///
                if (currentMap.InitialPosition != Player.Position && currentMap.ExitPosition != Player.Position) open = true;
            }
        }

        private void UpdateInterface()
        {
            if (Player != null)
                MainForm.label.Text = Player.Health.ToString();
        }
    }
}
