using System.Data.SqlTypes;
using System.Security.Cryptography.X509Certificates;

namespace Zadanie_2C____
{
    public class Individuals
    {
        private Random random = new Random();
        public double x;
        public double y;

        public double Generator()
        {
            return -5 + (random.NextDouble()) * 10;
        }

        public double Fitness
        {
            get
            {
                return Math.Pow((Math.Pow(x, 2) + y - 11), 2) + Math.Pow((x + Math.Pow(y, 2) - 7), 2);
            }
        }

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

    public class Bod
    {
        public double x;
        public double y;
        public Bod HladanieOblasti(double x, double y)
        {
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
            for(int i = 0; i < minFunkcie.Length; i++)
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
        public static List<Individuals> jednotlivci = new List<Individuals>();
        private class Population
        {
            private Random random = new Random();
            
            public int selection(double selectionRate)
            {
                jednotlivci = jednotlivci.OrderBy(ind => ind.Fitness).ToList();
                int dlzka = jednotlivci.Count;
                int alive = (int)Math.Round(dlzka * selectionRate);
                int pocetMrtvych = dlzka - alive;
                jednotlivci.RemoveRange(alive, pocetMrtvych);
                return pocetMrtvych;
            }

            public void cloning(double mutationRate)
            {
                int velkostPopulacie = jednotlivci.Count;
                int klonovanyJedinec = random.Next(0, velkostPopulacie - 1);
                jednotlivci.Add(new Individuals());
                int Alex = velkostPopulacie; //Alex == klon
                jednotlivci[Alex].x = jednotlivci[klonovanyJedinec].x;
                jednotlivci[Alex].y = jednotlivci[klonovanyJedinec].y;
                jednotlivci[Alex].mutate(mutationRate);
            }

            public Individuals baseGen()
            {
                jednotlivci = jednotlivci.OrderBy(ind => ind.Fitness).ToList();
                return jednotlivci[0];
            }

            public Individuals nextGen(double selectionRate, double mutationRate)
            {
                int pocetMrtvych = selection(selectionRate);
                for (int i = 0; i < pocetMrtvych; i++)
                {
                    cloning(mutationRate);
                }
                return baseGen();
            }

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
            uint maxPopulation = 0;

            Console.WriteLine("Zadanie Variant F: Umelá krajina");
            Console.WriteLine("\n------------------------------------------\n");

                try
                {
                    Console.WriteLine("Zadaj celé číslo");
                    maxPopulation = uint.Parse(Console.ReadLine());
                    if (maxPopulation < 4) throw new Exception("Hodnota musí byť väčšia ako 3");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n------------------------------------------");
                    Console.WriteLine("Zadal/a si zlú hodnotu\n" + e);
                    Console.WriteLine("------------------------------------------\n");           
                }

            Population populacia = new Population(maxPopulation);    
            Bod bodik = new Bod();
            for(int i = 0;i < 100;i++)
            {
                var navrat = populacia.nextGen(0.2, 00.2);
                var bodPriblizenia = bodik.HladanieOblasti(navrat.x, navrat.y);
                Console.WriteLine("Fitness: " + navrat.Fitness + " \t Súradnica x: " + navrat.x + " \t y: " + navrat.y + " \t  " + "Blíži sa k bodu x: " + bodPriblizenia.x + " \t " + " \t y: " + bodPriblizenia.y + " \t vzdialenosť od skutočného stredu: " + Math.Sqrt(Math.Pow(Math.Abs(navrat.x - bodPriblizenia.x), 2) + Math.Pow(Math.Abs(navrat.y - bodPriblizenia.y), 2)));
            }

            Console.ReadLine();
        }
    }
}
