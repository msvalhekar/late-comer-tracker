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
            commantText.AppendLine(" DECLARE @severity int, @teamId int, @empId int");
            commantText.AppendLine(" SELECT @severity = mtg_severity, @teamId = i.le_teamId, @empId = i.le_empId FROM Meeting");
            commantText.AppendLine("     JOIN inserted i ON i.le_mtgId = mtg_id");
            commantText.AppendLine("	IF EXISTS(SELECT * FROM EmployeeFine ef WHERE  ef.team_id = @teamId AND ef.emp_id = @empId)");
            commantText.AppendLine("		UPDATE EmployeeFine SET unsettled_points += @severity WHERE team_id = @teamId AND emp_id = @empId");
            commantText.AppendLine("	ELSE");
            commantText.AppendLine("		INSERT INTO EmployeeFine (team_id, emp_id, unsettled_points) VALUES (@teamId, @empId, @severity)");
            commantText.AppendLine("END");

            return ExecuteNonQuery(commantText.ToString());
        }

        public override bool Down()
        {
            return DropTrigger("LateEmployeeOnInsert");
        }
    }
}