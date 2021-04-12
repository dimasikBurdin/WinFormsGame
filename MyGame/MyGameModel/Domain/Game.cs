﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameModel.Domain
{
    public enum MapCell
    {
        Grass,
        Path,//тропинка
        Land,//можно ходить
        Rock,
        Forest//нельзя ходить
    }

    public enum ObjectType
    {
        Healer,
        Key,
        //weapon =>
        Sword, 
        Knife,
        Stick
        //=> вместо меча можно по ходу игры увеличивать дамаг, наносимый player'ом, что упростит механику инвентаря
        //однако если делать квесты, где нужны будут, например, ключи, надо будет делать все равно иначе
    }

    public class Game
    {
        public Map CurrentMap;
        public List<Map> AllMaps;
    }

    public class Inventory   //private?
    {
        public List<ObjectType> Healers { get; set; }//кол-во хилок отображается во время игры
        public List<ObjectType> Keys { get; set; }//мб тоже отображаются на экране
        public List<ObjectType> Weapon { get; set; }//находится в инвентаре => небольшая табличка

        public Inventory()
        {
            Healers = new List<ObjectType>();
            Keys = new List<ObjectType>();
            Weapon = new List<ObjectType>();
        }

        public int CountHealers
        {
            get
            {
                return Healers.Count;
            }
        }
        public int CountKeys
        {
            get
            {
                return Keys.Count;
            }
        }

        public void AddToInventory(GameObject newObject)
        {
            switch(newObject.ObjectType)
            {
                case ObjectType.Healer: 
                    Healers.Add(newObject.ObjectType);
                    break;
                case ObjectType.Key:
                    Keys.Add(newObject.ObjectType);
                    break;
                case var a when newObject.ObjectType == ObjectType.Knife || newObject.ObjectType == ObjectType.Stick || newObject.ObjectType == ObjectType.Sword:
                    Weapon.Add(newObject.ObjectType);
                    break;
            }
        }

        //public void UseObjectOfInventory(GameObject currentObject) ===> мб в player сделать
        //{
        //    switch(currentObject.ObjectType)
        //    {
        //        case ObjectType.Healer:
        //            if (Player.Health <= Player.MaxHealth - 20) Player.Health += 20;
        //            Healers.RemoveAt(0);
        //            break;
        //        throw new NotImplementedException();
        //        //case ObjectType.Key: throw new NotImplementedException();
        //            //TO DO ...
        //    }
        //}
    }

    public class GameObject
    {
        public Point Position { get; private set; }
        public ObjectType ObjectType { get; private set; }

        public GameObject(Point position, ObjectType objectType)
        {
            Position = position;
            ObjectType = objectType;
        }
    }

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
                if (Health <= MaxHealth - 20)
                {
                    Health += 20;
                    Inventory.Healers.RemoveAt(0);
                }
            }
        }
    }

    public class Map
    {
        public MapCell[,] Terrain { get; private set; }
        public Point InitialPosition { get; private set; }
        public Point ExitPosition { get; private set; }//точки перехода с одной части карты на другую
        public GameObject[] Objects { get; private set; }
        public Enemy[] Enemies { get; private set; }
        public Npc[] Npcs { get; private set; }
        public Point[] Puzzles { get; private set; }//???

        public Map(MapCell[,] terrain, Point initialPosition, Point exitPosition, GameObject[] objects, Enemy[] enemies, Npc[] npc, Point[] puzzles)
        {
            Terrain = terrain;
            InitialPosition = initialPosition;
            ExitPosition = exitPosition;
            Objects = objects;
            Enemies = enemies;
            Npcs = npc;
            Puzzles = puzzles;
        }
    }

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

    public class Npc
    {
        /// <summary>
        /// создаем каждого npc с определенными позицией и набором фраз, которые хранятся в массиве строк и выводятся по очереди по нажатию клавиши/клику мышки
        /// </summary>
        public Point Position { get; private set; }
        public string[] Messages { get; private set; }

        public Npc(Point position, string[] messages)
        {
            Position = position;
            Messages = messages;
        }

        public IEnumerable<string> TolkToPlayer()
        {
            foreach (var message in Messages.Where(x => x != null))
                yield return message;
        }

    }

    public class Puzzle
    {
        /*    ____
         *   |0__0|
         *    /||\
         *     /\     
         */
    }
}
