using MyGameModel.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MyGameModel.Domain
{
    public class Enemy
    {
        private bool canMove = true;
        private List<Point> resTrack = new List<Point>();
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

        #region
        //bool isLeftEnd;        

        //public void Move()//test
        //{
        //    if (IsCanGo(new Point(Position.X - 1, Position.Y)) && !isLeftEnd)
        //        Position = new Point { X = Position.X - 1, Y = Position.Y };
        //    else if (IsCanGo(new Point(Position.X + 1, Position.Y)))
        //    {
        //        isLeftEnd = true;
        //        Position = new Point { X = Position.X + 1, Y = Position.Y };
        //        if (Position.X == ScenePainter.currentMap.Terrain.GetLength(0)) isLeftEnd = false;
        //    }
        //    else isLeftEnd = false;
        //}
        #endregion

        private Point lastPosition;
        private int hitTick;

        public void Act()
        {
            var playerPos = ScenePainter.currentMap.Player.Position;
            var minRadius = new Point(Position.X - 5, Position.Y - 5);
            var maxRadius = new Point(Position.X + 5, Position.Y + 5);

            if(canMove && (playerPos.X >= minRadius.X && playerPos.Y >= minRadius.Y && playerPos.X <= maxRadius.X && playerPos.Y <= maxRadius.Y))
                CreateTrack();
            if (resTrack?.Count != 0)
            {
                lastPosition = Position;
                Position = resTrack.First();
                resTrack?.RemoveAt(0);
                if (Position == playerPos) Position = lastPosition;
            }
            HitPlayer(playerPos);
        }

        private void HitPlayer(Point playerPos)
        {
            if (new Point() { X = Position.X + 1, Y = Position.Y } == playerPos || new Point() { X = Position.X - 1, Y = Position.Y } == playerPos
                || new Point() { X = Position.X, Y = Position.Y + 1 } == playerPos || new Point() { X = Position.X, Y = Position.Y - 1 } == playerPos)
            {
                if (ScenePainter.currentMap.Player.Health == 0) return;
                if(hitTick == 0) ScenePainter.currentMap.Player.Health -= 20;
                hitTick++;
                if (hitTick == 4) hitTick = 0;
            }
        }

        private void CreateTrack()
        {
            canMove = false;

            resTrack = FindTrack(ScenePainter.currentMap.Player.Position);
            if (resTrack.Count > 5) 
                resTrack = new List<Point>();

            canMove = true;
        }

        private List<Point> FindTrack(Point target)
        {
            var track = new Dictionary<Point, Point>();
            track[Position] = new Point(int.MinValue, int.MinValue);

            var queue = new Queue<Point>();
            queue.Enqueue(Position);
            while(queue.Count != 0)
            {
                //if (track.Count > 5) return null;
                var currentPosition = queue.Dequeue();
                for(var dx = -1; dx <=1; dx++)
                    for(var dy = -1; dy <= 1; dy++)
                    {
                        if (dy != 0 && dx != 0) continue;
                        var nextPosition = new Point() { X = currentPosition.X + dx, Y = currentPosition.Y + dy };
                        if (track.ContainsKey(nextPosition) || !IsCanGo(nextPosition)) continue;
                        track[nextPosition] = currentPosition;
                        queue.Enqueue(nextPosition);
                    }
                if (track.ContainsKey(target)) break;                
            }
            var pathItem = target;
            var result = new List<Point>();
            while (pathItem != new Point(int.MinValue, int.MinValue))
            {
                result.Add(pathItem);
                pathItem = track[pathItem];
            }
            result.Reverse();
            result.RemoveAt(0);//убираем точку, на которой стоит враг
            return result;
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
