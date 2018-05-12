using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using QuizReader.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReader.ViewModel
{
    public class StartViewModel:ViewModelBase
    {

        private static string _whenLoadingText = "Click the button to choose quiz file:";
        private static string _whenLoadingButtonText = "Load";

        private static string _whenStartingText = "Click the button to start the quiz:";
        private static string _whenStartingButtonText = "Start";

        private readonly IFrameNavigationService _navigationService;
        private string _loadButtonText = _whenLoadingButtonText;
        private string _welcomeText = _whenLoadingText;
        public RelayCommand LoadCommand { get; private set; }
        public string LoadButtonText { get => _loadButtonText; set => Set(() => LoadButtonText, ref _loadButtonText, value); }
        public string WelcomeText { get => _welcomeText; set => Set(() => WelcomeText, ref _welcomeText, value); }

        public StartViewModel(IFrameNavigationService navigationService)
        {
            this._navigationService = navigationService;
            this.LoadCommand = new RelayCommand(OnLoad);
        }


        void OnLoad()
        {
            if (LoadButtonText == _whenStartingButtonText)
            {
                _navigationService.NavigateTo("Quiz");
            }
            LoadButtonText = _whenStartingButtonText;
            WelcomeText = _whenStartingText;

          

        }
    }
}
