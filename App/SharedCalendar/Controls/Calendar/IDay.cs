using System;
using System.Collections.ObjectModel;

namespace SharedCalendar.Controls
{
    public interface IDay
    {
        bool Today { get; }
        bool Enabled { get; }
        string Title { get; }
        bool HasEvents { get; }
        ObservableCollection<IEvent> Events { get; }
        DateTime? Date { get; }
        void AddEvent(IEvent @event);
    }

    public interface IEvent
    {
        int Id { get; }
        string Name { get; }
        object Data { get; }
    }

    public class Event : IEvent
    {
        public Event (int id, string name, object data)
        {
            Id = id;
            Name = name;
            Data = data;
        }
        public int Id { get; }
        public string Name { get; }
        public object Data { get; }
    }
}
