using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SharedCalendar.Controls
{
    internal class Day : IDay, INotifyPropertyChanged
    {
        public DateTime? Date { get; }
        public string Title { get; }
        public bool Enabled { get; }
        public bool Today { get; }
        public ObservableCollection<IEvent> Events { get; }

        #region Notified Property HasEvents
        /// <summary>
        /// HasEvents
        /// </summary>
        private bool hasevents;
        public bool HasEvents
        {
            get { return hasevents; }
            set { hasevents = value; OnPropertyChanged(); }
        }
        #endregion

        #region Notified Property IsSelected
        /// <summary>
        /// IsSelected
        /// </summary>
        private bool isselected;
        public bool IsSelected
        {
            get { return isselected; }
            set { isselected = value; OnPropertyChanged(); }
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public Day(DateTime? date, string title, bool enabled, bool today,  IEnumerable<IEvent> events = null)
        {
            Date = date;
            Title = title;
            Enabled = enabled;
            Today = today;
            if (events != null)
                Events = new ObservableCollection<IEvent>(events);
            else
                Events = new ObservableCollection<IEvent>();
            Events.CollectionChanged += Events_CollectionChanged;
        }

        private void Events_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            HasEvents = Events.Count > 0;
        }

        public void AddEvent(IEvent @event)
        {
            Events.Add(@event);
        }
    }
}
