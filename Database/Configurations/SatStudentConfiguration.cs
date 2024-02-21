using System;
using System.Linq;
using Database.Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class SatStudentConfiguration : IEntityTypeConfiguration<SatStudent> {

    public void Configure(EntityTypeBuilder<SatStudent> builder) {

        builder.HasKey(p => p.Id);
        builder.Property(p => p.FirstName).IsRequired();
        builder.Property(p => p.LastName).IsRequired();
        builder.Property(p => p.SchoolNumber).IsRequired();

        // Relations

        builder.HasMany(p => p.ExamResults).WithOne(p => p.Student).HasForeignKey(p => p.StudentId);
    }
}