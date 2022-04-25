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

  
  //TODO: Hide from GraphQL
  public virtual ICollection<ReferenceTableColumn> Columns { get; set; }
  [UseFiltering]
  public virtual ICollection<ReferenceTableRow> Rows { get; set; }
}
