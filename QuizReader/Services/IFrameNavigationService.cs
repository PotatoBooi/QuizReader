using GalaSoft.MvvmLight.Views;

namespace QuizReader.Services
{
    public interface IFrameNavigationService : INavigationService
    {
        object Parameter { get; }
    }
}
