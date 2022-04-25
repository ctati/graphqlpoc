using graphqlpoc.Models;
using Microsoft.EntityFrameworkCore;

namespace graphqlpoc.Data;

public partial class ReferenceTableContext : DbContext
{

    public ReferenceTableContext(DbContextOptions<ReferenceTableContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ReferenceTable> ReferenceTables { get; set; } = null!;
    public virtual DbSet<ReferenceTableColumn> ReferenceTableColumns { get; set; } = null!;
    public virtual DbSet<ReferenceTableColumnValue> ReferenceTableColumnValues { get; set; } = null!;
    public virtual DbSet<ReferenceTableRow> ReferenceTableRows { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ReferenceTable>(entity =>
        {
            entity.HasKey(e => e.Name)
                .HasName("PK__Referenc__733652EF39EF5B32");

            entity.ToTable("ReferenceTable");

            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("TableName");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ReferenceTableColumn>(entity =>
        {
           entity.HasKey(e => new { e.TableName, e.Name })
               .HasName("PK__Referenc__14010AA084C81C98");

           entity.ToTable("ReferenceTableColumn");

           entity.Property(e => e.TableName)
               .HasMaxLength(32)
               .IsUnicode(false);

           entity.Property(e => e.Name)
               .HasMaxLength(100)
               .IsUnicode(false);

           entity.HasOne(d => d.TableNameNavigation)
               .WithMany(p => p.Columns)
               .HasForeignKey(d => d.TableName)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_ReferenceTableColumn_ReferenceTable");
        });

        modelBuilder.Entity<ReferenceTableColumnValue>(entity =>
        {
            entity.HasKey(e => new { e.TableName, e.Key, e.ColumnName, e.Locale })
                .HasName("PK__Referenc__2417F9814E4B926C");

            entity.ToTable("ReferenceTableColumnValue");

            entity.Property(e => e.TableName)
                .HasMaxLength(32)
                .IsUnicode(false);

            entity.Property(e => e.Key)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.Property(e => e.ColumnName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Locale)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.EffectiveEndDate).HasColumnType("datetime");

            entity.Property(e => e.EffectiveStartDate).HasColumnType("datetime");

            entity.Property(e => e.Value)
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.HasOne(d => d.ReferenceTableColumn)
               .WithMany(p => p.Locales)
               .HasForeignKey(d => new { d.TableName, d.ColumnName })
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_ReferenceTableColumnValue_ReferenceTableColumn");

            entity.HasOne(d => d.ReferenceTableRow)
                .WithMany(p => p.Columns)
                .HasForeignKey(d => new { d.TableName, d.Key })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReferenceTableColumnValue_ReferenceTableRow");
        });

        modelBuilder.Entity<ReferenceTableRow>(entity =>
        {
            entity.HasKey(e => new { e.TableName, e.Key })
                .HasName("PK__Referenc__EF77B2C749A74400");

            entity.ToTable("ReferenceTableRow");

            entity.Property(e => e.TableName)
                .HasMaxLength(32)
                .IsUnicode(false);

            entity.Property(e => e.Key)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.Property(e => e.EffectiveEndDate).HasColumnType("datetime");

            entity.Property(e => e.EffectiveStartDate).HasColumnType("datetime");

            entity.HasOne(d => d.TableNameNavigation)
                .WithMany(p => p.Rows)
                .HasForeignKey(d => d.TableName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReferenceTableRow_ReferenceTable");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
