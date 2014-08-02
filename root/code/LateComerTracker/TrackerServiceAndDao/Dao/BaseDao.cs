using System.Data;
using LateComerTracker.DataAccess;

namespace LateComerTracker.Backend.Dao
{
    public class BaseDao
    {
        private readonly DataAccessObject _dataAccess;

        public BaseDao()
        {
            const string connString = "Data Source=localhost;Initial Catalog=tracker;User Id=SQLConnect;Password=Pench2006;";
            _dataAccess = new DataAccessObject(connString);
        }

        public DataTable GetDataTable(string commandText)
        {
            return _dataAccess.GetList(commandText);
        }
    }
}
