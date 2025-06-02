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
        }

        public override int DealDamage()
        {
            return rng.Next(2, 5);
        }

        public override void TakeDamage(int damage)
        {
            _health -= damage / 2;
        }

        public int SlimeGoop()
        {
            Console.WriteLine("**gurgling**");
            Random rng = new Random();
            return rng.Next(5, 12); 
        }

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
            return "Slime [Health: " + _health + ", Armour: " + _armour + "]";
        }
    }
}
