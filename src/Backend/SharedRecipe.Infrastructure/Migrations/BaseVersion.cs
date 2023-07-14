using FluentMigrator.Builders.Create.Table;

namespace SharedRecipe.Infrastructure.Migrations
{
    public static class BaseVersion
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax CreateTableColumn(ICreateTableWithColumnOrSchemaOrDescriptionSyntax tableColumn)
        {
            return tableColumn
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("CreationDate").AsDateTime().NotNullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable();
        }
    }
}
