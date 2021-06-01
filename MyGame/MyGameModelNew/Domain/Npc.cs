using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MyGameModelNew.Domain
{
    public class Npc
    {
        /// <summary>
        /// создаем каждого npc с определенными позицией и набором фраз, которые хранятся в массиве строк и выводятся по очереди по нажатию клавиши/клику мышки
        /// </summary>
        public Point Position { get; private set; }
        public string[] Messages { get; private set; }
        public GameObject Item { get; private set; }
        public bool MessagesIsEmpty { get; private set; }

        public Npc(Point position, string[] messages, GameObject item)
        {
            Position = position;
            Messages = messages;
            Item = item;
            MessagesIsEmpty = false;
        }

        public IEnumerable<string> TolkToPlayer()
        {
            foreach (var message in Messages.Where(x => x != null))
                yield return message;
            MessagesIsEmpty = true;
        }

        public void GiveItem()
        {

        }


    }
}
