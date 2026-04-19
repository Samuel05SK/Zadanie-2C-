using System.Data.SqlTypes;
using System.Security.Cryptography.X509Certificates;

namespace Zadanie_2C____
{
    public class Individuals //Definícia triedy Individuals podľa zadania
    {
        private Random random = new Random(); // Zadefinovanie random generátoru (Dosť používaný)
        // Zadefinovanie génov X,Y
        public double x;
        public double y;

        /*
         * Metóda Generator
         * 
         * Vytvára a vracia náhodné double hodnoty z rosahu -5 až 5
         */
        public double Generator()
        {
            return -5 + (random.NextDouble()) * 10;
        }


        /*
         * Metóda Fitness
         * 
         * Vypočíta a vráti funkčnú hodnotu f(x,y) pomocou Himmelblauovej funkcie (skús to povedať zasebou 100 krát :D)
         */
        public double Fitness
        {
            get
            {
                return Math.Pow((Math.Pow(x, 2) + y - 11), 2) + Math.Pow((x + Math.Pow(y, 2) - 7), 2);
            }
        }

        /*
         * Metóda mutate
         * 
         * parameter:
         * mutationRate - percentuálne množstvo (double) génov ktoré budú pozmenené
         * 
         * Metóda vytvorý náhodné číslo a keď je menšie ako mutationRate tak náhodne vymení jeden z génov
         */
        public void mutate(double mutationRate)
        {
            if(random.NextDouble() < mutationRate)
            {
                int randomXY = random.Next(1,3);
                switch(randomXY)
                {
                    case 1:
                        x = Generator();
                        break;
                    case 2:
                        y = Generator();
                        break;
                }       
            }
        }
    }

    public class Bod // Definovanie triedy Bod 
    {
        // X,Y sú súradnice
        public double x;
        public double y;

        /*
         * Metóda HladanieOblasti
         * 
         * parametre:
         * x - súradnica x (WOW! aké nápadité)
         * y - súradnica y (No to sú mi zmeny)
         * 
         * return:
         * bod ku ktorému sa približuje program ♥
         */
        public Bod HladanieOblasti(double x, double y)
        {
            //V minFunkcie sú uložené hodnoty miním Himmelblauovej funkcie
            Bod[] minFunkcie = new Bod[4];
            for(int i = 0; i< minFunkcie.Length; i++)
            {
                minFunkcie[i] = new Bod();
            }
            minFunkcie[0].x = 3;
            minFunkcie[0].y = 2;
            minFunkcie[1].x = -2.805118;
            minFunkcie[1].y = 3.131312;
            minFunkcie[2].x = -3.779310;
            minFunkcie[2].y = -3.283186;
            minFunkcie[3].x = 3.584428;
            minFunkcie[3].y = -1.848126;
        
            int bod = 0;
            double minVzdialenost = double.MaxValue;
            for(int i = 0; i < minFunkcie.Length; i++) //Hladanie minima ku ktorému sa približujeme, výpočet vzdialenosti
            {
                if (Math.Sqrt(Math.Pow(Math.Abs(x - minFunkcie[i].x),2) + Math.Pow(Math.Abs(y - minFunkcie[i].y), 2)) < minVzdialenost)
                {
                    minVzdialenost = Math.Sqrt(Math.Pow(Math.Abs(x - minFunkcie[i].x), 2) + Math.Pow(Math.Abs(y - minFunkcie[i].y), 2));
                    bod = i;
                }
            }
            return minFunkcie[bod];
        }
    }

    internal class Program
    {
        public static List<Individuals> jednotlivci = new List<Individuals>(); // Vytvorenie zoznamu
        private class Population //Definícia triedy Poipulation podľa zadania
        {
            private Random random = new Random(); // Zadefinovanie random generátoru (Veľmi používaný)
            
            /*
             * Metóda Selection
             * parameter:
             * selectionRate - percentuálne množstvo (double) populácie ktorá prežije do ďalšej generácie
             * 
             * Metóda vymaže zo zoznamu jedincov ktorý neprežijú do ďalšej generácie
             * 
             * return:
             * celočíselná hodnota počtu zneživených, ktorý budú neskôr nahradený inými Alexami(klonmi) a skríženými jedincami
             */
            public int selection(double selectionRate)
            {
                jednotlivci = jednotlivci.OrderBy(ind => ind.Fitness).ToList();
                int dlzka = jednotlivci.Count;
                int alive = (int)Math.Round(dlzka * selectionRate);
                int pocetMrtvych = dlzka - alive;
                jednotlivci.RemoveRange(alive, pocetMrtvych);
                return pocetMrtvych;
            }

            /*
             * Metóda cloning (Alexovanie)
             * parameter:
             * mutationRate - percentuálne množstvo (double) génov ktoré budú pozmenené
             * 
             * Metóda vytvára nových jedincov ktorý boli zneživený, skopíruje gény preživšieho jedinca a následne na nich volá metódu matete ktorá môže pozmeniť ich gény
             */
            public void cloning(double mutationRate)
            {
                int velkostPopulacie = jednotlivci.Count;
                int klonovanyJedinec = random.Next(0, velkostPopulacie);
                jednotlivci.Add(new Individuals());
                int Alex = velkostPopulacie; //Alex == klon
                jednotlivci[Alex].x = jednotlivci[klonovanyJedinec].x;
                jednotlivci[Alex].y = jednotlivci[klonovanyJedinec].y;
                jednotlivci[Alex].mutate(mutationRate);
            }

