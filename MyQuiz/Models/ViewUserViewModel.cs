using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
namespace MyQuiz.Models
{
    public class ViewUserViewModel
    {
        public ViewUserViewModel()
        {
        }

        public ViewUserViewModel(tblQuestion tblQuestion)
        {   QuizID = tblQuestion.QuizID;
            QuestionText = tblQuestion.QuestionText;

        }

        public int QuizID { get; set; }
        public string QuestionText { get; set; }
    }
}