using System.Drawing;

namespace MyGameModel.Domain
{
    public class GameObject
    {
        public Point Position { get; private set; }
        public GameObjectType ObjectType { get; private set; }

        public GameObject(Point position, GameObjectType objectType)
        {
            Position = position;
            ObjectType = objectType;
        }
    }
}
