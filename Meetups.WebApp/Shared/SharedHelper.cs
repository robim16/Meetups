namespace Meetups.WebApp.Shared
{
    public class SharedHelper
    {
        public List<string> GetCategories()
        {
            return Enum.GetNames(typeof(Shared.MeetupCategoriesEnum)).ToList();
        }
    }
}
