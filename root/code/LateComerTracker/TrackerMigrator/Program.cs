using System;
using LateComerTracker.Migrator.Engine;

namespace LateComerTracker.Migrator
{
    class Program
    {
        static void Main(string[] args)
        {
            // ----------------------------------
            // | Migrate to |   Arguments       |
            // |------------|-------------------|
            // | Latest     | nothing / null    |
            // | Empty Db   | 0                 |
            // | Migration  | <migrationNumber> |
            // ----------------------------------

            long? migNumber = null;
            if (0 < args.Length) migNumber = Convert.ToInt64(args[0]);

            new MigrationEngine().Run(migNumber);
            
            Console.WriteLine("\nHit 'Enter' to exit.");
            Console.ReadLine();
        }
    }
}
