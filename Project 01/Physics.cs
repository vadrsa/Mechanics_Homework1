namespace Mechanics_Homework
{
    public static class Physics
    {

        public static float GetDistance(float initialVelocity, float time, float accelleration)
        {
            return initialVelocity * time + (accelleration * time * time) / 2;
        }

        public static float GetDistance(float initialVelocity, float time, float accelleration, float maxSpeed)
        {
            float tMax = (maxSpeed - initialVelocity) / accelleration;
            if (tMax >= time)
                return GetDistance(initialVelocity, time, accelleration);

            return GetDistance(initialVelocity, tMax, accelleration) + maxSpeed * (time - tMax);
        }
    }
}
