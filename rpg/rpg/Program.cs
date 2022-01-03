using System;
using System.Threading;


namespace rpg
{
    interface Menu                  // interfejs do uruchomienia gry
    {
        public abstract void Boot();
    }

    abstract class MainMenu : Menu
    {
        public abstract void Boot();
    }

    class Game : MainMenu       // klasa gry
    {
        bool EndGame = false;   // rozpoczęcie gry i bootup
        bool Booting = true;

        public override void Boot()         // przeciążenie metody uruchamiania gry
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
                        Thread.Sleep(50);
                    }
                    Thread.Sleep(3000);
                    Environment.Exit(0);     // Wyjście z gry
                    
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
        public static void Introduce()              // metoda rozpoczęcia procesu wprowadzania do gry
        {
            string[] mes = {"Welcome.", "Welcome to the Survey Program.",};
            foreach (char c in mes[0])
            {
                Console.Write(c);
                Thread.Sleep(50);
            }
            Thread.Sleep(2000);
            Console.Clear();
            foreach (char c in mes[1])
            {
                Console.Write(c);
                Thread.Sleep(50);
            }
            Thread.Sleep(2000);
            Creator.Create();
        }

    }
    class Creator           // klasa creatora postaci
    {
        bool CreateEnd = false;

        public static void Create()                        
        {
            int SLength = 2;
            int ELength = 2;
            string[] BanSlash = new string[SLength];
            string[] WizSlash = new string[ELength];
            BanSlash[0] = " ";
            WizSlash[0] = ">";
            bool Clasa = false;
            int klasa = 1;                  // 1 = wizjoner, 2 = bandyta

            string[] mes = {"Choose your class...\n\n" };

            Console.Clear();
            foreach (char c in mes[0])
            {
                Console.Write(c);
                Thread.Sleep(50);
            }

            while (Clasa == false)               // pętle while, zmieniające znaki podczas wybierania
            {
                Console.Clear();
                Console.Write(mes[0]);

                Console.Write(WizSlash[0]);
                for (int i = 1; i < SLength; i++)
                {
                    WizSlash[i] = "Wizjoner";
                    Console.Write(WizSlash[i]);
                }
                Console.Write("\n" + BanSlash[0]);
                for (int i = 1; i < ELength; i++)
                {
                    BanSlash[i] = "Bandyta";
                    Console.Write(BanSlash[i]);
                }

                ConsoleKeyInfo keyinfo = Console.ReadKey(true);
                if (keyinfo.Key == ConsoleKey.W)
                {
                    WizSlash[0] = ">";
                    BanSlash[0] = " ";
                    klasa = 1;
                }
                else if (keyinfo.Key == ConsoleKey.S)
                {
                    BanSlash[0] = ">";
                    WizSlash[0] = " ";
                    klasa = 2;
                }

                if (keyinfo.Key == ConsoleKey.Enter && klasa == 1)      // if-y akceptopwania wyboru klasy
                {
                    Clasa = true;                
                    Console.Clear();
                    Console.WriteLine("passed - wizjoner");             
                }
                else if (keyinfo.Key == ConsoleKey.Enter && klasa == 2)
                {
                    Clasa = true;
                    Console.Clear();
                    Console.WriteLine("passed - bandyta");
                }
            }
        }
    }

    interface IPlayer                           // klasy gracza
    {

    }
    abstract class Player : IPlayer
    {

    }
    class Wizjoner : IPlayer
    {
        string Name { get; set; }
        int HeroHP = 10;
        int HeroATK = 20;

        public void setName(string newName)
        {
            Name = newName;
        }
    }
    class Bandyta : IPlayer
    {
        string Name { get; set; }
        int HeroHP = 30;
        int HeroATK = 17;

        public void setName(string newName)
        {
            Name = newName;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Game RPG = new Game();
            RPG.Boot();
        }
    }
}
