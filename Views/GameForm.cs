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
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AmpShell.Views
{
    public partial class GameForm : Form
    {
        public GameViewModel ViewModel { get; private set; }

        public GameForm()
        {
            InitializeComponent();
            ViewModel = new GameViewModel();
            Initialize();
        }

        public GameForm(Game editedGame)
        {
            InitializeComponent();
            ViewModel = new GameViewModel(editedGame);
            Initialize();

            ModifyViewForEditing();
        }

        private void Initialize()
        {
            GameIconPictureBox.DataBindings.Add("ImageLocation", ViewModel.Model, "Icon");
            GameIconPictureBox.DataBindings.Add("Image", ViewModel, "IconThumbnail");
            GameNameTextbox.DataBindings.Add("Text", ViewModel.Model, "Name");
            GameLocationTextbox.DataBindings.Add("Text", ViewModel.Model, "DOSEXEPath");
            GameDirectoryTextbox.DataBindings.Add("Text", ViewModel.Model, "Directory");
            GameCustomConfigurationTextbox.DataBindings.Add("Text", ViewModel.Model, "DBConfPath");
            GameCDPathTextBox.DataBindings.Add("Text", ViewModel.Model, "CDPath");
            GameAdditionalCommandsTextBox.DataBindings.Add("Text", ViewModel.Model, "AdditionalCommands");
            AlternateDOSBoxLocationTextbox.DataBindings.Add("Text", ViewModel.Model, "AlternateDOSBoxExePath");
            GameSetupTextBox.DataBindings.Add("Text", ViewModel.Model, "SetupEXEPath");
            NoConfigCheckBox.DataBindings.Add("Checked", ViewModel.Model, "NoConfig");
            NoConsoleCheckBox.DataBindings.Add("Checked", ViewModel.Model, "NoConsole");
            QuitOnExitCheckBox.DataBindings.Add("Checked", ViewModel.Model, "QuitOnExit");
            FullscreenCheckBox.DataBindings.Add("Checked", ViewModel.Model, "InFullScreen");
            UseIOCTLRadioButton.DataBindings.Add("Checked", ViewModel.Model, "UseIOCTL");
            IsAFloppyDiskRadioButton.DataBindings.Add("Checked", ViewModel.Model, "MountAsFloppy");
            if (UseIOCTLRadioButton.Checked == false && IsAFloppyDiskRadioButton.Checked == false)
            {
                NoneRadioButton.Checked = true;
            }
        }

        private void ModifyViewForEditing()
        {
            Text = "Editing " + ViewModel.Model.Name + "...";
            OK.Text = "&Save and apply";
            OK.Width = 102;
            OK.Location = new Point(Cancel.Location.X - 106, Cancel.Location.Y);
            OK.Image = Properties.Resources.saveHS;
            Cancel.Text = "&Don't save";
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            ViewModel.CancelModifications();
            Close();
        }

        /// <summary>
        /// EventHandler for when the user has clicked on "OK"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OK_Click(object sender, EventArgs e)
        {
            //if the game has no name
            if(string.IsNullOrWhiteSpace(GameNameTextbox.Text))
            {
                MessageBox.Show(this, "You must enter the game's name.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if(ViewModel.IsDataValid() == false)
            {
                MessageBox.Show(this, "You must enter the game's executable location or the directory that will be mounted as C:", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);//... show an error.
                return;
            }
            
            if (string.IsNullOrWhiteSpace(GameIconPictureBox.ImageLocation) == false)
            {
                ViewModel.Model.Icon = GameIconPictureBox.ImageLocation;
            }
            else
            {
                ViewModel.Model.Icon = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(GameCDPathTextBox.Text) == false)
            {
                if (File.Exists(GameCDPathTextBox.Text))
                {
                    ViewModel.Model.CDIsAnImage = true;
                }
                else
                {
                    ViewModel.Model.CDIsAnImage = false;
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// EventHandler to choose the Game's executable location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameLocationBrowseButton_Click(object sender, EventArgs e)
        {
            ViewModel.BrowseForGameLocation();
        }

        /// <summary>
        /// EventHandler to choose the game .conf (config) file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameCustomConfigurationBrowseButton_Click(object sender, EventArgs e)
        {
            ViewModel.BrowseForCustomConfigurationFile();
        }

        /// <summary>
        /// EventHandler for when the "Do not use any config file at all" checbox is (un)checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoConfigCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (NoConfigCheckBox.Checked == true)
            {
                GameCustomConfigurationTextbox.Enabled = false;
                GameCustomConfigurationBrowseButton.Enabled = false;
            }
            else
            {
                GameCustomConfigurationTextbox.Enabled = true;
                GameCustomConfigurationBrowseButton.Enabled = true;
            }
        }

        /// <summary>
        /// EventHandler to choose the game's CD image file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameCDPathBrowseButton_Click(object sender, EventArgs e)
        {
            ViewModel.BrowseForCDImageFile();
        }

        /// <summary>
        /// EventHandler for when the GameLocationTextbox (for the game's executable location) text is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameLocationTextbox_TextChanged(object sender, EventArgs e)
        {
            GameDirectoryTextbox.Enabled = true;
            GameDirectoryBrowseButton.Enabled = true;
            GameDirectoryLabel.Enabled = true;

            //if a location for the game's executable has not been entered
            if (string.IsNullOrWhiteSpace(GameLocationTextbox.Text) == true)
            {
                return;
            }
            //then the directory mounted has C: TextBox, BrowseButton, and Labeled are disabled
            //(because DOSBox already mounts the executable's directory path as C: )
            GameDirectoryTextbox.Enabled = false;
            GameDirectoryBrowseButton.Enabled = false;
            GameDirectoryLabel.Enabled = false;
            
            //if the entered executable does exist
            if (string.IsNullOrWhiteSpace(GameLocationTextbox.Text) == false && Directory.Exists(Path.GetDirectoryName(GameLocationTextbox.Text)))
            {
                //(even if the GameDirectory controls are not enabled : it's just to inform the user)
                GameDirectoryTextbox.Text = Path.GetDirectoryName(GameLocationTextbox.Text);
            }
        }

        /// <summary>
        /// EventHandler for when the GameDirectoryTextbox's Text has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameDirectoryTextbox_TextChanged(object sender, EventArgs e)
        {
            //make the controls for the game's executable location available
            GameLocationTextbox.Enabled = true;
            GameLocationBrowseButton.Enabled = true;
            GameLocationLabel.Enabled = true;

            //if the textBox is not empty
            if (string.IsNullOrWhiteSpace(GameDirectoryTextbox.Text) == true)
            {
                return;
            }
            //if the game location textbox is not empty
            if (string.IsNullOrWhiteSpace(GameLocationTextbox.Text) == true)
            {
                return;
            }
            //and if the specified directory does not equals to the game executable's directory
            if (Path.GetDirectoryName(GameLocationTextbox.Text) != GameDirectoryTextbox.Text)
            {
                //then this textbox has been entered first by the user
                //so the controls for the game's executable location will be made empty and disabled
                //because DOSBox cannot mount a directory as C: and have an executable specified.
                //(it's one or the other)
                GameLocationTextbox.Text = string.Empty;
                GameLocationTextbox.Enabled = false;
                GameLocationBrowseButton.Enabled = false;
                GameLocationLabel.Enabled = false;
            }
        }

        /// <summary>
        /// EventHandler to choose the directory mounted has C:
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameDirectoryBrowseButton_Click(object sender, EventArgs e)
        {
            ViewModel.BrowseForGameDirectory();
        }

        /// <summary>
        /// EventHandler for when the GameCDPathTextBox's text is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameCDPathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GameCDPathTextBox.Text))
            {
                MountingOptionsGroupBox.Enabled = false;
            }
            else
            {
                MountingOptionsGroupBox.Enabled = true;
                if (File.Exists(GameCDPathTextBox.Text) == false)
                {
                    UseIOCTLRadioButton.Enabled = true;
                    IsAFloppyDiskRadioButton.Enabled = false;
                }
                else
                {
                    UseIOCTLRadioButton.Enabled = false;
                    IsAFloppyDiskRadioButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// EventHandler to choose the game's setup executable location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameSetupBrowseButton_Click(object sender, EventArgs e)
        {
            ViewModel.BrowseForSetupExe();
        }

        /// <summary>
        /// EventHandler to choose the directory mounted as D:
        /// Because a CD can be a CD Image, or a drive.
        /// Or, the user just wants to mount another directory as D:
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameCDDirBrowseButton_Click(object sender, EventArgs e)
        {
            ViewModel.BrowseForCDDirectory();
        }

        private void QuitOnExitCheckBox_EnabledChanged(object sender, EventArgs e)
        {
            if (QuitOnExitCheckBox.Enabled == false)
            {
                QuitOnExitCheckBox.Checked = false;
            }
        }

        private void GameIconPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            ViewModel.BrowseForGameIcon();
        }

        private void ResetIconButton_Click(object sender, EventArgs e)
        {
            ViewModel.RemoveGameIcon();
        }

        private void AlternateDOSBoxLocationBrowsSearchButton_Click(object sender, EventArgs e)
        {
            ViewModel.BrowseForAlternateDOSBoxExecutable();
        }
    }
}