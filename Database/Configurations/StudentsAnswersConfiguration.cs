using System;
using System.Linq;
using Database.Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class StudentsAnswersConfiguration : IEntityTypeConfiguration<StudentsAnswers> {

    public void Configure(EntityTypeBuilder<StudentsAnswers> builder) {

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Point).IsRequired();
        builder.Property(p => p.isCorrect).IsRequired();
        builder.Property(p => p.StudentAnswer).IsRequired();
        builder.Property(p => p.CorrectAnswer).IsRequired();
        builder.Property(p => p.QuestionNumber).IsRequired();

        // Relations

        builder.HasOne(p => p.Module).WithMany(p => p.StudentsAnswers).HasForeignKey(p => p.ModuleId);
        builder.HasOne(p => p.StudentsExamResults).WithMany(p => p.StudentsAnswers).HasForeignKey(p => p.StudentsExamResultsId).OnDelete(DeleteBehavior.NoAction);
    }
}