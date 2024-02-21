using System;
using System.Linq;
using Database.Entities.Abstracts;

namespace Database.Entities.Concretes;

public class Answer : BaseEntity {

    public string Variant { get; set; }
    public int QuestionId { get; set; }
    public bool isCorrect { get; set; }
    public bool isOpenEnded { get; set; }
    public string AnswerText { get; set; }

    // Relations

    public virtual Question Question { get; set; }
}