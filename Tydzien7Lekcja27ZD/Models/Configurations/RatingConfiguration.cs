using System.Data.Entity.ModelConfiguration;
using Tydzien7Lekcja27ZD.Models.Domains;

namespace Tydzien7Lekcja27ZD.Models.Configurations
{
    public class RatingConfiguration : EntityTypeConfiguration<Rating>
    {
        public RatingConfiguration()
        {
            ToTable("dbo.Ratings");

            HasKey(x => x.Id);
        }
    }
}
