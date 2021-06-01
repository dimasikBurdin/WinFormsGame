using MyGameModelNew.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
//using Point = MyGameModel.Domain.Point;

namespace MyViews
{
    public class ScenePainter
    {
        private int mapNumber;
        private Map[] maps = null;
        private Bitmap bitmap;
        private bool open = false;
        public SizeF Size => new SizeF(currentMap.Terrain.GetLength(0), currentMap.Terrain.GetLength(1));
        public Size LevelSize => new Size(currentMap.Terrain.GetLength(0), currentMap.Terrain.GetLength(1));

        public static Map currentMap {get; set;}
        
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
            currentMap.Player.Inventory = Player.Inventory;
            Player = currentMap.Player;
            CreateMap();
        }

        private void CreateMap()
        {
            var cellWidth = Properties.Resources.newGrass1.Width;
            var cellHeight = Properties.Resources.newGrass1.Height;
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
                            case MapCell.Forest:
                                image = Properties.Resources.Tree1;
                                break;
                            case MapCell.Grass:
                                image = Properties.Resources.newGrass1;
                                break;
                            case MapCell.Land:
                                image = Properties.Resources.newLand1;
                                break;
                            case MapCell.Rock:
                                image = Properties.Resources.Rock;
                                break;
                            case MapCell.Trail:
                                image = Properties.Resources.newTrail1;
                                break;
                            case MapCell.Water:
                                image = Properties.Resources.newWater1;
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
                var image = Properties.Resources.ghosts;
                //graphics.DrawImage(Properties.Resources.ghost, new Rectangle(enemy.Position.X, enemy.Position.Y, 1, 1));
                //1 -> 0 += 40 || 2 -> 0 += 46
                graphics.DrawImage(image, new Rectangle(enemy.Position.X, enemy.Position.Y, 1, 1), enemy.CurrentFrame, enemy.CurrentAnimation, 40, 45, GraphicsUnit.Pixel);
            }
            foreach(var e in currentMap.Objects)
            {
                switch(e.ObjectType)
                {
                    case GameObjectType.Healer:
                        graphics.DrawImage(Properties.Resources.newHealer, new Rectangle(e.Position.X, e.Position.Y, 1, 1));
                        break;
                }
            }
            foreach(var e in currentMap.Puzzles)
            {

            }
            foreach(var e in currentMap.Npcs)
            {
                graphics.DrawImage(Properties.Resources.npc, new Rectangle(e.Position.X, e.Position.Y, 1, 1));
            }
            foreach(var e in currentMap.Fires)
            {
                var image = Properties.Resources.fire;
                graphics.DrawImage(image, new Rectangle(e.Position.X, e.Position.Y, 1, 1), 0, e.CurrentAnimation, 35, 35, GraphicsUnit.Pixel);
            }
            foreach(var e in currentMap.Keys)
            {
                switch(e.Type)
                {
                    case KeyAndGateType.Red:
                        graphics.DrawImage(Properties.Resources.key_red, new Rectangle(e.Position.X, e.Position.Y, 1, 1));
                        break;
                    case KeyAndGateType.Green:
                        graphics.DrawImage(Properties.Resources.key_green, new Rectangle(e.Position.X, e.Position.Y, 1, 1));
                        break;
                    case KeyAndGateType.Blue:
                        graphics.DrawImage(Properties.Resources.key_blue, new Rectangle(e.Position.X, e.Position.Y, 1, 1));
                        break;
                }
            }
            foreach (var e in currentMap.Gates)
            {
                switch (e.State)
                {
                    case GateState.Lock:
                        {
                            switch (e.Type)
                            {
                                case KeyAndGateType.Red:
                                    var image = Properties.Resources.RedGate;
                                    graphics.DrawImage(image, new Rectangle(e.Position.X, e.Position.Y, 1, 1), 0, 0, 100, 60, GraphicsUnit.Pixel);
                                    break;
                                case KeyAndGateType.Green:
                                    image = Properties.Resources.GreenGate;
                                    graphics.DrawImage(image, new Rectangle(e.Position.X, e.Position.Y, 1, 1), 0, 0, 100, 60, GraphicsUnit.Pixel);
                                    break;
                                case KeyAndGateType.Blue:
                                    image = Properties.Resources.BlueGate;
                                    graphics.DrawImage(image, new Rectangle(e.Position.X, e.Position.Y, 1, 1), 0, 0, 100, 60, GraphicsUnit.Pixel);
                                    break;
                            }
                            break;
                        }
                    case GateState.Open:
                        {
                            switch (e.Type)
                            {
                                case KeyAndGateType.Red:
                                    var image = Properties.Resources.RedGate;//0-> += 64
                                    graphics.DrawImage(image, new Rectangle(e.Position.X, e.Position.Y, 1, 1), 0, e.CurrentAnimation, 100, 60, GraphicsUnit.Pixel);
                                    break;
                                case KeyAndGateType.Green:
                                    image = Properties.Resources.GreenGate;//0-> += 64
                                    graphics.DrawImage(image, new Rectangle(e.Position.X, e.Position.Y, 1, 1), 0, e.CurrentAnimation, 100, 60, GraphicsUnit.Pixel);
                                    break;
                                case KeyAndGateType.Blue:
                                    image = Properties.Resources.BlueGate;//0-> += 64
                                    graphics.DrawImage(image, new Rectangle(e.Position.X, e.Position.Y, 1, 1), 0, e.CurrentAnimation, 100, 60, GraphicsUnit.Pixel);
                                    break;
                            }
                            break;
                        }
                }
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
            //1 and 2 -> += 7 (1,22)
            if (Player != null)
            {                    
                var image = Properties.Resources.soldier;
                //graphics.DrawImage(Properties.Resources.up0, new Rectangle(Player.Position.X, Player.Position.Y, 1, 1));///
                graphics.DrawImage(image, new Rectangle(Player.Position.X, Player.Position.Y, 1, 1), 9 * Player.CurrentFrame, 9 * Player.CurrentAnimation, 55, 56, GraphicsUnit.Pixel);///
                if (currentMap.InitialPosition != Player.Position && currentMap.ExitPosition != Player.Position) open = true;
            }
        }

        private void UpdateInterface()
        {
            if (Player != null)
            {
                MainForm.LabelHp.Text = Player.Health > 0 ? Player.Health.ToString() + "hp" : "0";

                MainForm.LabelHealerText.Text = Player.Inventory.CountHealers.ToString();
                MainForm.LabelHealerImage.Image = Properties.Resources.newHealer;
                
                MainForm.LabelRedKeyText.Text = Player.Inventory.CountRedKeys.ToString();
                MainForm.LabelRedKeyImage.Image = Properties.Resources.key_red;

                MainForm.LabelGreenKeyText.Text = Player.Inventory.CountGreenKeys.ToString();
                MainForm.LabelGreenKeyImage.Image = Properties.Resources.key_green;

                MainForm.LabelBlueKeyText.Text = Player.Inventory.CountBlueKeys.ToString();
                MainForm.LabelBlueKeyImage.Image = Properties.Resources.key_blue;
            }
        }
    }
}
