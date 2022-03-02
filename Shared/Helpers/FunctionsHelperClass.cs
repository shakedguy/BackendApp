using System.Text;
using System.Text.Json;
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

        public string GetRoute() =>
            NavigationManager.Uri.Replace(NavigationManager.BaseUri, "").Split('/', StringSplitOptions.RemoveEmptyEntries)[0];
        public string GenerateTitle(string i_Route)
        {
            string temp = i_Route.Split('/', StringSplitOptions.RemoveEmptyEntries)[0].ToLower();
            return new StringBuilder(temp[0].ToString().ToUpper() + temp.Substring(1)).ToString();
        }

        public string GetId(string i_Uri) => i_Uri.Split('/')[^1];

        public string GetId() => NavigationManager.Uri.Split('/')[^1];

        public bool DataGridFilter(string i_Input, IModel i_Item)
        {
            if (string.IsNullOrWhiteSpace(i_Input))
                return true;
            if (i_Item.Id.ToString().StartsWith(i_Input.Trim(), StringComparison.OrdinalIgnoreCase))
                return true;
            if (i_Item is MessageModel)
                return filterMessage(i_Input, i_Item as MessageModel);
            if (i_Item is DispatchModel)
                return filterDispatcher(i_Input, i_Item as DispatchModel);
            return false;
        }

        private bool filterDispatcher(string i_Input, DispatchModel i_Dispatcher)
        {
            if (i_Dispatcher.AgentId.ToString().StartsWith(i_Input.Trim(), StringComparison.OrdinalIgnoreCase))
                return true;
            if (i_Dispatcher.MessageId.ToString().StartsWith(i_Input.Trim(), StringComparison.OrdinalIgnoreCase))
                return true;
            if (i_Dispatcher.TimeStamp.ToString().StartsWith(i_Input.Trim(), StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        private bool filterMessage(string i_Input, MessageModel i_Message)
        {
            if (i_Message.Content.StartsWith(i_Input.Trim(), StringComparison.OrdinalIgnoreCase))
                return true;
            if (i_Message.DivisionId.ToString().StartsWith(i_Input.Trim(), StringComparison.OrdinalIgnoreCase))
                return true;
            if (i_Message.Done.ToString().StartsWith(i_Input.Trim(), StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        public void NavigateToGenericPage(bool i_Reload = false) =>
        NavigationManager.NavigateTo(
                    GetRoute(NavigationManager.Uri, NavigationManager.BaseUri), i_Reload);

        public void NavigateToItemPage(int i_Id) =>
            NavigationManager.NavigateTo(
                GetRoute(NavigationManager.Uri, NavigationManager.BaseUri) + $"/{i_Id}");

        public void Error() => NavigationManager.NavigateTo("error", true);
        public IEnumerable<T> Deserialize<T>(dynamic i_Object) =>
            JsonSerializer.Deserialize<IEnumerable<T>>(i_Object);

    }
}
