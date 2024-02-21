using System;
using System.Linq;
using Database.Entities.Abstracts;

namespace Database.Entities.Concretes;

public class SatStudent : BaseEntity {

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int SchoolNumber { get; set; }

    // Relations

    public virtual ICollection<ExamResult> ExamResults { get; set; }
    public virtual ICollection<StudentsExamResults> StudentsExamResults { get; set; }

}