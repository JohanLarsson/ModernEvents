namespace ModernEvents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Windows;
    using System.Windows.Controls;
    using FirstFloor.ModernUI.Windows;
    using FirstFloor.ModernUI.Windows.Controls;
    using FirstFloor.ModernUI.Windows.Navigation;

    public class NotifyingNavigator : DependencyObject, ILinkNavigator
    {
        public static NotifyingNavigator Instance = new NotifyingNavigator();
        private readonly DefaultLinkNavigator _navigator = new DefaultLinkNavigator();
        private readonly List<WeakReference> _subscriptions = new List<WeakReference>();
        private NotifyingNavigator()
        {
        }

        /// <summary>
        /// Occurs when navigation to a content fragment begins.
        /// 
        /// </summary>
        public event EventHandler<FragmentNavigationEventArgs> FragmentNavigation;

        /// <summary>
        /// Occurs when a new navigation is requested.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// The navigating event is also raised when a parent frame is navigating. This allows for cancelling parent navigation.
        /// 
        /// </remarks>
        public event EventHandler<EventArgs> Navigating;

        /// <summary>
        /// Occurs when navigation to new content has completed.
        /// 
        /// </summary>
        public event EventHandler<EventArgs> Navigated;

        /// <summary>
        /// Occurs when navigation has failed.
        /// 
        /// </summary>
        public event EventHandler<NavigationFailedEventArgs> NavigationFailed;

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
                    frame.FragmentNavigation += OnFragmentNavigation;
                    frame.Navigating += OnNavigating;
                    frame.Navigated += OnFrameNavigated;
                    frame.NavigationFailed += OnNavigationFailed;
                }

                else
                {
                    throw new NotImplementedException("message");
                }
                _subscriptions.Add(new WeakReference(source));
            }
            _navigator.Navigate(uri, source, parameter);
        }

        protected virtual void OnFragmentNavigation(object sender, FragmentNavigationEventArgs e)
        {
            var handler = FragmentNavigation;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        protected virtual void OnNavigating(object sender, EventArgs e)
        {
            var handler = Navigating;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        protected virtual void OnFrameNavigated(object sender, NavigationEventArgs e)
        {
            var content =(ContentControl) e.Content;
            var tab = content.Content as NavigatingTab;
            if (tab != null && _subscriptions.All(x => x.Target != tab))
            {
                tab.Navigating += OnNavigating;
                tab.Navigated += OnTabNavigated;
                _subscriptions.Add(new WeakReference(tab));
            }
            var handler = Navigated;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
        
        private void OnTabNavigated(object sender, TabNavigatedEventArgs e)
        {
            var handler = Navigated;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        protected virtual void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            var handler = NavigationFailed;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}
