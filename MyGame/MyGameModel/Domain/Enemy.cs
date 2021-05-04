using MyGameModel.Views;
using System.Drawing;

namespace MyGameModel.Domain
{
    public class Enemy
    {
        public const int MaxHealth = 100;
        public static int Health { get; set; }
        public double Speed { get; private set; }
        public double Damage { get; private set; }
        public Point Position { get; set; }//???

        public Enemy(int health, double speed, double damage, Point position)
        {
            Health = health;
            Speed = speed;
            Damage = damage;
            Position = position;
        }

        bool isLeftEnd;        

        public void Move()//test
        {
            if (IsCanGo(new Point(Position.X - 1, Position.Y)) && !isLeftEnd)
                Position = new Point { X = Position.X - 1, Y = Position.Y };
            else if (IsCanGo(new Point(Position.X + 1, Position.Y)))
            {
                isLeftEnd = true;
                Position = new Point { X = Position.X + 1, Y = Position.Y };
                if (Position.X == ScenePainter.currentMap.Terrain.GetLength(0)) isLeftEnd = false;
            }
            else isLeftEnd = false;
        }

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
