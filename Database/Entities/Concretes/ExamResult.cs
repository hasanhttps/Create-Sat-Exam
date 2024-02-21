using System;
using System.Linq;
using Database.Entities.Abstracts;

namespace Database.Entities.Concretes;

public class ExamResult : BaseEntity {

    public int ExamId { get; set; }
    public float Score { get; set; }
    public int StudentId { get; set; }
    public float MathScore { get; set; }
    public int CorrectCount { get; set; }
    public float VerbalScore { get; set; }
    public int QuestionCount { get; set; }

    // Relations

    public virtual Exam Exam { get; set; }
    public virtual SatStudent Student { get; set; }
}