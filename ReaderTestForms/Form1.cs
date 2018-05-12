using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ReaderTestForms
{
    


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //XmlSerializer serializer = new XmlSerializer(typeof(Question));
            
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var path = dialog.FileName;
                XDocument document = new XDocument();
                document = XDocument.Load(path);
                List<object> list = new List<object>();
                var input = document.Descendants("Question");
                foreach (var item in input)
                {
                    list.Add(new Question
                    {
                        QuestionText = item.Element("QuestionText").Value,
                        AnswerA = item.Element("AnswerA").Value,
                        AnswerB = item.Element("AnswerB").Value,
                        AnswerC = item.Element("AnswerC").Value,
                        AnswerD = item.Element("AnswerD").Value,
                        Time = int.Parse(item.Element("Time").Value)

                    });
                }


                dataGridView1.DataSource = list;
            }
            
        }
        public class Question
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
        }
    }
}
