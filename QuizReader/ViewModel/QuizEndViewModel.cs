using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using QuizReader.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizReader.ViewModel
{
    public class QuizEndViewModel : ViewModelBase
    {
        #region Private Fields
        private readonly IFrameNavigationService _navigationService;

        private string _endScore;

        #endregion
        #region Commands
        public RelayCommand GoToStartCommand { get; private set; }
        public RelayCommand ExitCommand { get; private set; }
        #endregion

        #region Ctor
        public QuizEndViewModel(IFrameNavigationService navigationService)
        {
            this._navigationService = navigationService;
            _endScore = navigationService.Parameter.ToString();
            GoToStartCommand = new RelayCommand(OnRestart);
            ExitCommand = new RelayCommand(OnExit);
        }
        #endregion

        #region Properties
        public string ScoreText { get => $"Your score: {_endScore}"; }
        #endregion
        #region Private Methods
        private void OnRestart()
        {
            _navigationService.NavigateTo("Start");
            ViewModelLocator.Cleanup();
        }

        private void OnExit()
        {
            Application.Current.Shutdown();
        }
        #endregion
    }
}
