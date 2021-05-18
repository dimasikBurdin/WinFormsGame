using MyGameModel.Views;
using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace MyGameModel.Domain
{
    public class Player
    {
        public const int MaxHealth = 100;
        public int Health { get; set; }
        public static double Speed { get; private set; }
        public static double Damage { get; private set; }
        public Point Position { get; set; }//??? как отрисовать при переходе с локации на локацию
        public Inventory Inventory { get; set; }
        public bool IsMoving { get; set; }
        public Point Delta { get; set; }


        public Player(int health, double speed, double damage, Point position, Inventory inventory)
        {
            Health = health;
            Speed = speed;
            Damage = damage;
            Position = position;
            Inventory = inventory;
            Delta = Point.Empty;
        }

        public void UseHealer()
        {
            Health = Inventory.PlayerUseHealer(Health, MaxHealth);
        }
        
        public void Act()
        {
            if (IsCanGo(Position + Delta))
                Position += Delta;
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

        private bool IsCanGo(Point position)
        {
            var terrain = ScenePainter.currentMap.Terrain;
            if (position.X < 0 || position.X >= terrain.GetLength(0)
                || position.Y < 0 || position.Y >= terrain.GetLength(1)) return false;
            var currentMapCellType = terrain[position.X, position.Y];
            return currentMapCellType != MapCell.Rock && currentMapCellType != MapCell.Water;
        }
    }
}
