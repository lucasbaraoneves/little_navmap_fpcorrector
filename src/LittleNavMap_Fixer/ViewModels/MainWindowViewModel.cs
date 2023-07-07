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
        private string _msfsFlightPlanFile;

        public string MsfsFlightPlanFile
        {
            get { return _msfsFlightPlanFile; }
            set
            {
                _msfsFlightPlanFile = value;
                NotifyPropertyChanged("MsfsFlightPlanFile");
            }
        }


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
        private ICommand _fileSelectCommand;

        public ICommand FileSelectCommand
        {
            get { return _fileSelectCommand; }
            set { _fileSelectCommand = value; }
        }

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
            FileSelectCommand = new RelayCommand(new Action<object>(FileSelect));
            TotalFixedFiles = string.Empty;
        }

        public void FileSelect(object sender)
        {
            // As of .Net 3.5 SP1, WPF's Microsoft.Win32.OpenFileDialog class still uses the old style
            VistaOpenFileDialog dialog = new VistaOpenFileDialog();
            dialog.Filter = "MSFS Flight Plan Files (*.pln)|*.pln";
            if (!VistaFileDialog.IsVistaFileDialogSupported)
                MessageBox.Show("Because you are not using Windows Vista or later, the regular open file dialog will be used. Please use Windows Vista to see the new dialog.", "Sample open file dialog");

            if ((bool)dialog.ShowDialog())
            {
                MsfsFlightPlanFile = dialog.FileName;
                MsfsFolder = string.Empty;
            }
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
                MsfsFlightPlanFile = string.Empty;
            }
        }

        public void ApplyCorrection(object sender)
        {
            if (!string.IsNullOrEmpty(MsfsFlightPlanFile))
            {
                PlnFixer.FixFlightPlan(MsfsFlightPlanFile, OverrideOriginals);
                TotalFixedFiles = "Total files fixed: 1";
            }

            if (!string.IsNullOrEmpty(MsfsFolder))
            {
                var baseFiles = Directory.GetFiles(MsfsFolder);

                var fileList = (from a in baseFiles where a.Contains(".pln") select a).ToList();

                int totalFiles = 0;

                foreach (var item in fileList)
                {
                    if (PlnFixer.FixFlightPlan(item, OverrideOriginals))
                        totalFiles = totalFiles + 1;
                }

                TotalFixedFiles = "Total files fixed: " + totalFiles.ToString();

            }
        }
    }
}
