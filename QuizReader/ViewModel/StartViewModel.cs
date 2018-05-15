using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmDialogs;
using MvvmDialogs.FrameworkDialogs.OpenFile;
using QuizReader.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuizReader.ViewModel
{
    public class StartViewModel : ViewModelBase
    {
        #region Private Fields
        private static string _whenLoadingText = "Click the button to select quiz file:";
        private static string _whenLoadingButtonText = "Load";

        private static string _whenStartingText = "Click the button to start the quiz:";
        private static string _whenStartingButtonText = "Start";
        private string filepath = string.Empty;
        private DataReader _reader;
        private readonly IFrameNavigationService _navigationService;
        private string _loadButtonText = _whenLoadingButtonText;
        private string _welcomeText = _whenLoadingText;
        private readonly IDialogService _dialogService;
        private bool canStart = false;
        #endregion

        #region Commands
        public RelayCommand LoadCommand { get; private set; }
        #endregion

        #region Properties
        public string LoadButtonText { get => _loadButtonText; set => Set(() => LoadButtonText, ref _loadButtonText, value); }
        public string WelcomeText { get => _welcomeText; set => Set(() => WelcomeText, ref _welcomeText, value); }
        #endregion

        #region Ctor
        public StartViewModel(IFrameNavigationService navigationService, IDialogService dialogService)
        {
            this._navigationService = navigationService;
            this._dialogService = dialogService;
            this.LoadCommand = new RelayCommand(OnLoad);

        }
        #endregion 

        #region Private Methods
        private void OnLoad()
        {



            if (LoadButtonText == _whenStartingButtonText)
            {
                _navigationService.NavigateTo("Quiz", _reader);
            }
            else
            {
                var settings = new OpenFileDialogSettings
                {
                    Title = "Select the quiz file",
                    InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    Filter = "XML Document (*.xml)|*.xml"
                };

                bool? success = _dialogService.ShowOpenFileDialog(this, settings);
                if (success == true)
                {
                    filepath = settings.FileName;
                    LoadData();
                }
            }

            if (canStart)
            {
                LoadButtonText = _whenStartingButtonText;
                WelcomeText = _whenStartingText;

            }

        }
        private void LoadData()
        {
            try
            {
                _reader = new DataReader(filepath);
                canStart = true;
            }
            catch (Exception ex)
            {
                canStart = false;
                _dialogService.ShowMessageBox(this, ex.Message, "Error!");           
            }

        }
        #endregion
    }
}
