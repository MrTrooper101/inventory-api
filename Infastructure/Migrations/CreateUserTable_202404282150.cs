using FluentMigrator;

namespace inventory_api.Infastructure.Migrations
{
    [Migration(202404282150)]
    public class CreateUserTable_202404282150 : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("FirstName").AsString(100).NotNullable()
                .WithColumn("MiddleName").AsString(100).Nullable()
                .WithColumn("LastName").AsString(100).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable().Unique()
                .WithColumn("ContactNumber").AsString(50).Nullable()
                .WithColumn("CompanyName").AsString(200).NotNullable()
                .WithColumn("CompanyAddress").AsString(500).Nullable()
                .WithColumn("PasswordHash").AsString(500).NotNullable()
                .WithColumn("IsEmailConfirmed").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("EmailConfirmationToken").AsString(500).Nullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("UpdatedAt").AsDateTime().Nullable()
                .WithColumn("DeletedAt").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}
