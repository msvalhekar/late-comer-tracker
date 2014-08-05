using LateComerTracker.Migrator.Engine;

namespace LateComerTracker.Migrator.Migrations
{
    [Migration(20140804202034)]
    class _20140804202034_CreateEmployeeFineTable : BaseMigration
    {
        public override void Up()
        {
            const string commantText = "CREATE TABLE [dbo].[EmployeeFine]"
                                       + "("
                                       + " [emp_id] [int] NOT NULL"
                                       + ",[unsettled_points] [int] NOT NULL"
                                       + ",CONSTRAINT FK_EmployeeFine_EmpId_Employee_Id FOREIGN KEY (emp_id) REFERENCES Employee(emp_id)"
                                       + ")";

            ExecuteNonQuery(commantText);
        }

        public override void Down()
        {
            ExecuteNonQuery("DROP TABLE [dbo].[EmployeeFine]");
        }
    }
}
