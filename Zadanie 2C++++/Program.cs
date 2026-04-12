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
            hodnota = -100 + (random.NextDouble()) *200;
            return hodnota;
        }

        public double Fitness(double x, double y)
        {
            double funkcna_hodnota = 0;
            funkcna_hodnota = -Math.Cos(x) * Math.Sin(y) * Math.Exp(-(Math.Pow(x - Math.PI, 2)) + (Math.Pow(y - Math.PI, 2)));
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

    }

    internal class Program
    {
        static void Main(string[] args)
        {

            Console.ReadLine();
        }
    }
}
