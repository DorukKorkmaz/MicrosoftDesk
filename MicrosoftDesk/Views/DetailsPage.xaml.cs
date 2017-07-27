using System;
using System.Collections.Generic;
using MicrosoftHelpDesk.Models;
using Xamarin.Forms;

namespace MicrosoftDesk.Views
{
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage(Request request)
        {
            InitializeComponent();
            BindingContext = request;
            if (request.Photo != null) image.Source = request.Photo;
        }
    }
}
