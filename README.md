"# graphqlpoc" 

# Get reference table data -- should not be used use version with locale
query GetRefTable (
  $tableName: String!
) {
  table (tableName: $tableName)
  {
    name
    description
    rows {
      key
      effectiveStartDate
      effectiveEndDate
      columns {
        columnName
        locale
        value        
      }
    }
  }
}

# Get reference table data for the specified locale
query GetRefTableForLocale (
  $tableName: String!,
  $locale: String!
) {
  table (tableName: $tableName)
  {
    name
    description
    rows {
      key
      effectiveStartDate
      effectiveEndDate
      columns ( where: {
        locale : {
          eq: $locale
        }
      }) {
        columnName
        value        
      }
    }
  }
}

# Get select column data from reference table for the specified locale
query GetRefTableColumn (
  $tableName: String!,
  $columnName: String!,
  $locale: String!
) {
  table (tableName: $tableName)
  {
    name
    description
    rows {
      key
      effectiveStartDate
      effectiveEndDate
      columns ( where: {
        and: [
          {
            columnName: {
              eq: $columnName
            }
          },
          {
            locale : {
              eq: $locale
            }
          }
        ]
      }) {
        columnName
        locale
        value        
      }
    }
  }
}

# Get reference table data for the specified locale -- simplified
query GetRefTableForLocale (
  $tableName: String!,
  $locale: String!
) {
  tableForLocale (tableName: $tableName, locale: $locale)
  {
    name
    description
    rows {
      key
      effectiveStartDate
      effectiveEndDate
      columns {
        columnName
        value
        locale   
      }
    }
  }
}

# Get all rows in a reference table -- should not be used
query GetAllRows (
  $tableName: String!
) {
  rows (tableName: $tableName) {
    key
    effectiveStartDate
    effectiveEndDate
    columns {
      columnName
      locale
      value        
    }
  }
}

# Get data rows for the specified locale
query GetAllRowsForLocale (
  $tableName: String!,
  $locale: String!
) {
  rows (tableName: $tableName)
  {
    key
    effectiveStartDate
    effectiveEndDate
    columns ( where: {
      locale : {
        eq: $locale
      }
    }) {
      columnName
      value     
      locale   
    }
  }
}

# Get select column data from reference table for the specified locale
query GetAllColumnValues (
  $tableName: String!,
  $columnName: String!,
  $locale: String!
) {
  rows (tableName: $tableName)
  {
    key
    effectiveStartDate
    effectiveEndDate
    columns ( where: {
      and: [
        {
          columnName: {
            eq: $columnName
          }
        },
        {
          locale : {
            eq: $locale
          }
        }
      ]
    }) {
      columnName
      locale
      value        
    }
  }
}

# test data
{
  "tableName": "USStates",
  "columnName": "Display",
  "locale": "es-MX"
}