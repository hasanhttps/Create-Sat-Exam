using System;
using System.Linq;
using Database.Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class ExamConfiguration : IEntityTypeConfiguration<Exam> {

    public void Configure(EntityTypeBuilder<Exam> builder) {

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.ActivationDate).IsRequired();

        // Relations

        builder.HasMany(p => p.Modules).WithOne(p => p.Exam).HasForeignKey(p => p.ExamId);
        builder.HasMany(p => p.ExamResults).WithOne(p => p.Exam).HasForeignKey(p => p.ExamId);

    }
}