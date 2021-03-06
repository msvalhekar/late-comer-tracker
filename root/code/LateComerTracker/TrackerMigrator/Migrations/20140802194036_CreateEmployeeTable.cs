﻿using LateComerTracker.Migrator.Engine;

namespace LateComerTracker.Migrator.Migrations
{
    [Migration(20140802194036)]
    class _20140802194036_CreateEmployeeTable : BaseMigration
    {
        public override bool Up()
        {
            const string commantText = "CREATE TABLE [dbo].[Employee]"
                                       + "("
                                       + " [emp_id] [int] NOT NULL IDENTITY(1,1)"
                                       + ",[emp_name] [varchar](20) NOT NULL"
                                       + ",[emp_emailId] [varchar](50) NOT NULL"
                                       + ",CONSTRAINT PK_Employee_Id PRIMARY KEY (emp_id)"
                                       + ",CONSTRAINT UK_Employee_emailId UNIQUE (emp_emailId)"
                                       + ")";

            return ExecuteNonQuery(commantText);
        }

        public override bool Down()
        {
            return DropTable("[dbo].[Employee]");
        }
    }
}
