﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GME1011A3
{
    internal class Skellie : Minion
    {
        private static Random rng = new Random();

        //Constructor - Skellies don't have armour, set it to 0
        public Skellie(int health, int armour) : base(health, armour)
        {
            _armour = 0;
        }

        //skellies take half damage - because they are skellies
        public override void TakeDamage(int damage)
        {
            _health -= damage / 2;
        }

        //Skelles do 2-8 damage by default
        public override int DealDamage()
        {
            return rng.Next(2, 9);
        }

        //Skellie special.
        public int SkellieRattle()
        {
            Console.WriteLine("**spooky rattling**");
            return rng.Next(7, 15);
        }

        public override void Special(Hero target)
        {
            int damage = SkellieRattle();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Skellie RATTLES and deals " + damage + " damage! Ouch!");
            Console.ResetColor();
            target.TakeDamage(damage);
        }

        public override string ToString()
        {
            return "Skellie [Health: " + _health + "]";
        }
    }
}
