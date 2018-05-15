using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using QuizReader.Services;
using System.Windows.Input;

namespace QuizReader.ViewModel
{
   
    public class MainViewModel : ViewModelBase
    {
    
        #region Private Fields
        private string _quizTitle;
        private readonly IFrameNavigationService _navigationService;
        #endregion

        #region Commands
        public RelayCommand OnLoadCommand { get; private set; }
        #endregion

        #region Ctor
        public MainViewModel(IFrameNavigationService navigationService)
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
            QuizTitle = "QuizReader ";
            this.OnLoadCommand = new RelayCommand(SwitchView);
            
            
        }
        #endregion

        #region Properties
        public string QuizTitle
        {
            get { return _quizTitle; }
            set { Set(()=>QuizTitle,ref _quizTitle,value); }
        }
        #endregion

        #region Private Methods
        private void SwitchView()
        {
            _navigationService.NavigateTo("Start");
            
        }
        #endregion

    }
}