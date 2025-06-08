using System.Collections.Generic;
using System.Threading;

namespace GME1011A3
{
    internal class Program
    {
        static void Main(string[] args)
        {

            bool playAgain = true;

            while (playAgain)
            {

                //Epic battle goes here :)
                Random rng = new Random();

                DateTime now = DateTime.Now;
                int hour = now.Hour;

                if (hour < 12)
                {
                    Thread.Sleep(1000);
                    Console.Write("Good morning, brave adventurer. ");
                }
                else if (hour < 18)
                {
                    Thread.Sleep(1000);
                    Console.Write("Good afternoon, brave adventurer. ");
                }
                else
                {
                    Thread.Sleep(1000);
                    Console.Write("Good evening, brave adventurer. ");
                }

                Thread.Sleep(1000);
                Console.WriteLine("Welcome to the battle. ");

                Thread.Sleep(1000);
                Console.Write("\nWhat shall we call you?: ");
                string name = Console.ReadLine();
                Thread.Sleep(1000);
                Console.WriteLine("\nHello, " + name + ".");

                Thread.Sleep(1000);
                Console.Write("\nEnter your health (1-100): ");
                int health = int.Parse(Console.ReadLine());

                Thread.Sleep(1000);
                Console.Write("\nHow strong are you? (1-10): ");
                int strength = int.Parse(Console.ReadLine());

                Console.Write("\nHow many baddies will you take on? ");
                int numBaddies = int.Parse(Console.ReadLine());
                int numAliveBaddies = numBaddies;

                Thread.Sleep(1000);
                Console.WriteLine("\n1 - Instant");
                Console.WriteLine("2 - Fast");
                Console.WriteLine("3 - Slow");
                Console.WriteLine("4 - Detailed");
                Console.Write("Set your game speed: ");
                int gameSpeed = int.Parse(Console.ReadLine()); // Prompts user for game speed,
                                                               // sets the thread delay amount used across project

                int delay;

                if (gameSpeed == 1)
                    delay = 0;
                else if (gameSpeed == 2)
                    delay = 50;
                else if (gameSpeed == 3)
                    delay = 800;
                else if (gameSpeed == 4)
                    delay = 2000;
                else
                    delay = 800;

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
                    int rollBadType = rng.Next(3); // Rolls 0, 1, or 2 = 33% chance

                    if (rollBadType == 0)
                    {
                        baddies.Add(new Goblin(rng.Next(30, 35), rng.Next(1, 5), rng.Next(1, 10)));
                    }
                    else if (rollBadType == 1)
                    {
                        baddies.Add(new Skellie(rng.Next(25, 31), 0));
                    }
                    else
                    {
                        baddies.Add(new Slime(rng.Next(20, 26), 0));
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
                    Thread.Sleep(delay);

                    int heroDamage;

                    if (rng.Next(100) < 33 && hero.GetStrength() > 0)
                    {
                        heroDamage = hero.Stoopid();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(hero.GetName() + " goes STOOPID CRAZY and deals " + heroDamage + " damage!");
                        Console.ResetColor();
                        if (delay > 0)
                            Thread.Sleep(delay);
                    }
                    else
                    {
                        if (hero.GetStrength() == 0)
                            Console.WriteLine("Oh no! " + hero.GetName() + " tries to go STOOPID CRAZY, but can't muster up the strength!");
                        if (delay > 0)
                            Thread.Sleep(delay);

                        heroDamage = hero.DealDamage();
                        Console.WriteLine("Hero deals " + heroDamage + " damage.");
                        if (delay > 0)
                            Thread.Sleep(delay);
                    }

                    baddies[indexOfEnemy].TakeDamage(heroDamage);
                    if (delay > 0)
                        Thread.Sleep(delay);

                    //did we vanquish the baddie we were battling?
                    if (baddies[indexOfEnemy].isDead())
                    {
                        numAliveBaddies--; //one less baddie to worry about.
                        Console.WriteLine("Enemy #" + (indexOfEnemy + 1) + " has been dispatched to the void.");
                        if (delay > 0)
                            Thread.Sleep(delay);
                    }
                    else //baddie survived, now attacks the hero
                    {
                        int rollBadSpec = rng.Next(100); // 0-99

                        if (rollBadSpec < 33) // 33% chance for special attack
                        {
                            Console.WriteLine("Enemy #" + (indexOfEnemy + 1) + " attempts a SPECIAL!");
                            baddies[indexOfEnemy].Special(hero);
                            if (delay > 0)
                                Thread.Sleep(delay);
                        }
                        else
                        {
                            int baddieDamage = baddies[indexOfEnemy].DealDamage();
                            Console.WriteLine("Enemy #" + (indexOfEnemy + 1) + " deals " + baddieDamage + " damage!");
                            hero.TakeDamage(baddieDamage);
                            if (delay > 0)
                                Thread.Sleep(delay);
                        }

                        //let's look in on our hero.
                        Console.WriteLine(hero.GetName() + " has " + hero.GetHealth() + " health remaining.");
                        if (delay > 0)
                            Thread.Sleep(delay);

                        if (hero.isDead()) //did the hero die
                        {
                            if (delay > 0)
                                Thread.Sleep(delay);
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(hero.GetName() + " has died. All hope is lost.");
                            Console.ResetColor();
                            Console.WriteLine();
                        }

                    }
                    Console.WriteLine("-----------------------------------------");
                    if (delay > 0)
                        Thread.Sleep(delay);
                }
                //if we made it this far, the hero is victorious! (that's what the message says.
                if (!hero.isDead())
                    Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("\nAll enemies have been dispatched!! " + hero.GetName() + " is victorious, with " + hero.GetHealth() + " to spare!");
                Console.ResetColor();

                // Play again? loop prompt
                Console.WriteLine();
                Console.WriteLine("\nPlay again? (Y/N)");
                string response = Console.ReadLine().Trim().ToLower();

                if (response != "y" && response != "yes")
                {
                    playAgain = false;
                }
                else
                {
                    Console.Clear();
                }

            }

        }

    }
}