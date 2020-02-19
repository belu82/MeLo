using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;
using MeLo.Domain;
using MeLo.Core;
using MeLo.DAL;
using System.Collections.ObjectModel;

namespace MeLo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ObservableCollection<Directory> myDirectories;

        public MainWindow()
        {
            InitializeComponent();
            DirectoryDAL directoryService = new DirectoryDAL();
            myDirectories = new ObservableCollection<Directory>(directoryService.GetAll());
            this.directories.ItemsSource = myDirectories;

        }



        private void AddDirectory(object sender, RoutedEventArgs e)
        {
            if (CommonFileDialog.IsPlatformSupported)
            {
                var folderSelectorDialog = new CommonOpenFileDialog();
                folderSelectorDialog.EnsureReadOnly = true;
                folderSelectorDialog.IsFolderPicker = true;
                folderSelectorDialog.AllowNonFileSystemItems = false;
                folderSelectorDialog.Multiselect = false;
                folderSelectorDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                folderSelectorDialog.Title = "Project Location";

                //If user selected a folder
                if (folderSelectorDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    string path = folderSelectorDialog.FileName;
                    string[] splitted = path.Split('\\');
                    string name = splitted[splitted.Length - 1];

                    var directoryDal = new DirectoryDAL();
                    Directory directory = new Directory
                    {
                        name = name,
                        path = path
                    };
                    var lastid = directoryDal.Insert(directory);


                    /*
                    dir = directoryService.Add(dir);
                    MainWindow.fileMonitors.Add(new SimpleFileInputMonitor(dir.Path));
                    SimpleFileSearcher fileSearcher = new SimpleFileSearcher(dir, new Extensions(), musicService, videoService, imageService);
                    myDirectories = new ObservableCollection<Directory>(directoryService.GetAll());
                    this.directories.ItemsSource = myDirectories;
                    */
                }
            }
        }
    }
}
