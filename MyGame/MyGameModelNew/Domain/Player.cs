using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace MyGameModelNew.Domain
{
    public class Player
    {
        public const int MaxHealth = 100;
        public int Health { get; set; }
        public Point Position { get; set; }
        public Inventory Inventory { get; set; }
        public bool IsMoving { get; set; }
        public Point Delta { get; set; }
        public bool CanHit { get; set; }
        public GameObject PikedHealer { get; set; }
        public GameObject PikedSwoard { get; set; }
        public Key PikedKey { get; set; }
        public int CurrentAnimation { get; set; }
        public int CurrentFrame { get; set; }
        public List<string> TalkedMessage { get; set; }
        public GameObjectType CurrentWeapon { get; set; }


        public Player(int health,Point position, Inventory inventory)
        {
            Health = health;
            Position = position;
            Inventory = inventory;
            Delta = Point.Empty;
            CurrentAnimation = 22;
            CurrentFrame = 1;
            TalkedMessage = new List<string>();
            CurrentWeapon = GameObjectType.Hand;
        }

        public void Act(Map currentMap)
        {
            if (IsCanGo(Position.Add(Delta), currentMap))
                Position = Position.Add(Delta);
            Animation();
            KeyCollision(currentMap);
            SwoardCollision(currentMap);
            GateCollision(currentMap);
            NpcCollision(currentMap);
            FireCollision(currentMap);
            HealerCollision(currentMap);
            EnemyCollision(currentMap);
            HitEnemy(currentMap);
        }

        public void UseHealer()
        {
            Health = Inventory.PlayerUseHealer(Health, MaxHealth);
        }

        public void OpenGate(Map currentMap)//класс ключей -> write keys on display
        {
            var gate = currentMap.Gates.Where(x => CheckPosition(x.Position)).FirstOrDefault();
            if (gate != null && Inventory.Keys.Any(x => x.Type == gate.Type))
            {
                gate.Open();
                Inventory.RemoveKey(gate.Type);
            }
        }

        public void TalkToNpc(Map currentMap)
        {
            var npc = currentMap.Npcs.Where(x => CheckPosition(x.Position)).FirstOrDefault();
            if(npc != null)
                TalkedMessage = npc.TolkToPlayer().ToList();
        }

        public void SwapWeapon(int numberWeapon)
        {
            switch(numberWeapon)
            {
                case 1:
                    CurrentWeapon = GameObjectType.Hand;
                    break;
                case 2:
                    if(Inventory.Weapon.Any(x => x == GameObjectType.WoodSword))
                        CurrentWeapon = GameObjectType.WoodSword;
                    break;
                case 3:
                    if (Inventory.Weapon.Any(x => x == GameObjectType.SteelSword))
                        CurrentWeapon = GameObjectType.SteelSword;
                    break;
            }
        }        

        private bool CheckPosition(Point position)
            => new Point() { X = Position.X + 1, Y = Position.Y } == position
                || new Point() { X = Position.X - 1, Y = Position.Y } == position
                || new Point() { X = Position.X, Y = Position.Y + 1 } == position
                || new Point() { X = Position.X, Y = Position.Y - 1 } == position;

        private void SwoardCollision(Map currentMap)
        {
            var swoard = currentMap.Objects
                .Where(x => x.Position.Equals(Position) && (x.ObjectType == GameObjectType.WoodSword || x.ObjectType == GameObjectType.SteelSword))
                .FirstOrDefault();
            if (swoard != null)
            {
                Inventory.AddToInventory(swoard);
                PikedSwoard = swoard;
            }
        }

        private void KeyCollision(Map currentMap)
        {
            var key = currentMap.Keys.Where(x => x.Position.Equals(Position)).FirstOrDefault();
            if (key != null)
            {
                Inventory.AddKeyToInventory(key);
                PikedKey = key;
            }
        }

        private void GateCollision(Map currentMap)
        {
            if (currentMap.Gates.Any(x => x.Position == Position && x.State == GateState.Lock))
                Position = Position.SubStract(Delta);
        }

        private void Animation()
        {
            if(Delta.X == 0 && Delta.Y == 1)
                CurrentAnimation = 15;
            if (Delta.X == 0 && Delta.Y == -1)
                CurrentAnimation = 1;
            if (Delta.X == 1 && Delta.Y == 0)
                CurrentAnimation = 22;
            if (Delta.X == -1 && Delta.Y == 0)
                CurrentAnimation = 8;

            if (Delta != Point.Empty) CurrentFrame += 7;
            if (CurrentFrame == 29) CurrentFrame = 1;
        }

        private void NpcCollision(Map currentMap)
        {
            if (currentMap.Npcs.Any(x => x.Position == Position))
                Position = Position.SubStract(Delta);
        }

        private void FireCollision(Map currentMap)
        {
            if (currentMap.Fires.Any(x => x.Position == Position))
                Position = Position.SubStract(Delta);
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
            var enemy = currentMap.Enemies.Where(x => CheckPosition(x.Position)).FirstOrDefault();
            if (CanHit && enemy != null)
            {
                switch(CurrentWeapon)
                {
                    case GameObjectType.Hand:
                        currentMap.Enemies.Where(x => x == enemy).First().Health -= 25;
                        break;
                    case GameObjectType.WoodSword:
                        currentMap.Enemies.Where(x => x == enemy).First().Health -= 40;
                        break;
                    case GameObjectType.SteelSword:
                        currentMap.Enemies.Where(x => x == enemy).First().Health -= 50;
                        break;
                }
            }
            CanHit = false;
        }

        private void EnemyCollision(Map currentMap)
        {
            if (currentMap.Enemies.Any(x => x.Position == Position))
                Position = Position.SubStract(Delta);
        }

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
