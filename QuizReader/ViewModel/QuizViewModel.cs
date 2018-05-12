using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using QuizReader.Models;
using QuizReader.Services;

namespace QuizReader.ViewModel
{
    public class QuizViewModel: ViewModelBase
    {


        private static string _whenQuizButtonText = "Next Question";
        private static string _whenExitButtonText = "Finish";
        

        private readonly IFrameNavigationService _navigationService;
        private readonly DataReader dataReader;
        private List<Question> questions;
        private Question _selectedQuestion;
        private string _nextQButtonText = _whenQuizButtonText;
        private string _timer;
        private int questionIndex = 0;

        public string QuestionCounter { get => (questionIndex + 1).ToString() + ". " + SelectedQuestion.Text; }
        public RelayCommand NextQuestionCommmand { get; private set; }

        public QuizViewModel(IFrameNavigationService navigationService)
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

            }
            else
            {
                // Code runs "for real"
            }
            
            this._navigationService = navigationService;
            this.dataReader = new DataReader(@"C:\Users\barce\Desktop\QuestionCreator\QuestionCreator\bin\Debug\test1.xml");

            loadQuestions();
            initQuestion();
            this.NextQuestionCommmand = new RelayCommand(onNextQuestion);


        }

        public Question SelectedQuestion { get => _selectedQuestion; set => Set(()=>SelectedQuestion,ref _selectedQuestion,value); }
        public string Timer { get => "Time: " + _selectedQuestion.Time.ToString() ; set => Set(() => Timer, ref _timer, value); }
        public string NextQButtonText
        {
            get => _nextQButtonText;
            set
            {
               
                Set(() => NextQButtonText, ref _nextQButtonText, value);
               
            }
        }

        private void onNextQuestion()
        {
            if (questionIndex == questions.Count - 2)
            {
                _nextQButtonText = _whenExitButtonText;
            }

            if (questionIndex < questions.Count - 1)
            {
                questionIndex++;
                initQuestion();

            }
            

        }
        private void initQuestion()
        {
            _selectedQuestion = questions[questionIndex];
            RaisePropertyChanged(() => SelectedQuestion);
            RaisePropertyChanged(() => Timer);
            RaisePropertyChanged(() => NextQButtonText);
        }
        private void loadQuestions()
        {
            this.questions = new List<Question>();
            dataReader.InputList.ForEach(data =>
            {
                questions.Add(
                    new Question(data.QuestionText, data.GetAnswers(), data.GetAnswersDictionary(), data.Time));
            }
            );
        }
    }
}
