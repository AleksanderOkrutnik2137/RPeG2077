using System;

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
                if (keyinfo.Key == ConsoleKey.W)        // if-y wybierania opcji
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
                    Console.WriteLine("Passed");    // Game.Start
                }
                else if (keyinfo.Key == ConsoleKey.Enter && Bstart == false)
                {
                    Booting = false;
                    System.Environment.Exit(0);     // Wyjście z gry
                }
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
