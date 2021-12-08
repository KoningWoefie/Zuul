using System;
using System.Collections.Generic;
using System.Text;

namespace Zuul
{
    class Inventory
    {
        private int maxWeight;
        private Dictionary<string, Item> items;
        private Command command;
        private int totalWeight = 0;
        public Inventory(int maxWeight)
        {
            this.maxWeight = maxWeight;
            this.items = new Dictionary<string , Item>();
            items.Add("hammer", new Item(3, "Hammer"));
        }
        public bool Put(string itemName, Item item)
        {
            foreach (KeyValuePair<string, Item> entry in items)
            {
                totalWeight = totalWeight + entry.Value.Weight;
            }
            if(totalWeight <= maxWeight)
            {
                return true;
            }
            // check the Weight of the Item!
            // put Item in the items Collection
            // return true/false for success/failure
            return false;
        }
        public Item Get(string itemName)
        {
            switch(itemName)
            {
                case "hammer":
                    if(items.ContainsKey("hammer") == true)
                    {

                    }
                    break;
            }
            // remove Item from items Collection if found
            // return Item
            return null;
        }
    }
}
