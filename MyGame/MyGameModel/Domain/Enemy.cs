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
    }
}
