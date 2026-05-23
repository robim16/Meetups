using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Meetups.WebApp.Shared.ViewModels
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

        [Required(ErrorMessage = "Cover image is required.")]
        public IBrowserFile CoverImage { get; set; }

        [Required]
        public string? ImageUrl { get; set; }

        [Range(0, int.MaxValue)]
        public int OrganizerId { get; set; }

        public EventViewModel()
        {
            BeginDate = DateOnly.FromDateTime(DateTime.Now);
            BeginTime = TimeOnly.FromDateTime(DateTime.Now);
            EndDate = DateOnly.FromDateTime(DateTime.Now);
            EndTime = TimeOnly.FromDateTime(DateTime.Now);

            Category = MeetupCategoriesEnum.InPerson.ToString();
            ImageUrl = $"images/image-placeholder.png";
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

            DateTime combinedBeginDateTime = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day, BeginTime.Hour, BeginTime.Minute, BeginTime.Second);
            DateTime combinedEndDateTime = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hour, EndTime.Minute, EndTime.Second);

            if (combinedBeginDateTime < DateTime.Now)
            {
                return "Begin date and time cannot be in the past.";
            }

            if (combinedEndDateTime <= combinedBeginDateTime)
            {
                return "End date and time cannot be before begin date and time.";
            }

            if (combinedEndDateTime - combinedBeginDateTime > TimeSpan.FromDays(1))
            {
                return "Event duration cannot exceed 24 hours.";
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
