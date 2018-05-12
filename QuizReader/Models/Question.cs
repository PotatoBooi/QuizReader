using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReader.Models
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Answers { get; set; }
        public int Time { get; set; }
        public Dictionary<string, bool> CorrectAnswers;

        public Question()
        {
            CorrectAnswers = new Dictionary<string, bool>();

        }
        public Question(string text, List<string> answers, Dictionary<string,bool> correnct, int time)
        {
            Text = text;
            Answers = answers;
            CorrectAnswers = correnct;
            Time = time;

        }
    }
}
