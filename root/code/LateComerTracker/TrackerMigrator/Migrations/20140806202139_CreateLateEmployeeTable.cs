using LateComerTracker.Migrator.Engine;

namespace LateComerTracker.Migrator.Migrations
{
    [Migration(20140806202139)]
    class _20140806202139_CreateLateEmployeeTable : BaseMigration
    {
        public override bool Up()
        {
            const string commantText = "CREATE TABLE [dbo].[LateEmployee]"
                                       + "("
                                       + " [le_id] [int] NOT NULL IDENTITY(1,1)"
                                       + ",[le_empId] [int] NOT NULL"
                                       + ",[le_mtgId] [int] NOT NULL"
                                       + ",[le_source] [varchar](50) NOT NULL"
                                       + ",[le_lateOn] [datetime] NOT NULL DEFAULT(GETDATE())"
                                       + ",CONSTRAINT FK_LateEmployee_EmpId_Employee_Id FOREIGN KEY (le_empId) REFERENCES Employee(emp_id)"
                                       + ",CONSTRAINT FK_LateEmployee_MtgId_Meeting_Id FOREIGN KEY (le_mtgId) REFERENCES Meeting(mtg_id)"
                                       + ")";

            return ExecuteNonQuery(commantText);
        }

        public override bool Down()
        {
            return ExecuteNonQuery("DROP TABLE [dbo].[LateEmployee]");
        }
    }
}
