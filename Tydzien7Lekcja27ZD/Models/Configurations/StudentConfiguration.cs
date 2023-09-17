using System.Data.Entity.ModelConfiguration;
using Tydzien7Lekcja27ZD.Models.Domains;

namespace Tydzien7Lekcja27ZD.Models.Configurations
{
    public class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            ToTable("dbo.Students");

            HasKey(x => x.Id);

            Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
