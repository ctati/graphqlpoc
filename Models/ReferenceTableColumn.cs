namespace graphqlpoc.Models;

public partial class ReferenceTableColumn
{
    public ReferenceTableColumn()
    {
        Locales = new HashSet<ReferenceTableColumnValue>();
    }

    public string TableName { get; set; } = null!;
    public string Name { get; set; } = null!;

    public virtual ReferenceTable TableNameNavigation { get; set; } = null!;
    public virtual ICollection<ReferenceTableColumnValue> Locales { get; set; }
}
