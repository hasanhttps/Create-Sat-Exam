using System;
using Database.Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class ExamResultConfiguration : IEntityTypeConfiguration<ExamResult> {
    public void Configure(EntityTypeBuilder<ExamResult> builder) {

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Score).IsRequired();
        builder.Property(p => p.MathScore).IsRequired();
        builder.Property(p => p.VerbalScore).IsRequired();
        builder.Property(p => p.CorrectCount).IsRequired();
        builder.Property(p => p.QuestionCount).IsRequired();

        // Relations

        builder.HasOne(p => p.Exam).WithMany(p => p.ExamResults).HasForeignKey(p => p.ExamId);
        builder.HasOne(p => p.Student).WithMany(p => p.ExamResults).HasForeignKey(p => p.StudentId);

    }
}