using FirstApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using static Xamarin.Essentials.Permissions;
using FirstApp.Models;


namespace FirstApp.Views
{
    
    public partial class CalcPage : ContentPage
    {
        
        private double _container;
        private double _totalMass;
        private int _numOfContainers;
        private string _result;
        int _containerId;
        Container container;
        public CalcPage()
        {
            InitializeComponent();
            BindingContext = new Container();
        }

        protected override async void OnAppearing()
        {
            MyPicker.ItemsSource = await App.ContainersDB.GetContainersAsync();           
            base.OnAppearing();   
        }

        private async void MyPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            _containerId = MyPicker.SelectedIndex + 1;
            container = await App.ContainersDB.GetContainerAsync(_containerId);
        }

        private void ContainerContentCounting()
        {
            if(container != null)
            {
                _container = Convert.ToDouble(container.MassOfContainer);
            }
            _totalMass = Convert.ToDouble(Mass.Text);
            _numOfContainers = Convert.ToInt32(numofcontainers.Text);
            _result = Convert.ToString(_totalMass - _container * _numOfContainers);
        }

        private void BttResult_Clicked(object sender, EventArgs e)
        {
            ContainerContentCounting();
            result_output.Text = _result;

        }

        private void BtnPlus_Clicked(object sender, EventArgs e)
        {

            if (result_output.Text == "0")
            {
                ContainerContentCounting();
                result_output.Text = _result;
            }
            else
            {
                ContainerContentCounting();
                result_output.Text = Convert.ToString(Convert.ToDouble(result_output.Text) + Convert.ToDouble(_result));
            }
        }

        private void BtnClr_Clicked(object sender, EventArgs e)
        {
            result_output.Text = "0";
        }

    }
}
