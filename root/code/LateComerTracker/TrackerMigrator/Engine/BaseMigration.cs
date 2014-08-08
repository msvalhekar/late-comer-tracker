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
    }
}
