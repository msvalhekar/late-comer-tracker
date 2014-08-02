using System;

namespace LateComerTracker.Migrator
{
    class MigrationAttribute : Attribute
    {
        public long Number { get; set; }
        public string Description { get; set; }

        public MigrationAttribute(long number)
        {
            Number = number;
        }
    }
}
