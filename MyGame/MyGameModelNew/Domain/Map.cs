using System.Collections.Generic;
using System.Drawing;
using System;

namespace MyGameModelNew.Domain
{
    public class Map
    {
        public Player Player { get; set; }
        public MapCell[,] Terrain { get; private set; }
        public Point InitialPosition { get; private set; }
        public Point ExitPosition { get; private set; }//точки перехода с одной части карты на другую
        public List<GameObject> Objects { get; private set; }
        public List<Enemy> Enemies { get; private set; }
        public Npc[] Npcs { get; private set; }
        public Puzzle[] Puzzles { get; private set; }//???
        public List<Fire> Fires { get; private set; }
        public List<Gate> Gates { get; private set; }
        public List<Key> Keys { get; private set; }

        public Map(MapCell[,] terrain, Player player, Point initialPosition,Point exitPosition, List<GameObject> objects,
            List<Enemy> enemies, Npc[] npc, Puzzle[] puzzles, List<Fire> fires, List<Gate> gates, List<Key> keys)
        {
            Terrain = terrain;
            Player = player;
            InitialPosition = initialPosition;
            ExitPosition = exitPosition;
            Objects = objects;
            Enemies = enemies;
            Npcs = npc;
            Puzzles = puzzles;
            Fires = fires;
            Gates = gates;
            Keys = keys;
        }

        public static Map FromText(string text)
        {
            var lines = text.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return FromLines(lines);
        }

        public static Map FromLines(string[] lines)
        {
            var terrain = new MapCell[lines[0].Length, lines.Length];
            var player = default(Player);
            var initialPosition = new Point(int.MinValue, int.MinValue);
            var exitPosition = new Point(int.MinValue, int.MinValue);
            var enemies = new List<Enemy>();
            var objects = new List<GameObject>();
            var npcS = new List<Npc>();
            var puzzles = new List<Puzzle>();
            var fires = new List<Fire>();
            var gates = new List<Gate>();
            var keys = new List<Key>();
            for(var y = 0; y < lines.Length; y++)
            {
                for(var x = 0; x < lines[0].Length; x++)//если у всего нестатичного текстуры будут не квадратные, а нормальные, то мб и не стоит на место этих сущностей ставить пустую клетку
                {
                    switch (lines[y][x])
                    {
                        case '<'://green key
                            terrain[x, y] = MapCell.Grass;
                            keys.Add(new Key(new Point(x, y), KeyAndGateType.Green));
                            break;
                        case '!'://red key
                            terrain[x, y] = MapCell.Grass;
                            keys.Add(new Key(new Point(x, y), KeyAndGateType.Red));
                            break;
                        case '>'://blue key
                            terrain[x, y] = MapCell.Grass;
                            keys.Add(new Key(new Point(x, y), KeyAndGateType.Blue));
                            break;
                        case '\\'://green gate
                            terrain[x, y] = MapCell.Trail;
                            gates.Add(new Gate(new Point(x, y), KeyAndGateType.Green));
                            break;
                        case '|'://red gate
                            terrain[x, y] = MapCell.Trail;
                            gates.Add(new Gate(new Point(x, y), KeyAndGateType.Red));
                            break;
                        case '/'://blue gate
                            terrain[x, y] = MapCell.Trail;
                            gates.Add(new Gate(new Point(x, y), KeyAndGateType.Blue));
                            break;
                        case 'O'://fire
                            terrain[x, y] = MapCell.Grass;
                            fires.Add(new Fire(new Point(x, y)));
                            break;
                        case 'N':
                            terrain[x, y] = MapCell.Grass;
                            npcS.Add(new Npc(new Point(x, y), null, new GameObject(new Point(x, y), GameObjectType.Healer)));//
                            break;
                        case 'E':
                            terrain[x, y] = MapCell.Trail;
                            enemies.Add(new Enemy(100, 30, 15, new Point(x, y)));
                            break;
                        case 'Z':  //puzzle
                            terrain[x, y] = MapCell.Empty;
                            puzzles.Add(new Puzzle());//доделать
                            break;
                        case 'H'://healer
                            terrain[x, y] = MapCell.Trail;
                            objects.Add(new GameObject(new Point(x, y), GameObjectType.Healer));
                            break;
                        case 'S'://stick
                            terrain[x, y] = MapCell.Empty;
                            objects.Add(new GameObject(new Point(x, y), GameObjectType.Stick));
                            break;
                        case 'K'://knife
                            terrain[x, y] = MapCell.Empty;
                            objects.Add(new GameObject(new Point(x, y), GameObjectType.Knife));
                            break;
                        case 'P':
                            terrain[x, y] = MapCell.Trail;//                            
                            initialPosition = new Point(x, y);
                            player = new Player(100, 30, 30, initialPosition, new Inventory());//
                            break;
                        case 'W':
                            terrain[x, y] = MapCell.Water;
                            break;
                        case 'G':
                            terrain[x, y] = MapCell.Grass;
                            break;
                        case 'T':
                            terrain[x, y] = MapCell.Trail;
                            break;
                        case 'L':
                            terrain[x, y] = MapCell.Land;
                            break;
                        case 'R':
                            terrain[x, y] = MapCell.Rock;
                            break;
                        case 'F':
                            terrain[x, y] = MapCell.Forest;
                            break;
                        case 'X'://exit
                            terrain[x, y] = MapCell.Trail;
                            exitPosition = new Point(x, y);
                            break;
                        default:
                            terrain[x, y] = MapCell.Empty;
                            break;
                    }
                }            
            }
            return new Map(terrain, player, initialPosition, exitPosition, objects, enemies, npcS.ToArray(), puzzles.ToArray(), fires, gates, keys);
        }
    }
}
