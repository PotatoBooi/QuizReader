using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using QuizReader.Services;
using System.Windows.Input;

namespace QuizReader.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 
        /// TODO:
        /// OpenFileDialogService
        /// Quiz Logic
        /// Finish screen
        /// Data Security enhancement
        /// GUI Polishing
        /// 

        private string _quizTitle;
        private readonly IFrameNavigationService _navigationService;
     
        public RelayCommand OnLoadCommand { get; private set; }
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
            QuizTitle = "MainWindow - QuizReader ";
            this.OnLoadCommand = new RelayCommand(SwitchView);
            
            
        }


        public string QuizTitle
        {
            get { return _quizTitle; }
            set { _quizTitle = value; }
        }

        public string PageKey
        {
            get; private set;
        }

        private void SwitchView()
        {
            _navigationService.NavigateTo("Start");
            
        }
      
    }
}