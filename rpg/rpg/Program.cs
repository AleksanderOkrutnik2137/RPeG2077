using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text;
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
    class Game : Menu
    {
        bool Booting = true;
        Player CharPlayer = new Player();
        Enemy Hostile = new Enemy();
        Encoding ascii = Encoding.ASCII;

        // Map variables
        int setMap = 1;
        public int x = 3, y = 4;
        public int NPCx = 4, NPCy = 10;
        public int EnemX = 6, EnemY = 21;
        static string[] dataMap1 = File.ReadAllLines(@"C:\Users\alexp\Documents\GitHub\RPeG2077\rpg\rpg\map1.txt");
        public string[,] tab = new string[dataMap1.Length, dataMap1[0].Length];
        public string[,] borders = new string[dataMap1.Length, dataMap1[0].Length];
        public bool Signal = false;
        public int mesNumber;
        public int Wait = 0;
        public bool Battle = false;
        //

        public override void Boot()         // przeciążenie metody uruchamiania gry / rozpoczęcie gry i bootup
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
                Console.SetCursorPosition(0, 0);

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
            bool Continue = true;
            int klasa = 1;

            string[] mesBegin = { "Welcome.", "Welcome to the rpg.exe - SURVEY_PROGRAM." };
            string[] mesClass = { "Choose your class.\n\n", "Selected class", "Visionary.", "Rioter.", " - " };
            string[] mesName = { "Name your", " Visionary.\n\n", " Rioter.\n\n", "You have choosen", " - ", "." };
            string[] mesComplete = { "Survey phase complete.", "\nSurvey results:", "\n\nChoosen class: ", "\nChoosen name: ", "\n\n\nPress Enter to continue." };
            string[] mesSimBegin = { "Closing SURVEY_PROGRAM...", "Booting SIMULATION_PROGRAM...", "\nBooting complete.", "Begining simulation", " now." };

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
                Console.SetCursorPosition(0, 0);

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

                    CharPlayer.setName(Console.ReadLine());
                    if (CharPlayer.Name.Length < 3)
                    {
                        CharPlayer.setName("The Player");
                    }

                    Dialogue.Generate(1, mesName, null, 3, 0, 1);
                    Dialogue.Generate(1, mesName, null, 4, 1500, 0);
                    Dialogue.Generate(2, null, CharPlayer.Name, 0, 0, 0);
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

                    CharPlayer.setName(Console.ReadLine());
                    if (CharPlayer.Name.Length < 3)
                    {
                        CharPlayer.setName("The Player");
                    }

                    Dialogue.Generate(1, mesName, null, 3, 0, 1);
                    Dialogue.Generate(1, mesName, null, 4, 1500, 0);
                    Dialogue.Generate(2, null, CharPlayer.Name, 0, 0, 0);
                    Dialogue.Generate(1, mesName, null, 5, 3000, 0);
                    Clasa = false;
                }
            }
            if (Clasa == false)
            {
                Dialogue.Generate(1, mesComplete, null, 0, 2000, 1);
                Dialogue.Generate(1, mesComplete, null, 1, 0, 0);
                Dialogue.Generate(1, mesComplete, null, 2, 0, 0);
                Dialogue.Generate(2, null, "ERROR - Selected class not found. Setting Player as no-class", 0, 0, 0);
                Dialogue.Generate(1, mesComplete, null, 3, 0, 0);
                Dialogue.Generate(2, null, CharPlayer.Name, 0, 0, 0);
                Dialogue.Generate(1, mesComplete, null, 4, 0, 0);
                while (Continue == true)
                {
                    ConsoleKeyInfo keyinfo = Console.ReadKey(true);
                    if (keyinfo.Key == ConsoleKey.Enter)
                    {
                        Continue = false;
                        Dialogue.Generate(1, mesSimBegin, null, 0, 2000, 1);
                        Dialogue.Generate(1, mesSimBegin, null, 1, 3000, 1);
                        Dialogue.Generate(1, mesSimBegin, null, 2, 2000, 0);
                        Dialogue.Generate(1, mesSimBegin, null, 3, 2000, 1);
                        Dialogue.Generate(1, mesSimBegin, null, 4, 1000, 0);
                        Console.Clear();
                        mapStart();
                    }
                }
            }
        }
        public void mapStart()
        {
            GenerateMap();
            while (Battle == false)
            {
                PrintMap();
                move();
                npcMove();
                enemyMove();
            }
            if (Battle == true)
            {
                Fight();
            }
        }
        public void GenerateMap()
        {
            for (int i = 0; i < dataMap1.Length; i++)
            {
                string tmp = dataMap1[i];
                string tmp2 = dataMap1[i];
                string tmp3 = dataMap1[i];
                for (int j = 0; j < dataMap1[i].Length; j++)
                {
                    string p = tmp[j].ToString();
                    string npc = tmp[j].ToString();
                    string enem = tmp[j].ToString();
                    if (p == "*")
                    {
                        tab[i, j] = tmp[j].ToString();
                        x = i;
                        y = j;
                    }
                    if (setMap == 2 & npc == "S")
                    {
                        tab[i, j] = tmp2[j].ToString();
                        NPCx = i;
                        NPCy = j;
                    }
                    if (setMap == 1 & enem == "E" & Hostile.EnemHP > 0)
                    {
                        tab[i, j] = tmp3[j].ToString();
                        EnemX = i;
                        EnemY = j;
                    }
                    else
                    {
                        tab[i, j] = tmp[j].ToString();
                    }
                }
            }
        }
        public void PrintMap()
        {
            string[] mesInteraction = { "                             \n                                                                ", "A Trauma Generator.\nBut it seems to be inactive...", "Go to the next room.\nThere's Trauma Generator. It can respawn dead enemies.", "An enemy!", "A Trauma Generator.\nIt looks like something happened inside." };

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                string tmp = dataMap1[i];
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if (i == x & j == y)
                    {
                        tab[i, j] = "*";
                    }
                    else if (setMap == 2 & i == NPCx & j == NPCy)
                    {
                        tab[i, j] = "S";
                    }
                    else if (setMap == 1 & i == EnemX & j == EnemY & Hostile.EnemHP > 0)
                    {
                        tab[i, j] = "E";
                    }
                    else
                    {
                        tab[i, j] = tmp[j].ToString();
                    }
                    if (i == 0) tab[i, j] = "\u2584"; //down
                    else if (i == tab.GetLength(0) - 1) tab[i, j] = "\u2580"; //upper
                    else if (j == 0) tab[i, j] = "\u2588";
                    else if (j == tab.GetLength(1) - 1) tab[i, j] = "\u2588";
                    Console.Write(tab[i, j]);
                }
                Console.Write(Environment.NewLine);
            }
            if (Signal == true)
            {
                Console.WriteLine(mesInteraction[mesNumber]);
                Signal = false;
            }
            else
            {
                mesNumber = 0;
                Console.WriteLine(mesInteraction[mesNumber]);
            }
            Console.SetCursorPosition(0, 0);
        }
        public void Fight()
        {

            Dialogue.Generate(2, null, Hostile.Name, 0, 0, 1);
            Dialogue.Generate(2, null, " spotted you.\n\n", 0, 0, 0);
            bool Continue = false;
            bool Round = true;
            bool now = false;
            bool Fight = true;
            bool Endgame = false;

            while (Fight == true)
            {
                int Selection = 1;
                int fightLength = 2;
                int defendLength = 2;
                int infoLength = 2;
                int passLength = 2;

                string[] fightSlash = new string[fightLength];
                string[] defendSlash = new string[defendLength];
                string[] infoSlash = new string[infoLength];
                string[] passSlash = new string[passLength];
                fightSlash[0] = ">";
                defendSlash[0] = " ";
                infoSlash[0] = " ";
                passSlash[0] = " ";

                if (Hostile.EnemHP <= 0)
                {
                    Round = false;
                    Fight = false;
                    Dialogue.Generate(2, null, "Enemy has been defeated.", 0, 0, 1);
                    Endgame = true;
                }
                if (CharPlayer.HeroHP <= 0)
                {
                    Round = false;
                    Fight = false;
                    Dialogue.Generate(2, null, "Simulation failed. Player died.", 0, 3000, 1);
                    Dialogue.Generate(2, null, "Closing SIMULATION_PROGRAM...", 0, 3000, 1);
                    Environment.Exit(0);
                }

                while (Round == true)
                {
                    if (now == true)
                    {
                        Console.WriteLine("...\n\n");
                    }
                    Console.SetCursorPosition(0, 2);
                    Console.Write(fightSlash[0]);               
                    for (int i = 1; i < fightLength; i++)
                    {
                        fightSlash[i] = "Fight";
                        Console.Write(fightSlash[i]);
                    }
                    Console.Write("\n" + defendSlash[0]);
                    for (int i = 1; i < defendLength; i++)
                    {
                        defendSlash[i] = "Heal";
                        Console.Write(defendSlash[i]);
                    }
                    Console.Write("\n" + infoSlash[0]);
                    for (int i = 1; i < infoLength; i++)
                    {
                        infoSlash[i] = "Info";
                        Console.Write(infoSlash[i]);
                    }
                    Console.Write("\n" + passSlash[0]);
                    for (int i = 1; i < passLength; i++)
                    {
                        passSlash[i] = "Pass";
                        Console.Write(passSlash[i]);
                    }

                    Console.SetCursorPosition(0, 2);
                    ConsoleKeyInfo keyinfo = Console.ReadKey(true);
                    if (keyinfo.Key == ConsoleKey.W)
                    {
                        if (Selection > 1)
                        {
                            Selection--;
                        }
                        if (Selection == 1)
                        {

                        }
                    }
                    else if (keyinfo.Key == ConsoleKey.S)
                    {
                        if (Selection < 4)
                        {
                            Selection++;
                        }
                        if (Selection == 4)
                        {

                        }
                    }
                    if (Selection == 1)
                    {
                        fightSlash[0] = ">";
                        defendSlash[0] = " ";
                        infoSlash[0] = " ";
                        passSlash[0] = " ";
                    }
                    if (Selection == 2)
                    {
                        fightSlash[0] = " ";
                        defendSlash[0] = ">";
                        infoSlash[0] = " ";
                        passSlash[0] = " ";
                    }
                    if (Selection == 3)
                    {
                        fightSlash[0] = " ";
                        defendSlash[0] = " ";
                        infoSlash[0] = ">";
                        passSlash[0] = " ";
                    }
                    if (Selection == 4)
                    {
                        fightSlash[0] = " ";
                        defendSlash[0] = " ";
                        infoSlash[0] = " ";
                        passSlash[0] = ">";
                    }

                    if (keyinfo.Key == ConsoleKey.Enter && Selection == 1)
                    {
                        Round = false;
                        Console.Clear();
                        Console.WriteLine("You charged towards an enemy.");
                        Thread.Sleep(2000);
                        CharPlayer.attackEnemy(Hostile);
                        Thread.Sleep(2000);
                        Hostile.attackPlayer(CharPlayer);
                        Console.WriteLine("\n\n Press Enter to continue.");
                        Continue = true;
                    }
                    if (keyinfo.Key == ConsoleKey.Enter && Selection == 2)
                    {
                        Round = false;
                        Console.Clear();
                        Console.WriteLine("You tried to heal.");
                        Thread.Sleep(2000);
                        CharPlayer.HeroHeal();
                        Console.WriteLine("\n\n Press Enter to continue.");
                        Continue = true;
                    }
                    if (keyinfo.Key == ConsoleKey.Enter && Selection == 3)
                    {
                        Round = false;
                        Console.Clear();
                        Console.WriteLine(Hostile);
                        Thread.Sleep(2000);
                        Hostile.attackPlayer(CharPlayer);
                        Console.WriteLine("\n\n Press Enter to continue.");
                        Continue = true;
                    }
                    if (keyinfo.Key == ConsoleKey.Enter && Selection == 4)
                    {
                        Round = false;
                        Console.Clear();
                        Console.WriteLine("You passed your turn.");
                        Thread.Sleep(2000);
                        Hostile.attackPlayer(CharPlayer);
                        Console.WriteLine("\n\n Press Enter to continue.");
                        Continue = true;
                    }

                }
                while (Continue == true)
                {
                    ConsoleKeyInfo keyinfo = Console.ReadKey(true);
                    if (keyinfo.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Continue = false;
                        now = true;
                        Round = true;
                    }
                }
                while (Endgame == true)
                {
                    ConsoleKeyInfo keyinfo = Console.ReadKey(true);
                    if (keyinfo.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Battle = false;
                        mapStart();
                    }
                }
            }
        }

        public void move()
        {
            ConsoleKeyInfo keyinfo = Console.ReadKey(true);
            if (keyinfo.Key == ConsoleKey.Escape)
            {
                Dialogue.Generate(2, null, "Closing SIMULATION_PROGRAM...", 0, 3000, 1);
                Environment.Exit(0);
            }
            if (keyinfo.Key == ConsoleKey.W)
            {
                Wait++;
                if (tab[(x - 1), y] == "A")
                {

                }
                else if (tab[(x - 1), y] == "O")
                {
                    if (setMap == 1)
                    {
                        setMap++;
                        dataMap1 = File.ReadAllLines(@"C:\Users\alexp\Documents\GitHub\RPeG2077\rpg\rpg\map2.txt");
                        x = 2;
                        y = 23;
                    }
                    else if (setMap == 2)
                    {
                        setMap++;
                        dataMap1 = File.ReadAllLines(@"C:\Users\alexp\Documents\GitHub\RPeG2077\rpg\rpg\map3.txt");
                        x = 11;
                        y = 2;
                    }
                }
                else if (tab[(x - 1), y] == "U")
                {
                    if (setMap == 3)
                    {
                        setMap--;
                        dataMap1 = File.ReadAllLines(@"C:\Users\alexp\Documents\GitHub\RPeG2077\rpg\rpg\map2.txt");
                        x = 11;
                        y = 25;
                    }
                    else if (setMap == 2)
                    {
                        setMap--;
                        dataMap1 = File.ReadAllLines(@"C:\Users\alexp\Documents\GitHub\RPeG2077\rpg\rpg\map1.txt");
                        x = 14;
                        y = 23;
                    }
                }
                else if (setMap == 3 & tab[(x - 1), y] == "T")
                {
                    if (Hostile.EnemHP > 0)
                    {
                        Signal = true;
                        mesNumber = 1;
                    }
                    if (Hostile.EnemHP <= 0)
                    {
                        Hostile.EnemHP = 30;
                        Signal = true;
                        mesNumber = 4;
                    }
                }
                else if (tab[(x - 1), y] == "S")
                {
                    Signal = true;
                    mesNumber = 2;
                }
                else if (tab[(x - 1), y] == "E")
                {
                    Battle = true;
                }
                else
                {
                    x--;
                }
            }
            if (keyinfo.Key == ConsoleKey.A)
            {
                Wait++;
                if (tab[x, (y - 1)] == "A" | tab[x, (y - 1)] == "|" | tab[x, (y - 1)] == "_")
                {

                }
                else if (tab[x, (y - 1)] == "O")
                {
                    if (setMap == 1)
                    {
                        setMap++;
                        dataMap1 = File.ReadAllLines(@"C:\Users\alexp\Documents\GitHub\RPeG2077\rpg\rpg\map2.txt");
                        x = 2;
                        y = 23;
                    }
                    else if (setMap == 2)
                    {
                        setMap++;
                        dataMap1 = File.ReadAllLines(@"C:\Users\alexp\Documents\GitHub\RPeG2077\rpg\rpg\map3.txt");
                        x = 11;
                        y = 2;
                    }
                }
                else if (tab[x, (y - 1)] == "U")
                {
                    if (setMap == 3)
                    {
                        setMap--;
                        dataMap1 = File.ReadAllLines(@"C:\Users\alexp\Documents\GitHub\RPeG2077\rpg\rpg\map2.txt");
                        x = 11;
                        y = 25;
                    }
                    else if (setMap == 2)
                    {
                        setMap--;
                        dataMap1 = File.ReadAllLines(@"C:\Users\alexp\Documents\GitHub\RPeG2077\rpg\rpg\map1.txt");
                        x = 14;
                        y = 23;
                    }
                }
                else if (setMap == 3 & tab[x, (y - 1)] == "T")
                {
                    if (Hostile.EnemHP > 0)
                    {
                        Signal = true;
                        mesNumber = 1;
                    }
                    if (Hostile.EnemHP <= 0)
                    {
                        Hostile.EnemHP = 30;
                        Signal = true;
                        mesNumber = 4;
                    }
                }
                else if (tab[x, (y - 1)] == "S")
                {
                    Signal = true;
                    mesNumber = 2;
                }
                else if (tab[x, (y - 1)] == "E")
                {
                    Battle = true;
                }
                else
                {
                    y--;
                }
            }

            if (keyinfo.Key == ConsoleKey.S)
            {
                Wait++;
                if (tab[(x + 1), y] == "A" | tab[(x + 1), y] == "|" | tab[(x + 1), y] == "_")
                {

                }
                else if (tab[(x + 1), y] == "O")
                {
                    if (setMap == 1)
                    {
                        setMap++;
                        dataMap1 = File.ReadAllLines(@"C:\Users\alexp\Documents\GitHub\RPeG2077\rpg\rpg\map2.txt");
                        x = 2;
                        y = 23;
                    }
                    else if (setMap == 2)
                    {
                        setMap++;
                        dataMap1 = File.ReadAllLines(@"C:\Users\alexp\Documents\GitHub\RPeG2077\rpg\rpg\map3.txt");
                        x = 11;
                        y = 2;
                    }
                }
                else if (tab[(x + 1), y] == "U")
                {
                    if (setMap == 3)
                    {
                        setMap--;
                        dataMap1 = File.ReadAllLines(@"C:\Users\alexp\Documents\GitHub\RPeG2077\rpg\rpg\map2.txt");
                        x = 11;
                        y = 25;
                    }
                    else if (setMap == 2)
                    {
                        setMap--;
                        dataMap1 = File.ReadAllLines(@"C:\Users\alexp\Documents\GitHub\RPeG2077\rpg\rpg\map1.txt");
                        x = 14;
                        y = 23;
                    }
                }
                else if (setMap == 3 & tab[(x + 1), y] == "T")
                {
                    if (Hostile.EnemHP > 0)
                    {
                        Signal = true;
                        mesNumber = 1;
                    }
                    if (Hostile.EnemHP <= 0)
                    {
                        Hostile.EnemHP = 30;
                        Signal = true;
                        mesNumber = 4;
                    }
                }
                else if (tab[(x + 1), y] == "S")
                {
                    Signal = true;
                    mesNumber = 2;
                }
                else if (tab[(x + 1), y] == "E")
                {
                    Battle = true;
                }
                else
                {
                    x++;
                }
            }
            if (keyinfo.Key == ConsoleKey.D)
            {
                Wait++;
                if (tab[x, (y + 1)] == "A" | tab[x, (y + 1)] == "|" | tab[x, (y + 1)] == "_")
                {

                }
                else if (tab[x, (y + 1)] == "O")
                {
                    if (setMap == 1)
                    {
                        setMap++;
                        dataMap1 = File.ReadAllLines(@"C:\Users\alexp\Documents\GitHub\RPeG2077\rpg\rpg\map2.txt");
                        x = 2;
                        y = 23;
                    }
                    else if (setMap == 2)
                    {
                        setMap++;
                        dataMap1 = File.ReadAllLines(@"C:\Users\alexp\Documents\GitHub\RPeG2077\rpg\rpg\map3.txt");
                        x = 11;
                        y = 2;
                    }
                }
                else if (tab[x, (y + 1)] == "U")
                {
                    if (setMap == 3)
                    {
                        setMap--;
                        dataMap1 = File.ReadAllLines(@"C:\Users\alexp\Documents\GitHub\RPeG2077\rpg\rpg\map2.txt");
                        x = 11;
                        y = 25;
                    }
                    else if (setMap == 2)
                    {
                        setMap--;
                        dataMap1 = File.ReadAllLines(@"C:\Users\alexp\Documents\GitHub\RPeG2077\rpg\rpg\map1.txt");
                        x = 14;
                        y = 23;
                    }
                }
                else if (setMap == 3 & tab[x, (y + 1)] == "T")
                {
                    if (Hostile.EnemHP > 0)
                    {
                        Signal = true;
                        mesNumber = 1;
                    }
                    if (Hostile.EnemHP <= 0)
                    {
                        Hostile.EnemHP = 30;
                        Signal = true;
                        mesNumber = 4;
                    }
                }
                else if (tab[x, (y + 1)] == "S")
                {
                    Signal = true;
                    mesNumber = 2;
                }
                else if (tab[x, (y + 1)] == "E")
                {
                    Battle = true;
                }
                else
                {
                    y++;
                }
            }
            //Console.Clear();
        }
        public void npcMove()
        {
            Random rand = new Random();
            int updown;
            if (setMap == 2 & Wait > 2)
            {
                Wait = 0;
                if (tab[(NPCx - 1), NPCy] == "A" | tab[(NPCx - 1), NPCy] == "|" | tab[(NPCx - 1), NPCy] == "_") // W
                {
                    NPCx = NPCx + 1;
                }
                else if (tab[(NPCx - 1), NPCy] == "*")
                {
                    Wait = 0;
                }
                else if (tab[NPCx, (NPCy - 1)] == "A" | tab[NPCx, (NPCy - 1)] == "|" | tab[NPCx, (NPCy - 1)] == "_") // A
                {
                    NPCy = NPCy + 1;
                }
                else if (tab[NPCx, (NPCy - 1)] == "*")
                {
                    Wait = 0;
                }
                else if (tab[(NPCx + 1), NPCy] == "A" | tab[(NPCx + 1), NPCy] == "|" | tab[(NPCx + 1), NPCy] == "_") // S
                {
                    NPCx = NPCx - 1;
                }
                else if (tab[(NPCx + 1), NPCy] == "*")
                {
                    Wait = 0;
                }
                else if (tab[NPCx, (NPCy + 1)] == "A" | tab[NPCx, (NPCy + 1)] == "|" | tab[NPCx, (NPCy + 1)] == "_") // D
                {
                    NPCy = NPCy - 1;
                }
                else if (tab[NPCx, (NPCy + 1)] == "*")
                {
                    Wait = 0;
                }
                else
                {
                    updown = rand.Next(5);       //x 1 y 0
                    if (updown == 1)
                    {
                        NPCy++;
                    }
                    else if (updown == 2)
                    {
                        NPCy--;
                    }
                    else if (updown == 3)
                    {
                        NPCx++;
                    }
                    else if (updown == 4)
                    {
                        NPCx++;
                    }
                }
            }
        }
        public void enemyMove()
        {
            Random rand = new Random();
            int updown;
            if (setMap == 1 & Wait > 2)
            {
                Wait = 0;
                if (tab[(EnemX - 1), EnemY] == "A") // W
                {
                    EnemX = EnemX + 1;
                }
                else if (tab[EnemX, (EnemY - 1)] == "A") // A
                {
                    EnemY = EnemY + 1;
                }
                else if (tab[(EnemX + 1), EnemY] == "A") // S
                {
                    EnemX = EnemX - 1;
                }
                else if (tab[EnemX, (EnemY + 1)] == "A") // D
                {
                    EnemY = EnemY - 1;
                }
                else if (tab[(EnemX - 1), EnemY] == "*")
                {
                    Wait = 0;
                }
                else if (tab[EnemX, (EnemY - 1)] == "*")
                {
                    Wait = 0;
                }
                else if (tab[(EnemX + 1), EnemY] == "*")
                {
                    Wait = 0;
                }
                else if (tab[EnemX, (EnemY + 1)] == "*")
                {
                    Wait = 0;
                }
                else
                {
                    updown = rand.Next(5);       //x 1 y 0
                    if (updown == 1)
                    {
                        EnemY++;
                    }
                    else if (updown == 2)
                    {
                        EnemY--;
                    }
                    else if (updown == 3)
                    {
                        EnemX++;
                    }
                    else if (updown == 4)
                    {
                        EnemX--;
                    }
                }
            }
        }
    }

    class Player
    {
        Random rand = new Random();
        public string Name { get; set; }
        public int HeroHP { get; set; }
        int HeroATK;
        public Player()
        {
            this.Name = Name;
            this.HeroHP = 30;
            this.HeroATK = 13;
        }
        public void setName(string newName)
        {
            this.Name = newName;
        }
        public void attackEnemy(Enemy myhostile)
        {
            myhostile.takeDamage(this.HeroATK);
        }
        public void takeDMG(int attack)
        {
            this.HeroHP -= attack;
            Console.WriteLine("\nEnemy attacked " + Name + " in revenge. \nCurrent Player HP is: " + this.HeroHP);
        }
        public void HeroHeal()
        {
            int HealValue = rand.Next(0, 9);
            this.HeroHP += HealValue;
            Console.WriteLine("\nPlayer has been healed for " + HealValue + " HP. \nCurrent Player HP is: " + this.HeroHP);
        }
    }
    class Wizjoner : Player
    {
        public string Klasa = "Visionary";
        int HeroHP;
        int HeroATK;

        public Wizjoner()
        {
            this.HeroHP = 30;
            this.HeroATK = 17;
        }
    }
    class Buntownik : Player
    {
        public string Klasa = "Rioter";
        int HeroHP;
        int HeroATK;

        public Buntownik()
        {
            this.HeroHP = 23;
            this.HeroATK = 20;
        }
    }

    class Enemy
    {
        public string Name { get; set; }
        public int EnemHP { get; set; }
        int EnemATK;
        public Enemy()
        {
            this.Name = ">Null< Soldier";
            this.EnemHP = 30;
            this.EnemATK = 4;
        }
        public override string ToString()
        {
            return Name + " - result of a secret government project. " + EnemHP + " HP. " + EnemATK + " ATK. \n";
        }
        public void attackPlayer(Player CharPlayer)
        {
            CharPlayer.takeDMG(this.EnemATK);
        }
        public void takeDamage(int attack)
        {
            this.EnemHP -= attack;
            Console.WriteLine("\nPlayer attacked enemy for " + attack + " points. \nCurrent enemy HP is: " + this.EnemHP);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game RPG = new Game();
            Console.CursorVisible = false;
            RPG.Boot();
        }
    }
}
