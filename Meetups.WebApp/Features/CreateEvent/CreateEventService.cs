using AutoMapper;
using Meetups.WebApp.Data;
using Meetups.WebApp.Data.Entities;
using Meetups.WebApp.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Meetups.WebApp.Features.CreateEvent
{
    public class CreateEventService
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;
        private readonly IMapper mapper;

        public CreateEventService(IDbContextFactory<ApplicationDbContext> contextFactory,
            IMapper mapper)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
        }

        public async Task CreateEventAsync(EventViewModel eventViewModel)
        {
            using var context = contextFactory.CreateDbContext();
            var eventEntity = mapper.Map<Event>(eventViewModel);

            context.Events.Add(eventEntity);
            await context.SaveChangesAsync();
        }
    }
}
