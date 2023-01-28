using FirstApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace FirstApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditingPage : ContentPage
    {
        
        public EditingPage()
        {
            InitializeComponent();
            
        }
        protected override async void OnAppearing()
        {
            try
            {

                collectionView.ItemsSource = await ContainersDB.GetContainersAsync();
            }
            catch(Exception ex) { _ = ex; }
            base.OnAppearing();
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}