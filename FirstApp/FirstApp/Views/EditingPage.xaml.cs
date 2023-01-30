using FirstApp.Data;
using FirstApp.Models;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace FirstApp.Views
{
    
    public partial class EditingPage : ContentPage
    {
        
        private int _containerId;

        public EditingPage()
        {
            InitializeComponent();
            BindingContext = new Container();
        }
        protected override async void OnAppearing()
        {
            collectionView.ItemsSource = await App.ContainersDB.GetContainersAsync();
            base.OnAppearing();
        }

        private async void AddBtn_Clicked(object sender, EventArgs e)
        {
            Container container = (Container)BindingContext;
            await App.ContainersDB.SaveContainerAsync(container);
            await Shell.Current.GoToAsync("..");
            collectionView.ItemsSource = await App.ContainersDB.GetContainersAsync();
        }

        private  void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection != null)
            {
                Container container = (Container)e.CurrentSelection.FirstOrDefault();
                _containerId = container.ID;
            }
        }

        private async void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            Container container = await App.ContainersDB.GetContainerAsync(_containerId);
            await App.ContainersDB.DeleteContainerAsync(container);
            collectionView.ItemsSource = await App.ContainersDB.GetContainersAsync();
        }

    }
}