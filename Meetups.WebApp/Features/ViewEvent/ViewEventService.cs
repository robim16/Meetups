using AutoMapper;
using Meetups.WebApp.Data;
using Meetups.WebApp.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Meetups.WebApp.Features.ViewEvent
{
    public class ViewEventService
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;
        private readonly IMapper mapper;

        public ViewEventService(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
        }

        public async Task<EventViewModel> GetEventByIdAsync(int eventId)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            var eventEntity = await context.Events?.FirstOrDefaultAsync(e => e.EventId == eventId);

            return mapper.Map<EventViewModel>(eventEntity);
        }
    }
}
