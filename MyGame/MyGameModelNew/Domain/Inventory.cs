using System.Collections.Generic;
using System.Linq;

namespace MyGameModelNew.Domain
{
    public class Inventory
    {
        private readonly List<GameObjectType> healers;
        public List<Key> Keys { get; set; }
        public List<GameObjectType> Weapon { get; set; }

        public Inventory()
        {
            healers = new List<GameObjectType>();
            Keys = new List<Key>();
            Weapon = new List<GameObjectType>();
        }

        public int CountHealers
            => healers.Count;

        public int CountKeys
            => Keys.Count;            

        public int CountRedKeys
        {
            get
            {
                var count = 0;
                Keys.Where(x => x.Type == KeyAndGateType.Red).Select(x => count++).ToArray();
                return count;
            }
        }

        public int CountGreenKeys
        {
            get
            {
                var count = 0;
                Keys.Where(x => x.Type == KeyAndGateType.Green).Select(x => count++).ToArray();
                return count;
            }
        }

        public int CountBlueKeys
        {
            get
            {
                var count = 0;
                Keys.Where(x => x.Type == KeyAndGateType.Blue).Select(x => count++).ToArray();
                return count;
            }
        }

        public int PlayerUseHealer(int currentHealthPlayer, int maxHealthPlayer)
        { 
            if (healers.Count == 0 || currentHealthPlayer == maxHealthPlayer) return currentHealthPlayer;            
            currentHealthPlayer += 20;
            if (currentHealthPlayer > maxHealthPlayer) currentHealthPlayer = maxHealthPlayer;
            healers.RemoveAt(0);
            return currentHealthPlayer;
        }

        public void AddToInventory(GameObject newObject)
        {
            switch(newObject.ObjectType)
            {
                case GameObjectType.Healer: 
                    healers.Add(newObject.ObjectType);
                    break;                
                case var a when newObject.ObjectType == GameObjectType.Knife || newObject.ObjectType == GameObjectType.SteelSword || newObject.ObjectType == GameObjectType.WoodSword:
                    Weapon.Add(newObject.ObjectType);
                    break;
            }
        }

        public void RemoveKey(KeyAndGateType keyType)
        {
            Keys.Remove(Keys.Where(x => x.Type == keyType).FirstOrDefault());
        }

        public void AddKeyToInventory(Key key)
        {
            Keys.Add(key);
        }
    }
}
