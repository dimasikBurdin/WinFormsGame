//using MyGameModel.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MyGameModelNew.Domain
{
    public class Enemy
    {
        private Map currentMap;
        private bool canMove = true;
        private List<Point> resTrack = new List<Point>();
        private Random rnd;
        public const int MaxHealth = 100;
        public int Health { get; set; }
        public double Speed { get; private set; }
        public double Damage { get; private set; }
        public Point Position { get; set; }
        public bool IsDeadEnemy { get; private set; }
        public int CurrentAnimation { get; set; }
        public int CurrentFrame { get; set; }
        private Point delta;

        public Enemy(int health, double speed, double damage, Point position)
        {
            Health = health;
            Speed = speed;
            Damage = damage;
            Position = position;
            rnd = new Random();
            CurrentAnimation = 46;
        }

        //#region
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
        //#endregion

        private Point lastPosition;
        private int hitTick;

        public void Act(Map map)
        {
            currentMap = map;
            if (Health <= 0)
            {
                //TerrainControl.RemoveEnemyFromList = this;
                IsDeadEnemy = true;
                return;
            }
            var playerPos = currentMap.Player.Position;
            var minRadius = new Point(Position.X - 5, Position.Y - 5);
            var maxRadius = new Point(Position.X + 5, Position.Y + 5);

            if (canMove && playerPos.X >= minRadius.X && playerPos.Y >= minRadius.Y && playerPos.X <= maxRadius.X && playerPos.Y <= maxRadius.Y)
                CreateTrack();
            if (resTrack?.Count != 0)
            {
                lastPosition = Position;
                Position = resTrack.First();
                delta = Position.SubStract(lastPosition);
                resTrack?.RemoveAt(0);
                if (Position == playerPos || currentMap.Enemies.Where(x => this != x).Any(x => x.Position == Position))
                    Position = lastPosition;
            }
            Animation();
            HitPlayer(playerPos);
        }

        private void Animation()
        {
            if (delta.X == 0 && delta.Y == 1)
            {
                CurrentAnimation = 92;
                CurrentFrame += 40;
            }
            if (delta.X == 0 && delta.Y == -1)
            {
                CurrentAnimation = 0;
                CurrentFrame += 40;
            }
            if (delta.X == 1 && delta.Y == 0)
            {
                CurrentAnimation = 138;
                CurrentFrame += 40;
            }
            if (delta.X == -1 && delta.Y == 0)
            {
                CurrentAnimation = 46;
                CurrentFrame += 40;
            }
            if (CurrentFrame == 120) CurrentFrame = 0;
        }

        private void HitPlayer(Point playerPos)
        {
            if (new Point() { X = Position.X + 1, Y = Position.Y } == playerPos || new Point() { X = Position.X - 1, Y = Position.Y } == playerPos
                || new Point() { X = Position.X, Y = Position.Y + 1 } == playerPos || new Point() { X = Position.X, Y = Position.Y - 1 } == playerPos)
            {
                if (currentMap.Player.Health == 0) return;
                if (hitTick == 2)
                    currentMap.Player.Health -= rnd.Next(10, 30);
                hitTick++;
                if (hitTick == 4) hitTick = 0;
            }
        }

        private void CreateTrack()
        {
            canMove = false;

            resTrack = FindTrack(currentMap.Player.Position);
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
            while (queue.Count != 0)
            {
                //if (track.Count > 5) return null;
                var currentPosition = queue.Dequeue();
                for (var dx = -1; dx <= 1; dx++)
                    for (var dy = -1; dy <= 1; dy++)
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
            var terrain = currentMap.Terrain;
            if (position.X < 0 || position.X >= terrain.GetLength(0)
                || position.Y < 0 || position.Y >= terrain.GetLength(1)) return false;
            var currentMapCellType = terrain[position.X, position.Y];
            return currentMapCellType != MapCell.Rock && currentMapCellType != MapCell.Water;
        }
    }
}
