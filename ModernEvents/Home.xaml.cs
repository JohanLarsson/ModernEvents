namespace ModernEventsBox
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
            DataContext = Vm.Instance;
        }
    }
}
