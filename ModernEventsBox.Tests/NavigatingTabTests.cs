namespace ModernEventsBox.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO.Packaging;
    using System.Linq;
    using NUnit.Framework;

    [RequiresSTA]
    public class NavigatingTabTests
    {
        private NavigatingTab _tab;
        private List<TabNavigationCanceledEventArgs> _cancels;
        private List<TabNavigationEventArgs> _navigations;
        private bool _cancel;

        [SetUp]
        public void SetUp()
        {
            _cancel = false;
            _tab = new NavigatingTab();
            _cancels = new List<TabNavigationCanceledEventArgs>();
            _navigations = new List<TabNavigationEventArgs>();
        }

        [TearDown]
        public void TearDown()
        {
            _tab.Navigated -= OnNavigated;
            ModernEvents.Navigated -= OnNavigated;
            _tab.Navigating -= OnNavigating;
            ModernEvents.Navigating -= OnNavigating;
        }

        [TestCase(null, "1.xaml")]
        [TestCase("1.xaml", "2.xaml")]
        public void FromToStandard(string @from, string to)
        {
            Uri fromUri;
            if (@from != null)
            {
                fromUri = new Uri(@from, UriKind.Relative);
                _tab.SelectedSource = fromUri;
            }
            else
            {
                fromUri = null;
            }
            var toUri = new Uri(to, UriKind.Relative);

            _tab.Navigating += OnNavigating;
            _tab.Navigated += OnNavigated;
            _tab.SelectedSource = toUri;
            Assert.IsFalse(_cancels.Single().Cancel);
            Assert.AreEqual(toUri, _cancels.Single().NavigatingTo);
            Assert.AreEqual(fromUri, _navigations.Single().NavigatedFrom);
            Assert.AreEqual(toUri, _navigations.Single().NavigatedTo);
        }

        [TestCase(null, "1.xaml")]
        [TestCase("1.xaml", "2.xaml")]
        public void FromToCanceled(string @from, string to)
        {
            _cancel = true;
            Uri fromUri;
            if (@from != null)
            {
                fromUri = new Uri(@from, UriKind.Relative);
                _tab.SelectedSource = fromUri;
            }
            else
            {
                fromUri = null;
            }
            var toUri = new Uri(to, UriKind.Relative);
            _tab.Navigating += OnNavigating;
            _tab.Navigated += OnNavigated;
            _cancel = true;
            _tab.SelectedSource = toUri;
            Assert.IsTrue(_cancels.Single().Cancel);
            Assert.AreEqual(toUri, _cancels.Single().NavigatingTo);
            CollectionAssert.IsEmpty( _navigations);
        }

        [TestCase(null, "1.xaml")]
        [TestCase("1.xaml", "2.xaml")]
        public void FromToStandardModernEvents(string @from, string to)
        {
            Uri fromUri;
            if (@from != null)
            {
                fromUri = new Uri(@from, UriKind.Relative);
                _tab.SelectedSource = fromUri;
            }
            else
            {
                fromUri = null;
            }
            var toUri = new Uri(to, UriKind.Relative);

            ModernEvents.Navigating += OnNavigating;
            ModernEvents.Navigated += OnNavigated;
            _tab.SelectedSource = toUri;
            Assert.IsFalse(_cancels.Single().Cancel);
            Assert.AreEqual(toUri, _cancels.Single().NavigatingTo);
            Assert.AreEqual(fromUri, _navigations.Single().NavigatedFrom);
            Assert.AreEqual(toUri, _navigations.Single().NavigatedTo);
        }

        private void OnNavigated(object sender, EventArgs eventArgs)
        {
            _navigations.Add((TabNavigationEventArgs) eventArgs);
        }

        private void OnNavigating(object sender, EventArgs args)
        {
            var eventArgs = (TabNavigationCanceledEventArgs) args;
            eventArgs.Cancel = _cancel;
            _cancels.Add(eventArgs);
        }
    }
}
