using FirstApp.Data;
using FirstApp.Models;
using System;
using Xamarin.Forms;



namespace FirstApp.Views
{
    
    public partial class EditingPage : ContentPage
    {
        
        private Container _container;

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
                collectionView.ItemsSource = await App.ContainersDB.GetContainersAsync();
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _container = collectionView.SelectedItem as Container;
        }

        private async void DeleteBtn_Clicked(object sender, EventArgs e)
        {
                await App.ContainersDB.DeleteContainerAsync(_container);
                collectionView.ItemsSource = await App.ContainersDB.GetContainersAsync();
        }

    }
}