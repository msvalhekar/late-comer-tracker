using System.Configuration;
using LateComerTracker.DataAccess;

namespace LateComerTracker.Migrator.Engine
{
    abstract class BaseMigration
    {
        public abstract bool Up();
        public abstract bool Down();

        private readonly DataAccessObject _dataAccess;

        protected BaseMigration()
        {
            var connString = ConfigurationManager.ConnectionStrings["DB_Connection_Tracker"].ConnectionString;
            _dataAccess = new DataAccessObject(connString);
        }

        protected bool ExecuteNonQuery(string commandText)
        {
            return -1 < _dataAccess.ExecuteNonQuery(commandText);
        }

        public bool DropTable(string tableName)
        {
            var commandText = string.Format("IF object_id('{0}') IS NOT NULL DROP TABLE {0}", tableName);
            return ExecuteNonQuery(commandText);
        }

        public bool DropTrigger(string triggerName)
        {
            var commandText = string.Format("IF EXISTS(SELECT * FROM sys.triggers WHERE name = '{0}') DROP TRIGGER {0}", triggerName);
            return ExecuteNonQuery(commandText);
        }
    }
}
