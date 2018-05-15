using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using QuizReader.Models;
using QuizReader.Services;

namespace QuizReader.ViewModel
{
    public class QuizViewModel : ViewModelBase
    {

        #region Private Fields
        private static string _whenQuizButtonText = "Next Question";
        private static string _whenExitButtonText = "Finish";
        private readonly IFrameNavigationService _navigationService;
        private readonly DataReader _dataReader;
        private List<Question> _questions;
        private Question _selectedQuestion;
        private string _nextQButtonText = _whenQuizButtonText;
        private string _timer;
        private int _questionIndex = 0;
        private int _currentTime;
        private DispatcherTimer _dispatcherTimer;
        private bool[] _checkedAnswers;
        private int _score = 0;
        private int _maxScore = 0;
        #endregion

        #region Commands
        public RelayCommand NextQuestionCommmand { get; private set; }
        #endregion
        
        #region Ctor
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
            this._dataReader = (DataReader)navigationService.Parameter;

            LoadQuestions();
            InitQuestion();
            SetupTimer();
            NextQuestionCommmand = new RelayCommand(OnNextQuestion);


        }
        #endregion

        #region Properties
        public string QuestionCounter { get => $"{(_questionIndex + 1)}/{_questions.Count}";  }
        public Question SelectedQuestion { get => _selectedQuestion; set => Set(() => SelectedQuestion, ref _selectedQuestion, value); }
        public string Timer { get => "Time: " + _currentTime.ToString(); set => Set(() => Timer, ref _timer, value); }
        public string NextQButtonText
        {
            get => _nextQButtonText;
            set
            {


                Set(() => NextQButtonText, ref _nextQButtonText, value);

            }
        }

        public bool[] CheckedAnswers { get => _checkedAnswers; set => Set(() => CheckedAnswers, ref _checkedAnswers, value); }
        #endregion

        #region Private Methods
        private void OnNextQuestion()
        {
            if (!IsEnd())
            {
                LoadNextQuestion();
            }
            else
            {
                FinishQuiz();
               
            }

        }
        private void InitQuestion()
        {

            ClearChecked();
            _selectedQuestion = _questions[_questionIndex];
            _currentTime = _selectedQuestion.Time;

            RaisePropertyChanged(() => SelectedQuestion);
            RaisePropertyChanged(() => Timer);
            RaisePropertyChanged(() => QuestionCounter);

        }
        private void LoadNextQuestion()
        {
            CheckScore();

            if (_questionIndex < _questions.Count - 1)
            {

                _questionIndex++;

                InitQuestion();
            }


            if (IsEnd())
            {
                _nextQButtonText = _whenExitButtonText;
                RaisePropertyChanged(() => NextQButtonText);
            }


        }
        private void LoadQuestions()
        {
            this._questions = new List<Question>();
            _dataReader.InputList.ForEach(data =>
            {
                _questions.Add(
                    new Question(data.QuestionText, data.GetAnswers(), data.GetAnswersDictionary(), data.Time));
            }
            );
            if (_questions.Count == 1)
                _nextQButtonText = _whenExitButtonText;

            _questions.ForEach(tmp =>
            {
                tmp.CorrectAnswers.Values.ToList().ForEach(val =>
                {
                    if(val == true)
                    {
                        _maxScore++;
                    }
                });
            });

        }
        private void SetupTimer()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1.0);
            _dispatcherTimer.Tick += TimerTick;
            _dispatcherTimer.Start();

        }
        private void TimerTick(object sender, EventArgs e)
        {
            if (_currentTime > 0)
                _currentTime--;
            else if(IsEnd() && _currentTime == 0)
            {
                FinishQuiz();
            }
            else
                LoadNextQuestion();

            RaisePropertyChanged(() => Timer);
        }

        private bool IsEnd()
        {
            return _selectedQuestion == _questions[_questions.Count - 1];
        }

  
        private void ClearChecked()
        {
            _checkedAnswers = new bool[4];
            RaisePropertyChanged(() => CheckedAnswers);
        }
        private void CheckScore()
        {
            for (int i = 0; i < _checkedAnswers.Length; i++)
            {
                var tmp = _selectedQuestion.CorrectAnswers[_selectedQuestion.Answers[i]];
                if (_checkedAnswers[i] == true && tmp == true)
                    _score++;
                Console.WriteLine($" {i} Score: {_score} , {_checkedAnswers[i]} , {tmp}");
            }
            

        }
        private void FinishQuiz()
        {
            _dispatcherTimer.Stop();
            CheckScore();
            _navigationService.NavigateTo("End", $"{_score}/{_maxScore}");
        }
        #endregion 
    }
}
