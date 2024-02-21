using System;
using Database.Entities.Abstracts;

namespace Database.Entities.Concretes;

public class StudentsExamResults : BaseEntity {

    public int Id { get; set; }
    public int ExamId { get; set; }
    public int StudentId { get; set; }

    // Relations

    public virtual Exam Exam { get; set; }
    public virtual SatStudent Student { get; set; }
    public virtual ICollection<StudentsAnswers> StudentsAnswers { get; set; }
}
