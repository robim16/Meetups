using System.ComponentModel.DataAnnotations;

namespace Meetups.WebApp.Features.Events.CreateEvent
{
    public class EventViewModel
    {
        public int EventId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string? Title { get; set; }

        [StringLength(maximumLength: 500)]
        public string Description { get; set; }

        [Required]
        public  DateOnly BeginDate { get; set; }

        [Required]
        public TimeOnly BeginTime { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }
        public string? Location { get; set; }
        public string? MeetupLink { get; set; }

        [Required]
        public string? Category { get; set; }
        public int Capacity { get; set; }

        [Range(1, int.MaxValue)]
        public int OrganizerId { get; set; }

        public EventViewModel()
        {
            BeginDate = DateOnly.FromDateTime(DateTime.Now);
            BeginTime = TimeOnly.FromDateTime(DateTime.Now);
            EndDate = DateOnly.FromDateTime(DateTime.Now);
            EndTime = TimeOnly.FromDateTime(DateTime.Now);

            Category = MeetupCategoriesEnum.InPerson.ToString();
        }

        public string? ValidateDates()
        {

            if (BeginDate > EndDate)
            {
                return "Begin date cannot be after end date.";
            }
            if (BeginDate == EndDate && BeginTime > EndTime)
            {
                return "Begin time cannot be after end time.";
            }
            return string.Empty;
        }

        public bool ValidateCapacity()
        {
            if (Capacity <= 0)
            {
                return false;
            }
            return true;
        }

        public string? ValidateLocation()
        {
            if (Category == MeetupCategoriesEnum.InPerson.ToString() && string.IsNullOrEmpty(Location))
            {
                return "Location is required for in-person events.";
            }
            return string.Empty;
        }

        public string? ValidateMeetupLink()
        {
            if (Category == MeetupCategoriesEnum.Online.ToString() && string.IsNullOrWhiteSpace(MeetupLink))
            {
                return "Meetup link is required for online events.";
            }
            return string.Empty;
        }
    }
}
