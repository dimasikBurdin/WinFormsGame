using MyGameModelNew.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class InventoryTests
    {
        [Test]
        public void AddItemsToEmptyInventory()
        {
            var firstItem = new GameObject(Point.Empty, GameObjectType.Healer);
            var seconItem = new GameObject(Point.Empty, GameObjectType.Healer);
            var inventory = new Inventory();
            var player = new Player(100, Point.Empty, inventory);
            player.Inventory.AddToInventory(firstItem);
            player.Inventory.AddToInventory(seconItem);
            Assert.AreEqual(2, player.Inventory.CountHealers);
        }

        [Test]
        public void UseAndDeleteHealerOfInventoryCountOne()
        {
            var firstItem = new GameObject(Point.Empty, GameObjectType.Healer);
            var inventory = new Inventory();
            var player = new Player(50, Point.Empty, inventory);
            player.Inventory.AddToInventory(firstItem);
            player.UseHealer();
            Assert.AreEqual(0, player.Inventory.CountHealers);
            Assert.AreEqual(70, player.Health);
        }

        [Test]
        public void UseHealerWhenHealthMoreFull()
        {
            var firstItem = new GameObject(Point.Empty, GameObjectType.Healer);
            var inventory = new Inventory();
            var player = new Player(90, Point.Empty, inventory);
            player.Inventory.AddToInventory(firstItem);
            player.UseHealer();
            Assert.AreEqual(100, player.Health);
        }

        [Test]
        public void UseHealerWhenHealthFull()
        {
            var firstItem = new GameObject(Point.Empty, GameObjectType.Healer);
            var inventory = new Inventory();
            var player = new Player(100, Point.Empty, inventory);
            player.Inventory.AddToInventory(firstItem);
            player.UseHealer();
            Assert.AreEqual(100, player.Health);
            Assert.AreEqual(1, player.Inventory.CountHealers);
        }

        [Test]
        public void PlayerOpenGreenGateAndCountKeyIsZero()
        {
            var map = Map.FromLines(new[]
            {
                "P",
                "\\"
            });
            map.Player.Inventory.AddKeyToInventory(new Key(Point.Empty, KeyAndGateType.Green));
            map.Player.OpenGate(map);
            Assert.AreEqual(0, map.Player.Inventory.CountKeys);
        }

        [Test]
        public void PlayerCannotOpenGreenGateAndCountKeyIsOne()
        {
            var map = Map.FromLines(new[]
            {
                "P",
                "\\"
            });
            map.Player.Inventory.AddKeyToInventory(new Key(Point.Empty, KeyAndGateType.Red));
            map.Player.OpenGate(map);
            Assert.AreEqual(1, map.Player.Inventory.CountKeys);
        }
    }
}
