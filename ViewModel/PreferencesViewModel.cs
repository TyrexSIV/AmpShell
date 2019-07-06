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
using AmpShell.Model;
using AmpShell.Notification;
using AmpShell.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmpShell.ViewModel
{
    public class PreferencesViewModel : PropertyChangedNotifier
    {
        public Preferences Model { get; private set; }

        private readonly Preferences _modelUndo;

        private bool _portableModeAvailable = false;

        public bool PortableModeAvailable
        {
            get => _portableModeAvailable;
            set { Set(ref _portableModeAvailable, value); }
        }

        private string _portableModeStatus = "";

        public string PortableModeStatus
        {
            get => _portableModeStatus;
            set { Set(ref _portableModeStatus, value); }
        }

        public PreferencesViewModel() : base()
        {
            Model = ObjectSerializer.CloneObject(UserDataAccessor.UserData);
            _modelUndo = UserDataAccessor.UserData;
            CheckForPortableModeAvailability();
        }

        public void CheckForPortableModeAvailability()
        {
            if (FileFinder.HasWriteAccessToAssemblyLocationFolder() == false)
            {
                PortableModeAvailable = false;
                Model.PortableMode = false;
                PortableModeStatus = "Portable Mode : unavailable (AmpShell cannot write in the folder where it is located).";
            }
            else
            {
                PortableModeStatus = "Portable Mode : available (but disabled).";
            }
        }

        public void EnablePortableMode()
        {
            Model.PortableMode = true;

            if (File.Exists(Path.Combine(Application.StartupPath, "\\dosbox.exe")))
            {
                Model.DBPath = Path.Combine(Application.StartupPath, "\\dosbox.exe");
            }
            else
            {
                Model.DBPath = "dosbox.exe isn't is the same directory as AmpShell.exe!";
            }

            if (Directory.GetFiles((Application.StartupPath), "*.conf").Length > 0)
            {
                Model.DBDefaultConfFilePath = Directory.GetFiles((Application.StartupPath), "*.conf")[0];
            }
            else
            {
                Model.DBDefaultConfFilePath = "No configuration file (*.conf) found in AmpShell's directory.";
            }

            if (Directory.GetFiles(Application.StartupPath, "*.lng").Length > 0)
            {
                Model.DBDefaultLangFilePath = Directory.GetFiles(Application.StartupPath, "*.lng")[0];
            }
            else
            {
                Model.DBDefaultLangFilePath = "No language file (*.lng) found in AmpShell's directory.";
            }

            if (File.Exists(Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "notepad.exe")))
            {
                Model.ConfigEditorPath = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "notepad.exe");
            }
            else if (File.Exists(Path.Combine(Application.StartupPath, "\\TextEditor.exe")))
            {
                Model.ConfigEditorPath = Path.Combine(Application.StartupPath, "\\TextEditor.exe");
            }
            else
            {
                Model.ConfigEditorPath = "No text editor (Notepad in Windows' directory, or TextEditor.exe in AmpShell's directory) found.";
            }

            PortableModeStatus = "Portable Mode : active (all files (or at least DOSBox, and all the games) must be in the same directory as AmpShell).";
        }

        public void BrowseForTextEditorPath()
        {
            OpenFileDialog textEdtiorFileDialog = new OpenFileDialog();
            if (string.IsNullOrWhiteSpace(Model.ConfigEditorPath) == false)
            {
                if (Directory.Exists(Path.GetDirectoryName(Model.ConfigEditorPath).ToString()))
                {
                    textEdtiorFileDialog.InitialDirectory = Path.GetDirectoryName(Model.ConfigEditorPath).ToString();
                }
                else
                {
                    textEdtiorFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                }
            }
            if (textEdtiorFileDialog.ShowDialog() == DialogResult.OK)
            {
                Model.ConfigEditorPath = textEdtiorFileDialog.FileName;
            }
        }

        public void BrowseForDOSBoxPath()
        {
            OpenFileDialog dosBoxExePathFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                Title = "Browsing for DOSBox' executable file...",
                Filter = "DOSBox executable (dosbox*)|dosbox*|All files|*"
            };
            if (dosBoxExePathFileDialog.ShowDialog() == DialogResult.OK)
            {
                //retrieve the selected dosbox.exe path into Amp.DBPath
                UserDataAccessor.UserData.DBPath = dosBoxExePathFileDialog.FileName;
                Model.DBPath = dosBoxExePathFileDialog.FileName;
            }
            else if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBPath))
            {
                MessageBox.Show("Location of DOSBox's executable unknown. You will not be able to run games!", "Select DOSBox's executable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void BrowseForDefaultDOSBoxConfigFile()
        {
            OpenFileDialog dosboxDefaultConfFileDialog = new OpenFileDialog();
            if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultConfFilePath) == false
                && Directory.Exists(Path.GetDirectoryName(UserDataAccessor.UserData.DBDefaultConfFilePath)))
            {
                dosboxDefaultConfFileDialog.InitialDirectory = Path.GetDirectoryName(UserDataAccessor.UserData.DBDefaultConfFilePath);
            }

            dosboxDefaultConfFileDialog.Title = "Browsing for the default DOSBox config file...";
            dosboxDefaultConfFileDialog.Filter = "DOSBox configuration files (*.conf)|*.conf|All files|*";
            if (dosboxDefaultConfFileDialog.ShowDialog() == DialogResult.OK)
            {
                UserDataAccessor.UserData.DBDefaultConfFilePath = dosboxDefaultConfFileDialog.FileName;
                Model.DBDefaultConfFilePath = dosboxDefaultConfFileDialog.FileName;
            }
        }

        public void BrowseForDefaultDOSBoxLanguageFile()
        {
            OpenFileDialog dosBoxDefaultLangFileDialog = new OpenFileDialog();
            if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultLangFilePath) == false
                && Directory.Exists(Path.GetDirectoryName(UserDataAccessor.UserData.DBDefaultLangFilePath)))
            {
                dosBoxDefaultLangFileDialog.InitialDirectory = Path.GetDirectoryName(UserDataAccessor.UserData.DBDefaultLangFilePath);
            }

            dosBoxDefaultLangFileDialog.Title = "Browsing for the default DOSBox language file...";
            dosBoxDefaultLangFileDialog.Filter = "DOSBox language files (*.lng)|*.lng|All files|*";
            if (dosBoxDefaultLangFileDialog.ShowDialog() == DialogResult.OK)
            {
                UserDataAccessor.UserData.DBDefaultLangFilePath = dosBoxDefaultLangFileDialog.FileName;
                Model.DBDefaultLangFilePath = dosBoxDefaultLangFileDialog.FileName;
            }
        }

        public void BrowseForDefaultGamesDirectory()
        {
            FolderBrowserDialog gamesFolderBrowserDialog = new FolderBrowserDialog();
            if (gamesFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Model.GamesDefaultDir = gamesFolderBrowserDialog.SelectedPath;
            }
        }

        public void BrowseForDefaultCDImagesDirectory()
        {
            FolderBrowserDialog cdImagesFolderBrowserDialog = new FolderBrowserDialog();
            if (cdImagesFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Model.CDsDefaultDir = cdImagesFolderBrowserDialog.SelectedPath;
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
