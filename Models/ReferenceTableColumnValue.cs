namespace graphqlpoc.Models;

public partial class ReferenceTableColumnValue
{
    public string TableName { get; set; } = null!;
    public string Key { get; set; } = null!;
    public string ColumnName { get; set; } = null!;
    public string Locale { get; set; } = null!;
    public string? Value { get; set; }
    public DateTime? EffectiveStartDate { get; set; }
    public DateTime? EffectiveEndDate { get; set; }

    public virtual ReferenceTableColumn ReferenceTableColumn { get; set; } = null!;
    public virtual ReferenceTableRow ReferenceTableRow { get; set; } = null!;
}
