using PROG7312_POEPART2.Models;

namespace PROG7312_POEPART2.Services
{
    public class EventService
    {
        // SORTED DICTIONARY: Events organized by date
        private SortedDictionary<DateTime, List<Event>> _eventsByDate;

        // HASH TABLE/DICTIONARY: Events organized by category
        private Dictionary<string, List<Event>> _eventsByCategory;

        // DICTIONARY: Fast lookup by ID
        private Dictionary<int, Event> _eventsById;

        // SETS: Unique categories and dates
        private HashSet<string> _categories;
        private HashSet<DateTime> _eventDates;

        // PRIORITY QUEUE: High-priority events
        private PriorityQueue<Event, int> _priorityEvents;

        // STACK: Recent search history (15 Marks)
        private Stack<SearchQuery> _searchHistory;

        // QUEUE: Upcoming events processing queue (15 Marks)
        private Queue<Event> _upcomingEventsQueue;

        // Recommendation tracking
        private Dictionary<string, int> _categorySearchCount;
        private Dictionary<string, int> _searchTermFrequency;

        public EventService()
        {
            InitializeDataStructures();
            SeedData();
        }

        private void InitializeDataStructures()
        {
            _eventsByDate = new SortedDictionary<DateTime, List<Event>>();
            _eventsByCategory = new Dictionary<string, List<Event>>();
            _eventsById = new Dictionary<int, Event>();
            _categories = new HashSet<string>();
            _eventDates = new HashSet<DateTime>();
            _priorityEvents = new PriorityQueue<Event, int>();
            _searchHistory = new Stack<SearchQuery>();
            _upcomingEventsQueue = new Queue<Event>();
            _categorySearchCount = new Dictionary<string, int>();
            _searchTermFrequency = new Dictionary<string, int>();
        }

        private void SeedData()
        {
            var events = new List<Event>
            {
                new Event { Id = 1, Title = "Summer Music Festival", Description = "Annual outdoor music festival featuring local bands",
                    EventDate = DateTime.Now.AddDays(7), Location = "Central Park", Category = "Music", Priority = 5,
                    ImageUrl = "/images/music-festival.jpg", Organizer = "City Arts Council", Capacity = 5000,
                    Tags = new List<string> { "outdoor", "live music", "family-friendly" }},

                new Event { Id = 2, Title = "Tech Innovation Conference", Description = "Explore the latest in AI and technology",
                    EventDate = DateTime.Now.AddDays(14), Location = "Convention Center", Category = "Technology", Priority = 4,
                    ImageUrl = "/images/tech-conf.jpg", Organizer = "TechHub", Capacity = 500,
                    Tags = new List<string> { "AI", "networking", "innovation" }},

                new Event { Id = 3, Title = "Community Food Fair", Description = "Taste dishes from around the world",
                    EventDate = DateTime.Now.AddDays(3), Location = "Downtown Square", Category = "Food", Priority = 3,
                    ImageUrl = "/images/food-fair.jpg", Organizer = "Local Restaurants Association", Capacity = 2000,
                    Tags = new List<string> { "food", "culture", "family" }},

                new Event { Id = 4, Title = "Art Gallery Opening", Description = "New contemporary art exhibition",
                    EventDate = DateTime.Now.AddDays(5), Location = "Modern Art Gallery", Category = "Arts", Priority = 4,
                    ImageUrl = "/images/art-gallery.jpg", Organizer = "Contemporary Arts Museum", Capacity = 200,
                    Tags = new List<string> { "art", "culture", "exhibition" }},

                new Event { Id = 5, Title = "Marathon 2024", Description = "Annual city marathon for all fitness levels",
                    EventDate = DateTime.Now.AddDays(21), Location = "City Streets", Category = "Sports", Priority = 5,
                    ImageUrl = "/images/marathon.jpg", Organizer = "Sports Council", Capacity = 10000,
                    Tags = new List<string> { "running", "fitness", "outdoor" }},

                new Event { Id = 6, Title = "Business Networking Evening", Description = "Connect with local entrepreneurs",
                    EventDate = DateTime.Now.AddDays(10), Location = "Business Hub", Category = "Business", Priority = 3,
                    ImageUrl = "/images/networking.jpg", Organizer = "Chamber of Commerce", Capacity = 150,
                    Tags = new List<string> { "networking", "business", "professional" }},

                new Event { Id = 7, Title = "Jazz Night", Description = "Live jazz performances by renowned artists",
                    EventDate = DateTime.Now.AddDays(2), Location = "Jazz Club", Category = "Music", Priority = 4,
                    ImageUrl = "/images/jazz.jpg", Organizer = "Jazz Society", Capacity = 300,
                    Tags = new List<string> { "jazz", "live music", "evening" }},

                new Event { Id = 8, Title = "Kids Science Workshop", Description = "Interactive science experiments for children",
                    EventDate = DateTime.Now.AddDays(4), Location = "Science Museum", Category = "Education", Priority = 3,
                    ImageUrl = "/images/science-kids.jpg", Organizer = "Science Museum", Capacity = 100,
                    Tags = new List<string> { "kids", "education", "science" }},

                new Event { Id = 9, Title = "Wine Tasting Evening", Description = "Sample premium wines from local vineyards",
                    EventDate = DateTime.Now.AddDays(12), Location = "Wine Bar", Category = "Food", Priority = 2,
                    ImageUrl = "/images/wine.jpg", Organizer = "Wine Enthusiasts Club", Capacity = 80,
                    Tags = new List<string> { "wine", "tasting", "adults" }},

                new Event { Id = 10, Title = "Digital Marketing Masterclass", Description = "Learn modern marketing strategies",
                    EventDate = DateTime.Now.AddDays(8), Location = "Learning Center", Category = "Technology", Priority = 4,
                    ImageUrl = "/images/marketing.jpg", Organizer = "Digital Academy", Capacity = 50,
                    Tags = new List<string> { "marketing", "digital", "education" }},

                new Event { Id = 11, Title = "Yoga in the Park", Description = "Free morning yoga session",
                    EventDate = DateTime.Now.AddDays(1), Location = "Riverside Park", Category = "Sports", Priority = 2,
                    ImageUrl = "/images/yoga.jpg", Organizer = "Wellness Community", Capacity = 100,
                    Tags = new List<string> { "yoga", "wellness", "outdoor" }},

                new Event { Id = 12, Title = "Film Festival", Description = "Independent and international films",
                    EventDate = DateTime.Now.AddDays(15), Location = "Cinema Complex", Category = "Arts", Priority = 5,
                    ImageUrl = "/images/film.jpg", Organizer = "Film Society", Capacity = 800,
                    Tags = new List<string> { "film", "cinema", "arts" }}
            };

            foreach (var evt in events)
            {
                AddEvent(evt);
            }
        }

