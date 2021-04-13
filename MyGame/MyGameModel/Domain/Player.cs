using System.Drawing;
using System.Linq;

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

        public Player(int health, double speed, double damage, Point position, Inventory inventory)
        {
            Health = health;
            Speed = speed;
            Damage = damage;
            Position = position;
            Inventory = inventory;
        }

        public void UseHealer()
        {
            if (Inventory.Healers.Count != 0)
            {
                if (Health == 100) return;
                Health += 20;
                if (Health > MaxHealth) Health = MaxHealth;
                Inventory.Healers.RemoveAt(0);
                
            }
        }
    }
}
