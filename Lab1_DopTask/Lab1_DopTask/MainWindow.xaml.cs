using Lab1_DopTask.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;


namespace Lab1_DopTask
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<BitmapImage> pictures =new List<BitmapImage>();
        
        int pictureIndex = 0;
        public MainWindow()
        {
            InitializeComponent();
           

        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> files = GetAllFilesFromFolders();

           
            foreach(string pic in files)
            {
                try
                {
                    pictures.Add(new BitmapImage(new Uri(pic)));
                }
                catch
                {
                        
                }
            }
            if(pictures.Count != 0)
            {
                SetImages(pictureIndex, pictures);
                forwardButton.IsEnabled = true;
                backButton.IsEnabled = true;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("В папке нет картинок", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void forwardButton_Click(object sender, RoutedEventArgs e)
        {
            pictureIndex += 3;
            SetImages(pictureIndex, pictures);

        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            pictureIndex -= 3;
            while(pictureIndex<0)
            {
                pictureIndex+=pictures.Count;
            }

            SetImages(pictureIndex,pictures);

        }


        public void SetImages(int index,List<BitmapImage> pics)
        {
            picture1.Source = pictures[index % pics.Count];
            picture2.Source = pictures[(index + 1) % pics.Count];
            picture3.Source = pictures[(index + 2) % pics.Count];
        }

        public List<string> GetAllFilesFromFolders()
        {
            List<string> ways = new List<string>();

            while (true)
            {
                try
                {
                    FolderBrowserDialog folder = new FolderBrowserDialog();

                    if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
                        !string.IsNullOrWhiteSpace(folder.SelectedPath))
                    {
                        ways= new DirectoryInfo(folder.SelectedPath)
                            .GetFiles("*.*", SearchOption.AllDirectories)
                            .OrderBy(x => x.Name)
                            .Select(f => f.FullName)
                            .ToList();

                    }
                    if(ways.Count()==0)
                    {
                        System.Windows.Forms.MessageBox.Show("Папка пуста", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }
                    break;
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("Нет доступа к папке", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }


            return ways;

        }
    }
}
