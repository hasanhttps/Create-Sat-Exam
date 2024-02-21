using System;
using System.Linq;
using Database.Entities.Concretes;
using System.Collections.ObjectModel;
using static Application.Models.DatabaseNamespace.Database;

namespace Application.ViewModels;

public class StudentsAnwersPageModel {

    // Private Fields

    // Binding Properties

    public ObservableCollection<StudentsAnswers> StudentsAnswers { get; set; } = new();
    public ObservableCollection<StudentsExamResults> StudentsExamResults { get; set; } = new();

    // Constructor

    public StudentsAnwersPageModel(ExamResult examResult) { 
    
        foreach(var item in DbContext.StudentsExamResults.ToList()) {
            if (item.StudentId == examResult.StudentId && item.ExamId == examResult.ExamId) {
                foreach (var studentanswer in item.StudentsAnswers) {
                    if (studentanswer.StudentsExamResultsId == item.Id)
                        StudentsAnswers.Add(studentanswer);
                }
            }
        }
    }
}