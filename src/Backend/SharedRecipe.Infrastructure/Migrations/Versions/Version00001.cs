using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace SharedRecipe.Infrastructure.Migrations.Versions
{
    [Migration((long)EnumVersion.CreateUserTable, "Create user table")]
    public class Version00001 : Migration
    {
        public override void Down() { }

        public override void Up()
        {
            ICreateTableColumnOptionOrWithColumnSyntax tableColumn = BaseVersion.CreateTableColumn(Create.Table("Users"));

            tableColumn
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Email").AsString(100).NotNullable()
                .WithColumn("Password").AsString(2000).NotNullable()
                .WithColumn("Phone").AsString(16).NotNullable();
        }
    }
}
