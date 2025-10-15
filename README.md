This is a comprehensive ASP.NET Core MVC application for managing and displaying local events and announcements. The application implements advanced data structures and algorithms to efficiently organize, search, and recommend events to users.
1. Advanced Data Structures
A. Stacks, Queues, Priority Queues
Stack Implementation - Search History:
private Stack<SearchQuery> _searchHistory;

Purpose: Maintains LIFO (Last-In-First-Out) order of user searches
Usage: Tracks most recent searches for user convenience and pattern analysis
Location: EventService.cs - GetRecentSearches() method
Display: Recent searches shown at top of Index page

Queue Implementation - Upcoming Events:
private Queue<Event> _upcomingEventsQueue;

Purpose: Manages FIFO (First-In-First-Out) processing of upcoming events
Usage: Events happening within 30 days are enqueued for efficient processing
Location: EventService.cs - AddEvent() method

Priority Queue Implementation - High Priority Events:
private PriorityQueue<Event, int> _priorityEvents;

Purpose: Maintains events sorted by priority level (1-5)
Usage: Quickly retrieves high-priority/featured events
Location: EventService.cs - GetHighPriorityEvents() method
Note: Uses negative priority values for max-heap behavior

B. Hash Tables, Dictionaries, Sorted Dictionaries
Sorted Dictionary - Events by Date:
private SortedDictionary<DateTime, List<Event>> _eventsByDate;

Purpose: Automatically maintains events in chronological order
Complexity: O(log n) insertion, O(log n) lookup
Usage: Efficient date-range queries
Location: GetEventsByDateRange() method

Dictionary - Events by Category:
private Dictionary<string, List<Event>> _eventsByCategory;

Purpose: Fast O(1) category-based filtering
Usage: Quick retrieval of all events in a specific category
Location: GetEventsByCategory() method

Dictionary - Fast ID Lookup:
private Dictionary<int, Event> _eventsById;

Purpose: O(1) event retrieval by ID
Usage: Details page, recommendations, similar events

Dictionary - Recommendation Tracking:
private Dictionary<string, int> _categorySearchCount;
private Dictionary<string, int> _searchTermFrequency;

Purpose: Tracks user preferences for recommendation algorithm
Usage: Records frequency of category searches and search terms

C. Sets (10 Marks)
HashSet - Unique Categories:
private HashSet<string> _categories;

Purpose: Maintains unique list of all event categories
Complexity: O(1) insertion and lookup
Usage: Category dropdown and filters

HashSet - Unique Event Dates:
private HashSet<DateTime> _eventDates;

Purpose: Tracks all unique dates with events
Usage: Calendar integrations and date filtering

2. Recommendation Feature
Algorithm Design
The recommendation system uses a hybrid scoring algorithm combining multiple factors:
public List<Event> GetRecommendedEvents(int count = 6)
Scoring Components:

Category Preference Scoring (Weight: 10)

Tracks which categories users search most frequently
Events in frequently searched categories get higher scores
Formula: score += categorySearchCount[category] * 10


Tag Matching (Weight: 5)

Matches event tags with user's search history
Each matching tag adds to the score
Formula: score += searchTermFrequency[tag] * 5


Title Keyword Matching (Weight: 3)

Analyzes title words against search history
Identifies events matching user interests
Formula: score += searchTermFrequency[word] * 3


Priority Boost (Weight: 2)

Featured events get additional score
Formula: score += event.Priority * 2


Temporal Proximity (Weight: 5-15)

Events happening soon get higher scores
Within 7 days: +15 points
Within 14 days: +10 points
Within 21 days: +5 points


Diversity Factor (Weight: 0-5)

Adds randomness to prevent recommendation stagnation
Ensures variety in suggestions



Content-Based Filtering - Similar Events:
public List<Event> GetSimilarEvents(int eventId, int count = 3)

Finds events similar to a specific event
Compares categories, tags, and priority levels
Uses intersection of tags for similarity scoring

User Pattern Analysis:

All searches are recorded in the stack
Category and search term frequencies are tracked
Patterns emerge over multiple searches
Recommendations improve with usage

Search Functionality
Multi-Criteria Search
The application supports comprehensive search with multiple filters:

Text Search: Title, description, and tags
Category Filter: Dropdown with all categories
Date Range: Start and end date filters
Combined Filters: All filters work together

Search Implementation
public List<Event> SearchEvents(string searchTerm, string category, 
    DateTime? startDate, DateTime? endDate)
Features:

Records search in Stack for history

Application Features
1. Event Display

Aesthetically pleasing card-based layout
Gradient backgrounds and modern design
Hover animations and transitions
Priority badges for featured events
Tag display for categorization

2. Search & Filter

Advanced multi-criteria search
Real-time category filtering
Date range selection
Quick filter badges
Recent search history display

3. Event Details

Comprehensive event information
Similar events section (content-based filtering)
Recommended events sidebar
Quick info cards with icons
Sticky sidebar for better UX

4. Recommendations

Personalized event suggestions
Based on search patterns
Displayed prominently on homepage
Updated dynamically with user behavior
Hybrid scoring algorithm

5. Statistics Dashboard

Total events found
Number of categories
Recent searches count
Recommendations count

Project Structure
Controllers
EventController.cs          // MVC controller
HomeController.cs
ReportController.cs
Data
ReportDbContext.cs
Migrations
20251015201312_ReportDb.cs
ReportDbContextModelSnapshot.cs
Models
ErrorViewModel.cs
Event.cs                    // Event model with properties
Issue.cs
Services
EventService.cs             // Core service with data structures
IssueService.cs
Views
Event
Details.cshtml              // Main events page
Index.cshtml                // Event details page
Home
Index.cshtml
Privacy.cshtml
Report
Index.cshtml
Success.cshtml
Shared
_Layout.cshtml              // Layout template
Prgram.cs                   // Application configuration

Prerequisites

.NET 7.0 SDK or later
Visual Studio 2022 or VS Code

Advanced Features

Responsive Design: Mobile-friendly Bootstrap layout
Modern UI: Gradient backgrounds, animations, shadows
Icon Integration: Bootstrap Icons throughout
Accessibility: Semantic HTML and ARIA labels
Performance: Efficient data structure usage
Scalability: Service-based architecture
Maintainability: Clean code and separation of concerns


Future Enhancements

User authentication and profiles
Event creation and management
Real-time notifications
Social sharing features
Calendar integration
Map-based event display
Ticket booking system
Event ratings and reviews


Conclusion
This application demonstrates comprehensive understanding of:

Advanced data structures in C#
Algorithm design for recommendations
ASP.NET Core MVC architecture
Modern web UI/UX principles
Efficient data organization and retrieval
Updates recommendation tracking
Uses LINQ for efficient filtering
Returns results sorted by date

If you want to check the link project:
https://localhost:7047/
