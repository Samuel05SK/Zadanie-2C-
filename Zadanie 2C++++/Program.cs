using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Zadanie_2C____
{
    public class Individuals
    {
        public double x;
        public double y;

        public double Generator()
        {
            double hodnota;
            Random random = new Random();
            hodnota = -100 + (random.NextDouble()) * 200;
            return hodnota;
        }

        public double Fitness()
        {
            return -Math.Cos(x) * Math.Cos(y) * Math.Exp(-(Math.Pow(x - Math.PI, 2)) + (Math.Pow(y - Math.PI, 2)));
        }

        public void mutate(double mutationRate)
        {
            Random random = new Random();
            double hodnota = random.NextDouble();
            if(hodnota < mutationRate)
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

    public class Population
    {
     
        List<Individuals> jednotlivci = new List<Individuals>();
        public int selection(double selectionRate)
        {
            jednotlivci.Sort((a, b) => a.Fitness().CompareTo(b.Fitness()));
            jednotlivci.Reverse();
            int dlzka = jednotlivci.Count;
            int alive = (int)Math.Round(dlzka * selectionRate);
            int pocetMrtvych = dlzka - alive;
            jednotlivci.RemoveRange(alive, pocetMrtvych);
            return pocetMrtvych;
        }

        public void cloning(double mutationRate)
        {
            int velkostPopulacie = jednotlivci.Count;
            Random random = new Random();
            int klonovanyJedinec = random.Next(0, velkostPopulacie - 2);
            
            jednotlivci.Add(new Individuals());
            int Alex = velkostPopulacie; //Alex == klon
            jednotlivci[Alex].x = jednotlivci[klonovanyJedinec].x;
            jednotlivci[Alex].y = jednotlivci[klonovanyJedinec].y;
            jednotlivci[Alex].mutate(mutationRate);
            
            /*Individuals klon = new Individuals();
            klon.x = jednotlivci[klonovanyJedinec].x;
            klon.y = jednotlivci[klonovanyJedinec].y;
            klon.mutate(mutationRate);
            jednotlivci.Add(klon);*/
        }

        public double baseGen()
        {
            jednotlivci.Sort((a, b) => a.Fitness().CompareTo(b.Fitness()));
            jednotlivci.Reverse();
            return jednotlivci[0].Fitness();
        }

        public double nextGen(double selectionRate, double mutationRate)
        {
            int pocetMrtvych = selection(selectionRate);
            for(int i = 0; i<pocetMrtvych; i++)
            {
                cloning(mutationRate);
            }
            return baseGen();
        }

        public Population(int maxPopulation)
        {
            for(int i = 0; i < maxPopulation; i++)
            {
                jednotlivci.Add(new Individuals());
                jednotlivci[i].x = jednotlivci[i].Generator();
                jednotlivci[i].y = jednotlivci[i].Generator();
            }

        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            //Console.OutputEncoding = System.Text.Encoding.UTF8;

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
                Console.WriteLine(populacia.nextGen(0.2, 0.02));
            }

            Console.ReadLine();
        }
    }
}
