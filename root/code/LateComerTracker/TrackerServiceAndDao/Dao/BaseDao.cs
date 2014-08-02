using System.Data;
using LateComerTracker.DataAccess;

namespace LateComerTracker.Backend.Dao
{
    public class BaseDao
    {
        private readonly DataAccessObject _dataAccess;

        public BaseDao(string connString)
        {
            _dataAccess = new DataAccessObject(connString);
        }

        public DataTable GetDataTable(string commandText)
        {
            return _dataAccess.GetList(commandText);
        }
    }
}
