using System.Text;
using LateComerTracker.Migrator.Engine;

namespace LateComerTracker.Migrator.Migrations
{
    [Migration(20140806224412)]
    internal class _20140806224412_CreateInsertTriggerOnLateEmployeeTable : BaseMigration
    {
        public override bool Up()
        {
            var commantText = new StringBuilder();
            commantText.AppendLine("CREATE TRIGGER LateEmployeeOnInsert ON [dbo].[LateEmployee]");
            commantText.AppendLine("AFTER INSERT");
            commantText.AppendLine("AS");
            commantText.AppendLine("BEGIN");
            commantText.AppendLine(" DECLARE @severity int, @empId int");
            commantText.AppendLine(" SELECT @severity = mtg_severity, @empId = i.le_empId FROM Meeting");
            commantText.AppendLine("     JOIN inserted i ON i.le_mtgId = mtg_id");
            commantText.AppendLine("	IF EXISTS(SELECT * FROM EmployeeFine ef WHERE ef.emp_id = @empId)");
            commantText.AppendLine("		UPDATE EmployeeFine SET unsettled_points += @severity WHERE emp_id = @empId");
            commantText.AppendLine("	ELSE");
            commantText.AppendLine("		INSERT INTO EmployeeFine (emp_id, unsettled_points) VALUES (@empId, @severity)");
            commantText.AppendLine("END");

            return ExecuteNonQuery(commantText.ToString());
        }

        public override bool Down()
        {
            return ExecuteNonQuery("DROP TRIGGER LateEmployeeOnInsert");
        }
    }
}