            /*
             * Metóda kríženie
             * 
             * Metóda vytvára nových jedincov ktorý boli zneživený (pomocou metódy kvietku a včielky 😉), skopíruje hodnoty génov preživšieho jedinca. Každý gén vyberie od jeného s dvoch rodičov ktorý boli randomne vybraný
             */
            public void krizenie()
            {
                int velkostPopulacie = jednotlivci.Count;
                // Kvietok, Vcielka sú rodičia (Pre zistenie dôvodu vybrania tohto názvoslovia prosím kontaktujte dospelú osobu, najlepšie rodičov)
                int Kvietok = random.Next(0, velkostPopulacie);
                int Vcielka;
                do
                {
                    Vcielka = random.Next(0, velkostPopulacie);
                }
                while (Kvietok == Vcielka);
                jednotlivci.Add(new Individuals());
                jednotlivci[velkostPopulacie].x = jednotlivci[Kvietok].x;
                jednotlivci[velkostPopulacie].y = jednotlivci[Vcielka].y;
            }

            /*
             * Metóda baseGen 
             * 
             * Metóda zoradí jednotlivcov podľa atribútu Fitness
             * 
             * return:
             * Vracia najlepšieho jedinca
             */
            public Individuals baseGen()
            {
                jednotlivci = jednotlivci.OrderBy(ind => ind.Fitness).ToList();
                return jednotlivci[0];
            }

            /*
             * Metóoda nextGen
             * 
             * parametre:
             * selectionRate - percentuálne množstvo (double) populácie ktorá prežije do ďalšej generácie
             * mutationRate - percentuálne množstvo (double) génov ktoré budú pozmenené
             * 
             * Metóda najskôr zneživí populáciu podĺa selectionRate a napokon ich nahradí náhodne pomocou cloning(Alexovanie) alebo krizenie
             * 
             * return:
             * Metóda baseGen ktorá je uvedená vyššie (alebo po úprave možno aj nižšie, človek nikdy nevie kam ho božie cesty zavedú)
             */
            public Individuals nextGen(double selectionRate, double mutationRate)
            {
                int pocetMrtvych = selection(selectionRate);
                for (int i = 0; i < pocetMrtvych; i++)
                {
                    if (random.Next(0, 2) == 1 && jednotlivci.Count >= 2) krizenie();
                    else cloning(mutationRate);
                }
                return baseGen();
            }

            /*
             * Konštruktor (s rovnakým názvom ako trieda, kto by to bol povedal 🙃)
             * 
             * parameter:
             * maxPopulation - počet jednicov ktorých konštruktor vytvorí
             * 
             * Konštruktor postupne vytvára jedincov a priraďuje im hodnoty
             */
            public Population(uint maxPopulation)
            {
                for (int i = 0; i < maxPopulation; i++)
                {
                    jednotlivci.Add(new Individuals());
                    jednotlivci[i].x = jednotlivci[i].Generator();
                    jednotlivci[i].y = jednotlivci[i].Generator();
                }

            }
        }


        static void Main(string[] args)
        {
            // Pre zachovanie svojho duševného zdravia odporúčame tento program precítiť nie kontrolovať ♥ 🤓
            uint maxPopulation = 0; // Maximálna hodnota populácie ktorú zadáva uživateľ
            bool funkciaOk; // Stará sa aby nepadol program ♥, a aby bola zadaná správna hodnota
                            
            Console.WriteLine("Zadanie Variant F: Umelá krajina");
            Console.WriteLine("\n------------------------------------------\n");

            // Načítanie hodnoty maxPopulation
            do
            {
                funkciaOk = true;
                try
                {
                    Console.WriteLine("Zadaj celé číslo väčšie ako 2");
                    maxPopulation = uint.Parse(Console.ReadLine());
                    if (maxPopulation < 3) throw new Exception("Hodnota musí byť väčšia ako 3 inak by "); 
                    // Pri populácii 2 a menej by program nefungoval správne ♥
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n------------------------------------------");
                    Console.WriteLine("Zadal/a si zlú hodnotu\n" + e);
                    Console.WriteLine("------------------------------------------\n");
                    funkciaOk = false;
                }
            }
            while(funkciaOk ==  false);

            Population populacia = new Population(maxPopulation);  // Vytvorenie objektu populacie typu Population
            Bod bodik = new Bod(); // Vytvorenie objektu bodik typu Bod

            //Opakovanie programu po dobu 100 generácií
            for (int i = 0;i < 100;i++)
            {
                var navrat = populacia.nextGen(0.2, 00.2);  
                var bodPriblizenia = bodik.HladanieOblasti(navrat.x, navrat.y); // Výpočet vzdialenosti od minima funkcie a ku ktorému minumu funkcia smeruje

                Console.WriteLine("Fitness: " + navrat.Fitness + " \t Súradnica x: " + navrat.x + " \t y: " + navrat.y + " \t  " + "Blíži sa k bodu x: " + bodPriblizenia.x + " \t " + " \t y: " + bodPriblizenia.y + " \t vzdialenosť od skutočného stredu: " + Math.Sqrt(Math.Pow(Math.Abs(navrat.x - bodPriblizenia.x), 2) + Math.Pow(Math.Abs(navrat.y - bodPriblizenia.y), 2)));
                // Výpis hodnôt (Radšej sa na to nepozeraj) (,,>﹏<,,)👉👈
            }

            Console.ReadLine(); // Iba z toho dôvodu aby si mohol použíavatel pozrieť hodnoty kým sa program vypne :D
        }
    }
}
