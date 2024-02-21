using System;
using System.Linq;
using Database.Entities.Abstracts;

namespace Database.Entities.Concretes;

public class Module : BaseEntity {

    public int? Time { get; set; }
    public int ExamId { get; set; }
    public string Subject { get; set; }
    public int? ModuleNumber { get; set; }

    // Relations

    public virtual Exam Exam { get; set; }  
    public virtual ICollection<Question> Questions { get; set; }
    public virtual ICollection<StudentsAnswers> StudentsAnswers { get; set; }
}