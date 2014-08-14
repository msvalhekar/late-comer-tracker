using LateComerTracker.Migrator.Engine;

namespace LateComerTracker.Migrator.Migrations
{
    [Migration(20140804202034)]
    class _20140804202034_CreateEmployeeFineTable : BaseMigration
    {
        public override bool Up()
        {
            const string commantText = "CREATE TABLE [dbo].[EmployeeFine]"
                                       + "("
                                       + " [team_id] [int] NOT NULL"
                                       + ",[emp_id] [int] NOT NULL"
                                       + ",[unsettled_points] [int] NOT NULL"
                                       + ",CONSTRAINT FK_EmployeeFine_TeamId_Team_Id FOREIGN KEY (team_id) REFERENCES Team(team_id)"
                                       + ",CONSTRAINT FK_EmployeeFine_EmpId_Employee_Id FOREIGN KEY (emp_id) REFERENCES Employee(emp_id)"
                                       + ")";

            return ExecuteNonQuery(commantText);
        }

        public override bool Down()
        {
            return DropTable("[dbo].[EmployeeFine]");
        }
    }
}
