using FirstApp.Data;
using FirstApp.Models;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace FirstApp.Views
{
    
    public partial class CategoryPage : ContentPage
    {

        private int _categoryId;

        public CategoryPage()
        {
            InitializeComponent();
            BindingContext = new Category();
        }


        protected override async void OnAppearing()
        {
            collectionView.ItemsSource = await App.CategoriesDB.GetCategoriesAsync();
            base.OnAppearing();
        }

        private async void AddBtn_Clicked(object sender, EventArgs e)
        {
            Category category = (Category)BindingContext;
            await App.CategoriesDB.SaveCategoryAsync(category);
            await Shell.Current.GoToAsync("..");
            collectionView.ItemsSource = await App.CategoriesDB.GetCategoriesAsync();
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Category category = (Category)e.CurrentSelection.FirstOrDefault();
                
                _categoryId = category.ID;
            }
        }

        private async void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            if (_categoryId != -1)
            {
                Category category = await App.CategoriesDB.GetCategoryAsync(_categoryId);
                await App.CategoriesDB.DeleteCategoryAsync(category);
                collectionView.ItemsSource = await App.CategoriesDB.GetCategoriesAsync();
            }
        }
    }
}