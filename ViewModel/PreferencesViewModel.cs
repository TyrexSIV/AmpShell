/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

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

        public PreferencesViewModel() : base()
        {
            Model = ObjectSerializer.CloneObject(UserDataAccessor.UserData);
            _modelUndo = UserDataAccessor.UserData;
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
