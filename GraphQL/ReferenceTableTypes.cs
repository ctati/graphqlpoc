using graphqlpoc.Models;

namespace graphqlpoc.GraphQL;

public class ReferenceTableType: ObjectType<ReferenceTable>
{
    protected override void Configure(IObjectTypeDescriptor<ReferenceTable> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(f => f.Columns);
        descriptor.Field(f => f.Rows).UseFiltering();
    }
}

public class ReferenceTableRowType: ObjectType<ReferenceTableRow>
{
    protected override void Configure(IObjectTypeDescriptor<ReferenceTableRow> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(f => f.TableName);
        descriptor.Ignore(f => f.TableNameNavigation);
        descriptor.Field(f => f.Columns).UseFiltering();
    }
}

public class ReferenceTableColumnType: ObjectType<ReferenceTableColumn>
{
    protected override void Configure(IObjectTypeDescriptor<ReferenceTableColumn> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(f => f.TableName);
        descriptor.Ignore(f => f.TableNameNavigation);
        descriptor.Field(f => f.Locales).Name("values");
    }
}

public class ReferenceTableColumnValueType: ObjectType<ReferenceTableColumnValue>
{
    protected override void Configure(IObjectTypeDescriptor<ReferenceTableColumnValue> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Ignore(f => f.TableName);
        descriptor.Ignore(f => f.EffectiveStartDate);
        descriptor.Ignore(f => f.EffectiveEndDate);
        descriptor.Ignore(f => f.ReferenceTableColumn);
        descriptor.Ignore(f => f.ReferenceTableRow);
    }
}