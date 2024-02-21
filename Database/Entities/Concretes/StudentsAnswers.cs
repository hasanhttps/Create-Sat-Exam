using System;
using Database.Entities.Abstracts;

namespace Database.Entities.Concretes;

public class StudentsAnswers : BaseEntity {

    public int Id { get; set; }
    public float Point { get; set; }
    public int ModuleId { get; set; }
    public string isCorrect { get; set; }
    public int QuestionNumber { get; set; }
    public string StudentAnswer { get; set; }
    public string CorrectAnswer { get; set; }
    public int StudentsExamResultsId { get; set; }

    // Relations

    public virtual Module Module { get; set; }
    public virtual StudentsExamResults StudentsExamResults { get; set; }
}
