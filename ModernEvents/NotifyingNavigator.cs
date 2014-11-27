namespace ModernEventsBox
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using FirstFloor.ModernUI.Windows;
    using FirstFloor.ModernUI.Windows.Controls;
    using FirstFloor.ModernUI.Windows.Navigation;

    public class NotifyingNavigator : DependencyObject, ILinkNavigator
    {
        private readonly DefaultLinkNavigator _navigator = new DefaultLinkNavigator();
        private readonly List<WeakReference> _subscriptions = new List<WeakReference>();

        public CommandDictionary Commands
        {
            get { return _navigator.Commands; }
            set { _navigator.Commands = value; }
        }

        public void Navigate(Uri uri, FrameworkElement source, string parameter = null)
        {
            if (_subscriptions.All(x => x.Target != source))
            {
                var frame = source as ModernFrame;

                if (frame != null)
                {
                    frame.FragmentNavigation += ModernEvents.OnFragmentNavigation;
                    frame.Navigating += ModernEvents.OnNavigating;
                    frame.Navigated += ModernEvents.OnNavigated;
                    frame.NavigationFailed += ModernEvents.OnNavigationFailed;
                }
                _subscriptions.Add(new WeakReference(source));
            }
            _navigator.Navigate(uri, source, parameter);
        }
    }
}
