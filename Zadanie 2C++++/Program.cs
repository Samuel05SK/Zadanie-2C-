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
            Random ran = new Random();
            hodnota = -100 + (ran.NextDouble()) *200;
            return hodnota;
        }

        public double Fitness(double x, double y)
        {
            double funkcna_hodnota = 0;
            return funkcna_hodnota;
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
