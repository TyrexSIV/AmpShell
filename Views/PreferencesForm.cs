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
using AmpShell.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AmpShell.Views
{
    public partial class PreferencesForm : Form
    {
        private readonly PreferencesViewModel _viewModel = new PreferencesViewModel();

        public PreferencesForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            EditorBinaryPathTextBox.DataBindings.Add("Text", _viewModel.Model, "ConfigEditorPath");
            DOSBoxPathTextBox.DataBindings.Add("Text", _viewModel.Model, "DBPath");
            DOSBoxConfFileTextBox.DataBindings.Add("Text", _viewModel.Model, "DBDefaultConfFilePath");
            DOSBoxLangFileTextBox.DataBindings.Add("Text", _viewModel.Model, "DBDefaultLangFilePath");
            GamesDirTextBox.DataBindings.Add("Text", _viewModel.Model, "GamesDefaultDir");
            CDImageDirTextBox.DataBindings.Add("Text", _viewModel.Model, "CDsDefaultDir");
            LargeViewModeSizeComboBox.DataBindings.Add("SelectedItem", _viewModel.Model, "LargeViewModeSize");
            CategoyDeletePromptCheckBox.DataBindings.Add("Checked", _viewModel.Model, "CategoryDeletePrompt");
            GameDeletePromptCheckBox.DataBindings.Add("Checked", _viewModel.Model, "GameDeletePrompt");
            WindowPositionCheckBox.DataBindings.Add("Checked", _viewModel.Model, "RememberWindowPosition");
            WindowSizeCheckBox.DataBindings.Add("Checked", _viewModel.Model, "RememberWindowSize");
            ShowMenuBarCheckBox.DataBindings.Add("Checked", _viewModel.Model, "MenuBarVisible");
            ShowToolBarCheckBox.DataBindings.Add("Checked", _viewModel.Model, "ToolBarVisible");
            ShowDetailsBarCheckBox.DataBindings.Add("Checked", _viewModel.Model, "StatusBarVisible");
            QuitOnExitCheckBox.DataBindings.Add("Checked", _viewModel.Model, "GamesQuitOnExit");
            NoConsoleCheckBox.DataBindings.Add("Checked", _viewModel.Model, "GamesNoConsole");
            FullscreenCheckBox.DataBindings.Add("Checked", _viewModel.Model, "GamesInFullScreen");
            GameAdditionalCommandsTextBox.DataBindings.Add("Text", _viewModel.Model, "GamesAdditionalCommands");
            DOSBoxPathTextBox.DataBindings.Add("Text", _viewModel.Model, "DBPath");
            DOSBoxConfFileTextBox.DataBindings.Add("Text", _viewModel.Model, "DBDefaultConfFilePath");
            DOSBoxLangFileTextBox.DataBindings.Add("Text", _viewModel.Model, "DBDefaultLangFilePath");
            EditorBinaryPathTextBox.DataBindings.Add("Text", _viewModel.Model, "ConfigEditorPath");
            AdditionnalParametersTextBox.DataBindings.Add("Text", _viewModel.Model, "ConfigEditorPath");
            CDImageDirTextBox.DataBindings.Add("Text", _viewModel.Model, "CDsDefaultDir");
            GamesDirTextBox.DataBindings.Add("Text", _viewModel.Model, "GamesDefaultDir");
            PortableModeCheckBox.DataBindings.Add("Checked", _viewModel.Model, "PortableMode");
            AllOfThemCheckBox.DataBindings.Add("Checked", _viewModel.Model, "DefaultIconViewOverride");

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == View.Details)
            {
                DetailsIconsRadioButton.Checked = true;
            }

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == View.LargeIcon)
            {
                LargeIconsRadioButton.Checked = true;
            }

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == View.List)
            {
                ListsIconsRadioButton.Checked = true;
            }

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == View.SmallIcon)
            {
                SmallIconsRadioButton.Checked = true;
            }

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == View.Tile)
            {
                TilesIconsRadioButton.Checked = true;
            }

        }

        private void BrowseForEditorButton_Click(object sender, EventArgs e)
        {
            _viewModel.BrowseForTextEditorPath();
        }


        private void DOSBoxPathBrowseButton_Click(object sender, EventArgs e)
        {
            _viewModel.BrowseForDOSBoxPath();
        }

        private void DOSBoxConfFileBrowseButton_Click(object sender, EventArgs e)
        {
            _viewModel.BrowseForDefaultDOSBoxConfigFile();
        }

        private void DOSBoxLangFileBrowseButton_Click(object sender, EventArgs e)
        {
            _viewModel.BrowseForDefaultDOSBoxLanguageFile();
        }

        private void BrowseGamesDirButton_Click(object sender, EventArgs e)
        {
            _viewModel.BrowseForDefaultGamesDirectory();
        }

        private void BrowseCDImageDirButton_Click(object sender, EventArgs e)
        {
            _viewModel.BrowseForDefaultCDImagesDirectory();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (LargeIconsRadioButton.Checked == true)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = View.LargeIcon;
            }

            if (SmallIconsRadioButton.Checked == true)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = View.SmallIcon;
            }

            if (ListsIconsRadioButton.Checked == true)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = View.List;
            }

            if (TilesIconsRadioButton.Checked == true)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = View.Tile;
            }

            if (DetailsIconsRadioButton.Checked == true)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = View.Details;
            }

            SyncCategoriesOrderWithTabOrder();

            if(_viewModel.IsDataValid())
            {
                MessageBox.Show("Location of DOSBox's executable unknown. You will not be able to run games!", "Select DOSBox's executable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Close();
        }

        private void SyncCategoriesOrderWithTabOrder()
        {
            if (CategoriesListView.Items.Count < 2)
            {
                return;
            }

            List<ListViewItem> tabs = CategoriesListView.Items.Cast<ListViewItem>().ToList();

            foreach (Category ConcernedCategory in UserDataAccessor.UserData.ListChildren)
            {
                UserDataAccessor.UserData.MoveChildToPosition(ConcernedCategory, tabs.IndexOf(tabs.FirstOrDefault(x => Convert.ToString(x.Tag) == ConcernedCategory.Signature)));
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            _viewModel.CancelModifications();
            Close();
        }

        private void Main_Prefs_Load(object sender, EventArgs e)
        {
            CheckForPortableModeAvailabilityAndUpdateUI();
            CategoriesListView.Columns.Add("Name");
            CategoriesListView.Columns[0].Width = CategoriesListView.Width;
            CategoriesListView.Items.Clear();
            foreach (Category categoryToDisplay in UserDataAccessor.UserData.ListChildren)
            {
                ListViewItem ItemToAdd = new ListViewItem(categoryToDisplay.Title)
                {
                    Tag = categoryToDisplay.Signature
                };
                CategoriesListView.Items.Add(ItemToAdd);
            }
            PortableModeCheckBox_CheckedChanged(sender, EventArgs.Empty);
        }

        private void CheckForPortableModeAvailabilityAndUpdateUI()
        {
            if (FileFinder.HasWriteAccessToAssemblyLocationFolder() == false)
            {
                PortableModeCheckBox.Enabled = false;
                PortableModeCheckBox.Checked = false;
                StatusStripLabel.Text = "Portable Mode : unavailable (AmpShell cannot write in the folder where it is located).";
            }
            else
            {
                StatusStripLabel.Text = "Portable Mode : available (but disabled).";
            }
        }

        private void MoveFirstButton_Click(object sender, EventArgs e)
        {
            CategoriesListViewItemMoveTo(0);
        }

        private void SortByNameButton_Click(object sender, EventArgs e)
        {
            CategoriesListView.Sorting = SortOrder.Ascending;
            CategoriesListView.Sort();
            CategoriesListView.Sorting = SortOrder.None;
        }

        private void MoveLastButton_Click(object sender, EventArgs e)
        {
            CategoriesListViewItemMoveTo(CategoriesListView.Items.Count - 1);
        }

        private void LargeIconsRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            SmallIconsRadioButton.Checked = false;
            TilesIconsRadioButton.Checked = false;
            ListsIconsRadioButton.Checked = false;
            DetailsIconsRadioButton.Checked = false;
        }

        private void SmallIconsRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            LargeIconsRadioButton.Checked = false;
            TilesIconsRadioButton.Checked = false;
            ListsIconsRadioButton.Checked = false;
            DetailsIconsRadioButton.Checked = false;
        }

        private void TilesIconsRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            SmallIconsRadioButton.Checked = false;
            LargeIconsRadioButton.Checked = false;
            ListsIconsRadioButton.Checked = false;
            DetailsIconsRadioButton.Checked = false;
        }

        private void ListsIconsRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            SmallIconsRadioButton.Checked = false;
            LargeIconsRadioButton.Checked = false;
            TilesIconsRadioButton.Checked = false;
            DetailsIconsRadioButton.Checked = false;
        }

        private void DetailsIconsRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            SmallIconsRadioButton.Checked = false;
            LargeIconsRadioButton.Checked = false;
            ListsIconsRadioButton.Checked = false;
            TilesIconsRadioButton.Checked = false;
        }

        private void MoveBackButton_Click(object sender, EventArgs e)
        {
            if (CategoriesListView.FocusedItem.Index > 0)
            {
                CategoriesListViewItemMoveTo(CategoriesListView.FocusedItem.Index - 1);
            }
        }

        private void MoveNextButton_Click(object sender, EventArgs e)
        {
            if (CategoriesListView.FocusedItem.Index < CategoriesListView.Items.Count)
            {
                CategoriesListViewItemMoveTo(CategoriesListView.FocusedItem.Index + 1);
            }
        }

        private void CategoriesListViewItemMoveTo(int index)
        {
            ListViewItem listViewItemCopy = new ListViewItem
            {
                Text = CategoriesListView.FocusedItem.Text,
                Tag = CategoriesListView.FocusedItem.Tag
            };
            CategoriesListView.Items.RemoveAt(CategoriesListView.FocusedItem.Index);
            CategoriesListView.Items.Insert(index, listViewItemCopy);
            CategoriesListView.FocusedItem = CategoriesListView.Items[(string)listViewItemCopy.Tag];
        }

        private void PortableModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PortableModeCheckBox.Checked == true)
            {
                ReScanDirButton.Enabled = true;
                DOSBoxPathBrowseButton.Enabled = false;
                DOSBoxConfFileTextBox.Enabled = false;
                DOSBoxConfFileBrowseButton.Enabled = false;
                DOSBoxLangFileTextBox.Enabled = false;
                DOSBoxLangFileBrowseButton.Enabled = false;
                DOSBoxPathTextBox.Enabled = false;
                GamesDirTextBox.Enabled = false;
                BrowseGamesDirButton.Enabled = false;
                CDImageDirTextBox.Enabled = false;
                BrowseCDImageDirButton.Enabled = false;
                EditorBinaryPathTextBox.Enabled = false;
                BrowseForEditorButton.Enabled = false;
                if (File.Exists(Path.Combine(Application.StartupPath, "\\dosbox.exe")))
                {
                    DOSBoxPathTextBox.Text = Path.Combine(Application.StartupPath, "\\dosbox.exe");
                }
                else
                {
                    DOSBoxPathTextBox.Text = "dosbox.exe isn't is the same directory as AmpShell.exe!";
                }

                if (Directory.GetFiles((Application.StartupPath), "*.conf").Length > 0)
                {
                    DOSBoxConfFileTextBox.Text = Directory.GetFiles((Application.StartupPath), "*.conf")[0];
                }
                else
                {
                    DOSBoxConfFileTextBox.Text = "No configuration file (*.conf) found in AmpShell's directory.";
                }

                if (Directory.GetFiles(Application.StartupPath, "*.lng").Length > 0)
                {
                    DOSBoxLangFileTextBox.Text = Directory.GetFiles(Application.StartupPath, "*.lng")[0];
                }
                else
                {
                    DOSBoxLangFileTextBox.Text = "No language file (*.lng) found in AmpShell's directory.";
                }

                if (File.Exists(Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "notepad.exe")))
                {
                    EditorBinaryPathTextBox.Text = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "notepad.exe");
                }
                else if (File.Exists(Path.Combine(Application.StartupPath, "\\TextEditor.exe")))
                {
                    EditorBinaryPathTextBox.Text = Path.Combine(Application.StartupPath, "\\TextEditor.exe");
                }
                else
                {
                    EditorBinaryPathTextBox.Text = "No text editor (Notepad in Windows' directory, or TextEditor.exe in AmpShell's directory) found.";
                }

                StatusStripLabel.Text = "Portable Mode : active (all files (or at least DOSBox, and all the games) must be in the same directory as AmpShell).";
            }
            else
            {
                ReScanDirButton.Enabled = false;
                DOSBoxPathBrowseButton.Enabled = true;
                DOSBoxConfFileTextBox.Enabled = true;
                DOSBoxConfFileBrowseButton.Enabled = true;
                DOSBoxLangFileTextBox.Enabled = true;
                DOSBoxLangFileBrowseButton.Enabled = true;
                DOSBoxPathTextBox.Enabled = true;
                GamesDirTextBox.Enabled = true;
                BrowseGamesDirButton.Enabled = true;
                CDImageDirTextBox.Enabled = true;
                BrowseCDImageDirButton.Enabled = true;
                EditorBinaryPathTextBox.Enabled = true;
                BrowseForEditorButton.Enabled = true;
            }
        }

        private void ReScanDirButton_Click(object sender, EventArgs e)
        {
            PortableModeCheckBox_CheckedChanged(sender, EventArgs.Empty);
        }
    }
}