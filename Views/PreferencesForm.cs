/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

using AmpShell.Model;
using AmpShell.ViewModel;

using System;
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
            EditorBinaryPathTextBox.DataBindings.Add("Text", _viewModel.Model, "ConfigEditorPath", false, DataSourceUpdateMode.OnPropertyChanged);
            DOSBoxPathTextBox.DataBindings.Add("Text", _viewModel.Model, "DBPath", false, DataSourceUpdateMode.OnPropertyChanged);
            DOSBoxConfFileTextBox.DataBindings.Add("Text", _viewModel.Model, "DBDefaultConfFilePath", false, DataSourceUpdateMode.OnPropertyChanged);
            DOSBoxLangFileTextBox.DataBindings.Add("Text", _viewModel.Model, "DBDefaultLangFilePath", false, DataSourceUpdateMode.OnPropertyChanged);
            GamesDirTextBox.DataBindings.Add("Text", _viewModel.Model, "GamesDefaultDir", false, DataSourceUpdateMode.OnPropertyChanged);
            CDImageDirTextBox.DataBindings.Add("Text", _viewModel.Model, "CDsDefaultDir", false, DataSourceUpdateMode.OnPropertyChanged);
            CategoyDeletePromptCheckBox.DataBindings.Add("Checked", _viewModel.Model, "CategoryDeletePrompt", false, DataSourceUpdateMode.OnPropertyChanged);
            GameDeletePromptCheckBox.DataBindings.Add("Checked", _viewModel.Model, "GameDeletePrompt", false, DataSourceUpdateMode.OnPropertyChanged);
            WindowPositionCheckBox.DataBindings.Add("Checked", _viewModel.Model, "RememberWindowPosition", false, DataSourceUpdateMode.OnPropertyChanged);
            WindowSizeCheckBox.DataBindings.Add("Checked", _viewModel.Model, "RememberWindowSize", false, DataSourceUpdateMode.OnPropertyChanged);
            ShowMenuBarCheckBox.DataBindings.Add("Checked", _viewModel.Model, "MenuBarVisible", false, DataSourceUpdateMode.OnPropertyChanged);
            ShowToolBarCheckBox.DataBindings.Add("Checked", _viewModel.Model, "ToolBarVisible", false, DataSourceUpdateMode.OnPropertyChanged);
            ShowDetailsBarCheckBox.DataBindings.Add("Checked", _viewModel.Model, "StatusBarVisible", false, DataSourceUpdateMode.OnPropertyChanged);
            ExitOnGameLaunchCheckBox.DataBindings.Add("Checked", _viewModel.Model, "ExitOnGameLaunch", false, DataSourceUpdateMode.OnPropertyChanged);
            PutAmpShellInTaskBarOnLaunchCheckBox.DataBindings.Add("Checked", _viewModel.Model, "GoToTaskBarOnGameLaunch", false, DataSourceUpdateMode.OnPropertyChanged);
            QuitOnExitCheckBox.DataBindings.Add("Checked", _viewModel.Model, "GamesQuitOnExit", false, DataSourceUpdateMode.OnPropertyChanged);
            NoConsoleCheckBox.DataBindings.Add("Checked", _viewModel.Model, "GamesNoConsole", false, DataSourceUpdateMode.OnPropertyChanged);
            FullscreenCheckBox.DataBindings.Add("Checked", _viewModel.Model, "GamesInFullScreen", false, DataSourceUpdateMode.OnPropertyChanged);
            GameAdditionalCommandsTextBox.DataBindings.Add("Text", _viewModel.Model, "GamesAdditionalCommands", false, DataSourceUpdateMode.OnPropertyChanged);
            AdditionnalParametersTextBox.DataBindings.Add("Text", _viewModel.Model, "ConfigEditorPath", false, DataSourceUpdateMode.OnPropertyChanged);
            PortableModeCheckBox.DataBindings.Add("Checked", _viewModel.Model, "PortableMode", false, DataSourceUpdateMode.OnPropertyChanged);
            PortableModeCheckBox.DataBindings.Add("Enabled", _viewModel, "PortableModeAvailable", false, DataSourceUpdateMode.OnPropertyChanged);
            DetailsIconsRadioButton.DataBindings.Add("Checked", _viewModel, "IsDefaultViewModeDetails", false, DataSourceUpdateMode.OnPropertyChanged);
            SmallIconsRadioButton.DataBindings.Add("Checked", _viewModel, "IsDefaultViewModeSmallIcons", false, DataSourceUpdateMode.OnPropertyChanged);
            LargeIconsRadioButton.DataBindings.Add("Checked", _viewModel, "IsDefaultViewModeLargeIcons", false, DataSourceUpdateMode.OnPropertyChanged);
            ListsIconsRadioButton.DataBindings.Add("Checked", _viewModel, "IsDefaultViewModeList", false, DataSourceUpdateMode.OnPropertyChanged);
            TilesIconsRadioButton.DataBindings.Add("Checked", _viewModel, "IsDefaultViewModeTiles", false, DataSourceUpdateMode.OnPropertyChanged);
            AllOfThemCheckBox.DataBindings.Add("Checked", _viewModel.Model, "DefaultIconViewOverride", false, DataSourceUpdateMode.OnPropertyChanged);
            ReScanDirButton.DataBindings.Add("Enabled", _viewModel, "PortableModeAvailable", false, DataSourceUpdateMode.OnPropertyChanged);
            PrefsStatusStrip.DataBindings.Add("Text", _viewModel, "PortableModeStatus", false, DataSourceUpdateMode.OnPropertyChanged);

            DOSBoxPathBrowseButton.DataBindings.Add(GetPortableModeBinding());
            DOSBoxConfFileTextBox.DataBindings.Add(GetPortableModeBinding());
            DOSBoxConfFileBrowseButton.DataBindings.Add(GetPortableModeBinding());
            DOSBoxLangFileTextBox.DataBindings.Add(GetPortableModeBinding());
            DOSBoxLangFileBrowseButton.DataBindings.Add(GetPortableModeBinding());
            DOSBoxPathTextBox.DataBindings.Add(GetPortableModeBinding());
            GamesDirTextBox.DataBindings.Add(GetPortableModeBinding());
            BrowseGamesDirButton.DataBindings.Add(GetPortableModeBinding());
            CDImageDirTextBox.DataBindings.Add(GetPortableModeBinding());
            BrowseCDImageDirButton.DataBindings.Add(GetPortableModeBinding());
            EditorBinaryPathTextBox.DataBindings.Add(GetPortableModeBinding());
            BrowseForEditorButton.DataBindings.Add(GetPortableModeBinding());

            var categoriesBindingSource = new BindingSource(_viewModel, "Categories");
            CategoriesListView.DataSource = categoriesBindingSource;

            LargeViewModeSizeComboBox.DataSource = new BindingSource(_viewModel, "LargeViewModeAvailableSizes");
            LargeViewModeSizeComboBox.DataBindings.Add("SelectedItem", _viewModel.Model, "LargeViewModeSize", false, DataSourceUpdateMode.OnPropertyChanged);

        }

        private Binding GetPortableModeBinding()
        {
            var isPortableModeDisabledBinding = new Binding("Enabled", _viewModel.Model, "PortableMode", true, DataSourceUpdateMode.OnPropertyChanged);
            isPortableModeDisabledBinding.Format += IsPortableModeDisabledBinding_Parse;
            isPortableModeDisabledBinding.Parse += IsPortableModeDisabledBinding_Parse;
            return isPortableModeDisabledBinding;
        }

        private void IsPortableModeDisabledBinding_Parse(object sender, ConvertEventArgs e)
        {
            e.Value = !(bool)e.Value;
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
            if(_viewModel.IsDataValid() == false)
            {
                MessageBox.Show("Location of DOSBox's executable unknown. You will not be able to run games!", "Select DOSBox's executable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            _viewModel.CancelModifications();
            Close();
        }

        private void MoveFirstButton_Click(object sender, EventArgs e)
        {
            _viewModel.MoveCategoryTo(CategoriesListView.FocusedItem.Index, 0);
        }


        private void MoveBackButton_Click(object sender, EventArgs e)
        {
            if (CategoriesListView.FocusedItem.Index > 0)
            {
                _viewModel.MoveCategoryTo(CategoriesListView.FocusedItem.Index, CategoriesListView.FocusedItem.Index - 1);
            }
        }

        private void MoveNextButton_Click(object sender, EventArgs e)
        {
            if (CategoriesListView.FocusedItem.Index < CategoriesListView.Items.Count)
            {
                _viewModel.MoveCategoryTo(CategoriesListView.FocusedItem.Index, CategoriesListView.FocusedItem.Index + 1);
            }
        }

        private void MoveLastButton_Click(object sender, EventArgs e)
        {
            _viewModel.MoveCategoryTo(CategoriesListView.FocusedItem.Index, CategoriesListView.Items.Count - 1);
        }

        private void SortByNameButton_Click(object sender, EventArgs e)
        {
            _viewModel.SortCategoriesByName();
        }

        private void PortableModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PortableModeCheckBox.Checked == true)
            {
                _viewModel.EnablePortableMode();
            }
            else
            {
                _viewModel.DisablePortableMode();
            }
        }

        private void ReScanDirButton_Click(object sender, EventArgs e)
        {
            _viewModel.ScanForPortableModeTools();
        }
    }
}