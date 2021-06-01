using System.Collections.Generic;
using System.Linq;

namespace MyGameModelNew.Domain
{
    public class Inventory   //private? мб нет, т к т.о. можно будет хранить лут во врагах, при убийстве которых он будет падать
    {
        private List<GameObjectType> Healers { get; set; }//кол-во хилок отображается во время игры
        public List<Key> Keys { get; set; }//мб тоже отображаются на экране
        public List<GameObjectType> Weapon { get; set; }//находится в инвентаре => небольшая табличка//почему приватное свойство?!

        public Inventory()
        {
            Healers = new List<GameObjectType>();
            Keys = new List<Key>();
            Weapon = new List<GameObjectType>();
        }

        public int CountHealers
        {
            get
            {
                return Healers.Count;
            }
        }
        public int CountKeys//подумать, т к в листе ключи 3 типов, а надо кол-во каждого типа
        {
            get
            {
                return Keys.Count;
            }
        }

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
            if (Healers.Count == 0 || currentHealthPlayer == maxHealthPlayer) return currentHealthPlayer;            
            currentHealthPlayer += 20;
            if (currentHealthPlayer > maxHealthPlayer) currentHealthPlayer = maxHealthPlayer;
            Healers.RemoveAt(0);
            return currentHealthPlayer;
        }

        public void AddToInventory(GameObject newObject)
        {
            switch(newObject.ObjectType)
            {
                case GameObjectType.Healer: 
                    Healers.Add(newObject.ObjectType);
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