        public void AddEvent(Event evt)
        {
            // Add to dictionary by ID
            _eventsById[evt.Id] = evt;

            // Add to sorted dictionary by date
            if (!_eventsByDate.ContainsKey(evt.EventDate.Date))
                _eventsByDate[evt.EventDate.Date] = new List<Event>();
            _eventsByDate[evt.EventDate.Date].Add(evt);

            // Add to dictionary by category
            if (!_eventsByCategory.ContainsKey(evt.Category))
                _eventsByCategory[evt.Category] = new List<Event>();
            _eventsByCategory[evt.Category].Add(evt);

            // Add to sets
            _categories.Add(evt.Category);
            _eventDates.Add(evt.EventDate.Date);

            // Add to priority queue (negate priority for max-heap behavior)
            _priorityEvents.Enqueue(evt, -evt.Priority);

            // Add to upcoming events queue if within next 30 days
            if (evt.EventDate >= DateTime.Now && evt.EventDate <= DateTime.Now.AddDays(30))
                _upcomingEventsQueue.Enqueue(evt);
        }

        public List<Event> GetAllEvents()
        {
            return _eventsById.Values.OrderBy(e => e.EventDate).ToList();
        }

        public List<Event> GetUpcomingEvents(int count = 10)
        {
            return _eventsById.Values
                .Where(e => e.EventDate >= DateTime.Now)
                .OrderBy(e => e.EventDate)
                .Take(count)
                .ToList();
        }

        public List<Event> GetEventsByCategory(string category)
        {
            return _eventsByCategory.ContainsKey(category)
                ? _eventsByCategory[category].OrderBy(e => e.EventDate).ToList()
                : new List<Event>();
        }

        public List<Event> GetEventsByDateRange(DateTime startDate, DateTime endDate)
        {
            var events = new List<Event>();

            // Use sorted dictionary for efficient range query
            foreach (var kvp in _eventsByDate)
            {
                if (kvp.Key >= startDate.Date && kvp.Key <= endDate.Date)
                    events.AddRange(kvp.Value);
            }

            return events.OrderBy(e => e.EventDate).ToList();
        }

        public List<Event> SearchEvents(string searchTerm, string category = null,
            DateTime? startDate = null, DateTime? endDate = null)
        {
            // Record search query in stack
            var query = new SearchQuery
            {
                SearchTerm = searchTerm,
                Category = category,
                StartDate = startDate,
                EndDate = endDate,
                SearchTimestamp = DateTime.Now
            };
            _searchHistory.Push(query);

            // Track category searches for recommendations
            if (!string.IsNullOrEmpty(category))
            {
                if (!_categorySearchCount.ContainsKey(category))
                    _categorySearchCount[category] = 0;
                _categorySearchCount[category]++;
            }

            // Track search terms for recommendations
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var terms = searchTerm.ToLower().Split(' ');
                foreach (var term in terms)
                {
                    if (!_searchTermFrequency.ContainsKey(term))
                        _searchTermFrequency[term] = 0;
                    _searchTermFrequency[term]++;
                }
            }

