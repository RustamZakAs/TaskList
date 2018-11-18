using System;
using TaskList;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime;
using System.Windows;
using System.Reflection;
using System.Data.Entity;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Serialization;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace TaskList
{
    [Serializable]
    public class MyTask : INotifyPropertyChanged
    {
        // стандартный конструктор без параметров
        public MyTask()
        { }

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

        private ObservableCollection<MyTask> tasksList = new ObservableCollection<MyTask>();
        public ObservableCollection<MyTask> OCTasksList { get => tasksList; set => Set(ref tasksList, value); }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            XmlDeSerialize();
        }

        private void MenuItemEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            XmlSerialize();
            Environment.Exit(0);
        }

        void XmlSerialize ()
        {
            // передаем в конструктор тип класса
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<MyTask>));

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("MyTask.xml", FileMode.Create))
            {
                formatter.Serialize(fs, OCTasksList);
            }
        }

        void XmlDeSerialize()
        {
            // передаем в конструктор тип класса
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<MyTask>));
            // десериализация
            using (FileStream fs = new FileStream("MyTask.xml", FileMode.OpenOrCreate))
            {
                OCTasksList = (ObservableCollection<MyTask>)formatter.Deserialize(fs);
            }
        }

        private void ItemDoneClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void FontUpClick(object sender, RoutedEventArgs e)
        {
            SizeTop++;
        }

        private void FontDownClick(object sender, RoutedEventArgs e)
        {
            SizeTop--;
        }

        private void ClickCategoryColorChenged(object sender, RoutedEventArgs e)
        {
            OCTasksList[listBox.SelectedIndex].TaskCategoryColor = FromName(((MenuItem)sender).Header.ToString());
        }

        public static Color FromName(String name)
        {
            var color_props = typeof(Colors).GetProperties();
            foreach (var c in color_props)
                if (name.Equals(c.Name, StringComparison.OrdinalIgnoreCase))
                    return (Color)c.GetValue(new Color(), null);
            return Colors.Transparent;
        }

        private void ItemDelete(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex >= 0)
                OCTasksList.Remove(OCTasksList[listBox.SelectedIndex]);
        }

        private void ItemAdd(object sender, RoutedEventArgs e)
        {
            MyTask tasks = new MyTask();
            tasks.TaskPriority = 0;
            tasks.TaskCategoryColor = Colors.Gray;
            tasks.TaskText = "";
            tasks.TaskStart = DateTime.Now;
            tasks.TaskDone = false;
            tasks.TaskEnd = DateTime.Parse("01.01.2050");
            OCTasksList.Add(tasks);
        }
    }
}



 
namespace SQLiteApp
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<MyTask> MyTasks { get; set; }
    }
}
/*
public static class Colorss
{
    private static readonly Dictionary<string, Color> dictionary =
        typeof(Color).GetProperties(BindingFlags.Public |
                                    BindingFlags.Static)
                     .Where(prop => prop.PropertyType == typeof(Color))
                     .ToDictionary(prop => prop.Name,
                                   prop => (Color)prop.GetValue(null, null));

    public static Color FromName(string name)
    {
        // Adjust behaviour for lookup failure etc
        return dictionary[name];
    }
}
*/
