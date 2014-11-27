namespace ModernEventsBox
{
    using System.Windows;
    using System.Windows.Navigation;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void RaiseNavigating(NavigatingCancelEventArgs e)
        {
            OnNavigating(e);
        }
    }
}
