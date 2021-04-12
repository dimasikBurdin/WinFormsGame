using MyGameModel.Domain;
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
            var firstItem = new GameObject(Point.Empty, ObjectType.Healer);
            var seconItem = new GameObject(Point.Empty, ObjectType.Healer);
            var inventory = new Inventory();
            var player = new Player(100, 30, 30, Point.Empty, inventory);
            player.Inventory.AddToInventory(firstItem);
            player.Inventory.AddToInventory(seconItem);
            Assert.AreEqual(2, player.Inventory.CountHealers);
        }

        [Test]
        public void UseAndDeleteHealerOfInventoryCountOne()
        {
            var firstItem = new GameObject(Point.Empty, ObjectType.Healer);
            var inventory = new Inventory();
            var player = new Player(50, 30, 30, Point.Empty, inventory);
            player.Inventory.AddToInventory(firstItem);
            player.UseHealer();
            Assert.AreEqual(0, player.Inventory.CountHealers);
            Assert.AreEqual(70, player.Health);
        }

    }
}
