namespace graphqlpoc.Models;

public partial class ReferenceTableColumnValue
{
    //TODO: Hide from GraphQL
    public string TableName { get; set; } = null!;
    public string Key { get; set; } = null!;
    public string ColumnName { get; set; } = null!;
    public string Locale { get; set; } = null!;
    public string? Value { get; set; }
    //TODO: Hide from GraphQL
    public DateTime? EffectiveStartDate { get; set; }
    //TODO: Hide from GraphQL
    public DateTime? EffectiveEndDate { get; set; }

    //TODO: Hide from GraphQL
    public virtual ReferenceTableColumn ReferenceTableColumn { get; set; } = null!;
    //TODO: Hide from GraphQL
    public virtual ReferenceTableRow ReferenceTableRow { get; set; } = null!;
}
