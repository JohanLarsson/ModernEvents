namespace ModernEventsBox
{
    using System;
    using FirstFloor.ModernUI.Windows.Navigation;

    public static class ModernEvents
    {
        /// <summary>
        /// Occurs when navigation to a content fragment begins.
        /// </summary>
        public static event EventHandler<FragmentNavigationEventArgs> FragmentNavigation;

        /// <summary>
        /// Occurs when a new navigation is requested.
        /// </summary>
        /// <remarks>
        /// The navigating event is also raised when a parent frame is navigating. This allows for cancelling parent navigation.
        /// </remarks>
        public static event EventHandler<EventArgs> Navigating;

        /// <summary>
        /// Occurs when navigation to new content has completed.
        /// </summary>
        public static event EventHandler<EventArgs> Navigated;

        /// <summary>
        /// Occurs when navigation has failed.
        /// </summary>
        public static event EventHandler<NavigationFailedEventArgs> NavigationFailed;

        public static void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            var handler = Navigating;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        public static void OnNavigating(NavigatingTab sender, TabNavigationCanceledEventArgs e)
        {
            var handler = Navigating;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        public static void OnNavigated(object sender, NavigationEventArgs e)
        {
            var handler = Navigated;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        public static void OnNavigated(object sender, TabNavigationEventArgs e)
        {
            var handler = Navigated;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        public static void OnFragmentNavigation(object sender, FragmentNavigationEventArgs e)
        {
            var handler = FragmentNavigation;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        public static void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            var handler = NavigationFailed;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
     }
}
