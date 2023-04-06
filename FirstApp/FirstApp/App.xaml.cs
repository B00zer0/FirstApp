using System;
using Xamarin.Forms;
using FirstApp.Data;
using System.IO;

namespace FirstApp
{
    public partial class App : Application
    {
        static ContainersDB containersDB;
        static CategoriesDB categoriesDB;
        static ActionsDB actionsDB;
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

        public static ActionsDB ActionsDB
        {
            get
            {
                if (actionsDB == null)
                {
                    actionsDB = new ActionsDB(
                        Path.Combine(Environment.GetFolderPath
                        (Environment.SpecialFolder.LocalApplicationData), "ActionsDatabase.db3"));
                }
                return actionsDB;
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
