using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics_Homework
{
    public class Intersection
    {


        private int _yellowDuration;
        private float _intersectionWidth;

        public int YellowDuration => _yellowDuration;
        public float IntersectionWidth => _intersectionWidth;

        public Intersection(int yellowDuration, float intersectionWidth)
        {
            this._yellowDuration = yellowDuration;
            this._intersectionWidth = intersectionWidth;
        }

        public Decision PassYellowLight(Car car, float carDistance)
        {
            var distanceToPass = carDistance + _intersectionWidth;
            float distanceCanPass = 0;
            if (car.HasMaxVelocity)
                distanceCanPass = Physics.GetDistance(car.Velocity, _yellowDuration, car.Accelleration, car.MaxVelocity);
            else
                distanceCanPass = Physics.GetDistance(car.Velocity, _yellowDuration, car.Accelleration);
            if (distanceCanPass > distanceToPass)
                return Decision.Accellerate;
            var distanceWillPass = Physics.GetDistance(car.Velocity, _yellowDuration, -1 * car.Accelleration);
            if(distanceWillPass  <= carDistance)
                return Decision.Break;
            return Decision.Unsure;
        }

        public Tuple<Decision, Decision> PassYellowLight(Car car1, Car car2, float car1Distance, float car2Distance)
        {
            var car1Only = PassYellowLight(car1, car1Distance);
            if (car1Only == Decision.Unsure)
                return new Tuple<Decision, Decision>(Decision.Unsure, Decision.Unsure);
            if (PassYellowLight(car1, car1Distance) == Decision.Break)
                return new Tuple<Decision, Decision>(Decision.Break, Decision.Break);
            if(car1.Accelleration < car2.Accelleration)
                return new Tuple<Decision, Decision>(Decision.Unsure, Decision.Unsure);
            var car2Only = PassYellowLight(car1, car1Distance);
             return new Tuple<Decision, Decision>(Decision.Accellerate, car2Only);
        }

        public static Intersection CreateFromConsole()
        {

            int yellow = Input.OfType<int>("Intersection's Yellow Duration", v => v >= 2 && v <= 5);
            int width = Input.OfType<int>("Intersection's Width", v => v >= 5 && v <= 20);
            return new Intersection(yellow, width);
        }
    }
}
