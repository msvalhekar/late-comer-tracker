using LateComerTracker.Migrator.Engine;

namespace LateComerTracker.Migrator.Migrations
{
    [Migration(20140806231425)]
    class _20140806231425_CreateConfigTable : BaseMigration
    {
        public override bool Up()
        {
            const string commantText = "CREATE TABLE [dbo].[Configuration]"
                                       + "("
                                       + " [cng_key] [varchar](20) NOT NULL"
                                       + ",[cng_value] [varchar](50) NOT NULL"
                                       + ",CONSTRAINT UK_Configuration_Key UNIQUE (cng_key)"
                                       + ")";
            var bSuccess = ExecuteNonQuery(commantText);
            if(bSuccess)
                bSuccess = ExecuteNonQuery(string.Format("INSERT INTO Configuration (cng_key, cng_value) VALUES ('{0}', '{1}')", "PointsPerPenalty", "10"));
            return bSuccess;
        }

        public override bool Down()
        {
            return DropTable("[dbo].[Configuration]");
        }
    }
}
