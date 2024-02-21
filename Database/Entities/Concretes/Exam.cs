using System;
using System.Linq;
using Database.Entities.Abstracts;

namespace Database.Entities.Concretes;

public class Exam : BaseEntity {
    
    public string Name { get; set; }
    public DateTime ActivationDate { get; set; }
    public int ParticipatedStudentCount { get; set; }

    // Relations

    public virtual ICollection<Module> Modules { get; set; }
    public virtual ICollection<ExamResult> ExamResults { get; set; }
    public virtual ICollection<StudentsExamResults> StudentsExamResults { get; set; }

}