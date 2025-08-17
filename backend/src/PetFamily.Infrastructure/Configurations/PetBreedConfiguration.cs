using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;

namespace PetFamily.Infrastructure.Configurations;

public class PetBreedConfiguration: IEntityTypeConfiguration<PetBreed>
{
    public void Configure(EntityTypeBuilder<PetBreed> builder)
    {
        builder.ToTable("pet_breeds");
        builder.HasKey(b => b.Id)
            .HasName("pk_pet_breed_id");

        builder.Property(b => b.Name)
            .HasColumnName("name")
            .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH)
            .IsRequired();
    }
}