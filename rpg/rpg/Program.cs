using System;
using System.IO;
using System.Threading;


namespace rpg
{
    public class Dialogue                    // Prototyp generatora animowanych dialogów (na razie tylko na "exicie")
    {
        public static void Generate(int stringChar, string[] mes, string Smes, int index, int time, int clear)      // stringchar - 1/2 (pobiera array/string), mes - array, smes - string, index - do array, time - odczekaj po dialogu, clear - czyść przed dialogiem
        {
            if (clear == 1)
            {
                Console.Clear();
            }
            if (stringChar == 1)
            {
                foreach (char c in mes[index])
                {
                    Console.Write(c);
                    Thread.Sleep(50);
                }
            }
            else if (stringChar == 2)
            {
                foreach (char c in Smes)
                {
                    Console.Write(c);
                    Thread.Sleep(50);
                }
            }
            Thread.Sleep(time);
        }
    }
    interface MainMenu                  // interfejs do uruchomienia gry
    {
        public abstract void Boot();
    }

    abstract class Menu : MainMenu
    {
        public abstract void Boot();
    }

    class Game : Menu       // klasa gry
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
                    Start();               // Rozpoczyna grę 
                }
                else if (keyinfo.Key == ConsoleKey.Enter && Bstart == false)
                {
                    Booting = false;
                    Random rand = new Random();
                    Dialogue.Generate(1, Ending, null, rand.Next(Ending.Length), 3000, 1);
                    Environment.Exit(0);     // Wyjście z gry
                }
            }
        }
        public void Start()
        {
            int SLength = 2;
            int ELength = 2;
            string[] BanSlash = new string[SLength];
            string[] WizSlash = new string[ELength];
            BanSlash[0] = " ";
            WizSlash[0] = ">";
            bool Clasa = true;
            int klasa = 1;

            string[] mesBegin = { "Welcome.", "Welcome to the rpg.exe - SURVEY_PROGRAM." };
            string[] mesClass = { "Choose your class.\n\n", "Selected class", "Visionary.", "Rioter.", " - " };
            string[] mesName = { "Name your", " Visionary.\n\n", " Rioter\n\n", "You have choosen", " - ", "." };

            Dialogue.Generate(1, mesBegin, null, 0, 2000, 1);
            Dialogue.Generate(1, mesBegin, null, 1, 2000, 1);
            Dialogue.Generate(1, mesClass, null, 0, 0, 1);

            while (Clasa == true)               // pętle while, zmieniające znaki podczas wybierania
            {
                Console.Clear();
                Console.Write(mesClass[0]);

                Console.Write(WizSlash[0]);
                for (int i = 1; i < SLength; i++)
                {
                    WizSlash[i] = "Visionary";
                    Console.Write(WizSlash[i]);
                }
                Console.Write("\n" + BanSlash[0]);
                for (int i = 1; i < ELength; i++)
                {
                    BanSlash[i] = "Rioter";
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
                    Dialogue.Generate(1, mesClass, null, 1, 0, 1);
                    Dialogue.Generate(1, mesClass, null, 4, 1500, 0);
                    Dialogue.Generate(1, mesClass, null, 2, 3000, 0);

                    Dialogue.Generate(1, mesName, null, 0, 0, 1);
                    Dialogue.Generate(1, mesName, null, 1, 0, 0);
                    Wizjoner Charac = new Wizjoner(Console.ReadLine());
                    Dialogue.Generate(1, mesName, null, 3, 1500, 1);
                    Dialogue.Generate(1, mesName, null, 4, 0, 0);
                    Dialogue.Generate(2, null, Charac.Name, 0, 0, 0);
                    Dialogue.Generate(1, mesName, null, 5, 3000, 0);
                    Clasa = false;
                }
                else if (keyinfo.Key == ConsoleKey.Enter && klasa == 2)
                {
                    Dialogue.Generate(1, mesClass, null, 1, 0, 1);
                    Dialogue.Generate(1, mesClass, null, 4, 1500, 0);
                    Dialogue.Generate(1, mesClass, null, 3, 3000, 0);

                    Dialogue.Generate(1, mesName, null, 0, 0, 1);
                    Dialogue.Generate(1, mesName, null, 2, 0, 0);
                    Buntownik Charac = new Buntownik(Console.ReadLine());
                    Dialogue.Generate(1, mesName, null, 3, 1500, 1);
                    Dialogue.Generate(1, mesName, null, 4, 0, 0);
                    Dialogue.Generate(2, null, Charac.Name, 0, 0, 0);
                    Dialogue.Generate(1, mesName, null, 5, 3000, 0);
                    Clasa = false;
                }
            }
        }
    }

    class Creator           // klasa creatora postaci
    {
        public static void CreateClassName(int klasa)
        {
            Completing();
        }
        public static void Completing()
        {
            string[] mes = { "Survey phase complete.", "\nSurvey results:", "\n\nChoosen class:", "\nChoosen name:", };
            Dialogue.Generate(1, mes, null, 0, 2000, 1);
            Dialogue.Generate(1, mes, null, 1, 1000, 0);
            Dialogue.Generate(1, mes, null, 2, 0, 0);
            Dialogue.Generate(1, mes, null, 3, 5000, 0);
            
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
        public string Klasa = "Visionary";
        public string Name { get; set; }
        int HeroHP;
        int HeroATK;

        public Wizjoner(string wizName)
        {
            this.Name = wizName;
            this.HeroHP = 30;
            this.HeroATK = 17;
        }

        public void setName(string newName)
        {
            this.Name = newName;
        }
        public string getName()
        {
            return this.Name;
        }

        
    }
    class Buntownik : IPlayer
    {
        public string Klasa = "Rioter";
        public string Name { get; set; }
        int HeroHP;
        int HeroATK;

        public Buntownik(string buntName)
        {
            this.Name = buntName;
            this.HeroHP = 23;
            this.HeroATK = 20;
        }
        public void setName(string newName)
        {
            Name = newName;
        }
        public string getName()
        {
            return this.Name;
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
