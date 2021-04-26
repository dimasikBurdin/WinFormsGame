using System.Collections.Generic;
using System.Drawing;
using System;

namespace MyGameModel.Domain
{
    public class Map
    {
        public Player Player { get; set; }
        public MapCell[,] Terrain { get; private set; }
        public Point InitialPosition { get; private set; }
        public Point ExitPosition { get; private set; }//точки перехода с одной части карты на другую
        public GameObject[] Objects { get; private set; }
        public Enemy[] Enemies { get; private set; }
        public Npc[] Npcs { get; private set; }
        public Puzzle[] Puzzles { get; private set; }//???

        public Map(MapCell[,] terrain, Player player, Point exitPosition, GameObject[] objects, Enemy[] enemies, Npc[] npc, Puzzle[] puzzles)
        {
            Terrain = terrain;
            Player = player;
            ExitPosition = exitPosition;
            Objects = objects;
            Enemies = enemies;
            Npcs = npc;
            Puzzles = puzzles;
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
            var initialPosition = Point.Empty;
            var exitPosition = Point.Empty;
            var enemies = new List<Enemy>();
            var objects = new List<GameObject>();
            var npcS = new List<Npc>();
            var puzzles = new List<Puzzle>();
            for(var y = 0; y < lines.Length; y++)
            {
                for(var x = 0; x < lines[0].Length; x++)//если у всего нестатичного текстуры будут не квадратные, а нормальные, то мб и не стоит на место этих сущностей ставить пустую клетку
                {
                    switch (lines[y][x])
                    {
                        case 'N':
                            terrain[x, y] = MapCell.Empty;
                            npcS.Add(new Npc(new Point(x, y), null));//
                            break;
                        case 'E':
                            terrain[x, y] = MapCell.Empty;
                            enemies.Add(new Enemy(100, 30, 15, new Point(x, y)));
                            break;
                        case 'Z':  //puzzle
                            terrain[x, y] = MapCell.Empty;
                            puzzles.Add(new Puzzle());//доделать
                            break;
                        case 'H'://healer
                            terrain[x, y] = MapCell.Empty;
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
                            terrain[x, y] = MapCell.Empty;
                            exitPosition = new Point(x, y);
                            break;
                        default:
                            terrain[x, y] = MapCell.Empty;
                            break;
                    }
                }            
            }
            return new Map(terrain, player, exitPosition, objects.ToArray(), enemies.ToArray(), npcS.ToArray(), puzzles.ToArray());
        }
    }
}
