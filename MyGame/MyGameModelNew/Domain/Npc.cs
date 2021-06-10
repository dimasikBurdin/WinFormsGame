using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MyGameModelNew.Domain
{
    public class Npc
    {
        public Point Position { get; private set; }
        public List<string> Messages { get; private set; }
        public GameObject Item { get; private set; }
        public bool MessagesIsEmpty { get; private set; }

        public Npc(Point position, List<string> messages, GameObject item)
        {
            Position = position;
            Messages = messages;
            Item = item;
            MessagesIsEmpty = false;
        }

        public IEnumerable<string> TolkToPlayer()
        {
            if (Messages == null) yield break;
            foreach (var message in Messages.Where(x => x != null))
                yield return message;
            MessagesIsEmpty = true;
        }

        public void GiveItem()
        {

        }
    }
}
