namespace Zadanie_2C____
{
    public class Individuals
    {
        private Random random = new Random();
        public double x;
        public double y;

        private Random random = new Random();

        public double Generator()
        {
            return -10 + (random.NextDouble()) * 20;
        }

        public double Fitness()
        {
            return 0.26 * (Math.Pow(x, 2) + Math.Pow(y, 2) - 0.48 * x * y);
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

   

    internal class Program
    {
        public static List<Individuals> jednotlivci = new List<Individuals>();
        private class Population
        {
            private Random random = new Random();
            
            public int selection(double selectionRate)
            {
                jednotlivci = jednotlivci.OrderByDescending(ind => ind.Fitness()).ToList();
                foreach (Individuals i in jednotlivci)
                {
                    Console.WriteLine("Fitness:    " + i.Fitness());
                }
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

            public double baseGen()
            {
                jednotlivci = jednotlivci.OrderByDescending(ind => ind.Fitness()).ToList();
                return jednotlivci[0].Fitness();
            }

            public double nextGen(double selectionRate, double mutationRate)
            {
                int pocetMrtvych = selection(selectionRate);
                for (int i = 0; i < pocetMrtvych; i++)
                {
                    cloning(mutationRate);
                }
                return baseGen();
            }

            public Population(int maxPopulation)
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
            int maxPopulation = 0;
            bool FunkciaOk = true;
            Console.WriteLine("Zadanie Variant F: Umelá krajina");
            Console.WriteLine("\n------------------------------------------\n");
            do
            {
                FunkciaOk = true;
                try
                {
                    Console.WriteLine("Zadaj celé číslo");
                    maxPopulation = int.Parse(Console.ReadLine());
                    if (maxPopulation < 4) throw new Exception("Hodnota musí byť väčšia ako 3");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n------------------------------------------");
                    Console.WriteLine("Zadal/a si zlú hodnotu\n" + e);
                    Console.WriteLine("------------------------------------------\n");
                    FunkciaOk = false;
                }
            }
            while (FunkciaOk == false);

            Population populacia = new Population(maxPopulation);
            for(int i = 0;i < 100;i++)
            {
                Console.WriteLine(populacia.nextGen(0.2, 0.2));
            }

            Console.ReadLine();
        }
    }
}
