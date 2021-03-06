/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:QuizReader"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizReader.Views;
using System.Windows.Navigation;
using QuizReader.Services;
using MvvmDialogs;

namespace QuizReader.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    /// public const string FirstPageKey = "FirstPage";
    
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
      
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            // NavigationService navigation = new NavigationService();

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            SimpleIoc.Default.Unregister<IFrameNavigationService>();
            SimpleIoc.Default.Unregister<MvvmDialogs.IDialogService>();


            SimpleIoc.Default.Register<MvvmDialogs.IDialogService>(() => new DialogService());
            SetupNavigation();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<StartViewModel>();
            SimpleIoc.Default.Register<QuizViewModel>();
            SimpleIoc.Default.Register<QuizEndViewModel>();

        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public QuizViewModel Quiz
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QuizViewModel>();
            }
        }
        public StartViewModel Start
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StartViewModel>();
            }
        }

        public QuizEndViewModel QuizEnd
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QuizEndViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
            SimpleIoc.Default.Unregister<StartViewModel>();
            SimpleIoc.Default.Unregister<QuizViewModel>();
            SimpleIoc.Default.Unregister<QuizEndViewModel>();
            SimpleIoc.Default.Register<StartViewModel>();
            SimpleIoc.Default.Register<QuizViewModel>();
            SimpleIoc.Default.Register<QuizEndViewModel>();
        }

        private static void SetupNavigation()
        {
            var navigationService = new FrameNavigationService();
            navigationService.Configure("Start", new Uri("../Views/StartView.xaml", UriKind.Relative));
            navigationService.Configure("Quiz", new Uri("../Views/QuizView.xaml", UriKind.Relative));
            navigationService.Configure("End", new Uri("../Views/QuizEndView.xaml", UriKind.Relative));
            SimpleIoc.Default.Register<IFrameNavigationService>(() => navigationService);
        }
       
    }
}