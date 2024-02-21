using System;
using System.Linq;
using Database.Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class StudentsExamResultsConfiguration : IEntityTypeConfiguration<StudentsExamResults> {

    public void Configure(EntityTypeBuilder<StudentsExamResults> builder) {

        builder.HasKey(p => p.Id);

        // Relations

        builder.HasOne(p => p.Exam).WithMany(p => p.StudentsExamResults).HasForeignKey(p => p.ExamId);
        builder.HasOne(p => p.Student).WithMany(p => p.StudentsExamResults).HasForeignKey(p => p.StudentId);
        builder.HasMany(p => p.StudentsAnswers).WithOne(p => p.StudentsExamResults).HasForeignKey(p => p.StudentsExamResultsId).OnDelete(DeleteBehavior.NoAction);
    }
}