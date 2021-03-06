﻿namespace ModernEventsBox
{
    using System.Windows;
    using FirstFloor.ModernUI.Windows.Controls;
    using FirstFloor.ModernUI.Windows.Navigation;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        private int count = 0;
        public MainWindow()
        {
            InitializeComponent();
            LinkNavigator = new NotifyingNavigator();
            ModernEvents.Navigating += (sender, args) =>
            {
                if (count % 3 == 0)
                {
                    var tabNavigationCanceledEventArgs = args as TabNavigationCanceledEventArgs;
                    if (tabNavigationCanceledEventArgs != null)
                    {
                        tabNavigationCanceledEventArgs.Cancel = true;
                    }
                    var navigatingCancelEventArgs = args as NavigatingCancelEventArgs;
                    if (navigatingCancelEventArgs != null)
                    {
                        navigatingCancelEventArgs.Cancel = true;
                    }
                }
                Vm.Instance.Navigatings.Insert(0, args);

                count++;
            };

            ModernEvents.NavigationFailed += (sender, args) => MessageBox.Show(args.Error.Message);
        }
    }
}
