﻿using FirstApp.Data;
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
            if(!String.IsNullOrWhiteSpace(container.Text) & !String.IsNullOrWhiteSpace(container.MassOfContainer))
            {
                await App.ContainersDB.SaveContainerAsync(container);
                await Shell.Current.GoToAsync("..");
                collectionView.ItemsSource = await App.ContainersDB.GetContainersAsync();
                BindingContext = new Container();
            }
        }

        private async void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            _containerId = (item.CommandParameter as Container).ID;
            Container container = await App.ContainersDB.GetContainerAsync(_containerId);
            if(container != null)
            {
                await App.ContainersDB.DeleteContainerAsync(container);
                collectionView.ItemsSource = await App.ContainersDB.GetContainersAsync();
            }
        }



        private async void EditingBtn_Clicked(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            _containerId = (item.CommandParameter as Container).ID;
            Container container = await App.ContainersDB.GetContainerAsync(_containerId);
            if(container != null)
            {
                BindingContext = container;
            }
        }
    }
}