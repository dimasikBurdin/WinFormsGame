using MyGameModel.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace MyGameModel.Views
{
    public class ScenePainter
    {
        public SizeF Size => new SizeF(currentMap.Terrain.GetLength(0), currentMap.Terrain.GetLength(1));
        public Size LevelSize => new Size(currentMap.Terrain.GetLength(0), currentMap.Terrain.GetLength(1));

        private Map currentMap;
        private Bitmap bitmap;

        public ScenePainter(Map[] maps)
        {
            currentMap = maps[0];
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
                            //case MapCell.Land:
                            //    image = Properties.Resources...
                            case MapCell.Rock:
                                image = Properties.Resources.Rock;
                                break;
                            case MapCell.Trail:
                                image = Properties.Resources.MyTrail;
                                break;
                        }
                        graphics.DrawImage(image, new Rectangle(x * cellWidth, y * cellHeight, cellWidth, cellHeight));
                    }
                }
            }
        }

        public void Paint(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            DrawLevel(g);
        }

        private void DrawLevel(Graphics graphics)
        {
            graphics.DrawImage(bitmap, 0, 0, LevelSize.Width, LevelSize.Height);
            foreach(var e in currentMap.Enemies)
            {

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
    }
}
