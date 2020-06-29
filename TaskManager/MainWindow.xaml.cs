using System;
using System.ComponentModel;
using System.Windows;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager
{
     /// <summary>
     /// Interaction logic for MainWindow.xaml
     /// </summary>
     public partial class MainWindow : Window
     {
          private readonly string PATH = $"{Environment.CurrentDirectory}\\TaskData.text";
          private BindingList<TaskModel> _taskData;
          private FileWorkService _fileWorkService;
          public MainWindow()
          {
               InitializeComponent();
          }

          private void Window_Loaded(object sender, RoutedEventArgs e)
          {
               _fileWorkService = new FileWorkService(PATH);
               try
               {
                    _taskData = _fileWorkService.LoadData();
               }
               catch (Exception ex)
               {
                    MessageBox.Show(ex.Message);
                    this.Close();
               }
               dgTaskManager.ItemsSource = _taskData;
               _taskData.ListChanged += TaskData_ListChanged; 
          }


          private void TaskData_ListChanged(object sender, ListChangedEventArgs e)
          {
               if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
               {
                    try
                    {
                         _fileWorkService.SaveData(sender);
                    }
                    catch (Exception ex)
                    {
                         MessageBox.Show(ex.Message);
                         Close();
                    }

               }
          }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
               var items = (BindingList<TaskModel>)dgTaskManager.ItemsSource;
               items.Clear();
               try
               {
                    _fileWorkService.SaveData(items);
               }
               catch (Exception ex)
               {
                    MessageBox.Show(ex.Message);
                    Close();
               }


          }
    }
}