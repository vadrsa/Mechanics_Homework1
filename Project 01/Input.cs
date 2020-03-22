using System;

namespace Mechanics_Homework
{
    public static class Input
    {
        public static T OfType<T>(string description, Func<T, bool> validator = null)
        {
            Console.Write(description + ": ");
            try
            {
                var value = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
                if (validator != null && !validator(value))
                    throw new ArgumentException();

                return value;
            }
            catch
            {
                Console.WriteLine("Invalid value, please try again.");
                return OfType<T>(description, validator);
            }
        }
    }
}
