//using MyGameModel.Views;
using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
//using System.Windows.Forms;

namespace MyGameModelNew.Domain
{
    public class Player
    {
        public const int MaxHealth = 100;
        public int Health { get; set; }
        public static double Speed { get; private set; }
        public static double Damage { get; private set; }
        public Point Position { get; set; }
        public Inventory Inventory { get; set; }
        public bool IsMoving { get; set; }
        public Point Delta { get; set; }
        public bool CanHit { get; set; }
        public GameObject PikedHealer { get; set; }
        public int CurrentAnimation { get; set; }
        public int CurrentFrame { get; set; }


        public Player(int health, double speed, double damage, Point position, Inventory inventory)
        {
            Health = health;
            Speed = speed;
            Damage = damage;
            Position = position;
            Inventory = inventory;
            Delta = Point.Empty;
            CurrentAnimation = 22;
            CurrentFrame = 1;
        }

        public void UseHealer()
        {
            Health = Inventory.PlayerUseHealer(Health, MaxHealth);
        }
        
        public void Act(Map currentMap)//
        {
            //if (Health <= 0)
            //      Game.CurrentGameStage = GameStage.GameOver;
            if (IsCanGo(Position.Add(Delta), currentMap))
                Position = Position.Add(Delta);
            Animation();
            HealerCollision(currentMap);
            EnemyCollision(currentMap);
            HitEnemy(currentMap);
        }

        private void Animation()
        {
            if(Delta.X == 0 && Delta.Y == 1)
            {
                CurrentAnimation = 15;
                CurrentFrame += 7;
            }
            if (Delta.X == 0 && Delta.Y == -1)
            {
                CurrentAnimation = 1;
                CurrentFrame += 7;
            }
            if (Delta.X == 1 && Delta.Y == 0)
            {
                CurrentAnimation = 22;
                CurrentFrame += 7;
            }
            if (Delta.X == -1 && Delta.Y == 0)
            {
                CurrentAnimation = 8;
                CurrentFrame += 7;                
            }
            if (CurrentFrame == 29) CurrentFrame = 1;
        }

        private void HealerCollision(Map currentMap)
        {
            var healer = currentMap.Objects.Where(x => x.Position.Equals(Position) && x.ObjectType == GameObjectType.Healer).FirstOrDefault();
            if(healer != null)
            {
                Inventory.AddToInventory(healer);
                PikedHealer = healer;
            }
        }

        private void HitEnemy(Map currentMap)
        {            
            var enemy = currentMap.Enemies.Where(x =>
            new Point() { X = Position.X + 1, Y = Position.Y } == x.Position
            || new Point() { X = Position.X - 1, Y = Position.Y } == x.Position
            || new Point() { X = Position.X, Y = Position.Y + 1 } == x.Position
            || new Point() { X = Position.X, Y = Position.Y - 1 } == x.Position).FirstOrDefault();
            if (CanHit && enemy != null)
            {
                currentMap.Enemies.Where(x => x == enemy).First().Health -= 50;
            }
            CanHit = false;
        }

        private void EnemyCollision(Map currentMap)
        {
            if (currentMap.Enemies.Any(x => x.Position == Position))
                Position = Position.SubStract(Delta);
        }

        //public void MovePlayer(KeyEventArgs e)
        //{
        //    switch (e.KeyCode)
        //    {                
        //        case Keys.D:
        //            //Health -= 20;//fast test view health value
        //            if (IsCanGo(new Point() { X = Position.X + 1, Y = Position.Y }))
        //                Position = new Point() { X = Position.X + 1, Y = Position.Y };
        //            break;
        //        case Keys.A:
        //            if (IsCanGo(new Point() { X = Position.X - 1, Y = Position.Y }))
        //                Position = new Point() { X = Position.X - 1, Y = Position.Y };
        //            break;
        //        case Keys.W:
        //            if (IsCanGo(new Point() { X = Position.X, Y = Position.Y - 1 }))
        //                Position = new Point() { X = Position.X, Y = Position.Y - 1 };
        //            break;
        //        case Keys.S:
        //            if (IsCanGo(new Point() { X = Position.X, Y = Position.Y + 1 }))
        //                Position = new Point() { X = Position.X, Y = Position.Y + 1 };
        //            break;
        //    }
        //}

        private bool IsCanGo(Point position, Map currentMap)
        {
            var terrain = currentMap.Terrain;
            if (position.X < 0 || position.X >= terrain.GetLength(0)
                || position.Y < 0 || position.Y >= terrain.GetLength(1)) return false;
            var currentMapCellType = terrain[position.X, position.Y];
            return currentMapCellType != MapCell.Rock && currentMapCellType != MapCell.Water && currentMapCellType != MapCell.Forest;
        }
    }
}
