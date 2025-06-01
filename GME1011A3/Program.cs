using System.Collections.Generic;

namespace GME1011A3
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Epic battle goes here :)
            Random rng = new Random();

            Thread.Sleep(1000);
            Console.Write("Welcome. ");
            Thread.Sleep(1000);
            Console.WriteLine("Welcome to the battle. ");

            Thread.Sleep(1000);
            Console.Write("\nWhat shall we call you?: ");
            string name = Console.ReadLine();
            Thread.Sleep(1000);
            Console.WriteLine("\nHello, " + name + ".");

            Thread.Sleep(1000);
            Console.Write("\nEnter your health, hero: ");
            int health = int.Parse(Console.ReadLine());

            Thread.Sleep(1000);
            Console.Write("\nHow strong are you? (1-10): ");
            int strength = int.Parse(Console.ReadLine());

            Console.Write("\nHow many baddies will you take on? ");
            int numBaddies = int.Parse(Console.ReadLine());
            int numAliveBaddies = numBaddies;

            Thread.Sleep(1000);
            Console.Write("\nWonderful. ");
            Thread.Sleep(500);
            Console.WriteLine("Good luck, " + name + ".");
            Console.WriteLine();
            Thread.Sleep(2000);

            Fighter hero = new Fighter(health, name, strength);

            List<Minion> baddies = new List<Minion>();

            for (int i = 0; i < numBaddies; i++)
            {
                int roll = rng.Next(2); // Rolls 0 or 1, so a 50/50 coin flip
                if (roll == 0)
                {
                    baddies.Add(new Goblin(rng.Next(30, 35), rng.Next(1, 5), rng.Next(1, 10)));
                }
                else
                {
                    baddies.Add(new Skellie(rng.Next(25, 31), 0));
                }
            }

            Console.WriteLine("Here are the baddies!!!");
            for (int i = 0; i < baddies.Count; i++)
            {
                Console.WriteLine(baddies[i]);
            }
            Thread.Sleep(2000);
            Console.WriteLine("\n\n");
            Console.WriteLine("Let the EPIC battle begin!!!");
            Console.WriteLine("----------------------------");
            Thread.Sleep(1000);


            //loop runs as long as there are baddies still alive and the hero is still alive!!
            while (numAliveBaddies > 0 && !hero.isDead())
            {
                //figure out which enemy we are going to battle - the first one that isn't dead
                int indexOfEnemy = 0;
                while (baddies[indexOfEnemy].isDead())
                {
                    indexOfEnemy++;
                }

                //hero deals damage first
                Console.WriteLine(hero.GetName() + " is attacking enemy #" + (indexOfEnemy + 1) + " of " + numBaddies + ". Eek, it's a " + baddies[indexOfEnemy].GetType().Name);

                int heroDamage;

                if (rng.Next(100) < 33 && hero.GetStrength() > 0)
                {
                    heroDamage = hero.Stoopid();
                    Console.WriteLine(hero.GetName() + " goes STOOPID CRAZY and deals " + heroDamage + " damage!");
                }
                else
                {
                    if (hero.GetStrength() == 0)
                        Console.WriteLine("Oh no! " + hero.GetName() + " tries to go STOOPID CRAZY, but can't muster up the strength!");

                    heroDamage = hero.DealDamage();
                    Console.WriteLine("Hero deals " + heroDamage + " heroic damage.");
                }

                baddies[indexOfEnemy].TakeDamage(heroDamage);

                //did we vanquish the baddie we were battling?
                if (baddies[indexOfEnemy].isDead())
                {
                    numAliveBaddies--; //one less baddie to worry about.
                    Console.WriteLine("Enemy #" + (indexOfEnemy + 1) + " has been dispatched to void.");
                }
                else //baddie survived, now attacks the hero
                {
                    int baddieDamage = baddies[indexOfEnemy].DealDamage();  //how much damage?
                    Console.WriteLine("Enemy #" + (indexOfEnemy + 1) + " deals " + baddieDamage + " damage!");
                    hero.TakeDamage(baddieDamage); //hero takes damage




                    //TODO: The baddie doesn't ever use their special attack - but they should. Change the above to 
                    //have a 33% chance that the baddie uses their special, and 67% that they use their regular attack.




                    //let's look in on our hero.
                    Console.WriteLine(hero.GetName() + " has " + hero.GetHealth() + " health remaining.");
                    if (hero.isDead()) //did the hero die
                    {
                        Console.WriteLine(hero.GetName() + " has died. All hope is lost.");
                    }

                }
                Console.WriteLine("-----------------------------------------");
            }
            //if we made it this far, the hero is victorious! (that's what the message says.
            if (!hero.isDead())
                Console.WriteLine("\nAll enemies have been dispatched!! " + hero.GetName() + " is victorious!");
        }

    }
}