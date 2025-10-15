namespace PROG7312_POEPART2.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public int Priority { get; set; }
        public string ImageUrl { get; set; }
        public string Organizer { get; set; }
        public int Capacity { get; set; }
        public List<string> Tags { get; set; }

        public Event()
        {
            Tags = new List<string>();
        }
    }

    public class SearchQuery
    {
        public string SearchTerm { get; set; }
        public string Category { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime SearchTimestamp { get; set; }
    }

    public class EventViewModel
    {
        public List<Event> Events { get; set; }
        public List<Event> RecommendedEvents { get; set; }
        public List<string> Categories { get; set; }
        public string SelectedCategory { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SearchTerm { get; set; }
        public Dictionary<string, int> CategoryCount { get; set; }
        public List<SearchQuery> RecentSearches { get; set; }

        public EventViewModel()
        {
            Events = new List<Event>();
            RecommendedEvents = new List<Event>();
            Categories = new List<string>();
            CategoryCount = new Dictionary<string, int>();
            RecentSearches = new List<SearchQuery>();
        }
    }
}
