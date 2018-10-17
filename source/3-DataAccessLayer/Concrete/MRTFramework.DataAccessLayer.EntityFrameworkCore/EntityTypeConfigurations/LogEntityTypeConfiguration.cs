using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MRTFramework.Model.BaseModels.Concrete;

namespace MRTFramework.DataAccessLayer.EntityFrameworkCore.EntityTypeConfigurations
{
    public class LogEntityTypeConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Logs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Date).HasMaxLength(100);
            builder.Property(x => x.Level).HasMaxLength(50);
            builder.Property(x => x.Message);
            builder.Property(x => x.MachineName);
            builder.Property(x => x.UserName);
            builder.Property(x => x.Thread).HasMaxLength(100);
            builder.Property(x => x.Exception);
        }
    }
}
