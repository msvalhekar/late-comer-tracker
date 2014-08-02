namespace LateComerTracker.Migrator
{
    [Migration(20140802194036)]
    class _20140802194036_CreateEmployeeTable : BaseMigration
    {
        public override void Up()
        {
            const string commantText = "CREATE TABLE [dbo].[Employee]"
                                       + "("
                                       + " [emp_id] [int] NOT NULL IDENTITY(1,1)"
                                       + ",[emp_name] [nvarchar](20) NOT NULL"
                                       + ",[emp_emailId] [nvarchar](50) NOT NULL"
                                       + ",CONSTRAINT PK_Employee_Id PRIMARY KEY (emp_id)"
                                       + ",CONSTRAINT UK_Employee_emailId UNIQUE (emp_emailId)"
                                       + ")";

            ExecuteNonQuery(commantText);
        }

        public override void Down()
        {
            ExecuteNonQuery("DROP TABLE [dbo].[Employee]");
        }
    }
}
