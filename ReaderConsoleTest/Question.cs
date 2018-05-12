using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderConsoleTest
{
    public class Question
    {
        private string _questionText;
        private string[] _answers;
        private int _time;

       
        public string[] Answers { get => _answers; set => _answers = value; }
        public int Time { get => _time; set => _time = value; }
        public string QuestionText { get => _questionText; set => _questionText = value; }
        public Question(string text, string[] answers, int time)
        {
            this._questionText = text;
            this._answers = answers;
            this._time = time;
        }
    }
}
