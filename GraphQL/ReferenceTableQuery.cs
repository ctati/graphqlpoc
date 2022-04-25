using graphqlpoc.Data;
using graphqlpoc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace graphqlpoc.GraphQL;

/// <summary>
/// 
/// </summary>
public class ReferenceTableQuery
{
    private const string DefaultLocale = "en-US";
    private const string DefaultColumnName = "Display";

    [UseSingleOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ReferenceTable> GetTable([Service] ReferenceTableContext context,
        string tableName)
    {
        return context.ReferenceTables.Where(rt => rt.Name == tableName);
    }
            
    public IQueryable<ReferenceTable> GetTableForLocale([Service] ReferenceTableContext context,
        string tableName,
        string locale = DefaultLocale,
        DateTime? effectiveDate = null)
    {
        // filter by table name
        var query = context.ReferenceTables.Where(t => t.Name == tableName);
        
        // filter by effective date if one was provided
        IIncludableQueryable<ReferenceTable, IEnumerable<ReferenceTableRow>> rowQuery = query.Include(t => t.Rows);
        if (effectiveDate != null) {
            rowQuery = query.Include(t => t.Rows.Where(r =>
                            (r.EffectiveStartDate == null || r.EffectiveStartDate.Value <= effectiveDate.Value) &&
                            (r.EffectiveEndDate == null || r.EffectiveEndDate.Value >= effectiveDate.Value)
                        ));
        };
        
        // filter by locale
        var columnQuery = rowQuery.ThenInclude(r => r.Columns.Where(c => c.Locale == locale));
        
        return columnQuery;
    }


    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ReferenceTableRow> GetRows([Service] ReferenceTableContext context,
            string tableName,
            DateTime? effectiveDate = null)
    {
        // filter by table name
        var query = context.ReferenceTableRows.Where(row => row.TableName == tableName);

         // filter by effective date if one was provided
        if (effectiveDate != null) {
            query = query.Where(r => 
                            (r.EffectiveStartDate == null || r.EffectiveStartDate.Value <= effectiveDate.Value) &&
                            (r.EffectiveEndDate == null || r.EffectiveEndDate.Value >= effectiveDate.Value)
                        );
        };

        return query;
  }

    public IQueryable<ReferenceTableRow> GetRowsByKey([Service] ReferenceTableContext context,
            string tableName,
            string[] keys,
            string locale = DefaultLocale,
            DateTime? effectiveDate = null)
    {
        // filter by table name and keys
        var query = context.ReferenceTableRows.Where(row => (row.TableName == tableName) &&
                                                            (keys.Contains(row.Key)));
        
        // filter by effective date if one was provided
        if (effectiveDate != null) {
            query = query.Where(r => 
                            (r.EffectiveStartDate == null || r.EffectiveStartDate.Value <= effectiveDate.Value) &&
                            (r.EffectiveEndDate == null || r.EffectiveEndDate.Value >= effectiveDate.Value)
                        );
        };

        // filter by locale
        var columnQuery = query.Include(r => r.Columns.Where(c => c.Locale == locale));

        return columnQuery;
    }

    public IQueryable<ReferenceTableRow> GetRowsByColumnValue([Service] ReferenceTableContext context,
            string tableName,
            string[] columnValues,
            string columnName = DefaultColumnName,
            string locale = DefaultLocale,
            DateTime? effectiveDate = null)
    {
        // filter by table name
        var query = context.ReferenceTableRows.Where(row => row.TableName == tableName);
        
        // filter by effective date if one was provided
        if (effectiveDate != null) {
            query = query.Where(r => 
                            (r.EffectiveStartDate == null || r.EffectiveStartDate.Value <= effectiveDate.Value) &&
                            (r.EffectiveEndDate == null || r.EffectiveEndDate.Value >= effectiveDate.Value)
                        );
        };

        // filter by locale
        var columnQuery = query.Include(r => r.Columns.Where(c => c.Locale == locale));

        // filter by column name and value
        if (columnValues.Length > 0)
        {
            columnQuery = query.Include(r => r.Columns.Where(c => 
                                                        (c.Locale == locale) &&
                                                        (c.ColumnName == columnName) &&
                                                        (columnValues.Contains(c.Value))));
        }

        return columnQuery;
    }


    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ReferenceTableColumnValue> GetColumns([Service] ReferenceTableContext context,
        string tableName,
        string[] keys)
    {
        // filter by table name
        var query = context.ReferenceTableColumnValues.Where(c => (c.TableName == tableName) &&
                                                                    (keys.Contains(c.Key)));

        return query;
    }

    public IQueryable<ReferenceTableColumnValue> GetColumnsForLocale([Service] ReferenceTableContext context,
        string tableName,
        string[] keys,
        string locale = DefaultLocale,
        DateTime? effectiveDate = null)
    {

        var query = context.ReferenceTableColumnValues.Where(c => (c.TableName == tableName) &&
                                                                    (c.Locale == locale));

        if (keys.Length > 0)
        {
            query = query.Where(c => keys.Contains(c.Key));
        }

        if (effectiveDate is not null)
        {
            query = query.Where(c =>
                        (c.ReferenceTableRow.EffectiveStartDate == null || c.ReferenceTableRow.EffectiveStartDate.Value <= effectiveDate.Value) &&
                        (c.ReferenceTableRow.EffectiveEndDate == null || c.ReferenceTableRow.EffectiveEndDate.Value >= effectiveDate.Value)
                    );
        }
        return query;
    }

    public IQueryable<ReferenceTableColumnValue> GetColumn([Service] ReferenceTableContext context,
        string tableName,
        string[] keys,
        string columnName = DefaultColumnName,
        string locale = DefaultLocale,
        DateTime? effectiveDate = null)
    {

        var query = context.ReferenceTableColumnValues.Where(c => (c.TableName == tableName) && 
                                                                    (c.ColumnName == columnName) &&
                                                                    (c.Locale == locale));

        if (keys.Length > 0)
        {
            query = query.Where(c => keys.Contains(c.Key));
        }

        if (effectiveDate is not null)
        {
            query = query.Where(c =>
                        (c.ReferenceTableRow.EffectiveStartDate == null || c.ReferenceTableRow.EffectiveStartDate.Value <= effectiveDate.Value) &&
                        (c.ReferenceTableRow.EffectiveEndDate == null || c.ReferenceTableRow.EffectiveEndDate.Value >= effectiveDate.Value)
                    );
        }
        return query;
    }
}