            var results = _eventsById.Values.AsQueryable();

            // Filter by search term
            if (!string.IsNullOrEmpty(searchTerm))
            {
                results = results.Where(e =>
                    e.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    e.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    e.Tags.Any(t => t.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
            }

            // Filter by category
            if (!string.IsNullOrEmpty(category))
                results = results.Where(e => e.Category == category);

            // Filter by date range
            if (startDate.HasValue)
                results = results.Where(e => e.EventDate >= startDate.Value);

            if (endDate.HasValue)
                results = results.Where(e => e.EventDate <= endDate.Value);

            return results.OrderBy(e => e.EventDate).ToList();
        }

        public List<Event> GetHighPriorityEvents(int count = 5)
        {
            // Create temporary priority queue to avoid dequeuing from original
            var tempQueue = new PriorityQueue<Event, int>();
            var allEvents = _eventsById.Values.Where(e => e.EventDate >= DateTime.Now);

            foreach (var evt in allEvents)
                tempQueue.Enqueue(evt, -evt.Priority);

            var priorityEvents = new List<Event>();
            for (int i = 0; i < count && tempQueue.Count > 0; i++)
                priorityEvents.Add(tempQueue.Dequeue());

            return priorityEvents;
        }

        public HashSet<string> GetAllCategories()
        {
            return _categories;
        }

        public Dictionary<string, int> GetCategoryCount()
        {
            return _eventsByCategory.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Count
            );
        }

        public List<SearchQuery> GetRecentSearches(int count = 5)
        {
            return _searchHistory.Take(count).ToList();
        }

        // RECOMMENDATION ALGORITHM
        public List<Event> GetRecommendedEvents(int count = 6)
        {
            var upcomingEvents = _eventsById.Values
                .Where(e => e.EventDate >= DateTime.Now)
                .ToList();

            if (upcomingEvents.Count == 0)
                return new List<Event>();

            var scoredEvents = new Dictionary<Event, double>();

            foreach (var evt in upcomingEvents)
            {
                double score = 0;

                // Score based on category search frequency
                if (_categorySearchCount.ContainsKey(evt.Category))
                    score += _categorySearchCount[evt.Category] * 10;

                // Score based on search term matches
                foreach (var tag in evt.Tags)
                {
                    if (_searchTermFrequency.ContainsKey(tag.ToLower()))
                        score += _searchTermFrequency[tag.ToLower()] * 5;
                }

                // Score based on title keyword matches
                var titleWords = evt.Title.ToLower().Split(' ');
                foreach (var word in titleWords)
                {
                    if (_searchTermFrequency.ContainsKey(word))
                        score += _searchTermFrequency[word] * 3;
                }

                // Boost high-priority events
                score += evt.Priority * 2;

                // Boost events happening soon
                var daysUntilEvent = (evt.EventDate - DateTime.Now).Days;
                if (daysUntilEvent <= 7)
                    score += 15;
                else if (daysUntilEvent <= 14)
                    score += 10;
                else if (daysUntilEvent <= 21)
                    score += 5;

                // Add some randomness to diversify recommendations
                score += new Random(evt.Id).NextDouble() * 5;

                scoredEvents[evt] = score;
            }

            // Return top recommended events
            return scoredEvents
                .OrderByDescending(kvp => kvp.Value)
                .Take(count)
                .Select(kvp => kvp.Key)
                .ToList();
        }

        // Content-based filtering for similar events
        public List<Event> GetSimilarEvents(int eventId, int count = 3)
        {
            if (!_eventsById.ContainsKey(eventId))
                return new List<Event>();

            var targetEvent = _eventsById[eventId];
            var similarEvents = new Dictionary<Event, int>();

            foreach (var evt in _eventsById.Values)
            {
                if (evt.Id == eventId || evt.EventDate < DateTime.Now)
                    continue;

                int similarity = 0;

                // Same category
                if (evt.Category == targetEvent.Category)
                    similarity += 10;

                // Common tags
                var commonTags = evt.Tags.Intersect(targetEvent.Tags).Count();
                similarity += commonTags * 5;

                // Similar priority
                if (Math.Abs(evt.Priority - targetEvent.Priority) <= 1)
                    similarity += 3;

                if (similarity > 0)
                    similarEvents[evt] = similarity;
            }

            return similarEvents
                .OrderByDescending(kvp => kvp.Value)
                .Take(count)
                .Select(kvp => kvp.Key)
                .ToList();
        }
    }
}
