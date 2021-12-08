using System;
using System.Collections.Generic;
using System.Text;

namespace Zuul
{
    public class Player
    {
        private int health;
        public Room CurrentRoom { get; set; }
        public Player()
        {
            CurrentRoom = null;
            health = 100;
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
    }
}
