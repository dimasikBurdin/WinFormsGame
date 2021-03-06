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
        public Point ExitPosition { get; private set; }
        public List<GameObject> Objects { get; private set; }
        public List<Enemy> Enemies { get; private set; }
        public List<Npc> Npcs { get; private set; }
        public Puzzle[] Puzzles { get; private set; }
        public List<Fire> Fires { get; private set; }
        public List<Gate> Gates { get; private set; }
        public List<Key> Keys { get; private set; }

        public Map(MapCell[,] terrain, Player player, Point initialPosition,Point exitPosition, List<GameObject> objects,
            List<Enemy> enemies, List<Npc> npc, Puzzle[] puzzles, List<Fire> fires, List<Gate> gates, List<Key> keys)
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
                for(var x = 0; x < lines[0].Length; x++)
                {
                    switch (lines[y][x])
                    {
                        case '@'://wood swoard
                            terrain[x, y] = MapCell.Grass;
                            objects.Add(new GameObject(new Point(x, y), GameObjectType.WoodSword));
                            break;
                        case '#'://steel swoard
                            terrain[x, y] = MapCell.Grass;
                            objects.Add(new GameObject(new Point(x, y), GameObjectType.SteelSword));
                            break;
                        case '1'://npc from 1 map
                            terrain[x, y] = MapCell.Grass;
                            npcS.Add(new Npc(new Point(x, y), new List<string> 
                            {
                                "Приветствую тебя", 
                                "Можешь не рассказывать, как ты тут оказался. Ты далеко не первый здесь.", 
                                "Я расскажу тебе то, что поможет в твоем пути",
                                "На твоем пути тебе могут встретиться волшебные ворота. Их здесь 3 типа - красные, заленые и синие.",
                                "Чтобы открыть такие ворота, тебе необходимо найти ключ соответсвующего цвета",
                                "Если ты найдешь такой ключ, просто подойди к воротам и нажми \"E\"",
                                "Также на твоем пути могут встретиться монстры. Ты можешь отбиваться от них тремя видами оружия",
                                " - твои руки, деревянный меч и стальной меч - самое лучшее оружие",
                                "Чтобы переключаться между твоим оружием, нажми на 1 / 2 / 3. Справа снизу ты увидишь какое оружие у тебя сейчас в руках.",
                                "Чтобы отбиваться от монстров, нажимай на \"Space\"", 
                                "Если ты поранишься, ты сможешь восполнить твое здоровья, но для этого тебе нужно найти аптечку",
                                "Кол-во здоровья и кол-во аптечек отображается справа сверху на экране.",
                                "Чтобы восполнить здоровье, нажми \"H\"",
                                "И наконец, чтобы перемещаться с локации на локацию иди по тропинке",
                                "Удачи тебе, может именно тебе и повезет..."
                            },
                            new GameObject(new Point(x, y), GameObjectType.Healer)));
                            break;
                        case '2'://npc from 1 map
                            terrain[x, y] = MapCell.Grass;
                            npcS.Add(new Npc(new Point(x, y), new List<string>
                            {
                                "Здравствуй.",
                                "Я приберег для тебя полезную тебе вещь.",
                                "Можешь взять за костром.",                                
                            },
                            new GameObject(new Point(x, y), GameObjectType.Healer)));
                            break;
                        case '3'://npc from 1 map
                            terrain[x, y] = MapCell.Grass;
                            npcS.Add(new Npc(new Point(x, y), new List<string>
                            {
                                "Поздравляю!",
                                "Ты смог добраться до сюда.",
                                "Теперь ты будешь счастлив.",
                                "До встречи!.",
                                "Нажмите F для продолжения"
                            },
                            new GameObject(new Point(x, y), GameObjectType.Healer)));
                            break;
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
                            enemies.Add(new Enemy(100, 15, new Point(x, y)));
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
                            player = new Player(100, initialPosition, new Inventory());//
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
                    }
                }            
            }
            return new Map(terrain, player, initialPosition, exitPosition, objects, enemies, npcS, puzzles.ToArray(), fires, gates, keys);
        }
    }
}
