using System;

namespace LateComerTracker.Migrator
{
    class Program
    {
        static void Main(string[] args)
        {
            new MigrationEngine().Run(null); // Up
            //new MigrationEngine().Run(0); // down to zero
            //new MigrationEngine().Run(20140802194036); // down to zero
            Console.ReadLine();
        }
    }
}
