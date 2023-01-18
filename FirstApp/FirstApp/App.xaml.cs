using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FirstApp.Data;
namespace FirstApp
{
    public partial class App : Application
    {
        static ContainersDB containersDB;

        public static ContainersDB ContainersDB
        {
            get           
            {
               if(containersDB == null)
                {
                    containersDB = new ContainersDB()
                } 
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
