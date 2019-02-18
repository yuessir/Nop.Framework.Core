using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NopMimic.Core.Domain.Students;

namespace NopMimic.Data.Mapping.StudentMapping
{
    public class StudentMap : NopEntityTypeConfiguration<Student>
    {


        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable(nameof(Student));
            builder.HasKey(blogPost => blogPost.Id);

            builder.Property(p => p.Class).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Name).HasMaxLength(50);


            base.Configure(builder);
        }
    }
}
