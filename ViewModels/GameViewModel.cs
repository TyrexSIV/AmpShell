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
using AmpShell.Models;
using AmpShell.Serialization;

using Avalonia.Controls;
using Avalonia.Diagnostics.ViewModels;

using System.IO;

namespace AmpShell.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        public Game Model { get; private set; }

        private readonly Game _modelUndo;

        public GameViewModel() : base()
        {
            Model = new Game
            {
                NoConsole = UserDataAccessor.UserData.GamesNoConsole,
                InFullScreen = UserDataAccessor.UserData.GamesInFullScreen,
                QuitOnExit = UserDataAccessor.UserData.GamesQuitOnExit,
                AdditionalCommands = UserDataAccessor.UserData.GamesAdditionalCommands
            };
        }

        public GameViewModel(Game model)
        {
            Model = model;
            _modelUndo = ObjectSerializer.CloneObject(model);
        }

        public void BrowseForGameIcon()
        {
            OpenFileDialog iconOpenFileDialog = new OpenFileDialog();

            if (UserDataAccessor.UserData.PortableMode == true)
            {
                iconOpenFileDialog.InitialDirectory = PathFinder.GetStartupPath();
            }
            else if (string.IsNullOrWhiteSpace(Model.Icon) == false && Directory.Exists(Path.GetDirectoryName(Model.Icon)))
            {
                iconOpenFileDialog.InitialDirectory = Path.GetDirectoryName(Model.Icon);
            }
            else
            {
                iconOpenFileDialog.InitialDirectory = SearchFolderDialogStartDirectory();
            }

            //if (iconOpenFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    UpdateIconThumbnail(iconOpenFileDialog.FileName);
            //}
        }

        public void RemoveGameIcon()
        {
            Model.Icon = string.Empty;
        }

        public void CancelModifications()
        {
            Model = _modelUndo;
        }

        public void BrowseForGameLocation()
        {
            OpenFileDialog gameExeOpenFileDialog = new OpenFileDialog();
            if (UserDataAccessor.UserData.PortableMode == true)
            {
                gameExeOpenFileDialog.InitialDirectory = PathFinder.GetStartupPath();
            }
            else if (string.IsNullOrWhiteSpace(Model.DOSEXEPath) == false && Directory.Exists(Path.GetDirectoryName(Model.DOSEXEPath)))
            {
                gameExeOpenFileDialog.InitialDirectory = Path.GetDirectoryName(Model.DOSEXEPath);
            }
            else
            {
                gameExeOpenFileDialog.InitialDirectory = SearchFolderDialogStartDirectory();
            }

            gameExeOpenFileDialog.Title = "Browsing for the game's executable file...";
            //gameExeOpenFileDialog.Filter = "DOS executable files (*.bat;*.cmd;*.com;*.exe)|*.bat;*.cmd;*.com;*.exe;*.BAT;*.CMD;*.COM;*.EXE";
            //if (gameExeOpenFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    Model.DOSEXEPath = gameExeOpenFileDialog.FileName;
            //}
        }

        public void BrowseForGameDirectory()
        {
            OpenFolderDialog cMountFolderBrowserDialog = new OpenFolderDialog
            {
                Title = "Browsing for the directory mounted as C:..."
            };
            var result = cMountFolderBrowserDialog.ShowAsync(null).Result;
            if (string.IsNullOrWhiteSpace(result) == false)
            {
                Model.Directory = result;
            }
        }

        public void BrowseForCustomConfigurationFile()
        {
            OpenFileDialog customConfigOpenFileDialog = new OpenFileDialog();
            if (UserDataAccessor.UserData.PortableMode == true)
            {
                customConfigOpenFileDialog.InitialDirectory = PathFinder.GetStartupPath();
            }
            else if (string.IsNullOrWhiteSpace(Model.DBConfPath) == false && Directory.Exists(Path.GetDirectoryName(Model.DBConfPath)))
            {
                customConfigOpenFileDialog.InitialDirectory = Path.GetDirectoryName(Model.DBConfPath);
            }
            else
            {
                customConfigOpenFileDialog.InitialDirectory = SearchFolderDialogStartDirectory();
            }

            customConfigOpenFileDialog.Title = "Browsing for a custom DOSBox configuration file...";
            //customConfigOpenFileDialog.Filter = "DOSBox configuration file (*.conf)|*.conf;*.CONF";
            //if (customConfigOpenFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    Model.DBConfPath = customConfigOpenFileDialog.FileName;
            //}
        }

        internal void BrowseForSetupExe()
        {
            OpenFileDialog setupExeOpenFileDialog = new OpenFileDialog
            {
                Title = "Browsing for the setup executable...",
                //Filter = "DOS executable files (*.bat;*.com;*.exe)|*.bat;*.com;*.exe;*.BAT;*.COM;*.EXE"
            };
            if (UserDataAccessor.UserData.PortableMode == true)
            {
                setupExeOpenFileDialog.InitialDirectory = PathFinder.GetStartupPath();
            }
            else
            {
                setupExeOpenFileDialog.InitialDirectory = SearchFolderDialogStartDirectory();
            }

            //if (setupExeOpenFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    Model.SetupEXEPath = setupExeOpenFileDialog.FileName;
            //}
        }

        public void BrowseForCDImageFile()
        {
            OpenFileDialog cdImageOpenFileDialog = new OpenFileDialog
            {
                Title = "Browsing for a CD image file...",
                //Filter = "DOSBox compatible CD images (*.bin;*.cue;*.iso;*.img)|*.bin;*.cue;*.iso;*.img;*.BIN;*.CUE;*.ISO;*.IMG"
            };
            if (UserDataAccessor.UserData.PortableMode == true)
            {
                cdImageOpenFileDialog.InitialDirectory = PathFinder.GetStartupPath();
            }
            else if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.CDsDefaultDir) == false && Directory.Exists(UserDataAccessor.UserData.CDsDefaultDir))
            {
                cdImageOpenFileDialog.InitialDirectory = UserDataAccessor.UserData.CDsDefaultDir;
            }
            else
            {
                cdImageOpenFileDialog.InitialDirectory = SearchFolderDialogStartDirectory();
            }

            //if (cdImageOpenFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    Model.CDPath = cdImageOpenFileDialog.FileName;
            //}
        }

        public void BrowseForCDDirectory()
        {
            //FolderBrowserDialog cdDriveFlderBrowserDialog = new FolderBrowserDialog();
            //if (cdDriveFlderBrowserDialog.ShowDialog() == DialogResult.OK)
            //{
            //    Model.CDPath = cdDriveFlderBrowserDialog.SelectedPath;
            //}
        }

        public void BrowseForAlternateDOSBoxExecutable()
        {
            OpenFileDialog alternateDOSBoxExeOpenFileDialog = new OpenFileDialog();
            if (UserDataAccessor.UserData.PortableMode == true)
            {
                alternateDOSBoxExeOpenFileDialog.InitialDirectory = PathFinder.GetStartupPath();
            }
            else if (string.IsNullOrWhiteSpace(Model.AlternateDOSBoxExePath) == false && Directory.Exists(Path.GetDirectoryName(Model.AlternateDOSBoxExePath)))
            {
                alternateDOSBoxExeOpenFileDialog.InitialDirectory = Path.GetDirectoryName(Model.AlternateDOSBoxExePath);
            }
            else if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBPath) == false && Directory.Exists(Path.GetDirectoryName(UserDataAccessor.UserData.DBPath)))
            {
                alternateDOSBoxExeOpenFileDialog.InitialDirectory = UserDataAccessor.UserData.DBPath;
            }
            else
            {
                alternateDOSBoxExeOpenFileDialog.InitialDirectory = SearchFolderDialogStartDirectory();
            }

            alternateDOSBoxExeOpenFileDialog.Title = "Browsing for an alternate DOSBox executable...";
            //alternateDOSBoxExeOpenFileDialog.Filter = "DOSBox executable file (*.exe)|*.exe;*.EXE";
            //if (alternateDOSBoxExeOpenFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    Model.AlternateDOSBoxExePath = alternateDOSBoxExeOpenFileDialog.FileName;
            //}
        }

        private string SearchFolderDialogStartDirectory()
        {
            string initialDirectory = string.Empty;
            if (string.IsNullOrWhiteSpace(Model.DOSEXEPath) == false && Directory.Exists(Path.GetDirectoryName(Model.DOSEXEPath)))
            {
                initialDirectory = Path.GetDirectoryName(Model.DOSEXEPath);
            }
            else if (string.IsNullOrWhiteSpace(Model.Directory) == false && Directory.Exists(Model.Directory))
            {
                initialDirectory = Model.Directory;
            }
            else if (string.IsNullOrWhiteSpace(Model.SetupEXEPath) == false && Directory.Exists(Path.GetDirectoryName(Model.SetupEXEPath)))
            {
                initialDirectory = Path.GetDirectoryName(Model.SetupEXEPath);
            }
            else if (string.IsNullOrWhiteSpace(Model.Icon) == false && File.Exists(Model.Icon))
            {
                initialDirectory = Path.GetDirectoryName(Model.Icon);
            }
            else if (string.IsNullOrWhiteSpace(Model.DBConfPath) == false && Directory.Exists(Path.GetDirectoryName(Model.DBConfPath)))
            {
                initialDirectory = Path.GetDirectoryName(Model.DBConfPath);
            }
            else if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.GamesDefaultDir) == false && Directory.Exists(UserDataAccessor.UserData.GamesDefaultDir))
            {
                initialDirectory = UserDataAccessor.UserData.GamesDefaultDir;
            }

            if (string.IsNullOrWhiteSpace(initialDirectory))
            {
                if (string.IsNullOrWhiteSpace(Model.DOSEXEPath) == false && Directory.Exists(Path.GetDirectoryName(Model.DOSEXEPath)))
                {
                    initialDirectory = Path.GetDirectoryName(Model.DOSEXEPath);
                }
                else if (string.IsNullOrWhiteSpace(Model.SetupEXEPath) == false && Directory.Exists(Path.GetDirectoryName(Model.SetupEXEPath)))
                {
                    initialDirectory = Path.GetDirectoryName(Model.SetupEXEPath);
                }
                else if (string.IsNullOrWhiteSpace(Model.DBConfPath) == false && Directory.Exists(Path.GetDirectoryName(Model.DBConfPath)))
                {
                    initialDirectory = Path.GetDirectoryName(Model.DBConfPath);
                }
                else if (string.IsNullOrWhiteSpace(Model.Icon) == false && Directory.Exists(Path.GetDirectoryName(Model.Icon)))
                {
                    initialDirectory = Path.GetDirectoryName(Model.Icon);
                }
            }
            return initialDirectory;
        }

        public bool IsDataValid()
        {
            if (string.IsNullOrWhiteSpace(Model.DOSEXEPath) == false && string.IsNullOrWhiteSpace(Model.Directory) == true)
            {
                Model.Directory = Path.GetDirectoryName(Model.DOSEXEPath);
            }

            if (string.IsNullOrWhiteSpace(Model.Name) || string.IsNullOrWhiteSpace(Model.DOSEXEPath) || string.IsNullOrWhiteSpace(Model.Directory))
            {
                return false;
            }
            return true;
        }
    }
}