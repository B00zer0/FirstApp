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
using static SQLite.SQLite3;
using Xamarin.Forms.Internals;
using System.Data.Common;

namespace FirstApp.Views
{
    
    public partial class CalcPage : ContentPage
    {
        
        private double _container;
        private double _totalMass;
        private int _numOfContainers;
        private string _result;
        private Category category;
        private Container container;
        private CategoryAction categoryAction;
        private readonly int deadLine = 30; //Кол-во обектов в истории, при достижении которого начинается ее отчистка
        private readonly int saveRange = 10; //Кол-во последних объектов, которые мы хотим оставить, после частичной отчистки истории 
        private int lowId; //Id после которого начинается удаление объектов из истории
        private int allRows; //Все строки в истории
        public CalcPage()
        {
            InitializeComponent();
            BindingContext = new Container();
        }

        protected override async void OnAppearing()
        {
            MyPicker.ItemsSource = await App.ContainersDB.GetContainersAsync();
            SavePicker.ItemsSource = await App.CategoriesDB.GetCategoriesAsync();
            History.ItemsSource = await App.ActionsDB.GetCategoryActions();
            base.OnAppearing();   
        }

        private void MyPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            container = (Container)MyPicker.SelectedItem;
        }

        // Подсчет содержимого контейнера/банки
        private void ContainerContentCounting()
        {
            if(container != null)
            {
                _container = Convert.ToDouble(container.MassOfContainer);
            }
            _totalMass = Convert.ToDouble(Mass.Text);
            _numOfContainers = Convert.ToInt32(numofcontainers.Text);
            _result = Convert.ToString(_totalMass - _container * _numOfContainers);
            if (_totalMass < _container * _numOfContainers)
            {
                DisplayAlert("Превышение допустимой массы контейнера(-ов)", "Вес пустого(-ых) контейнера(-ов) не может превышать общий вес", "OK");
                _result = "0";
            }
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

        private void SavePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            category = (Category)SavePicker.SelectedItem;
            if (SavePicker.SelectedIndex >= 0)
            {
                BtnAdd.IsEnabled = true;
                BtnSave.IsEnabled = true;
            }
            else
            {
                BtnSave.IsEnabled = false;
                BtnAdd.IsEnabled = false;
            }
        }

        // Сохранение/добавление результата в категорию
        private async void AddInCategory(bool isSave)
        {


            categoryAction = new CategoryAction
            {
                OldMass = category.CategoryMass
            };
            

            if (isSave && category != null) 
            {
                categoryAction.Action = $"{category.Name}({category.CategoryMass}) = {result_output.Text}";     
                await App.ActionsDB.SaveCategoryAction(categoryAction);
                History.ItemsSource = await App.ActionsDB.GetCategoryActions();     //Если нажата кнопка сохранения в катеорию,
                category.CategoryMass = result_output.Text;                         //то записываем результат вычислений
                await App.CategoriesDB.SaveCategoryAsync(category);                 //вместо предыдущего и делаем запись в историю


            }
            else if (category != null) 
            {
                category.CategoryMass = Convert.ToString(Convert.ToDouble(result_output.Text)           //В обратном случае -
                                                         + Convert.ToDouble(category.CategoryMass));    //прибавляем результат к массе в категории
                await App.CategoriesDB.SaveCategoryAsync(category);                                     //Также отмечая это в истории        
                categoryAction.Action = $"{category.Name}({categoryAction.OldMass} +" +
                                        $" {result_output.Text}) = {category.CategoryMass}";
                await App.ActionsDB.SaveCategoryAction(categoryAction);
                History.ItemsSource = await App.ActionsDB.GetCategoryActions();
            }

            allRows = await App.ActionsDB.GetAllRows();
            lowId = categoryAction.Id - saveRange;
            //Чистим историю при достижении предела, оставляя часть последних действий
            if (allRows >= deadLine)
            {
                await App.ActionsDB.DeleteAllActions(lowId);
            }
        }
        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            AddInCategory(true);
        }

        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            AddInCategory(false);
        }
    }
}
