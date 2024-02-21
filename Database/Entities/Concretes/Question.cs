using System;
using System.Linq;
using Database.Entities.Abstracts;

namespace Database.Entities.Concretes;

public class Question : BaseEntity {

    public int ModuleId { get; set; }
    public bool isOpenEnded { get; set; }
    public int QuestionPoint { get; set; }
    public int QuestionNumber { get; set; }
    public string QuestionText { get; set; }
    public byte[] QuestionImage { get; set; }

    // Relations

    public virtual Module Module { get; set; }
    public virtual ICollection<Answer> Answers { get; set;}
}