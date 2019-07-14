/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

using AmpShell.AutoConfig;
using AmpShell.DAL;
using AmpShell.Enums;
using AmpShell.Models;
using AmpShell.Serialization;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Diagnostics.ViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace AmpShell.ViewModels
{
    public class PreferencesViewModel : ViewModelBase
    {
        public Preferences Model { get; private set; }

        private readonly Preferences _modelUndo;

        private bool _portableModeAvailable = false;

        public bool PortableModeAvailable
        {
            get => _portableModeAvailable;
            set => this.RaiseAndSetIfChanged(ref _portableModeAvailable, value);
        }

        private string _portableModeStatus = "";

        public string PortableModeStatus
        {
            get => _portableModeStatus;
            set => this.RaiseAndSetIfChanged(ref _portableModeStatus, value);
        }

        public List<string> LargeViewModeAvailableSizes { get; } = new List<string>(new[]
            {"48x48",
            "64x64",
            "80x80",
            "96x96",
            "112x112",
            "128x128",
            "144x144",
            "160x160",
            "176x176",
            "192x192",
            "208x208",
            "224x224",
            "240x240",
            "256x256" });

        private ObservableCollection<Category> _categories = new ObservableCollection<Category>();

        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set => this.RaiseAndSetIfChanged(ref _categories, value);
        }

        private bool _isDefaultViewModeDetails = false;

        public bool IsDefaultViewModeDetails
        {
            get => _isDefaultViewModeDetails;
            set => this.RaiseAndSetIfChanged(ref _isDefaultViewModeDetails, value);
        }

        private bool _isDefaultViewModeList = false;

        public bool IsDefaultViewModeList
        {
            get => _isDefaultViewModeList;
            set => this.RaiseAndSetIfChanged(ref _isDefaultViewModeList, value);
        }

        private bool _isDefaultViewModeLargeIcons = false;

        public bool IsDefaultViewModeLargeIcons
        {
            get => _isDefaultViewModeLargeIcons;
            set => this.RaiseAndSetIfChanged(ref _isDefaultViewModeLargeIcons, value);
        }

        private bool _isDefaultViewModeSmallIcons = false;

        public bool IsDefaultViewModeSmallIcons
        {
            get => _isDefaultViewModeSmallIcons;
            set => this.RaiseAndSetIfChanged(ref _isDefaultViewModeSmallIcons, value);
        }

        private bool _isDefaultViewModeTiles = false;

        public bool IsDefaultViewModeTiles
        {
            get => _isDefaultViewModeTiles;
            set => this.RaiseAndSetIfChanged(ref _isDefaultViewModeTiles, value);
        }

        public PreferencesViewModel() : base()
        {
            Model = ObjectSerializer.CloneObject(UserDataAccessor.UserData);
            _modelUndo = UserDataAccessor.UserData;
            Categories = new ObservableCollection<Category>(UserDataAccessor.GetAllCategories());
            CheckForPortableModeAvailability();

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == ViewMode.Details)
            {
                IsDefaultViewModeDetails = true;
            }

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == ViewMode.LargeIcon)
            {
                IsDefaultViewModeLargeIcons = true;
            }

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == ViewMode.List)
            {
                IsDefaultViewModeList = true;
            }

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == ViewMode.SmallIcon)
            {
                IsDefaultViewModeSmallIcons = true;
            }

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == ViewMode.Tile)
            {
                IsDefaultViewModeTiles = true;
            }
        }

        public void MoveCategoryTo(int startIndex, int newIndex)
        {
            Categories.Move(startIndex, newIndex);
        }

        public void SortCategoriesByName()
        {
            Categories.OrderBy(x => x.Title);
        }

        private void CheckForPortableModeAvailability()
        {
            if (FileFinder.HasWriteAccessToAssemblyLocationFolder() == false)
            {
                PortableModeAvailable = false;
                PortableModeStatus = "Portable Mode : unavailable (AmpShell cannot write in the folder where it is located).";
            }
            else
            {
                PortableModeAvailable = true;
                PortableModeStatus = "Portable Mode : available (but disabled).";
            }
        }

        public void DisablePortableMode()
        {
            Model.DBPath = _modelUndo.DBPath;
            Model.DBDefaultConfFilePath = _modelUndo.DBDefaultConfFilePath;
            Model.DBDefaultLangFilePath = _modelUndo.DBDefaultLangFilePath;
            Model.ConfigEditorPath = _modelUndo.ConfigEditorPath;
        }

        public void EnablePortableMode()
        {
            ScanForPortableModeTools();
        }

        public void ScanForPortableModeTools()
        {
            if (File.Exists(Path.Combine(PathFinder.GetStartupPath(), "\\dosbox.exe")))
            {
                Model.DBPath = Path.Combine(PathFinder.GetStartupPath(), "\\dosbox.exe");
            }
            else
            {
                Model.DBPath = "dosbox.exe isn't in the same directory as AmpShell.exe!";
            }

            if (Directory.GetFiles((PathFinder.GetStartupPath()), "*.conf").Length > 0)
            {
                Model.DBDefaultConfFilePath = Directory.GetFiles((PathFinder.GetStartupPath()), "*.conf")[0];
            }
            else
            {
                Model.DBDefaultConfFilePath = "No configuration file (*.conf) found in AmpShell's directory.";
            }

            if (Directory.GetFiles(PathFinder.GetStartupPath(), "*.lng").Length > 0)
            {
                Model.DBDefaultLangFilePath = Directory.GetFiles(PathFinder.GetStartupPath(), "*.lng")[0];
            }
            else
            {
                Model.DBDefaultLangFilePath = "No language file (*.lng) found in AmpShell's directory.";
            }

            if (File.Exists(Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "notepad.exe")))
            {
                Model.ConfigEditorPath = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "notepad.exe");
            }
            else if (File.Exists(Path.Combine(PathFinder.GetStartupPath(), "\\TextEditor.exe")))
            {
                Model.ConfigEditorPath = Path.Combine(PathFinder.GetStartupPath(), "\\TextEditor.exe");
            }
            else
            {
                Model.ConfigEditorPath = "No text editor (Notepad in Windows' directory, or TextEditor.exe in AmpShell's directory) found.";
            }

            PortableModeStatus = "Portable Mode : active (all files (or at least DOSBox, and all the games) must be in the same directory as AmpShell).";
        }

        public void BrowseForTextEditorPath()
        {
            OpenFileDialog textEdtiorOpenFileDialog = new OpenFileDialog()
            {
                Title = "Browsing for a text file editor...",
                AllowMultiple = false
            };

            if (string.IsNullOrWhiteSpace(Model.ConfigEditorPath) == false)
            {
                if (Directory.Exists(Path.GetDirectoryName(Model.ConfigEditorPath).ToString()))
                {
                    textEdtiorOpenFileDialog.InitialDirectory = Path.GetDirectoryName(Model.ConfigEditorPath).ToString();
                }
                else
                {
                    textEdtiorOpenFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                }
            }

            string filePath = textEdtiorOpenFileDialog.ShowAsync(null).Result.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(filePath) == false)
            {
                Model.ConfigEditorPath = filePath;
            }
        }

        public void BrowseForDOSBoxPath()
        {
            //OpenFileDialog dosBoxExePathOpenFileDialog = new OpenFileDialog
            //{
            //    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
            //    Title = "Browsing for DOSBox' executable file..."
            //};
            //if (dosBoxExePathOpenFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    //retrieve the selected dosbox.exe path into Amp.DBPath
            //    UserDataAccessor.UserData.DBPath = dosBoxExePathOpenFileDialog.FileName;
            //    Model.DBPath = dosBoxExePathOpenFileDialog.FileName;
            //}
            //else if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBPath))
            //{
            //    MessageBox.Show("Location of DOSBox's executable unknown. You will not be able to run games!", "Select DOSBox's executable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        public void BrowseForDefaultDOSBoxConfigFile()
        {
            //OpenFileDialog dosboxDefaultConfOpenFileDialog = new OpenFileDialog();
            //if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultConfFilePath) == false
            //    && Directory.Exists(Path.GetDirectoryName(UserDataAccessor.UserData.DBDefaultConfFilePath)))
            //{
            //    dosboxDefaultConfOpenFileDialog.InitialDirectory = Path.GetDirectoryName(UserDataAccessor.UserData.DBDefaultConfFilePath);
            //}

            //dosboxDefaultConfOpenFileDialog.Title = "Browsing for the default DOSBox config file...";
            //dosboxDefaultConfOpenFileDialog.Filter = "DOSBox configuration files (*.conf)|*.conf|All files|*";
            //if (dosboxDefaultConfOpenFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    UserDataAccessor.UserData.DBDefaultConfFilePath = dosboxDefaultConfOpenFileDialog.FileName;
            //    Model.DBDefaultConfFilePath = dosboxDefaultConfOpenFileDialog.FileName;
            //}
        }

        public void BrowseForDefaultDOSBoxLanguageFile()
        {
            //OpenFileDialog dosBoxDefaultLangOpenFileDialog = new OpenFileDialog();
            //if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultLangFilePath) == false
            //    && Directory.Exists(Path.GetDirectoryName(UserDataAccessor.UserData.DBDefaultLangFilePath)))
            //{
            //    dosBoxDefaultLangOpenFileDialog.InitialDirectory = Path.GetDirectoryName(UserDataAccessor.UserData.DBDefaultLangFilePath);
            //}

            //dosBoxDefaultLangOpenFileDialog.Title = "Browsing for the default DOSBox language file...";
            //dosBoxDefaultLangOpenFileDialog.Filter = "DOSBox language files (*.lng)|*.lng|All files|*";
            //if (dosBoxDefaultLangOpenFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    UserDataAccessor.UserData.DBDefaultLangFilePath = dosBoxDefaultLangOpenFileDialog.FileName;
            //    Model.DBDefaultLangFilePath = dosBoxDefaultLangOpenFileDialog.FileName;
            //}
        }

        public void BrowseForDefaultGamesDirectory()
        {
            //OpenFolderDialog gamesFolderBrowserDialog = new OpenFolderDialog();
            //if (gamesFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            //{
            //    Model.GamesDefaultDir = gamesFolderBrowserDialog.SelectedPath;
            //}
        }

        public void BrowseForDefaultCDImagesDirectory()
        {
            OpenFolderDialog cdImagesFolderBrowserDialog = new OpenFolderDialog();
            Model.CDsDefaultDir = cdImagesFolderBrowserDialog.ShowAsync(Application.Current.MainWindow).Result;
        }

        public void ApplyModifications()
        {
            UserDataAccessor.SetCategoriesOrder(Categories);
            if (IsDefaultViewModeDetails)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = ViewMode.Details;
            }
            if (IsDefaultViewModeLargeIcons)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = ViewMode.LargeIcon;
            }
            if (IsDefaultViewModeSmallIcons)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = ViewMode.SmallIcon;
            }
            if (IsDefaultViewModeList)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = ViewMode.List;
            }
            if (IsDefaultViewModeTiles)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = ViewMode.Tile;
            }
        }

        public void CancelModifications()
        {
            Model = _modelUndo;
        }

        public bool IsDataValid()
        {
            return string.IsNullOrWhiteSpace(Model.DBPath) == false && File.Exists(Model.DBPath);
        }
    }
}