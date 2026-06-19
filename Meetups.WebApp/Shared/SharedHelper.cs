using Microsoft.AspNetCore.Components;

namespace Meetups.WebApp.Shared
{
    public class SharedHelper
    {
        private readonly NavigationManager navigationManager;

        public SharedHelper(NavigationManager navigationManager) 
        {
            this.navigationManager = navigationManager;
        }

        public List<string> GetCategories()
        {
            return Enum.GetNames(typeof(Shared.MeetupCategoriesEnum)).ToList();
        }

        public string GetQueryParamValue(string paramName)
        {
            var uri = new Uri(navigationManager.Uri);
            var queryParams = System.Web.HttpUtility.ParseQueryString(uri.Query);
            return queryParams[paramName] ?? "";
        }
    }
}
