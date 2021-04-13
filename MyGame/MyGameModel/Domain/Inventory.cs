using System.Collections.Generic;

namespace MyGameModel.Domain
{
    public class Inventory   //private? мб нет, т к т.о. можно будет хранить лут во врагах, при убийстве которых он будет падать
    {
        public List<ObjectType> Healers { get; set; }//кол-во хилок отображается во время игры
        public List<ObjectType> Keys { get; set; }//мб тоже отображаются на экране
        public List<ObjectType> Weapon { get; set; }//находится в инвентаре => небольшая табличка

        public Inventory()
        {
            Healers = new List<ObjectType>();
            Keys = new List<ObjectType>();
            Weapon = new List<ObjectType>();
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

        public void AddToInventory(GameObject newObject)
        {
            switch(newObject.ObjectType)
            {
                case ObjectType.Healer: 
                    Healers.Add(newObject.ObjectType);
                    break;
                case ObjectType.Key:
                    Keys.Add(newObject.ObjectType);
                    break;
                case var a when newObject.ObjectType == ObjectType.Knife || newObject.ObjectType == ObjectType.Stick || newObject.ObjectType == ObjectType.Sword:
                    Weapon.Add(newObject.ObjectType);
                    break;
            }
        }
    }
}
