using AmpShell.DAL;
using AmpShell.Model;
using AmpShell.Notification;
using AmpShell.Serialization;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AmpShell.ViewModel
{
    public class GameViewModel : PropertyChangedNotifier
    {
        public Game Model { get; private set; }

        private readonly Game _modelUndo;

        private Image _iconThumbnail;

        public Image IconThumbnail
        {
            get { return _iconThumbnail; }
            private set { Set(ref _iconThumbnail, value); }
        }

        public GameViewModel() : base()
        {
            Model = new Game
            {
                NoConsole = UserDataAccessor.UserData.GamesNoConsole,
                InFullScreen = UserDataAccessor.UserData.GamesInFullScreen,
                QuitOnExit = UserDataAccessor.UserData.GamesQuitOnExit,
                AdditionalCommands = UserDataAccessor.UserData.GamesAdditionalCommands
            };
            Initialize();
        }

        public GameViewModel(Game model)
        {
            Model = model;
            _modelUndo = ObjectSerializer.CloneObject(model);
            Initialize();
        }

        private void Initialize()
        {
            UpdateIconThumbnail(Model.Icon);
        }

        private void UpdateIconThumbnail(string imageFile)
        {
            if (string.IsNullOrWhiteSpace(imageFile))
            {
                IconThumbnail = Properties.Resources.Generic_Application1;
                return;
            }
            if (File.Exists(imageFile))
            {
                try
                {
                    IconThumbnail = Image.FromFile(imageFile).GetThumbnailImage(64, 64, null, IntPtr.Zero);

                }
                catch (OutOfMemoryException)
                {
                    MessageBox.Show("There was an error in the image file, or it's format is not supported. Please check the file.", "Changing the game's icon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            Model.Icon = imageFile;
        }

        public void BrowseForGameIcon()
        {
            OpenFileDialog iconFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.bmp;*.exif;*.gif;*.ico;*.jp*;*.png;*.tif*)|*.bmp;*.BMP;*.exif;*.EXIF;*.gif;*.GIF;*.ico;*.ICO;*.jp*;*.JP*;*.png;*.PNG;*.tif*;*.TIF*"
            };
            if (UserDataAccessor.UserData.PortableMode == true)
            {
                iconFileDialog.InitialDirectory = Application.StartupPath;
            }
            else if (string.IsNullOrWhiteSpace(Model.Icon) == false && Directory.Exists(Path.GetDirectoryName(Model.Icon)))
            {
                iconFileDialog.InitialDirectory = Path.GetDirectoryName(Model.Icon);
            }
            else
            {
                iconFileDialog.InitialDirectory = SearchFolderDialogStartDirectory();
            }

            if (iconFileDialog.ShowDialog() == DialogResult.OK)
            {
                UpdateIconThumbnail(iconFileDialog.FileName);
            }
        }

        public void RemoveGameIcon()
        {
            IconThumbnail = Properties.Resources.Generic_Application1;
            Model.Icon = string.Empty;
        }

        public void CancelModifications()
        {
            Model = _modelUndo;
        }

        public void BrowseForGameLocation()
        {
            OpenFileDialog gameExeFileDialog = new OpenFileDialog();
            if (UserDataAccessor.UserData.PortableMode == true)
            {
                gameExeFileDialog.InitialDirectory = Application.StartupPath;
            }
            else if (string.IsNullOrWhiteSpace(Model.DOSEXEPath) == false && Directory.Exists(Path.GetDirectoryName(Model.DOSEXEPath)))
            {
                gameExeFileDialog.InitialDirectory = Path.GetDirectoryName(Model.DOSEXEPath);
            }
            else
            {
                gameExeFileDialog.InitialDirectory = SearchFolderDialogStartDirectory();
            }

            gameExeFileDialog.Title = "Browsing for the game's executable file...";
            gameExeFileDialog.Filter = "DOS executable files (*.bat;*.cmd;*.com;*.exe)|*.bat;*.cmd;*.com;*.exe;*.BAT;*.CMD;*.COM;*.EXE";
            if (gameExeFileDialog.ShowDialog() == DialogResult.OK)
            {
                Model.DOSEXEPath = gameExeFileDialog.FileName;
            }
        }

        public void BrowseForGameDirectory()
        {
            FolderBrowserDialog cMountFolderBrowserDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = true,
                Description = "Browsing for the directory mounted as C:..."
            };
            if (cMountFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Model.Directory = cMountFolderBrowserDialog.SelectedPath;
            }
        }

        public void BrowseForCustomConfigurationFile()
        {
            OpenFileDialog customConfigFileDialog = new OpenFileDialog();
            if (UserDataAccessor.UserData.PortableMode == true)
            {
                customConfigFileDialog.InitialDirectory = Application.StartupPath;
            }
            else if (string.IsNullOrWhiteSpace(Model.DBConfPath) == false && Directory.Exists(Path.GetDirectoryName(Model.DBConfPath)))
            {
                customConfigFileDialog.InitialDirectory = Path.GetDirectoryName(Model.DBConfPath);
            }
            else
            {
                customConfigFileDialog.InitialDirectory = SearchFolderDialogStartDirectory();
            }

            customConfigFileDialog.Title = "Browsing for a custom DOSBox configuration file...";
            customConfigFileDialog.Filter = "DOSBox configuration file (*.conf)|*.conf;*.CONF";
            if (customConfigFileDialog.ShowDialog() == DialogResult.OK)
            {
                Model.DBConfPath = customConfigFileDialog.FileName;
            }
        }

        internal void BrowseForSetupExe()
        {
            OpenFileDialog setupExeFileDialog = new OpenFileDialog
            {
                Title = "Browsing for the setup executable...",
                Filter = "DOS executable files (*.bat;*.com;*.exe)|*.bat;*.com;*.exe;*.BAT;*.COM;*.EXE"
            };
            if (UserDataAccessor.UserData.PortableMode == true)
            {
                setupExeFileDialog.InitialDirectory = Application.StartupPath;
            }
            else
            {
                setupExeFileDialog.InitialDirectory = SearchFolderDialogStartDirectory();
            }

            if (setupExeFileDialog.ShowDialog() == DialogResult.OK)
            {
                Model.SetupEXEPath = setupExeFileDialog.FileName;
            }
        }

        public void BrowseForCDImageFile()
        {
            OpenFileDialog cdImageFileDialog = new OpenFileDialog
            {
                Title = "Browsing for a CD image file...",
                Filter = "DOSBox compatible CD images (*.bin;*.cue;*.iso;*.img)|*.bin;*.cue;*.iso;*.img;*.BIN;*.CUE;*.ISO;*.IMG"
            };
            if (UserDataAccessor.UserData.PortableMode == true)
            {
                cdImageFileDialog.InitialDirectory = Application.StartupPath;
            }
            else if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.CDsDefaultDir) == false && Directory.Exists(UserDataAccessor.UserData.CDsDefaultDir))
            {
                cdImageFileDialog.InitialDirectory = UserDataAccessor.UserData.CDsDefaultDir;
            }
            else
            {
                cdImageFileDialog.InitialDirectory = SearchFolderDialogStartDirectory();
            }

            if (cdImageFileDialog.ShowDialog() == DialogResult.OK)
            {
                Model.CDPath = cdImageFileDialog.FileName;
            }
        }

        public void BrowseForCDDirectory()
        {
            FolderBrowserDialog cdDriveFlderBrowserDialog = new FolderBrowserDialog();
            if (cdDriveFlderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Model.CDPath = cdDriveFlderBrowserDialog.SelectedPath;
            }
        }

        public void BrowseForAlternateDOSBoxExecutable()
        {
            OpenFileDialog alternateDOSBoxExeFileDialog = new OpenFileDialog();
            if (UserDataAccessor.UserData.PortableMode == true)
            {
                alternateDOSBoxExeFileDialog.InitialDirectory = Application.StartupPath;
            }
            else if (string.IsNullOrWhiteSpace(Model.AlternateDOSBoxExePath) == false && Directory.Exists(Path.GetDirectoryName(Model.AlternateDOSBoxExePath)))
            {
                alternateDOSBoxExeFileDialog.InitialDirectory = Path.GetDirectoryName(Model.AlternateDOSBoxExePath);
            }
            else if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBPath) == false && Directory.Exists(Path.GetDirectoryName(UserDataAccessor.UserData.DBPath)))
            {
                alternateDOSBoxExeFileDialog.InitialDirectory = UserDataAccessor.UserData.DBPath;
            }
            else
            {
                alternateDOSBoxExeFileDialog.InitialDirectory = SearchFolderDialogStartDirectory();
            }

            alternateDOSBoxExeFileDialog.Title = "Browsing for an alternate DOSBox executable...";
            alternateDOSBoxExeFileDialog.Filter = "DOSBox executable file (*.exe)|*.exe;*.EXE";
            if (alternateDOSBoxExeFileDialog.ShowDialog() == DialogResult.OK)
            {
                Model.AlternateDOSBoxExePath = alternateDOSBoxExeFileDialog.FileName;
            }
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
            if (string.IsNullOrWhiteSpace(Model.Name) || string.IsNullOrWhiteSpace(Model.DOSEXEPath) || string.IsNullOrWhiteSpace(Model.Directory))
            {
                return false;
            }
            return true;
        }
    }
}
