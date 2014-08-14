using LateComerTracker.Migrator.Engine;

namespace LateComerTracker.Migrator.Migrations
{
    [Migration(20140806225554)]
    class _20140806225554_CreatePenaltyTable : BaseMigration
    {
        public override bool Up()
        {
            const string commantText = "CREATE TABLE [dbo].[Penalty]"
                                       + "("
                                       + " [pn_id] [int] NOT NULL IDENTITY(1,1)"
                                       + ",[pn_teamId] [int] NOT NULL"
                                       + ",[pn_empId] [int] NOT NULL"
                                       + ",[pn_how] [varchar](100) NOT NULL"
                                       + ",[pn_source] [varchar](50) NOT NULL"
                                       + ",[pn_servedOn] [datetime] NOT NULL"
                                       + ",CONSTRAINT FK_Penalty_TeamId_Team_Id FOREIGN KEY (pn_teamId) REFERENCES Team(team_id)"
                                       + ",CONSTRAINT FK_Penalty_EmpId_Employee_Id FOREIGN KEY (pn_empId) REFERENCES Employee(emp_id)"
                                       + ")";

            return ExecuteNonQuery(commantText);
        }

        public override bool Down()
        {
            return DropTable("[dbo].[Penalty]");
        }
    }
}
