using FluentMigrator;

namespace PlayerCrudApi.Data.Migrations
{
    [Migration(423202411)]
    public class TestMigrate : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Execute.Script(@"./Data/Scripts/data.sql");
        }
    }
}
