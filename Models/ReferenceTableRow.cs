namespace graphqlpoc.Models;

public partial class ReferenceTableRow
{
    public ReferenceTableRow()
    {
        Columns = new HashSet<ReferenceTableColumnValue>();
    }

    public string TableName { get; set; } = null!;
    public string Key { get; set; } = null!;
    public DateTime? EffectiveStartDate { get; set; }
    public DateTime? EffectiveEndDate { get; set; }

    public virtual ReferenceTable TableNameNavigation { get; set; } = null!;
    public virtual ICollection<ReferenceTableColumnValue> Columns { get; set; }
}
