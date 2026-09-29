using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HalachevAccounting.Infrastructure.Migrations;

public partial class AddProductionRowLevelSecurity : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("""
            ALTER TABLE "ContactRequests" ENABLE ROW LEVEL SECURITY;
            ALTER TABLE "ContactRequests" FORCE ROW LEVEL SECURITY;
            DROP POLICY IF EXISTS "ContactRequests_Select" ON "ContactRequests";
            DROP POLICY IF EXISTS "ContactRequests_Insert" ON "ContactRequests";
            DROP POLICY IF EXISTS "ContactRequests_Update" ON "ContactRequests";
            DROP POLICY IF EXISTS "ContactRequests_Delete" ON "ContactRequests";

            CREATE POLICY "ContactRequests_Select" ON "ContactRequests"
            FOR SELECT USING (
                current_setting('app.current_is_system', true) = 'true'
                OR current_setting('app.current_is_admin', true) = 'true'
            );
            CREATE POLICY "ContactRequests_Insert" ON "ContactRequests"
            FOR INSERT WITH CHECK (true);
            CREATE POLICY "ContactRequests_Update" ON "ContactRequests"
            FOR UPDATE USING (
                current_setting('app.current_is_system', true) = 'true'
                OR current_setting('app.current_is_admin', true) = 'true'
            ) WITH CHECK (
                current_setting('app.current_is_system', true) = 'true'
                OR current_setting('app.current_is_admin', true) = 'true'
            );
            CREATE POLICY "ContactRequests_Delete" ON "ContactRequests"
            FOR DELETE USING (
                current_setting('app.current_is_system', true) = 'true'
                OR current_setting('app.current_is_admin', true) = 'true'
            );

            ALTER TABLE "ServiceRequests" ENABLE ROW LEVEL SECURITY;
            ALTER TABLE "ServiceRequests" FORCE ROW LEVEL SECURITY;
            DROP POLICY IF EXISTS "ServiceRequests_Select" ON "ServiceRequests";
            DROP POLICY IF EXISTS "ServiceRequests_Insert" ON "ServiceRequests";
            DROP POLICY IF EXISTS "ServiceRequests_Update" ON "ServiceRequests";
            DROP POLICY IF EXISTS "ServiceRequests_Delete" ON "ServiceRequests";

            CREATE POLICY "ServiceRequests_Select" ON "ServiceRequests"
            FOR SELECT USING (
                current_setting('app.current_is_system', true) = 'true'
                OR current_setting('app.current_is_admin', true) = 'true'
            );
            CREATE POLICY "ServiceRequests_Insert" ON "ServiceRequests"
            FOR INSERT WITH CHECK (true);
            CREATE POLICY "ServiceRequests_Update" ON "ServiceRequests"
            FOR UPDATE USING (
                current_setting('app.current_is_system', true) = 'true'
                OR current_setting('app.current_is_admin', true) = 'true'
            ) WITH CHECK (
                current_setting('app.current_is_system', true) = 'true'
                OR current_setting('app.current_is_admin', true) = 'true'
            );
            CREATE POLICY "ServiceRequests_Delete" ON "ServiceRequests"
            FOR DELETE USING (
                current_setting('app.current_is_system', true) = 'true'
                OR current_setting('app.current_is_admin', true) = 'true'
            );

            ALTER TABLE "EmailLogs" ENABLE ROW LEVEL SECURITY;
            ALTER TABLE "EmailLogs" FORCE ROW LEVEL SECURITY;
            DROP POLICY IF EXISTS "EmailLogs_Select" ON "EmailLogs";
            DROP POLICY IF EXISTS "EmailLogs_Insert" ON "EmailLogs";
            DROP POLICY IF EXISTS "EmailLogs_Update" ON "EmailLogs";
            DROP POLICY IF EXISTS "EmailLogs_Delete" ON "EmailLogs";

            CREATE POLICY "EmailLogs_Select" ON "EmailLogs"
            FOR SELECT USING (
                current_setting('app.current_is_system', true) = 'true'
                OR current_setting('app.current_is_admin', true) = 'true'
            );
            CREATE POLICY "EmailLogs_Insert" ON "EmailLogs"
            FOR INSERT WITH CHECK (true);
            CREATE POLICY "EmailLogs_Update" ON "EmailLogs"
            FOR UPDATE USING (
                current_setting('app.current_is_system', true) = 'true'
                OR current_setting('app.current_is_admin', true) = 'true'
            ) WITH CHECK (
                current_setting('app.current_is_system', true) = 'true'
                OR current_setting('app.current_is_admin', true) = 'true'
            );
            CREATE POLICY "EmailLogs_Delete" ON "EmailLogs"
            FOR DELETE USING (
                current_setting('app.current_is_system', true) = 'true'
                OR current_setting('app.current_is_admin', true) = 'true'
            );
            """);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("""
            ALTER TABLE "ContactRequests" DISABLE ROW LEVEL SECURITY;
            ALTER TABLE "ServiceRequests" DISABLE ROW LEVEL SECURITY;
            ALTER TABLE "EmailLogs" DISABLE ROW LEVEL SECURITY;
            DROP POLICY IF EXISTS "ContactRequests_Select" ON "ContactRequests";
            DROP POLICY IF EXISTS "ContactRequests_Insert" ON "ContactRequests";
            DROP POLICY IF EXISTS "ContactRequests_Update" ON "ContactRequests";
            DROP POLICY IF EXISTS "ContactRequests_Delete" ON "ContactRequests";
            DROP POLICY IF EXISTS "ServiceRequests_Select" ON "ServiceRequests";
            DROP POLICY IF EXISTS "ServiceRequests_Insert" ON "ServiceRequests";
            DROP POLICY IF EXISTS "ServiceRequests_Update" ON "ServiceRequests";
            DROP POLICY IF EXISTS "ServiceRequests_Delete" ON "ServiceRequests";
            DROP POLICY IF EXISTS "EmailLogs_Select" ON "EmailLogs";
            DROP POLICY IF EXISTS "EmailLogs_Insert" ON "EmailLogs";
            DROP POLICY IF EXISTS "EmailLogs_Update" ON "EmailLogs";
            DROP POLICY IF EXISTS "EmailLogs_Delete" ON "EmailLogs";
            """);
    }
}
