using LateComerTracker.Migrator.Engine;

namespace LateComerTracker.Migrator.Migrations
{
    [Migration(20140804211523)]
    class _20140804211523_CreateMeetingTable : BaseMigration
    {
        public override bool Up()
        {
            const string commantText = "CREATE TABLE [dbo].[Meeting]"
                                       + "("
                                       + " [mtg_id] [int] NOT NULL IDENTITY(1,1)"
                                       + ",[mtg_name] [varchar](20) NOT NULL"
                                       + ",[mtg_description] [varchar](50)"
                                       + ",[mtg_severity] [int] NOT NULL"
                                       + ",CONSTRAINT PK_Meeting_Id PRIMARY KEY (mtg_id)"
                                       + ",CONSTRAINT UK_Meeting_Name UNIQUE (mtg_name)"
                                       + ")";

            return ExecuteNonQuery(commantText);
        }

        public override bool Down()
        {
            return ExecuteNonQuery("DROP TABLE [dbo].[Meeting]");
        }
    }
}
