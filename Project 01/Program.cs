using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics_Homework
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        static void OneCar()
        {

            bool hasMax = Input.OfType<string>("Does the car have max velocity(Y/N)") == "Y";
            var car = Car.CreateFromConsole(hasMax);
            float distance = Input.OfType<float>("Distance of Car to the intersection: ", v => v >= 10 && v <= 150);
            var intersection = Intersection.CreateFromConsole();
            WriteDecision(intersection.PassYellowLight(car, distance));
            Console.ReadLine();
        }

        static void TwoCars()
        {

            var car = Car.CreateFromConsole();
            float distance = Input.OfType<float>("Distance of Car to the intersection: ", v => v >= 10 && v <= 150);

            var car2 = Car.CreateFromConsole();
            float distance2 = Input.OfType<float>("Distance of Car to the next Car: ", v => v >= 10 && v <= 100);
            var intersection = Intersection.CreateFromConsole();
            var result = intersection.PassYellowLight(car, car2, distance, distance2);
            Console.Write("Car 1: ");
            WriteDecision(result.Item1);
            Console.Write("Car 2: ");
            WriteDecision(result.Item2);
            Console.ReadLine();
        }

        static void WriteDecision(Decision decision)
        {
            switch (decision)
            {
                case Decision.Accellerate:
                    Console.WriteLine("Push the gas pedal!");
                    break;
                case Decision.Break:
                    Console.WriteLine("Break!");
                    break;
                default:
                    Console.WriteLine("It's too late to make a decision");
                    break;
            }
        }
    }
}
