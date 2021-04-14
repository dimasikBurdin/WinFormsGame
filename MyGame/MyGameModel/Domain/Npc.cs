using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MyGameModel.Domain
{
    public class Npc
    {
        /// <summary>
        /// создаем каждого npc с определенными позицией и набором фраз, которые хранятся в массиве строк и выводятся по очереди по нажатию клавиши/клику мышки
        /// </summary>
        public Point Position { get; private set; }
        public string[] Messages { get; private set; }

        public Npc(Point position, string[] messages)
        {
            Position = position;
            Messages = messages;
        }

        public IEnumerable<string> TolkToPlayer()
        {
            foreach (var message in Messages.Where(x => x != null))
                yield return message;
        }

    }
}
