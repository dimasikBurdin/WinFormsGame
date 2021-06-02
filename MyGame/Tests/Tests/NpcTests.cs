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
    public class NpcTests
    {
        [Test]
        public void CreateNpc()
        {
            var npc = new Npc(Point.Empty, null, null);
            Assert.AreEqual(Point.Empty, npc.Position);
            Assert.IsTrue(npc.Messages == null);
            Assert.IsTrue(npc.Item == null);
        }

        [Test]
        public void NpcGetMessage()
        {
            var npc = new Npc(Point.Empty, 
                new List<string> 
                {
                    "Test1",
                    "Test2"
                },
                null);
            Assert.AreEqual("Test1", npc.Messages[0]);
            Assert.AreEqual("Test2", npc.Messages[1]);
        }
    }
}
