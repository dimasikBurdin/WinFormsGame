using System.Drawing;

namespace MyGameModel.Domain
{
    public class Map
    {
        public MapCell[,] Terrain { get; private set; }
        public Point InitialPosition { get; private set; }
        public Point ExitPosition { get; private set; }//точки перехода с одной части карты на другую
        public GameObject[] Objects { get; private set; }
        public Enemy[] Enemies { get; private set; }
        public Npc[] Npcs { get; private set; }
        public Puzzle[] Puzzles { get; private set; }//???

        public Map(MapCell[,] terrain, Point initialPosition, Point exitPosition, GameObject[] objects, Enemy[] enemies, Npc[] npc, Puzzle[] puzzles)
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
}
