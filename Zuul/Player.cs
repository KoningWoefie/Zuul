using System;
using System.Collections.Generic;
using System.Text;

namespace Zuul
{
    public class Player
    {
        private int health;
        Inventory begpeg;
        public Room CurrentRoom { get; set; }
        public Player()
        {
            CurrentRoom = null;
            health = 100;
            begpeg = new Inventory(2000);
        }
        public int Health()
        {
            return health;
        }
        public void ShowHealth()
        {
            Console.WriteLine("Your health is now " + health);
        }
        public void Damage(int amount)
        {
            health = health - amount;
        }

        public void Heal(int amount)
        {
            if(health < 100)
            {
                health = health + amount;
            }
        }

        public bool IsAlive()
        {
            if (health == 0)
            {
                return false;
            }
            return true;
        }

		public bool TakeFromChest(string itemName)
		{
			Item item = CurrentRoom.Chest.Get(itemName);
			if(item != null)
			{
				if(begpeg.RemainingWeight() >= item.Weight)
				{
					begpeg.Put(itemName, item);
					Console.WriteLine("You picked up " + itemName);
					return true;
				}
				else if(begpeg.RemainingWeight() <= item.Weight)
				{
					CurrentRoom.Chest.Put(itemName, item);
					Console.WriteLine("You failed to pick up " + itemName);
				}
			}
			if(item == null)
			{
				Console.WriteLine("There is no item named " + itemName + " in the room.");
			}
			return false;
		}

		public bool DropToChest(string itemName)
		{
			Item item = begpeg.Get(itemName);
			if(item != null)
			{
				CurrentRoom.Chest.Put(itemName, item);
				Console.WriteLine("You have thrown " + itemName + " away like an old rag");
				return true;
			}
			// remove Item from your inventory.
			// Add the Item to the Room
			// inspect returned values
			// communicate to the user what's happening
			// return true/false for success/failure
			Console.WriteLine("You don't have " + itemName + " in your inventory.");
			return false;
		}
    }
}
