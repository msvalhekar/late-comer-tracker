using LateComerTracker.Migrator.Engine;

namespace LateComerTracker.Migrator.Migrations
{
    [Migration(20140802183305)]
    class _20140802183305_CreateTeamTable : BaseMigration
    {
        public override bool Up()
        {
            const string commantText = "CREATE TABLE [dbo].[Team]"
                                       + "("
                                       + " [team_id] [int] NOT NULL IDENTITY(1,1)"
                                       + ",[team_name] [varchar](20) NOT NULL"
                                       + ",[team_description] [varchar](50) NULL"
                                       + ",CONSTRAINT PK_Team_Id PRIMARY KEY (team_id)"
                                       + ",CONSTRAINT UK_Team_Name UNIQUE (team_name)"
                                       + ")";

            return ExecuteNonQuery(commantText);
        }

        public override bool Down()
        {
            return ExecuteNonQuery("DROP TABLE [dbo].[Team]");
        }
    }
}
