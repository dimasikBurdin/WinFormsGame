using MyGameModelNew.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyViews
{
    public class ScenePainter
    {
        private int mapNumber;
        private readonly Map[] maps = null;
        private Bitmap bitmap;
        private bool open = false;
        public SizeF Size => new SizeF(CurrentMap.Terrain.GetLength(0), CurrentMap.Terrain.GetLength(1));
        public Size LevelSize => new Size(CurrentMap.Terrain.GetLength(0), CurrentMap.Terrain.GetLength(1));
        public static Map CurrentMap {get; set;}        
        public static Player Player { get; set; }

        public ScenePainter(Map[] maps)
        {
            this.maps = maps;
            CurrentMap = maps[0];        
            Player = CurrentMap.Player;
            CreateMap();
        }

        public void Paint(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            DrawLevel(g);
            DrawPlayer(g);
            UpdateInterface();
        }

        private void CreateMap()
        {
            var cellWidth = Properties.Resources.newGrass1.Width;
            var cellHeight = Properties.Resources.newGrass1.Height;
            bitmap = new Bitmap(LevelSize.Width * cellWidth, LevelSize.Height * cellHeight);
            using(var graphics = Graphics.FromImage(bitmap))
            {
                for(int x = 0; x < CurrentMap.Terrain.GetLength(0); x++)
                {
                    for(int y = 0; y < CurrentMap.Terrain.GetLength(1); y++)
                    {
                        Bitmap image = default;
                        switch(CurrentMap.Terrain[x, y])
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

        private void DrawLevel(Graphics graphics)
        {
            graphics.DrawImage(bitmap, 0, 0, LevelSize.Width, LevelSize.Height);
            foreach(var enemy in CurrentMap.Enemies)
            {
                var image = Properties.Resources.ghosts;
                //1 -> 0 += 40 || 2 -> 0 += 46
                graphics.DrawImage(image, new Rectangle(enemy.Position.X, enemy.Position.Y, 1, 1), enemy.CurrentFrame, enemy.CurrentAnimation, 40, 45, GraphicsUnit.Pixel);
            }
            foreach(var e in CurrentMap.Objects)
            {
                switch(e.ObjectType)
                {
                    case GameObjectType.Healer:
                        graphics.DrawImage(Properties.Resources.newHealer, new Rectangle(e.Position.X, e.Position.Y, 1, 1));
                        break;
                    case GameObjectType.WoodSword:
                        graphics.DrawImage(Properties.Resources.swordWood, new Rectangle(e.Position.X, e.Position.Y, 1, 1));
                        break;
                    case GameObjectType.SteelSword:
                        graphics.DrawImage(Properties.Resources.sword, new Rectangle(e.Position.X, e.Position.Y, 1, 1));
                        break;
                }
            }
            foreach(var e in CurrentMap.Puzzles)
            {

            }
            foreach(var e in CurrentMap.Npcs)
            {
                graphics.DrawImage(Properties.Resources.npc, new Rectangle(e.Position.X, e.Position.Y, 1, 1));
            }
            foreach(var e in CurrentMap.Fires)
            {
                var image = Properties.Resources.fire;
                graphics.DrawImage(image, new Rectangle(e.Position.X, e.Position.Y, 1, 1), 0, e.CurrentAnimation, 35, 35, GraphicsUnit.Pixel);
            }
            foreach(var e in CurrentMap.Keys)
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
            foreach (var e in CurrentMap.Gates)
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

        private void NextMap()
        {
            if (mapNumber >= 0 && mapNumber < maps.Length)
                CurrentMap = maps[mapNumber];
            CurrentMap.Player.Health = Player.Health;
            CurrentMap.Player.Inventory = Player.Inventory;
            CurrentMap.Player.CurrentWeapon = Player.CurrentWeapon;
            Player = CurrentMap.Player;
            CreateMap();
        }

        private void DrawPlayer(Graphics graphics)
        {
            if (CurrentMap.ExitPosition == Player?.Position && open)
            {
                mapNumber++;
                NextMap();
                open = false;
                Player.Delta = Point.Empty;
            }

            if (CurrentMap.InitialPosition == Player.Position && CurrentMap != maps[0] && open)
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
                graphics.DrawImage(image, new Rectangle(Player.Position.X, Player.Position.Y, 1, 1), 9 * Player.CurrentFrame, 9 * Player.CurrentAnimation, 55, 56, GraphicsUnit.Pixel);
                if (CurrentMap.InitialPosition != Player.Position && CurrentMap.ExitPosition != Player.Position) open = true;
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


                switch(Player.CurrentWeapon)
                {
                    case GameObjectType.Hand:
                        MainForm.LabelHandImage.Image = Properties.Resources.fist1;
                        MainForm.LabelWoodSwoardImage.Image = null;
                        MainForm.LabelSteelSwoardImage.Image = null;
                        break;
                    case GameObjectType.WoodSword:
                        MainForm.LabelWoodSwoardImage.Image = Properties.Resources.swordWood;
                        MainForm.LabelHandImage.Image = null;
                        MainForm.LabelSteelSwoardImage.Image = null;
                        break;
                    case GameObjectType.SteelSword:
                        MainForm.LabelSteelSwoardImage.Image = Properties.Resources.sword;
                        MainForm.LabelHandImage.Image = null;
                        MainForm.LabelWoodSwoardImage.Image = null;
                        break;
                }
                MainForm.LabelHandText.Text = "1";                

                MainForm.LabelWoodSwoardText.Text = "2";                

                MainForm.LabelSteelSwoardText.Text = "3";
            }
        }
    }
}
