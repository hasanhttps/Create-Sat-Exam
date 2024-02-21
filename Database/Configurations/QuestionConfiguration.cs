using System;
using Database.Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question> {

    public void Configure(EntityTypeBuilder<Question> builder) {

        builder.HasKey(p => p.Id);
        builder.Property(p => p.isOpenEnded).IsRequired();
        builder.Property(p => p.QuestionText).IsRequired();
        builder.Property(p => p.QuestionPoint).IsRequired();
        builder.Property(p => p.QuestionNumber).IsRequired();
        builder.Property(p => p.QuestionImage).IsRequired(false);

        // Relations

        builder.HasOne(p => p.Module).WithMany(p => p.Questions).HasForeignKey(p => p.ModuleId);
        builder.HasMany(p => p.Answers).WithOne(p => p.Question).HasForeignKey(p => p.QuestionId);

    }
}