using System.Text;
using LateComerTracker.Migrator.Engine;

namespace LateComerTracker.Migrator.Migrations
{
    [Migration(20140806234912)]
    internal class _20140806234912_CreateInsertTriggerOnPenaltyTable : BaseMigration
    {
        public override bool Up()
        {
            var commantText = new StringBuilder();
            commantText.AppendLine("CREATE TRIGGER PenaltyOnInsert ON Penalty");
            commantText.AppendLine("AFTER INSERT");
            commantText.AppendLine("AS");
            commantText.AppendLine("BEGIN");
            commantText.AppendLine(" DECLARE @empId int, @pointsPerPenalty int");
            commantText.AppendLine(" SELECT @empId = i.pn_empId FROM inserted i");
            commantText.AppendLine(" SELECT @pointsPerPenalty = cng_value FROM Configuration where cng_key = 'PointsPerPenalty'");
            commantText.AppendLine(" UPDATE EmployeeFine SET unsettled_points -= @pointsPerPenalty WHERE emp_id = @empId");
            commantText.AppendLine("END");

            return ExecuteNonQuery(commantText.ToString());
        }

        public override bool Down()
        {
            return ExecuteNonQuery("DROP TRIGGER PenaltyOnInsert");
        }
    }
}