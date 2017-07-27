using System;
using System.Collections.Generic;
using MicrosoftDesk.Services;
using MicrosoftHelpDesk.Models;
using Plugin.Media;
using Xamarin.Forms;

namespace MicrosoftHelpDesk.Views
{
    public partial class IssuePage : ContentPage
    {

        private String path;

        public IssuePage()
        {
            InitializeComponent();
            locationPicker.ItemsSource = new List<String>
            {
                "Basement", "1st Floor", "2nd Floor", "3rd Floor", "Terrace"
            };
            sublocationPicker.ItemsSource = new List<String>
            {
                "Conference Room", "Elevator", "Gym", "Kitchen",
                "Resource Room 1", "Resource Room 2",
                "Restroom",
                "Work Room 1","Work Room 2", "Working Room 3"
            };
        }

        public async void onAddClick(object sender, EventArgs e)
        {
            Entry[] entries = new Entry[]{
                nameEntry, priorityEntry,itemEntry, phoneEntry, subjectEntry, descriptionEntry
            };

            Picker[] pickers = new Picker[]{
                locationPicker, sublocationPicker
            };

            Boolean empty = false;

            foreach (Entry entry in entries)
            {
                if (entry.Text == null)
                {
                    empty = true;
                    await DisplayAlert("Entry Empty", "Please fill the entry", "Ok");
                    break;
                }
            }
            if (empty == false)
            {
                foreach (Picker picker in pickers)
                {
                    if (picker.SelectedItem == null)
                    {
                        empty = true;
                        await DisplayAlert("Picker Empty", "Please fill the picker", "Ok");
                        break;
                    }
                }
            }

            if (empty == false)
            {
                indicator.IsRunning = true;
                indicator.IsVisible = true;

                var azureService = new AzureService();

                if (path == null)
                {
                    await azureService.InsertRequest(
                    new Request
                    {
                        Name = nameEntry.Text,
                        Priority = priorityEntry.Text,
                        Location = locationPicker.SelectedItem.ToString(),
                        Sublocation = sublocationPicker.SelectedItem.ToString(),
                        Item = itemEntry.Text,
                        AccessiblePhone = phoneEntry.Text,
                        Subject = subjectEntry.Text,
                        Description = descriptionEntry.Text,
                    });
                }

                else
                {
                    var url = await BlobManager.Instance.UploadFileAsync(path);

                    await azureService.InsertRequest(
                    new Request
                    {
                        Name = nameEntry.Text,
                        Priority = priorityEntry.Text,
                        Location = locationPicker.SelectedItem.ToString(),
                        Sublocation = sublocationPicker.SelectedItem.ToString(),
                        Item = itemEntry.Text,
                        Photo = url.ToString(),
                        AccessiblePhone = phoneEntry.Text,
                        Subject = subjectEntry.Text,
                        Description = descriptionEntry.Text,
                    });
                }

                indicator.IsRunning = false;
                indicator.IsVisible = false;

                await Navigation.PopAsync();
            }
        }

        public async void onTakePhoto(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable ||
                !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Error", "There is a problem in your camera!", "Ok");
                return;
            }

            var mediaFile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "photo.jpg"
            });

            if (mediaFile == null)
            {
                return;
            }

            path = mediaFile.Path;

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = mediaFile.GetStream();
                mediaFile.Dispose();
                return stream;
            });

        }

        public async void onPickPhoto(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", "There is a problem in picking photo!", "Ok");
                return;
            }

            var mediaFile = await CrossMedia.Current.PickPhotoAsync();

            if (mediaFile == null)
            {
                return;
            }

            path = mediaFile.Path;
            image.Source = ImageSource.FromStream(() =>
            {
                var stream = mediaFile.GetStream();
                mediaFile.Dispose();
                return stream;
            });
        }
    }
}
