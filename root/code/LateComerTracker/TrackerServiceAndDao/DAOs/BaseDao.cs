using System;
using System.Data;
using LateComerTracker.DataAccess;

namespace LateComerTracker.Backend.DAOs
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

        public int ExecuteScalar(string commandText)
        {
            return Convert.ToInt32(_dataAccess.ExecuteScalar(commandText));
        }

        public int ExecuteNonQuery(string commandText)
        {
            return Convert.ToInt32(_dataAccess.ExecuteNonQuery(commandText));
        }
    }
}
