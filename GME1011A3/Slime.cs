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

        public override void Special(Hero target)
        {
            int slimeDamage = DealDamage();
            Console.WriteLine("Slime shoots ACID GOOP and deals " + slimeDamage + " damage.");
            target.TakeDamage(slimeDamage);
        }

        public override string ToString()
        {
            return "Slime[" + _health + "," + _armour + "]";
        }
    }
}
