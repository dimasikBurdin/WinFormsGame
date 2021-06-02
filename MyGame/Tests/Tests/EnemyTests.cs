using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGameModelNew.Domain;
using MyViews;
using System.Drawing;

namespace Tests
{
    [TestFixture]
    public class EnemyTests
    {
        [Test]
        public void CreateEnemy()
        {
            var enemy = new Enemy(100, 30, new Point(1, 5));
            Assert.AreEqual(100, enemy.Health);
            Assert.AreEqual(30, enemy.Damage);
            Assert.AreEqual(new Point(1, 5), enemy.Position);
        }

        [Test]
        public void CollisionWithForestAndWater()
        {
            var map = Map.FromLines(new[]
            {
                "EFP",
                "WWG"
            });
            for (var i = 0; i < 5; i++)
                map.Enemies[0].Act(map);
            Assert.AreEqual(Point.Empty, map.Enemies[0].Position);
        }

        [Test]
        public void MoveToPlayer2x3()
        {
            var map = Map.FromLines(new[]
            {
                "EGG",
                "GGP"
            });
            map.Enemies[0].Act(map);
            map.Enemies[0].Act(map);
            Assert.AreEqual(new Point(1, 1), map.Enemies[0].Position);
        }

        [Test]
        public void HitPlayer()
        {
            var map = Map.FromLines(new[]
            {
                "EGG",
                "GGP"
            });
            for (var j = 0; j < 5; j++)
            {
                map.Player.Health = 100;
                for (var i = 0; i < 30; i++)
                {
                    map.Enemies[0].Act(map);
                }
                Assert.IsTrue(map.Player.Health <= 0);
            }
        }

        [Test]
        public void BigRadiusToPlayer_EnemyStay()
        {
            var map = Map.FromLines(new[]
            {
                "EGGGGGGG",
                "GGGGGGGP"
            });
            map.Enemies[0].Act(map);
            Assert.AreEqual(Point.Empty, map.Enemies[0].Position);
        }

        [Test]
        public void NormalRadiusToPlayer_EnemyStay()
        {
            var map = Map.FromLines(new[]
            {
                "EGGGG",
                "GGGGP"
            });
            map.Enemies[0].Act(map);
            Assert.IsTrue(map.Enemies[0].Position != Point.Empty);
        }
    }
}
