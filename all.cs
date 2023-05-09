using System;
namespace ConsoleApp1
{

    class card
    {
        private string name;
        private bool fighter;
        private int health;
        private int defence;
        private int attack;
        public card(string nm, bool fght, int heal, int def, int att){
            this.name = nm; //персонаж
            this.fighter = true;    
            this.health = heal;
            this.defence = def;
            this.attack = att;
        }
        public card(string nm,  int heal, int def, int att)
        { //амуниция 
            this.name = nm;
            this.fighter = false ;
            this.health = heal;
            this.defence = def;
            this.attack = att;
        }
        public card()
        { // дает задохлика
            this.name = "Задохлик";
            this.fighter = true;
            this.health = 0;
            this.defence = 0;
            this.attack = 02;
        }
        public int Health
        {
            get { return health; }
        }
        public int Defence
        {
            get { return defence; }
        }
        public int Attack
        {
            get { return attack; }
        }
        public string Name
        {
            get { return name; }
        }
        public bool is_char
        {
            get { return fighter; }
        }
    }

    class fighter
    { // боец
        private int health;
        private int defence;
        private string name;
        private int attack;
        public fighter(int health, int attack, int deffence, string name)
        {
            this.health = health;
            this.defence = deffence;
            this.name = name;
            this.attack = attack;
        }
        public int Health
        {
            get { return health; }
        }
        public int Defence
        {
            get { return defence; }
        }
        public int Attack
        {
            get { return attack; }
        }
        public string Name
        {
            get { return name; }
        }
    }
    class Game
    {//жизнь защита атака
        private fighter angel;
        private fighter devil;
        static Random rand = new Random();
        public Game(card[] deck_ang, card[] deck_dev, int[] choise_ang, int[] choise_dev)
        {
            int att_ang = 0;
            int def_ang = 0;
            int heal_ang = 0;
            string name_ang ="Задохлик";
            for (int i = 0; i < deck_ang.Length; i++)
            {
                if (choise_ang.Contains(i)) { 
                if (deck_ang[i].is_char) name_ang = deck_ang[i].Name;
                    att_ang += deck_ang[i].Attack;
                    def_ang += deck_ang[i].Attack;
                    heal_ang += deck_ang[i].Health;
                }
            }
            this.angel = new fighter(heal_ang, att_ang, def_ang, name_ang); // собрали бойца ангела
            int att_dev = 0; 
            int def_dev = 0;
            int heal_dev = 0;
            string name_dev = "Задохлик";
            for (int i = 0; i < deck_dev.Length; i++)
            {
                if (choise_dev.Contains(i))
                {
                    if (deck_dev[i].is_char) name_dev = deck_dev[i].Name;
                    att_dev += deck_dev[i].Attack;
                    def_dev += deck_dev[i].Attack;
                    heal_dev += deck_dev[i].Health;
                }
            }
            this.devil = new fighter(heal_dev, att_dev, def_dev, name_dev); // собрали бойца дьявола

        }
        
        public static void print_deck(card[] deck)
        {
            for (int i = 0; i < deck.Length-1; i++)
            {
                if (deck[i].is_char)     
                Console.WriteLine($"{i+1}. Персонаж: {deck[i].Name} Очки Здоровьь: {deck[i].Health} Очки Защиты: {deck[i].Defence} Очки Атаки: {deck[i].Attack} ");
                else Console.WriteLine($"{i+1}. Аммуниция: {deck[i].Name} Очки Здоровья: {deck[i].Health} Очки Защиты: {deck[i].Defence} Очки Атаки: {deck[i].Attack}");
            }
        }
        public static card[] game(out card[] deck1){ // создать одному колоду
            string[] lines = File.ReadLines("C:\\Users\\sergekuz\\Desktop\\Прога проект\\ДуэльСвет.txt").ToArray();
            string crd;
            string name;
            int health;
            int def;
            int attack;
            card[] deck = new card[7];
            deck1 = new card[7];
            deck[0] = new card("Задохлик", true, 0, 0, 0);
            deck1[0] = new card("Задохлик", true, 0, 0, 0);
            for (int i = 1; i < 6; i++)
            {
                crd = lines[rand.Next(lines.Length)];
                if ( crd[0] == '1')
                {
                    Game.stat(crd, out name, out health, out attack, out def);
                    deck[i] = new card(name, true, health, def, attack);
                }
                else
                {
                    Game.stat(crd, out name, out health, out attack, out def);
                    deck[i] = new card(name, health, def, attack);
                }
            }
            // 5 карт сил света
            string[] lines1 = File.ReadLines("C:\\Users\\sergekuz\\Desktop\\Прога проект\\ДуэльТьма.txt").ToArray();
            for (int i = 1; i < 6; i++)
            {
                crd = lines1[rand.Next(lines1.Length)];
                if (crd[0] == '1')
                {
                    Game.stat(crd, out name, out health, out attack, out def);
                    deck1[i] = new card(name, true, health, def, attack);
                }
                else
                {
                    Game.stat(crd, out name, out health, out attack, out def);
                    deck1[i] = new card(name, health, def, attack);
                }
            } // 5 карт сил тьмы

            return deck;
        }
        public static void stat(string str, out string name, out int health, out int attack, out int def)
        {
            string[] words = str.Split(' ');
            name = words[1];
            
            string[] nm = name.Split('_');
            name = "";
            for (int i =0; i < nm.Length; i++)
            {
                name += nm[i]+' ';
            }
            health = int.Parse(words[2]);
            def = int.Parse(words[3]);
            attack = int.Parse(words[4]);
        }
            
}
            

