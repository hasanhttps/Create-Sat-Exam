using System;
using Database.Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer> {

    public void Configure(EntityTypeBuilder<Answer> builder) {

        builder.HasKey(p => p.Id);
        builder.Property(p => p.isCorrect).IsRequired();
        builder.Property(p => p.AnswerText).IsRequired();
        builder.Property(p => p.isOpenEnded).IsRequired();
        builder.Property(p => p.Variant).IsRequired(false);

        // Relations

        builder.HasOne(p => p.Question).WithMany(p => p.Answers).HasForeignKey(p => p.QuestionId);

    }
}