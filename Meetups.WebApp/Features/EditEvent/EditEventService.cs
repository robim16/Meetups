using AutoMapper;
using Meetups.WebApp.Data;
using Meetups.WebApp.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Meetups.WebApp.Features.EditEvent
{
    public class EditEventService
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;
        private readonly IMapper mapper;

        public EditEventService(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
        }

        public async Task UpdateEventAsync(EventViewModel eventViewModel)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            var eventEntity = await context.Events?.FirstOrDefaultAsync(e => e.EventId == eventViewModel.EventId);
            if (eventEntity != null)
            {
                mapper.Map(eventViewModel, eventEntity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<EventViewModel> GetEventByIdAsync(int eventId)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            var eventEntity = await context.Events?.FirstOrDefaultAsync(e => e.EventId == eventId);

            return mapper.Map<EventViewModel>(eventEntity);
        }
    }
}
