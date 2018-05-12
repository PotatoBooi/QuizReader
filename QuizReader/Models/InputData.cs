using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReader.Models
{
    public class InputData
    {
        public string QuestionText { get; set; }
        public string AnswerA { get; set; }

        public string AnswerB { get; set; }

        public string AnswerC { get; set; }

        public string AnswerD { get; set; }
        public int Time { get; set; }
        public string CorrectA { get; set; }
        public string CorrectB { get; set; }
        public string CorrectC { get; set; }
        public string CorrectD { get; set; }
        



        public List<string> GetAnswers()
        {
            var list = new List<string>();
            list.Add(AnswerA);
            list.Add(AnswerB);
            list.Add(AnswerC);
            list.Add(AnswerD);

            return list;
        }
        private List<bool> GetCorrectAnswers()
        {
            var list = new List<string>();
            list.Add(CorrectA);
            list.Add(CorrectB);
            list.Add(CorrectC);
            list.Add(CorrectD);

            var boolList = new List<bool>();
            list.ForEach(thingy =>
            {
                if(thingy == "Checked")
                {
                    boolList.Add(true);
                }
                else if(thingy == "Unchecked")
                {
                    boolList.Add(false);
                }

            }
            );

            return boolList;
        }

        public Dictionary<string, bool> GetAnswersDictionary()
        {
            var dict = new Dictionary<string, bool>();
            var answers = GetAnswers();
            var bools = GetCorrectAnswers();
            for (int i = 0; i < answers.Count; i++)
            {
                dict.Add(answers[i], bools[i]);
            }
            return dict;
        }

    }
}
