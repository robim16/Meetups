using AutoMapper;
using Meetups.WebApp.Data;
using Meetups.WebApp.Data.Entities;
using Meetups.WebApp.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Meetups.WebApp.Features.DiscoverEvents
{
    public class DiscoverEventsService
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;
        private readonly IMapper mapper;

        public DiscoverEventsService(IDbContextFactory<ApplicationDbContext> contextFactory,
            IMapper mapper)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
        }

        public async Task<List<EventViewModel>> GetEventsAsync(string? filter = "")
        {
            using var context = contextFactory.CreateDbContext();

            var events = await SearchEvents(filter, context);

            if (!string.IsNullOrWhiteSpace(filter) && events.Count == 0)
            {
                filter = null;
                events = await SearchEvents(filter, context);
            }

            return mapper.Map<List<EventViewModel>>(events);
        }

        private async Task<List<Event>> SearchEvents(string? filter, ApplicationDbContext context)
        {

            return await (context.Events?
                .Where(e => (string.IsNullOrEmpty(filter) ||
                             e.Title.Contains(filter) ||
                             e.Description.Contains(filter) ||
                             e.Location.Contains(filter)) &&
                            (e.BeginDate > DateOnly.FromDateTime(DateTime.Now) ||
                            (e.BeginDate == DateOnly.FromDateTime(DateTime.Now) && e.BeginTime > TimeOnly.FromDateTime(DateTime.Now))))
                .OrderByDescending(e => e.BeginDate)
                .ThenByDescending(e => e.BeginTime)
                .ToListAsync() ?? Task.FromResult(new List<Event>()));

        }
    }
}