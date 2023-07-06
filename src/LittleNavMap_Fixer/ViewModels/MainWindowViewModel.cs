using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LittleNavMap_Fixer.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private string _msfsFolder;
        private bool _overrideOriginals;
        private string _totalFixedFiles;

        public string TotalFixedFiles
        {
            get { return _totalFixedFiles; }
            set
            {
                _totalFixedFiles = value;

                NotifyPropertyChanged("TotalFixedFiles");
            }
        }


        public bool OverrideOriginals
        {
            get { return _overrideOriginals; }
            set
            {
                _overrideOriginals = value;
                NotifyPropertyChanged("OverrideOriginals");
            }
        }

        private ICommand _folderSelectCommand;
        private ICommand _applyCorrectionCommand;

        public ICommand ApplyCorrectionCommand
        {
            get { return _applyCorrectionCommand; }
            set { _applyCorrectionCommand = value; }
        }


        public ICommand FolderSelectCommand
        {
            get { return _folderSelectCommand; }
            set { _folderSelectCommand = value; }
        }


        public string MsfsFolder
        {
            get { return _msfsFolder; }
            set
            {
                _msfsFolder = value;
                NotifyPropertyChanged("MsfsFolder");
            }
        }

        public MainWindowViewModel()
        {
            FolderSelectCommand = new RelayCommand(new Action<object>(FolderSelect));
            ApplyCorrectionCommand = new RelayCommand(new Action<object>(ApplyCorrection));
            TotalFixedFiles = string.Empty;
        }

        public void FolderSelect(object sender)
        {
            var dialog = new VistaFolderBrowserDialog();
            dialog.Description = "Please select a folder.";
            dialog.UseDescriptionForTitle = true; // This applies to the Vista style dialog only, not the old dialog.

            if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
            {
                MessageBox.Show("Because you are not using Windows Vista or later, the regular folder browser dialog will be used. Please use Windows Vista to see the new dialog.", "Sample folder browser dialog");
            }

            if ((bool)dialog.ShowDialog())
            {
                MsfsFolder = dialog.SelectedPath;

                //MessageBox.Show($"The selected folder was:{Environment.NewLine}{dialog.SelectedPath}", "Sample folder browser dialog");
            }
        }

        public void ApplyCorrection(object sender)
        {
            var files = Directory.GetFiles(MsfsFolder);

            int totalFiles = 0;

            foreach (var item in files)
            {
                if (PlnFixer.FixFlightPlan(item, OverrideOriginals))
                    totalFiles = totalFiles + 1;
            }

            TotalFixedFiles = "Total files fixed: " + totalFiles.ToString();
        }
    }
}
