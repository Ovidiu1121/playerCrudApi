using FluentMigrator;

namespace PlayerCrudApi.Data.Migrations
{
    [Migration(4242024)]
    public class CreateSchema : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Create.Table("player")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString(128).NotNullable()
                .WithColumn("number").AsInt32().NotNullable()
                .WithColumn("value").AsInt32().NotNullable();
        }
    }
}
