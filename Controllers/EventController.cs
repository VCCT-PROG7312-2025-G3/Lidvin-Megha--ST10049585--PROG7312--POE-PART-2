using Microsoft.AspNetCore.Mvc;
using PROG7312_POEPART2.Models;
using PROG7312_POEPART2.Services;

namespace PROG7312_POEPART2.Controllers
{
    public class EventController : Controller
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        public IActionResult Index(string searchTerm = null, string category = null,
            DateTime? startDate = null, DateTime? endDate = null)
        {
            var viewModel = new EventViewModel();
            // Get events based on filters
            if (!string.IsNullOrEmpty(searchTerm) || !string.IsNullOrEmpty(category) ||
                startDate.HasValue || endDate.HasValue)
            {
                viewModel.Events = _eventService.SearchEvents(searchTerm, category, startDate, endDate);
                viewModel.SearchTerm = searchTerm;
                viewModel.SelectedCategory = category;
                viewModel.StartDate = startDate;
                viewModel.EndDate = endDate;
            }
            else
            {
                viewModel.Events = _eventService.GetUpcomingEvents(50);
            }

            // Get recommended events based on search patterns
            viewModel.RecommendedEvents = _eventService.GetRecommendedEvents(6);

            // Get all categories for dropdown
            viewModel.Categories = _eventService.GetAllCategories().OrderBy(c => c).ToList();

            // Get category counts
            viewModel.CategoryCount = _eventService.GetCategoryCount();

            // Get recent searches
            viewModel.RecentSearches = _eventService.GetRecentSearches(5);

            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var allEvents = _eventService.GetAllEvents();
            var eventItem = allEvents.FirstOrDefault(e => e.Id == id);

            if (eventItem == null)
                return NotFound();

            ViewBag.SimilarEvents = _eventService.GetSimilarEvents(id, 3);
            ViewBag.RecommendedEvents = _eventService.GetRecommendedEvents(4);

            return View(eventItem);
        }

        public IActionResult Category(string category)
        {
            var viewModel = new EventViewModel
            {
                Events = _eventService.GetEventsByCategory(category),
                SelectedCategory = category,
                Categories = _eventService.GetAllCategories().OrderBy(c => c).ToList(),
                CategoryCount = _eventService.GetCategoryCount(),
                RecommendedEvents = _eventService.GetRecommendedEvents(4)
            };

            return View("Index", viewModel);
        }

        public IActionResult HighPriority()
        {
            var viewModel = new EventViewModel
            {
                Events = _eventService.GetHighPriorityEvents(10),
                Categories = _eventService.GetAllCategories().OrderBy(c => c).ToList(),
                CategoryCount = _eventService.GetCategoryCount()
            };

            ViewBag.Title = "High Priority Events";
            return View("Index", viewModel);
        }

        [HttpGet]
        public JsonResult SearchEventsJson(string searchTerm, string category,
            DateTime? startDate, DateTime? endDate)
        {
            var events = _eventService.SearchEvents(searchTerm, category, startDate, endDate);
            return Json(new
            {
                events = events,
                count = events.Count,
                recommendations = _eventService.GetRecommendedEvents(4)
            });
        }

        // API endpoint for recommendations
        [HttpGet]
        public JsonResult GetRecommendations()
        {
            var recommendations = _eventService.GetRecommendedEvents(6);
            return Json(recommendations);
        }
    }
}
