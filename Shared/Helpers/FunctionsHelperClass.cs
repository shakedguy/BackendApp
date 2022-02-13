using System.Text;
using BackendApp.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace BackendApp.Shared.Helpers
{
    public class FunctionsHelperClass
    {
        public NavigationManager NavigationManager { get; set; }

        public FunctionsHelperClass(NavigationManager i_NavigationManager)
            => this.NavigationManager = i_NavigationManager;
        public string GetRoute(string i_Uri, string i_UriBase) =>
            i_Uri.Replace(i_UriBase, "").Split('/', StringSplitOptions.RemoveEmptyEntries)[0];

        public string GenerateTitle(string i_Route)
        {
            string temp = i_Route.Split('/', StringSplitOptions.RemoveEmptyEntries)[0].ToLower();
            return new StringBuilder(temp[0].ToString().ToUpper() + temp.Substring(1)).ToString();
        }

        public string GetId(string i_Uri) => i_Uri.Split('/')[^1];

        public bool DataGridFilter(string i_Input, IModel i_Item)
        {
            if (string.IsNullOrWhiteSpace(i_Input))
                return true;
            if (i_Item.Id.ToString().Contains(i_Input.Trim(), StringComparison.OrdinalIgnoreCase))
                return true;
            if (i_Item is MessageModel)
            {
                var message = i_Item as MessageModel;
                if (message.Content.Contains(i_Input.Trim(), StringComparison.OrdinalIgnoreCase))
                    return true;
                if (message.DivisionId.ToString().Contains(i_Input.Trim(), StringComparison.OrdinalIgnoreCase))
                    return true;
                if (message.Done.ToString().Contains(i_Input.Trim(), StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        public void NavigateToGenericPage() =>
            NavigationManager.NavigateTo(
                GetRoute(NavigationManager.Uri, NavigationManager.BaseUri));

        public void NavigateToItemPage(int i_Id) =>
            NavigationManager.NavigateTo(
                GetRoute(NavigationManager.Uri, NavigationManager.BaseUri) + $"/{i_Id}");
    }
}
