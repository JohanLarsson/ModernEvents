﻿namespace ModernEventsBox
{
    using System;
    using System.Windows;
    using FirstFloor.ModernUI.Windows.Controls;

    public class NavigatingTab : ModernTab
    {
        static NavigatingTab()
        {
            ModernTab.SelectedSourceProperty.OverrideMetadata(
                typeof(NavigatingTab), new PropertyMetadata(null)
                {
                    PropertyChangedCallback = OnSelectedSourceChanged,
                    CoerceValueCallback = CoerceSelectedSourceChanged
                });
        }

        /// <summary>
        /// Occurs when a new navigation is requested.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// The navigating event is also raised when a parent frame is navigating. This allows for cancelling parent navigation.
        /// </remarks>
        public event EventHandler<TabNavigationCanceledEventArgs> Navigating;

        /// <summary>
        /// Occurs when navigation to new content has completed.
        /// 
        /// </summary>
        public event EventHandler<TabNavigationEventArgs> Navigated;

        protected virtual void OnNavigating(TabNavigationCanceledEventArgs e)
        {
            var handler = Navigating;
            if (handler != null)
            {
                handler(this, e);
            }
            ModernEvents.OnNavigating(this, e);
        }

        protected virtual void OnNavigated(TabNavigationEventArgs e)
        {
            var handler = Navigated;
            if (handler != null)
            {
                handler(this, e);
            }
            ModernEvents.OnNavigated(this, e);
        }

        private static void OnSelectedSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (Equals(e.NewValue, e.OldValue))
            {
                return;
            }
            var navigatingTab = ((NavigatingTab)o);
            var args = new TabNavigationEventArgs(navigatingTab, (Uri)e.OldValue, (Uri)e.NewValue);
            navigatingTab.OnNavigated(args);
        }

        private static object CoerceSelectedSourceChanged(DependencyObject o, object baseValue)
        {
            var navigatingTab = ((NavigatingTab)o);
            if (Equals(baseValue, navigatingTab.SelectedSource))
            {
                return baseValue;
            }
            var args = new TabNavigationCanceledEventArgs(navigatingTab, (Uri)baseValue);

            navigatingTab.OnNavigating(args);
            if (args.Cancel)
            {
                return navigatingTab.SelectedSource;
            }
            return baseValue;
        }
    }
}
