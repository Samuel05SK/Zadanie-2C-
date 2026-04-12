using System.Collections;
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

        public double Fitness(double x, double y)
        {
            double funkcna_hodnota = 0;
            funkcna_hodnota = -Math.Cos(x) * Math.Cos(y) * Math.Exp(-(Math.Pow(x - Math.PI, 2)) + (Math.Pow(y - Math.PI, 2)));
            return funkcna_hodnota;
        }

        public void mutate(double mutationRate)
        {
            Random random = new Random();
            double hodnota = random.NextDouble();
            if(hodnota < mutationRate)
            {
                int randomXY = random.Next(1,2);
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
        public void selection(double selectionRate)
        {
            
        }

        public void cloning(double mutationRate)
        {
            
        }

        public Population(int maxPopulation)
        {
            List<Individuals> jednotlivci = new List<Individuals>();
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
            Console.ReadLine();
        }
    }
}
