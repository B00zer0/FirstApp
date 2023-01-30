using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FirstApp.Data;
using System.IO;

namespace FirstApp
{
    public partial class App : Application
    {
        static ContainersDB containersDB;
        static CategoriesDB categoriesDB;

        public static ContainersDB ContainersDB
        {
            get           
            {
               if(containersDB == null)
                {
                    containersDB = new ContainersDB(
                        Path.Combine(Environment.GetFolderPath
                        (Environment.SpecialFolder.LocalApplicationData), "ContainersDatabase.db3"));
                } 
               return containersDB;
            }
        }
        public static CategoriesDB CategoriesDB
        {
            get           
            {
               if(categoriesDB == null)
                {
                    categoriesDB = new CategoriesDB(
                        Path.Combine(Environment.GetFolderPath
                        (Environment.SpecialFolder.LocalApplicationData), "CategoriesDatabase.db3"));
                } 
               return categoriesDB;
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
