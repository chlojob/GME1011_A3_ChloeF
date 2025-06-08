using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GME1011A3
{
    internal class Slime : Minion
    {
        private static Random rng = new Random();
        public Slime(int health, int armour) : base(health, armour)
        {
            _armour = 0; // Slimes are goop. No armour!
        }

        public override int DealDamage()
        {
            return rng.Next(2, 5); // 2-4 damage normally
        }

        public override void TakeDamage(int damage)
        {
            _health -= (int)(damage * 1.1); // goop absorbs more damage
        }

        public int SlimeGoop()
        {
            Console.WriteLine("**gurgling**");
            Random rng = new Random();
            return rng.Next(5, 12); 
        }
                                                        // Slime special
        public override void Special(Hero target)
        {
            int slimeDamage = SlimeGoop();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Slime shoots ACID GOOP and deals " + slimeDamage + " damage.");
            Console.ResetColor();
            target.TakeDamage(slimeDamage);
        }

        public override string ToString()
        {
            return "Slime [Health: " + _health + "]";
        }
    }
}
