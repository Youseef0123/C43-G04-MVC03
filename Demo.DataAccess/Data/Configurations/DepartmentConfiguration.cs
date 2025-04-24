
using Demo.DataAccess.model.shared;

namespace Demo.DataAccess.Data.Configurations
{
    internal class DepartmentConfiguration : BaseConfiguration<Department>,IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Name).HasColumnType("varchar(20)");
            builder.Property(D => D.code).HasColumnType("varchar(20)");


            //the realtion one to many M-->Empolyee    1-->Department 

            builder.HasMany(m => m.Employees)
                   .WithOne(m => m.Department)
                   .HasForeignKey(m => m.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull);



            base.Configure(builder);
            //builder.Property(D => D.CreatedOn).HasDefaultValueSql("GETDATE()");
            //builder.Property(D => D.LastModifyOn).HasComputedColumnSql("GETDATE()");
        }
    }
}
