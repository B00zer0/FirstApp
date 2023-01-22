using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

namespace FirstApp.Views
{
    public partial class CalcPage : ContentPage
    {
        
        private double _container;
        private double _mass;
        private int _numOfContainers;
        private string _result;


        public CalcPage()
        {
            InitializeComponent();

        }

        private void ContainerContentCounting()
        {
            _container = Convert.ToDouble(ContainerB.Text);
            _mass = Convert.ToDouble(Mass.Text);
            _numOfContainers = Convert.ToInt32(numofcontainers.Text);
            _result = Convert.ToString(_mass - _container * _numOfContainers);
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