    class Program
        
    {
        static void Choise_Angel(out int[] choise, card[] deck1)
        {
            int k = 0;
            choise = new int[3];
            string ch;
            bool h_char = false;
            for (int i = 1; i < deck1.Length; i++)
            {
                if (deck1[i].is_char)
                {
                    h_char = true;
                    break;
                }
            } // смотрим есть ли среди карт персы
            while (k < 3)
            {
                ch = Console.ReadLine();

                if (int.TryParse(ch, out int j))
                {
                    if (j < deck1.Length - 1) // можно выкинуть Exception со словами мол чет такой карты не вижу
                    {
                        if (k == 2 & h_char) continue; // можно выкинуть Exception со словами типа вам выпал перс но вы его не выбрали
                        if (k < 2 & !h_char & deck1[j-1].is_char) continue; // добавить проверку что перс не задохлик 
                        if (k < 2 & h_char & deck1[j - 1].is_char) { 
                            h_char = false; 
                        } // если перса нет, но его выбрали 
                        choise[k] = j - 1;
                        k++;
                        Console.WriteLine(k);
                    }
                }
            }
        }
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Добро пожаловать в игру Дуэль!");
            Console.WriteLine("В нашей игре есть две стороны, сила света и сила тьмы");
            Console.WriteLine("На старте игроки имеют по одному 'голому' персонажу");
            Console.WriteLine("Такие карты выпали игроку сил свет:");
            Console.ForegroundColor = ConsoleColor.White;
            card[] deck_dev = new card[7];
            card[] deck_ang = new card[7];
            deck_ang = Game.game(out deck_dev);
            Game.print_deck(deck_ang);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Игрок сил света выбирает 3 карты");
            Console.WriteLine("В случае,игрок выбирает в качестве персонажа карту отличную от 'Задохлик', игроку требуется выбрать ровно ОДНУ карту персонажа и ДВЕ карты амуниции");
            Console.WriteLine("В противном же случае игроку позволяется выбрать ТРИ карты аммуници");
            int[] choise_ang;
            Choise_Angel(out choise_ang, deck_dev);
            Console.WriteLine("На бой от сил света отправится, а также возьмет с собой такую амуницию :");
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < choise_ang.Length; i++) { 
                if (deck_ang[choise_ang[i]].is_char)
                Console.WriteLine($"{i + 1}. Персонаж:  {deck_ang[choise_ang[i]].Name} "); 
                else Console.WriteLine($"{i + 1}. Аммуниция:  {deck_ang[choise_ang[i]].Name} ");
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Игрок сил тьмы выбирает 3 карты");
            Console.WriteLine("В случае,игрок выбирает в качестве персонажа карту отличную от 'Задохлик', игроку требуется выбрать ровно ОДНУ карту персонажа и ДВЕ карты амуниции");
            Console.WriteLine("В противном же случае игроку позволяется выбрать ТРИ карты аммуници");
            Game.print_deck(deck_dev);
            int[] choise_dev;
            Choise_Angel(out choise_dev, deck_dev);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("На бой от сил тьмы отправится, а также возьмет с собой такую амуницию :");
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < choise_dev.Length; i++)
            {
                if (deck_dev[choise_dev[i]].is_char)
                    Console.WriteLine($"{i + 1}. Персонаж:  {deck_dev[choise_dev[i]].Name} ");
                else Console.WriteLine($"{i + 1}. Аммуниция:  {deck_dev[choise_dev[i]].Name} ");
            } // на этом моменте у нас есть три карты двух игроков

        }

    }
}
