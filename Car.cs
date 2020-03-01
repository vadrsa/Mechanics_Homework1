using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics_Homework
{
    public class Car
    {
        private float _velocity;
        private float _accelleration;
        private float _negativeAccelleration;
        private float _maxVelocity;
        private bool _hasMaxVelocity;

        public float Velocity => _velocity;
        public float Accelleration => _accelleration;
        public float NegativeAccelleration => _negativeAccelleration;
        public float MaxVelocity => _maxVelocity;
        public bool HasMaxVelocity => _hasMaxVelocity;

        public Car(float velocity, float accelleration, float negativeAccelleration)
        {
            this._velocity = velocity;
            this._accelleration = accelleration;
            this._negativeAccelleration = negativeAccelleration;
        }

        public Car(float velocity, float accelleration, float negativeAccelleration, float maxVelocity) : this(velocity, accelleration, negativeAccelleration)
        {
            if (maxVelocity > 0)
            {
                this._maxVelocity = maxVelocity;
                this._hasMaxVelocity = true;
            }
        }

        public static Car CreateFromConsole(bool hasMaxSpeed = false)
        {
            float velocity = Input.OfType<float>("Car's initial Velocity(km/h)", v => v >= 20 && v <= 80);
            float velocityInMS = velocity/3.6f;
            float maxVelocityinMS = -1;
            if (hasMaxSpeed)
            {
                float maxVelocity = Input.OfType<float>("Car's maximal Velocity(km/h)", v => v >= 50 && v <= 100 && v >= velocity);
                maxVelocityinMS = maxVelocity / 3.6f;
            }
            float accelleration = Input.OfType<float>("Car's Accelleration", v => v >= 1 && v <= 3);
            float negative = Input.OfType<float>("Car's Negative Accelleration", v => v >= 1 && v <= 3);
            return new Car(velocityInMS, accelleration, negative, maxVelocityinMS);
        }
        
    }
}
