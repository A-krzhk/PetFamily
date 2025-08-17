using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;
using PetFamily.Domain.Entities;
using Constants = PetFamily.Domain.Shared.Constants;

namespace PetFamily.Infrastructure.Configurations;

public class VolunteerConfiguration: IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");
        builder.HasKey(v => v.Id).HasName("pk_volunteer");
        
        builder.ComplexProperty(v => v.FullName, vb =>
        {
            vb.Property(n => n.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH)
                .IsRequired();

            vb.Property(n => n.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH)
                .IsRequired();

            vb.Property(n => n.MiddleName)
                .HasColumnName("middle_name")
                .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH);
        });
            
        builder.OwnsOne(v => v.Email, vb =>
        {
            vb.Property(v => v.EmailAddress)
                .HasColumnName("email");
        });
        
        builder.Property(v => v.Description)
            .HasColumnName("description")
            .HasMaxLength(Constants.MEDIUM_TEXT_MAX_LENGTH);
            
        builder.Property(v => v.WorkExperience)
            .HasColumnName("work_experience")
            .IsRequired();
            
        builder.ComplexProperty(v => v.PhoneNumber, vb =>
        {
            vb.Property(p => p.CountryCode)
                .HasColumnName("country_code")
                .IsRequired();
            vb.Property(p => p.Number)
                .HasColumnName("number")
                .IsRequired();
        });
        
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id");
            
        builder.ComplexProperty(v => v.HelpRequisites, vb =>
        {
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

        builder.OwnsMany(v => v.SocialMedias, vb =>
        {
            vb.ToJson("social_medias");
            
            vb.Property(sm => sm.Name)
                .HasColumnName("name")
                .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH)
                .IsRequired();
                
            vb.Property(sm => sm.Url)
                .HasColumnName("url")
                .HasMaxLength(Constants.SHORT_TEXT_MAX_LENGTH)
                .IsRequired();
        });
    }
}