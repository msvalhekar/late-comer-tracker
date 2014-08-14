using LateComerTracker.Migrator.Engine;

namespace LateComerTracker.Migrator.Migrations
{
    [Migration(20140802194215)]
    class _20140802194215_CreateTeamEmployeeTable : BaseMigration
    {
        public override bool Up()
        {
            const string commantText = "CREATE TABLE [dbo].[TeamEmployee]"
                                       + "("
                                       + " [team_id] [int] NOT NULL"
                                       + ",[emp_id] [int] NOT NULL"
                                       + ",CONSTRAINT FK_TeamEmployee_TeamId_Team_Id FOREIGN KEY (team_id) REFERENCES Team(team_id)"
                                       + ",CONSTRAINT FK_TeamEmployee_EmpId_Employee_Id FOREIGN KEY (emp_id) REFERENCES Employee(emp_id)"
                                       + ")";

            return ExecuteNonQuery(commantText);
        }

        public override bool Down()
        {
            return DropTable("[dbo].[TeamEmployee]");
        }
    }
}
