using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGameModelNew.Domain;
using MyViews;

namespace Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void CreatePlayer()
        {
            var player = new Player(65, new Point(10, 10), new Inventory());
            Assert.AreEqual(65, player.Health);
            Assert.AreEqual(new Point(10, 10), player.Position);
            Assert.AreEqual(0, player.Inventory.CountHealers);
            Assert.AreEqual(0, player.Inventory.CountKeys);
        }

        [Test]
        public void SwapWeapon()
        {
            var player = new Player(100, new Point(10, 10), new Inventory());
            player.Inventory.AddToInventory(new GameObject(Point.Empty, GameObjectType.WoodSword));
            Assert.AreEqual(GameObjectType.Hand, player.CurrentWeapon);
            player.SwapWeapon(2);
            Assert.AreEqual(GameObjectType.WoodSword, player.CurrentWeapon);
            player.SwapWeapon(3);
            Assert.AreEqual(GameObjectType.WoodSword, player.CurrentWeapon);
        }

        [Test]
        public void MovePlayer()
        {
            var map = Map.FromLines(new[]
            {
                "GP",
                "GT"
            });
            map.Player.Delta = new Point(0, 1);
            map.Player.Act(map);
            Assert.AreEqual(new Point(1, 1), map.Player.Position);
        }

        [Test]
        public void PlayerPickGreenKey()
        {
            var map = Map.FromLines(new[]
            {
                "P",
                "<"
            });
            map.Player.Delta = new Point(0, 1);
            map.Player.Act(map);
            Assert.AreEqual(1, map.Player.Inventory.CountGreenKeys);
        }

        [Test]
        public void PlayerPickRedKey()
        {
            var map = Map.FromLines(new[]
            {
                "P",
                "!"
            });
            map.Player.Delta = new Point(0, 1);
            map.Player.Act(map);
            Assert.AreEqual(1, map.Player.Inventory.CountRedKeys);
        }

        [Test]
        public void PlayerPickBlueKey()
        {
            var map = Map.FromLines(new[]
            {
                "P",
                ">"
            });
            map.Player.Delta = new Point(0, 1);
            map.Player.Act(map);
            Assert.AreEqual(1, map.Player.Inventory.CountBlueKeys);
        }

        [Test]
        public void PlayerPickHealer()
        {
            var map = Map.FromLines(new[]
            {
                "P",
                "H"
            });
            map.Player.Delta = new Point(0, 1);
            map.Player.Act(map);
            Assert.AreEqual(1, map.Player.Inventory.CountHealers);
        }

        [Test]
        public void PlayerPickWoodSwoard()
        {
            var map = Map.FromLines(new[]
            {
                "P",
                "@"
            });
            map.Player.Delta = new Point(0, 1);
            map.Player.Act(map);
            Assert.IsTrue(map.Player.Inventory.Weapon.Any(x => x == GameObjectType.WoodSword));
        }

        [Test]
        public void PlayerPickSteelSwoard()
        {
            var map = Map.FromLines(new[]
            {
                "P",
                "#"
            });
            map.Player.Delta = new Point(0, 1);
            map.Player.Act(map);
            Assert.IsTrue(map.Player.Inventory.Weapon.Any(x => x == GameObjectType.SteelSword));
        }

        [Test]
        public void PlayerCollisionWithNpc()
        {
            var map = Map.FromLines(new[]
            {
                "P",
                "N"
            });
            map.Player.Delta = new Point(0, 1);
            map.Player.Act(map);
            Assert.AreEqual(Point.Empty, map.Player.Position);
        }

        [Test]
        public void PlayerCollisionWithEnemy()
        {
            var map = Map.FromLines(new[]
            {
                "P",
                "E"
            });
            map.Player.Delta = new Point(0, 1);
            map.Player.Act(map);
            Assert.AreEqual(Point.Empty, map.Player.Position);
        }

        [Test]
        public void PlayerCollisionWithFire()
        {
            var map = Map.FromLines(new[]
            {
                "P",
                "O"
            });
            map.Player.Delta = new Point(0, 1);
            map.Player.Act(map);
            Assert.AreEqual(Point.Empty, map.Player.Position);
        }

        [Test]
        public void PlayerCollisionWithGate()
        {
            var map = Map.FromLines(new[]
            {
                "P",
                "\\"
            });
            map.Player.Delta = new Point(0, 1);
            map.Player.Act(map);
            Assert.AreEqual(Point.Empty, map.Player.Position);
        }

        [Test]
        public void PlayerCollisionWithForestAndWater()
        {
            var map = Map.FromLines(new[]
            {
                "PW",
                "FW"
            });
            map.Player.Delta = new Point(0, 1);
            map.Player.Act(map);
            map.Player.Delta = new Point(1, 0);
            Assert.AreEqual(Point.Empty, map.Player.Position);
        }

        [Test]
        public void PlayerIsDamagedEnemy()
        {
            var map = Map.FromLines(new[]
           {
                "P",
                "E"
            });            
            map.Player.Act(map);
            map.Enemies.ForEach(x => x.Act(map));
            map.Enemies.ForEach(x => x.Act(map));
            map.Enemies.ForEach(x => x.Act(map));//т к враг акткует на 2 тик таймера
            Assert.IsTrue(map.Player.Health <= 90);
        }

        [Test]
        public void PlayerOpenGreenGate()
        {
            var map = Map.FromLines(new[]
            {
                "P",
                "\\"
            });
            map.Player.Inventory.AddKeyToInventory(new Key(Point.Empty, KeyAndGateType.Green));
            map.Player.OpenGate(map);
            Assert.IsTrue(map.Gates.Any(x => x.Type == KeyAndGateType.Green && x.State == GateState.Open));
        }

        [Test]
        public void PlayerCannotOpenGreenGate()
        {
            var map = Map.FromLines(new[]
            {
                "P",
                "\\"
            });
            map.Player.Inventory.AddKeyToInventory(new Key(Point.Empty, KeyAndGateType.Red));
            map.Player.OpenGate(map);
            Assert.IsFalse(map.Gates.Any(x => x.Type == KeyAndGateType.Green && x.State == GateState.Open));
        }

        [Test]
        public void HitEnemy()
        {
            var map = Map.FromLines(new[]
            {
                "P",
                "E"
            });
            map.Player.CanHit = true;
            map.Player.Act(map);
            Assert.IsTrue(map.Enemies[0].Health != 100);
        }
    }
}
