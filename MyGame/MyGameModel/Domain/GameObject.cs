using System.Drawing;

namespace MyGameModel.Domain
{
    public class GameObject
    {
        public Point Position { get; private set; }
        public ObjectType ObjectType { get; private set; }

        public GameObject(Point position, ObjectType objectType)
        {
            Position = position;
            ObjectType = objectType;
        }
    }
}
