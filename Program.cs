using System;
using Hospital;

namespace TriagePlease
{
    class Program
    {
        static private EmergencyRoom Er;
        static void Main()
        {
            RunWith();
            RunWithout();
            Console.ReadKey();
        }
        private static void RunWith()
        {
            Console.WriteLine("Using Priority Que");
            Er = new EmergencyRoom();
            Er.processPatients(true);
        }

        private static void RunWithout()
        {
            Console.WriteLine("NOT using Priority Que");
            Er = new EmergencyRoom();
            Er.processPatients(false);
        }
    }
}
