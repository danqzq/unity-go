namespace UnityGo.Models
{
    [System.Serializable]
    public class Player 
    {
        public string name;
        public int health;
        public int mana;
        public Item[] inventory;
    }

    [System.Serializable]
    public class Item 
    {
        public string name;
        public string type;
        public int quantity;
    }

    [System.Serializable]
    public class Enemy 
    {
        public string name;
        public int health;
        public string type;
    }

}
