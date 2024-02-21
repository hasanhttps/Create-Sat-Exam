using System;
using System.Linq;
using Database.Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class ModuleConfiguration : IEntityTypeConfiguration<Module> {

    public void Configure(EntityTypeBuilder<Module> builder) {

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Subject).IsRequired();
        builder.Property(p => p.ModuleNumber).IsRequired();
        builder.Property(p => p.Time).IsRequired();

        // Relations

        builder.HasOne(p => p.Exam).WithMany(p => p.Modules).HasForeignKey(p => p.ExamId);
        builder.HasMany(p => p.Questions).WithOne(p => p.Module).HasForeignKey(p => p.ModuleId);
        builder.HasMany(p => p.StudentsAnswers).WithOne(p => p.Module).HasForeignKey(p => p.ModuleId);
    }
}