using System;
using System.Collections.Generic;
using System.Text;

namespace Zuul
{
    public class Inventory
    {
        private int maxWeight;
        private Dictionary<string, Item> items;
        private int totalWeight = 0;
		public string itemList = null;
		private int remainingWeight;
        public Inventory(int maxWeight)
        {
            this.maxWeight = maxWeight;
            this.items = new Dictionary<string , Item>();
        }
        public bool Put(string itemName, Item item)
        {
				items.Add(itemName, item);
                return true;
        }
        public Item Get(string itemName)
        {
			Item item = null;
			if(items.ContainsKey(itemName))
			{
				item = items[itemName];
				items.Remove(itemName);
			}
			return item;
        }

		public string ListChestItems()
		{
			foreach (KeyValuePair<string, Item> entry in items)
			{
				itemList +=  entry.Key + " " + entry.Value.Description + " " + entry.Value.Weight;
			}
			return itemList;
		}
		
		public int RemainingWeight()
		{
			foreach (KeyValuePair<string, Item> entry in items)
			{
				totalWeight = totalWeight + entry.Value.Weight;
			}
			remainingWeight = maxWeight - totalWeight;
			return remainingWeight;
		}
    }
}
