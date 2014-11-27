namespace ModernEventsBox
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Annotations;

    public class Vm : INotifyPropertyChanged
    {
        public static Vm Instance = new Vm();
        private readonly ObservableCollection<EventArgs> _navigatings = new ObservableCollection<EventArgs>();
        private readonly ObservableCollection<EventArgs> _navigateds = new ObservableCollection<EventArgs>();
        
        private Vm()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<EventArgs> Navigatings
        {
            get { return _navigatings; }
        }

        public ObservableCollection<EventArgs> Navigateds
        {
            get { return _navigateds; }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
