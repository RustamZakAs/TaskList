using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace TaskList
{
    public class Tasks : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void Set<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName]string prop = "")
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private int taskId;
        public int TaskId { get => taskId; set => Set(ref taskId, value); }

        private Color taskCategoryColor;
        public Color TaskCategoryColor { get => taskCategoryColor; set => Set(ref taskCategoryColor, value); }

        private int taskPriority;
        public int TaskPriority { get => taskPriority; set => Set(ref taskPriority, value); }

        private string taskText;
        public string TaskText { get => taskText; set => Set(ref taskText, value); }

        private bool taskDone;
        public bool TaskDone { get => taskDone; set => Set(ref taskDone, value); }

        private DateTime taskStart;
        public DateTime TaskStart { get => taskStart; set => Set(ref taskStart, value); }

        private DateTime taskEnd;
        public DateTime TaskEnd { get => taskEnd; set => Set(ref taskEnd, value); }
    }

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void Set<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName]string prop = "")
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private int sizeTop = 11;
        public int SizeTop { get => sizeTop; set => Set(ref sizeTop, value); }

        private ObservableCollection<Tasks> tasksList = new ObservableCollection<Tasks>();
        public ObservableCollection<Tasks> OCTasksList { get => tasksList; set => Set(ref tasksList, value); }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Tasks tasks = new Tasks();
            tasks.TaskPriority = 99;
            tasks.TaskCategoryColor = Colors.Black;
            tasks.TaskText = "Test text";
            tasks.TaskStart = DateTime.Now;
            tasks.TaskDone = false;
            tasks.TaskEnd = DateTime.Parse("01.01.2050");
            OCTasksList.Add(tasks);
        }

        private void MenuItemEdit_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            MessageBox.Show(menuItem.Header.ToString());

            ListBoxItem listBox = new ListBoxItem();
            listBox.Content = "text";
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ItemDoneClick(object sender, RoutedEventArgs e)
        {
            Tasks tasks = new Tasks();
            tasks.TaskPriority = 99;
            tasks.TaskCategoryColor = Colors.Black;
            tasks.TaskText = "Test text";
            tasks.TaskStart = DateTime.Now;
            tasks.TaskDone = false;
            tasks.TaskEnd = DateTime.Parse("01.01.2050");
            //OCTasksList = new ObservableCollection<Tasks>();
            OCTasksList.Add(tasks);
        }

        private void FontUpClick(object sender, RoutedEventArgs e)
        {
            SizeTop++;
        }

        private void FontDownClick(object sender, RoutedEventArgs e)
        {
            SizeTop--;
        }
    }
}
