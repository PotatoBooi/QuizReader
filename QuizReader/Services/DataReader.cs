using GalaSoft.MvvmLight.Messaging;
using QuizReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuizReader.Services
{
    public class DataReader
    {
        private string _path;

        private List<InputData> _input;
        private XDocument inputDocument;

        public string Path { get => _path; set => _path = value; }
        public List<InputData> InputList { get => _input; set => _input = value; }



        public DataReader(string path)
        {
            _path = path;
            initialize();
            GetData();




        }
        private void initialize()
        {
            this._input = new List<InputData>();
            this.inputDocument = new XDocument();

        }
        private void GetData()
        {
            inputDocument = XDocument.Load(_path);


            var data = inputDocument.Descendants("Question");
            if (data.Any())
            {

                foreach (var item in data)
                {
                    _input.Add(new InputData
                    {
                        QuestionText = item.Element("QuestionText").Value,
                        AnswerA = item.Element("AnswerA").Value,
                        AnswerB = item.Element("AnswerB").Value,
                        AnswerC = item.Element("AnswerC").Value,
                        AnswerD = item.Element("AnswerD").Value,
                        CorrectA = item.Element("CorrectA").Value,
                        CorrectB = item.Element("CorrectB").Value,
                        CorrectC = item.Element("CorrectC").Value,
                        CorrectD = item.Element("CorrectD").Value,
                        Time = int.Parse(item.Element("Time").Value)

                    });
                }
            }
            else
            {
                throw new Exception("Selected file has no question data!");
            }



        }

    }
}
