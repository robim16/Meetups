using AutoMapper;
using Meetups.WebApp.Data;
using Meetups.WebApp.Data.Entities;
using Meetups.WebApp.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Meetups.WebApp.Features.DeleteEvent
{
    public class DeleteEventService
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;
        private readonly IMapper mapper;

        public DeleteEventService(IDbContextFactory<ApplicationDbContext> contextFactory,
            IMapper mapper)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
        }

        public bool IsEventDeletable(int? eventId)
        {
            if (eventId == null)
            {
                return false;
            }
            using var context = contextFactory.CreateDbContext();

            var eventEntity = context.Events?.FirstOrDefault(e => e.EventId == eventId);
            if (eventEntity != null)
            {
                if (eventEntity.BeginDate < DateOnly.FromDateTime(DateTime.Now) || 
                    (eventEntity.BeginDate == DateOnly.FromDateTime(DateTime.Now) &&
                    eventEntity.BeginTime < TimeOnly.FromDateTime(DateTime.Now)))
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteEventAsync(int? eventId)
        {
            if (eventId == null)
            {
                return false;
            }

            using var context = contextFactory.CreateDbContext();
            var eventEntity = await context.Events?.FirstOrDefaultAsync(e => e.EventId == eventId);

            if (eventEntity != null)
            {
                context.Events.Remove(eventEntity);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
