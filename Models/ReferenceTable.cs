namespace graphqlpoc.Models;

public partial class ReferenceTable
{
  public ReferenceTable()
  {
    Columns = new HashSet<ReferenceTableColumn>();
    Rows = new HashSet<ReferenceTableRow>();
  }
  
  public string Name { get; set; } = null!;
  public string? Description { get; set; }

  
  public virtual ICollection<ReferenceTableColumn> Columns { get; set; }
  public virtual ICollection<ReferenceTableRow> Rows { get; set; }
}
