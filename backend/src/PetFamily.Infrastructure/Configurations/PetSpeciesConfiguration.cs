using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;

namespace PetFamily.Infrastructure.Configurations;

public class PetSpeciesConfiguration : IEntityTypeConfiguration<PetSpecies>
{
    public void Configure(EntityTypeBuilder<PetSpecies> builder)
    {
        builder.ToTable("pet_species");
        builder.HasKey(s => s.Id)
            .HasName("pk_pet_species_id");

        builder.Property(s => s.Name)
            .HasColumnName("name")
            .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH)
            .IsRequired();

        builder.HasMany(s => s.Breeds)
            .WithOne()
            .HasForeignKey("species_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}