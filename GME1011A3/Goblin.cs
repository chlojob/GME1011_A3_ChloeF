using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GME1011A3
{
    internal class Goblin : Minion
    {
        private static Random rng = new Random();

        //Goblins have dexterity that helps them to dodge and attack
        private int _dexterity;

        //Constructor
        public Goblin(int health, int armour, int dexterity) : base(health, armour)
        {
            if (dexterity < 0 || dexterity > 10)
                dexterity = 5;
            _dexterity = dexterity;
        }

        //goblins can dodge - increased chance if dexterity is high
        public override void TakeDamage(int damage)
        {
            if ((rng.Next(1, 15) < _dexterity))
            {
                Console.WriteLine("**Goblin-dodge, sneaky**");
            }
            else
            {
                //Console.WriteLine("Goblin takes damage.");
                _health -= (damage - _armour);
            }

        }

        //default damage is based on dexterity
        public override int DealDamage()
        {
            return rng.Next(1, _dexterity) + 1;
        }

        //Goblin special
        public int GoblinBite()
        {
            Console.WriteLine("**CHOMP**");
            return _dexterity * rng.Next(1, 3);
        }

        public override void Special(Hero target)
        {
            int damage = GoblinBite();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Goblin BITES and deals " + damage + " damage! Ow!");
            Console.ResetColor();
            target.TakeDamage(damage);
        }

        public override string ToString()
        {
            return "Goblin [Health: " + _health + ", Armour: " + _armour + ", Dexterity: " + _dexterity + "]";
        }
    }
}
