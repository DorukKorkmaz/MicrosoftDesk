using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicrosoftDesk.Services;
using MicrosoftDesk.Views;
using MicrosoftHelpDesk.Models;
using Xamarin.Forms;

namespace MicrosoftHelpDesk.Views
{
    public partial class RequestsPage : ContentPage
    {
        public RequestsPage()
        {
            InitializeComponent();

            var azureService = new AzureService();

            listView.BindingContext = azureService.GetRequests();
        }

        public async void onItemNewRequest(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IssuePage());
        }

        public void onItemSelected(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            if (list.SelectedItem != null)
            {
                Navigation.PushAsync(new DetailsPage(list.SelectedItem as Request));
            }
            list.SelectedItem = null;
        }

        public async void onItemDelete(object sender, EventArgs e)
        {
            indicator.IsVisible = true;
            indicator.IsRunning = true;
            var selectedMenuItem = (MenuItem)sender;
            var selectedItem = (Request)selectedMenuItem.BindingContext;
            var azureService = new AzureService();
            await azureService.DeleteTaskAsync(selectedItem);
            listView.BindingContext = await azureService.GetRequests();
            indicator.IsVisible = false;
            indicator.IsRunning = false;
        }

        protected async override void OnAppearing()
        {
            var azureService = new AzureService();
            var list = await azureService.GetRequests();
            listView.BindingContext = list;
        }
    }
}
