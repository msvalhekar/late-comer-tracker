using System.Configuration;
using LateComerTracker.DataAccess;

namespace LateComerTracker.Migrator
{
    abstract class BaseMigration
    {
        public abstract void Up();
        public abstract void Down();

        private readonly DataAccessObject _dataAccess;

        protected BaseMigration()
        {
            var connString = ConfigurationManager.ConnectionStrings["DB_Connection_Tracker"].ConnectionString;
            _dataAccess = new DataAccessObject(connString);
        }

        protected void ExecuteNonQuery(string commandText)
        {
            _dataAccess.ExecuteNonQuery(commandText);
        }
    }
}
