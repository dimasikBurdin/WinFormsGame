using System.Collections.Generic;

namespace MyGameModelNew.Domain
{
    public class Inventory   //private? мб нет, т к т.о. можно будет хранить лут во врагах, при убийстве которых он будет падать
    {
        private List<GameObjectType> Healers { get; set; }//кол-во хилок отображается во время игры
        private List<GameObjectType> Keys { get; set; }//мб тоже отображаются на экране
        private List<GameObjectType> Weapon { get; set; }//находится в инвентаре => небольшая табличка

        public Inventory()
        {
            Healers = new List<GameObjectType>();
            Keys = new List<GameObjectType>();
            Weapon = new List<GameObjectType>();
        }

        public int CountHealers
        {
            get
            {
                return Healers.Count;
            }
        }
        public int CountKeys
        {
            get
            {
                return Keys.Count;
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
                case GameObjectType.Key:
                    Keys.Add(newObject.ObjectType);
                    break;
                case var a when newObject.ObjectType == GameObjectType.Knife || newObject.ObjectType == GameObjectType.Stick || newObject.ObjectType == GameObjectType.Sword:
                    Weapon.Add(newObject.ObjectType);
                    break;
            }
        }
    }
}
