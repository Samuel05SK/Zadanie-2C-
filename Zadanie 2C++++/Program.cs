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

        public double Fitness()
        {
            return Math.Pow((Math.Pow(x, 2) + y - 11), 2) + Math.Pow((x + Math.Pow(y, 2) - 7), 2);
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
                jednotlivci = jednotlivci.OrderBy(ind => ind.Fitness()).ToList();
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
                jednotlivci = jednotlivci.OrderBy(ind => ind.Fitness()).ToList();
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
                var navrat = populacia.nextGen(0.2, 00.2);
                Console.WriteLine(navrat.Fitness() + " \t " + navrat.x + " \t " + navrat.y + " \t ");
            }

            Console.ReadLine();
        }
    }
}
