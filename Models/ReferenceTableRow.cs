namespace graphqlpoc.Models;

public partial class ReferenceTableRow
{
    public ReferenceTableRow()
    {
        Columns = new HashSet<ReferenceTableColumnValue>();
    }

    //TODO: Hide from GraphQL
    public string TableName { get; set; } = null!;
    public string Key { get; set; } = null!;
    public DateTime? EffectiveStartDate { get; set; }
    public DateTime? EffectiveEndDate { get; set; }

    //TODO: Hide from GraphQL
    public virtual ReferenceTable TableNameNavigation { get; set; } = null!;
    [UseFiltering]
    public virtual ICollection<ReferenceTableColumnValue> Columns { get; set; }
}
