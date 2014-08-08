using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using LateComerTracker.Backend.Models;

namespace LateComerTracker.Backend.DAOs
{
    public class ConfigurationDao : BaseDao
    {
        public IList<Configuration> GetAll()
        {
            var table = GetDataTable("select cng_key, cng_value from Configuration");

            return (from DataRow row in table.Rows
                select new Configuration
                {
                    Key = row["cng_key"].ToString(),
                    Value = row["cng_value"].ToString()
                }).ToList();
        }

        //public Configuration Add(Configuration configuration)
        //{
        //    if (configuration == null) return null;

        //    var commandText = string.Format("INSERT INTO Configuration (cng_key, cng_value) VALUES ('{0}', '{1}')",
        //        configuration.Key, configuration.Value);

        //    ExecuteNonQuery(commandText);
        //    return configuration;
        //}

        //public bool Delete(Configuration configuration)
        //{
        //    var commandText = string.Format("DELETE Configuration WHERE cng_key = '{0}'", configuration.Key);

        //    return -1 < ExecuteNonQuery(commandText);
        //}

        public void Update(Configuration configuration)
        {
            var commandText = string.Format("UPDATE Configuration SET cng_value = '{1}' WHERE cng_key = '{0}'",
                configuration.Key, configuration.Value);

            ExecuteNonQuery(commandText);
        }
    }
}
