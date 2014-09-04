using LateComerTracker.Migrator.Engine;

namespace LateComerTracker.Migrator.Migrations
{
    [Migration(20140904125739)]
    class _20140904125739_AlterLateEmployeeTable_AddReason : BaseMigration
    {
        public override bool Up()
        {
            const string commantText = "ALTER TABLE [dbo].[LateEmployee]"
                                       + "ADD [le_reason] [varchar](100) NULL";

            return ExecuteNonQuery(commantText);
        }

        public override bool Down()
        {
            const string commantText = "ALTER TABLE [dbo].[LateEmployee] DROP [le_reason]";

            return ExecuteNonQuery(commantText);
        }
    }
}
