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
    public class GateTests
    {
        [Test]
        public void CreateGateAndGateIsLock()
        {
            var map = Map.FromLines(new[]
            {                
                "\\",
                "|",
                "/"
            });
            Assert.IsTrue(map.Gates.All(x => x.State == GateState.Lock));
        }

        [Test]
        public void OpeningGreenGate()
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
        public void OpeningRedGate()
        {
            var map = Map.FromLines(new[]
            {
                "P",
                "|"
            });
            map.Player.Inventory.AddKeyToInventory(new Key(Point.Empty, KeyAndGateType.Red));
            map.Player.OpenGate(map);
            Assert.IsTrue(map.Gates.Any(x => x.Type == KeyAndGateType.Red && x.State == GateState.Open));
        }

        [Test]
        public void OpeningBlueGate()
        {
            var map = Map.FromLines(new[]
            {
                "P",
                "/"
            });
            map.Player.Inventory.AddKeyToInventory(new Key(Point.Empty, KeyAndGateType.Blue));
            map.Player.OpenGate(map);
            Assert.IsTrue(map.Gates.Any(x => x.Type == KeyAndGateType.Blue && x.State == GateState.Open));
        }
    }
}
