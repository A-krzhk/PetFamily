using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Entities;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using System.Text.Json; 
using System.Text.Json.Serialization;

namespace PetFamily.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");
        builder
            .HasKey(p => p.Id)
            .HasName("pk_id");
            
        builder
            .Property(p => p.Name)
            .HasColumnName("name")
            .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH)
            .IsRequired();
            
        builder
            .Property(p => p.Description)
            .HasColumnName("description")
            .HasMaxLength(Constants.MEDIUM_TEXT_MAX_LENGTH);
        
        builder.OwnsOne(p => p.PetType, pb =>
        {
            pb.ToJson("pet_type");
            pb.Property(type => type.SpeciesId)
                .HasColumnName("species_id")
                .IsRequired();
            pb.Property(type => type.BreedId)
                .HasColumnName("breed_id")
                .IsRequired();
        });
        
        builder
            .Property(p => p.Color)
            .HasColumnName("color")
            .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH)
            .IsRequired();

        builder.Property(p => p.Castrated)
            .HasColumnName("castrated")
            .IsRequired();

        builder.Property(p => p.BirthDate)
            .HasColumnName("birth_date")
            .IsRequired();

        builder.Property(p => p.Created)
            .HasColumnName("created")
            .IsRequired();

        builder.Property(p => p.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();
        
        builder.OwnsOne(p => p.Address, ab =>
        {
            ab.ToJson("address");
            ab.Property(a => a.City)
                .HasColumnName("city")
                .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH)
                .IsRequired();

            ab.Property(a => a.Street)
                .HasColumnName("street")
                .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH)
                .IsRequired();

            ab.Property(a => a.Region)
                .HasColumnName("region")
                .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH)
                .IsRequired();
        });
        
        builder.OwnsOne(p => p.BodyParams, bp =>
        {
            bp.ToJson("body_params");
            bp.Property(p => p.Weight)
                .HasColumnName("weight")
                .IsRequired();
            bp.Property(p => p.Height)
                .HasColumnName("height")
                .IsRequired();
        });

        builder.OwnsOne(p => p.VolunteerPhoneNumber, vb =>
        {
            vb.ToJson("volunteer_phone_number");
            vb.Property(p => p.Number)
                .HasColumnName("number")
                .IsRequired();
        });
        
        builder.OwnsOne(v => v.HelpRequisites, vb =>
        {
            vb.ToJson("help_requisites");
            
            vb.Property(p => p.Acc)
                .HasColumnName("acc")
                .IsRequired()
                .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH);
                
            vb.Property(p => p.CorrAcc)
                .HasColumnName("corr_acc")
                .IsRequired()
                .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH);
                
            vb.Property(p => p.RCBIC)
                .HasColumnName("rcbic")
                .IsRequired()
                .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH);
                
            vb.Property(p => p.RCRN)
                .HasColumnName("rcrn")
                .IsRequired()
                .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH);
                
            vb.Property(p => p.Recipient)
                .HasColumnName("recipient")
                .IsRequired()
                .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH);
                
            vb.Property(p => p.TIN)
                .HasColumnName("tin")
                .IsRequired()
                .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH);
                
            vb.Property(p => p.TRRC)
                .HasColumnName("trrc")
                .IsRequired()
                .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH);
        });
        
        builder.OwnsMany(p => p.Vaccines, vb =>
        {
            vb.ToJson("vaccines");
            vb.Property(v => v.Name)
                .HasColumnName("name")
                .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH)
                .IsRequired();
            vb.Property(v => v.Date)
                .HasColumnName("date")
                .IsRequired();
            vb.Property(v => v.Description)
                .HasColumnName("description")
                .HasMaxLength(Constants.MEDIUM_TEXT_MAX_LENGTH)
                .IsRequired();
        });
        
        var jsonOptions = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() } 
        };
        
        builder.Property(p => p.HealthStates)
            .HasColumnName("health_states")
            .HasConversion(
                v => JsonSerializer.Serialize(v, jsonOptions),
                v => JsonSerializer.Deserialize<IReadOnlyList<PetHealthState>>(v, jsonOptions) ?? new List<PetHealthState>());
    }
}