using System;
using System.Threading;

namespace rpg
{

    interface Menu                                      // interfejs do uruchomienia gry
    {
        public abstract void Boot();
    }

    abstract class MainMenu : Menu
    {
        public abstract void Boot();
    }

    class Game : MainMenu                               // klasa gry
    {
        bool EndGame = false;                           // rozpoczęcie gry i bootup
        bool Booting = true;

        public override void Boot()                     // przeciążenie metody uruchamiania gry
        {
            bool Bstart = true;
            int SLength = 2;
            int ELength = 2;
            string[] Sslash = new string[SLength];
            string[] Eslash = new string[ELength];
            Eslash[0] = " ";
            Sslash[0] = ">";
            string[] Ending = {"Still thinking...", "Realizing...", "What a joke...", "Getting closer...", "He's watching...", "Not yet..."};

            while (Booting == true)
            {
                Console.Clear();
                Console.WriteLine("... \n\n\n\n");

                Console.Write(Sslash[0]);               // pętle for, zmieniające znaki podczas wybierania
                for (int i = 1; i < SLength; i++)
                {
                    Sslash[i] = "Start";
                    Console.Write(Sslash[i]);
                }

                Console.Write("\n" + Eslash[0]);
                for (int i = 1; i < ELength; i++)
                {
                    Eslash[i] = "Exit";
                    Console.Write(Eslash[i]);
                }

                ConsoleKeyInfo keyinfo = Console.ReadKey(true);
                if (keyinfo.Key == ConsoleKey.W)    // if-y wybierania opcji
                {
                    Sslash[0] = ">";
                    Eslash[0] = " ";
                    Bstart = true;
                }
                else if (keyinfo.Key == ConsoleKey.S)
                {
                    Eslash[0] = ">";
                    Sslash[0] = " ";
                    Bstart = false;
                }

                if (keyinfo.Key == ConsoleKey.Enter && Bstart == true)      // if-y akceptopwania wyboru
                {
                    Booting = false;                // zwolnienie pętli
                    Console.Clear();
                    Start.Begining();               // Rozpoczyna grę 
                }
                else if (keyinfo.Key == ConsoleKey.Enter && Bstart == false)
                {
                    Booting = false;

                    Console.Clear();
                    Random rand = new Random();
                    int index = rand.Next(Ending.Length);
                    string Quote = Ending[index];
                    foreach (char c in Quote)
                    {
                        Console.Write(c);
                        Thread.Sleep(100);
                    }
                    Thread.Sleep(3000);
                    System.Environment.Exit(0);     // Wyjście z gry
                }
            }
        }
    }

    public class Start
    {
        public static void Begining()
        {
            Introduce();
        }
        private static void Introduce()
        {
            string mesOne = "Welcome.";
            string mesTwo = "Welcome to the Survey Program.";
            string mesThree = "Name your Character...";
            foreach (char c in mesOne)
            {
                Console.Write(c);
                Thread.Sleep(100);
            }
            Thread.Sleep(2000);
            Console.Clear();
            foreach (char c in mesTwo)
            {
                Console.Write(c);
                Thread.Sleep(100);
            }
            Thread.Sleep(2000);
            Console.Clear();
            foreach(char c in mesThree)
            {
                Console.Write(c);
                Thread.Sleep(100);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game RPG = new Game();
            RPG.Boot();
        }
    }
}
