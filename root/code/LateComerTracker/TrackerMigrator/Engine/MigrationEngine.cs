using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using LateComerTracker.DataAccess;

namespace LateComerTracker.Migrator.Engine
{
    class MigrationEngine
    {
        public void Run(long? migrationNumber)
        {
            // ensure SchemaInfo table exists
            CreateSchemaInfoIfNotExists();

            if (migrationNumber.HasValue) // Down
                ExecuteDownMigration(migrationNumber.Value);
            else // Up
                ExecuteUpMigration();
        }

        private void ExecuteUpMigration()
        {
            // 1. get most recent db.MigrationNumber
            // 2. get all classes with MigrationAttribute and derived from BaseMigration
            // 3. get all asm.MigrationNumbers which are ahead of (1)
            // 4. order (3) collection by ascending asm.MigrationNumber
            // 5. foreach migration in (4) collection
            // 5.1 run .Up()
            // 5.2 if success, insert MigrationNumber in SchemaInfo
            // 5.2 else, stop and report error to be corrected first 

            long recentMigration = GetRecentMigration();
            var assemblyMigrations = GetAssemblyMigrations();
            var migrationsToApply = assemblyMigrations.Select(type =>
            {
                var typeMigrrationNumber = type.GetCustomAttribute<MigrationAttribute>().Number;
                if (recentMigration < typeMigrrationNumber)
                    return new KeyValuePair<long, Type>(typeMigrrationNumber, type);
                return new KeyValuePair<long, Type>();
            })
            .Where(x => x.Key != 0)
            .OrderBy(x => x.Key).ToList();

            foreach (var migrationPair in migrationsToApply)
            {
                if (!CreateInstanceAndRunMigration(migrationPair, true))
                {
                    Console.WriteLine("{0} failed.", migrationPair.Key);
                    break;
                }
            }
        }

        private void ExecuteDownMigration(long migrationNumber)
        {
            // 1. get all db.MigrationNumber(s) which are ahead minMigrationNumber
            // 2. get all classes with MigrationAttribute Number in (1) collection and derived from BaseMigration
            // 3. order (2) collection by descending MigrationNumber
            // 4. foreach migration in (3) collection
            // 4.1 run .Down()
            // 4.2 delete MigrationNumber from SchemaInfo (irrespective of result :( 

            var dbMigrationsToRemove = GetMigrationsAbove(migrationNumber).ToList();
            var maxMigrationNumber = dbMigrationsToRemove.LastOrDefault() == 0 ? long.MaxValue : dbMigrationsToRemove.LastOrDefault();
            var assemblyMigrationsToRemove = GetAssemblyMigrations(migrationNumber, maxMigrationNumber)
                .Select(type => new KeyValuePair<long, Type>(type.GetCustomAttribute<MigrationAttribute>().Number, type))
                .OrderByDescending(x => x.Key).ToList();

            foreach (var migrationPair in assemblyMigrationsToRemove)
            {
                if (!CreateInstanceAndRunMigration(migrationPair, false))
                    Console.WriteLine("{0} failed.", migrationPair.Key);
            }
        }

        private bool CreateInstanceAndRunMigration(KeyValuePair<long, Type> pair, bool runUpMigration)
        {
            try
            {
                var instance = Activator.CreateInstance(pair.Value);
                if (instance is BaseMigration)
                {
                    var migration = (instance as BaseMigration);
                    if (runUpMigration)
                    {
                        Console.Write(pair.Key + " UP ");
                        migration.Up();
                        DataAccessObject.ExecuteScalar("INSERT INTO SchemaInfo ([Version]) VALUES (" + pair.Key + ")");
                    }
                    else
                    {
                        Console.Write(pair.Key + " DOWN ");
                        migration.Down();
                        DataAccessObject.ExecuteScalar("DELETE SchemaInfo WHERE [Version] = " + pair.Key);
                    }
                }
                Console.WriteLine("SUCCESS");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("FAILED");
                return false;
            }
        }

        private IEnumerable<long> GetMigrationsAbove(long migrationNumber)
        {
            var dataTable = DataAccessObject.GetList("SELECT [Version] FROM SchemaInfo WHERE [Version] > " + migrationNumber);
            return from DataRow dataRow in dataTable.Rows select Convert.ToInt64(dataRow["Version"]);
        }

        private long GetRecentMigration()
        {
            var result = DataAccessObject.ExecuteScalar("SELECT TOP 1 [Version] FROM dbo.SchemaInfo ORDER BY [Version] DESC");
            return Convert.ToInt64(result);
        }

        private IEnumerable<Type> GetAssemblyMigrations(long minMigrationNumber = 0, long maxMigrationNumber = long.MaxValue)
        {
            return GetType().Assembly.GetTypes()
                .Where(type =>
                {
                    if (type.IsDefined(typeof (MigrationAttribute), true) && type.IsSubclassOf(typeof (BaseMigration)))
                    {
                        var typeMigrationNumber = type.GetCustomAttribute<MigrationAttribute>().Number;
                        return minMigrationNumber < typeMigrationNumber && typeMigrationNumber <= maxMigrationNumber;
                    }
                    return false;
                });
        }

        private void CreateSchemaInfoIfNotExists()
        {
            const string commantText = "IF NOT EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME='SchemaInfo' and xtype='U')"
                                       + "CREATE TABLE [dbo].[SchemaInfo]"
                                       + "( [Version] [bigint] NOT NULL )";

            DataAccessObject.ExecuteNonQuery(commantText);
        }

        private string ConnectionString {
            get { return ConfigurationManager.ConnectionStrings["DB_Connection_Tracker"].ConnectionString; }
        }

        private DataAccessObject _dataAccessObject;
        private DataAccessObject DataAccessObject {
            get { return _dataAccessObject ?? (_dataAccessObject = new DataAccessObject(ConnectionString)); }
        }
    }
}
