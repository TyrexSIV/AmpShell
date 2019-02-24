﻿/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/
namespace AmpShell.WinForms
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.GameLocationTextbox = new System.Windows.Forms.TextBox();
            this.GameCustomConfigurationTextbox = new System.Windows.Forms.TextBox();
            this.GameNameTextbox = new System.Windows.Forms.TextBox();
            this.GameCDPathTextBox = new System.Windows.Forms.TextBox();
            this.NoConsoleCheckBox = new System.Windows.Forms.CheckBox();
            this.NoConfigCheckBox = new System.Windows.Forms.CheckBox();
            this.FullscreenCheckBox = new System.Windows.Forms.CheckBox();
            this.QuitOnExitCheckBox = new System.Windows.Forms.CheckBox();
            this.GameAdditionalCommandsTextBox = new System.Windows.Forms.TextBox();
            this.OtherOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.GameDirectoryTextbox = new System.Windows.Forms.TextBox();
            this.GameSetupTextBox = new System.Windows.Forms.TextBox();
            this.UseIOCTLRadioButton = new System.Windows.Forms.RadioButton();
            this.IsAFloppyDiskRadioButton = new System.Windows.Forms.RadioButton();
            this.MountingOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.NoneRadioButton = new System.Windows.Forms.RadioButton();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.ResetIconButton = new System.Windows.Forms.Button();
            this.GameIconPictureBox = new System.Windows.Forms.PictureBox();
            this.GameCDDirBrowseButton = new System.Windows.Forms.Button();
            this.GameSetupBrowseButton = new System.Windows.Forms.Button();
            this.GameSetupLabel = new System.Windows.Forms.Label();
            this.GameDirectoryBrowseButton = new System.Windows.Forms.Button();
            this.GameDirectoryLabel = new System.Windows.Forms.Label();
            this.GameAdditionalCommandsLabel = new System.Windows.Forms.Label();
            this.GameCDPathBrowseButton = new System.Windows.Forms.Button();
            this.GameCDPathLabel = new System.Windows.Forms.Label();
            this.GameNameLabel = new System.Windows.Forms.Label();
            this.GameCustomConfigurationBrowseButton = new System.Windows.Forms.Button();
            this.GameCustomCofigurationLabel = new System.Windows.Forms.Label();
            this.GameLocationBrowseButton = new System.Windows.Forms.Button();
            this.GameLocationLabel = new System.Windows.Forms.Label();
            this.OtherOptionsGroupBox.SuspendLayout();
            this.MountingOptionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GameIconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GameLocationTextbox
            // 
            this.GameLocationTextbox.Location = new System.Drawing.Point(2, 80);
            this.GameLocationTextbox.Name = "GameLocationTextbox";
            this.GameLocationTextbox.Size = new System.Drawing.Size(382, 20);
            this.GameLocationTextbox.TabIndex = 4;
            this.GameLocationTextbox.TextChanged += new System.EventHandler(this.GameLocationTextbox_TextChanged);
            // 
            // GameCustomConfigurationTextbox
            // 
            this.GameCustomConfigurationTextbox.Location = new System.Drawing.Point(2, 197);
            this.GameCustomConfigurationTextbox.Name = "GameCustomConfigurationTextbox";
            this.GameCustomConfigurationTextbox.Size = new System.Drawing.Size(382, 20);
            this.GameCustomConfigurationTextbox.TabIndex = 13;
            // 
            // GameNameTextbox
            // 
            this.GameNameTextbox.Location = new System.Drawing.Point(2, 41);
            this.GameNameTextbox.Name = "GameNameTextbox";
            this.GameNameTextbox.Size = new System.Drawing.Size(342, 20);
            this.GameNameTextbox.TabIndex = 2;
            // 
            // GameCDPathTextBox
            // 
            this.GameCDPathTextBox.Location = new System.Drawing.Point(2, 259);
            this.GameCDPathTextBox.Name = "GameCDPathTextBox";
            this.GameCDPathTextBox.Size = new System.Drawing.Size(382, 20);
            this.GameCDPathTextBox.TabIndex = 17;
            this.GameCDPathTextBox.TextChanged += new System.EventHandler(this.GameCDPathTextBox_TextChanged);
            // 
            // NoConsoleCheckBox
            // 
            this.NoConsoleCheckBox.AutoSize = true;
            this.NoConsoleCheckBox.Location = new System.Drawing.Point(6, 19);
            this.NoConsoleCheckBox.Name = "NoConsoleCheckBox";
            this.NoConsoleCheckBox.Size = new System.Drawing.Size(78, 17);
            this.NoConsoleCheckBox.TabIndex = 27;
            this.NoConsoleCheckBox.Text = "no console";
            this.NoConsoleCheckBox.UseVisualStyleBackColor = true;
            // 
            // NoConfigCheckBox
            // 
            this.NoConfigCheckBox.AutoSize = true;
            this.NoConfigCheckBox.Location = new System.Drawing.Point(2, 223);
            this.NoConfigCheckBox.Name = "NoConfigCheckBox";
            this.NoConfigCheckBox.Size = new System.Drawing.Size(319, 17);
            this.NoConfigCheckBox.TabIndex = 15;
            this.NoConfigCheckBox.Text = "No config file at all (may not work with DOSBox 0.73 or newer)";
            this.NoConfigCheckBox.UseVisualStyleBackColor = true;
            this.NoConfigCheckBox.CheckedChanged += new System.EventHandler(this.NoConfigCheckBox_CheckedChanged);
            // 
            // FullscreenCheckBox
            // 
            this.FullscreenCheckBox.AutoSize = true;
            this.FullscreenCheckBox.Location = new System.Drawing.Point(90, 19);
            this.FullscreenCheckBox.Name = "FullscreenCheckBox";
            this.FullscreenCheckBox.Size = new System.Drawing.Size(71, 17);
            this.FullscreenCheckBox.TabIndex = 28;
            this.FullscreenCheckBox.Text = "fullscreen";
            this.FullscreenCheckBox.UseVisualStyleBackColor = true;
            // 
            // QuitOnExitCheckBox
            // 
            this.QuitOnExitCheckBox.AutoSize = true;
            this.QuitOnExitCheckBox.Location = new System.Drawing.Point(167, 19);
            this.QuitOnExitCheckBox.Name = "QuitOnExitCheckBox";
            this.QuitOnExitCheckBox.Size = new System.Drawing.Size(77, 17);
            this.QuitOnExitCheckBox.TabIndex = 29;
            this.QuitOnExitCheckBox.Text = "quit on exit";
            this.QuitOnExitCheckBox.UseVisualStyleBackColor = true;
            this.QuitOnExitCheckBox.EnabledChanged += new System.EventHandler(this.QuitOnExitCheckBox_EnabledChanged);
            // 
            // GameAdditionalCommandsTextBox
            // 
            this.GameAdditionalCommandsTextBox.Location = new System.Drawing.Point(1, 351);
            this.GameAdditionalCommandsTextBox.Name = "GameAdditionalCommandsTextBox";
            this.GameAdditionalCommandsTextBox.Size = new System.Drawing.Size(382, 20);
            this.GameAdditionalCommandsTextBox.TabIndex = 25;
            // 
            // OtherOptionsGroupBox
            // 
            this.OtherOptionsGroupBox.Controls.Add(this.FullscreenCheckBox);
            this.OtherOptionsGroupBox.Controls.Add(this.NoConsoleCheckBox);
            this.OtherOptionsGroupBox.Controls.Add(this.QuitOnExitCheckBox);
            this.OtherOptionsGroupBox.Location = new System.Drawing.Point(-1, 377);
            this.OtherOptionsGroupBox.Name = "OtherOptionsGroupBox";
            this.OtherOptionsGroupBox.Size = new System.Drawing.Size(414, 40);
            this.OtherOptionsGroupBox.TabIndex = 26;
            this.OtherOptionsGroupBox.TabStop = false;
            this.OtherOptionsGroupBox.Text = "Other options";
            // 
            // GameDirectoryTextbox
            // 
            this.GameDirectoryTextbox.Location = new System.Drawing.Point(2, 119);
            this.GameDirectoryTextbox.Name = "GameDirectoryTextbox";
            this.GameDirectoryTextbox.Size = new System.Drawing.Size(382, 20);
            this.GameDirectoryTextbox.TabIndex = 7;
            this.GameDirectoryTextbox.TextChanged += new System.EventHandler(this.GameDirectoryTextbox_TextChanged);
            // 
            // GameSetupTextBox
            // 
            this.GameSetupTextBox.Location = new System.Drawing.Point(2, 158);
            this.GameSetupTextBox.Name = "GameSetupTextBox";
            this.GameSetupTextBox.Size = new System.Drawing.Size(382, 20);
            this.GameSetupTextBox.TabIndex = 10;
            // 
            // UseIOCTLRadioButton
            // 
            this.UseIOCTLRadioButton.AutoSize = true;
            this.UseIOCTLRadioButton.Enabled = false;
            this.UseIOCTLRadioButton.Location = new System.Drawing.Point(6, 19);
            this.UseIOCTLRadioButton.Name = "UseIOCTLRadioButton";
            this.UseIOCTLRadioButton.Size = new System.Drawing.Size(152, 17);
            this.UseIOCTLRadioButton.TabIndex = 21;
            this.UseIOCTLRadioButton.TabStop = true;
            this.UseIOCTLRadioButton.Text = "Use IOCTL (for a CD drive)";
            this.UseIOCTLRadioButton.UseVisualStyleBackColor = true;
            // 
            // IsAFloppyDiskRadioButton
            // 
            this.IsAFloppyDiskRadioButton.AutoSize = true;
            this.IsAFloppyDiskRadioButton.Enabled = false;
            this.IsAFloppyDiskRadioButton.Location = new System.Drawing.Point(164, 19);
            this.IsAFloppyDiskRadioButton.Name = "IsAFloppyDiskRadioButton";
            this.IsAFloppyDiskRadioButton.Size = new System.Drawing.Size(186, 17);
            this.IsAFloppyDiskRadioButton.TabIndex = 22;
            this.IsAFloppyDiskRadioButton.TabStop = true;
            this.IsAFloppyDiskRadioButton.Text = "Floppy disk image (mounted as A:)";
            this.IsAFloppyDiskRadioButton.UseVisualStyleBackColor = true;
            // 
            // MountingOptionsGroupBox
            // 
            this.MountingOptionsGroupBox.Controls.Add(this.NoneRadioButton);
            this.MountingOptionsGroupBox.Controls.Add(this.UseIOCTLRadioButton);
            this.MountingOptionsGroupBox.Controls.Add(this.IsAFloppyDiskRadioButton);
            this.MountingOptionsGroupBox.Enabled = false;
            this.MountingOptionsGroupBox.Location = new System.Drawing.Point(2, 294);
            this.MountingOptionsGroupBox.Name = "MountingOptionsGroupBox";
            this.MountingOptionsGroupBox.Size = new System.Drawing.Size(411, 38);
            this.MountingOptionsGroupBox.TabIndex = 20;
            this.MountingOptionsGroupBox.TabStop = false;
            this.MountingOptionsGroupBox.Text = "Mounting options";
            // 
            // NoneRadioButton
            // 
            this.NoneRadioButton.AutoSize = true;
            this.NoneRadioButton.Location = new System.Drawing.Point(356, 19);
            this.NoneRadioButton.Name = "NoneRadioButton";
            this.NoneRadioButton.Size = new System.Drawing.Size(51, 17);
            this.NoneRadioButton.TabIndex = 23;
            this.NoneRadioButton.TabStop = true;
            this.NoneRadioButton.Text = "None";
            this.NoneRadioButton.UseVisualStyleBackColor = true;
            // 
            // OK
            // 
            this.OK.Image = ((System.Drawing.Image)(resources.GetObject("OK.Image")));
            this.OK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OK.Location = new System.Drawing.Point(230, 419);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(96, 23);
            this.OK.TabIndex = 30;
            this.OK.Text = "&Add this game";
            this.OK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Image = global::AmpShell.Properties.Resources.DeleteHS;
            this.Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Cancel.Location = new System.Drawing.Point(332, 419);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(82, 23);
            this.Cancel.TabIndex = 31;
            this.Cancel.Text = "&Don\'t add it";
            this.Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // ResetIconButton
            // 
            this.ResetIconButton.Image = global::AmpShell.Properties.Resources.DeleteHS;
            this.ResetIconButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ResetIconButton.Location = new System.Drawing.Point(264, 0);
            this.ResetIconButton.Name = "ResetIconButton";
            this.ResetIconButton.Size = new System.Drawing.Size(80, 23);
            this.ResetIconButton.TabIndex = 30;
            this.ResetIconButton.Text = "Reset icon";
            this.ResetIconButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ResetIconButton.UseVisualStyleBackColor = true;
            this.ResetIconButton.Click += new System.EventHandler(this.ResetIconButton_Click);
            // 
            // GameIconPictureBox
            // 
            this.GameIconPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GameIconPictureBox.Image = global::AmpShell.Properties.Resources.Generic_Application1;
            this.GameIconPictureBox.Location = new System.Drawing.Point(347, 0);
            this.GameIconPictureBox.Name = "GameIconPictureBox";
            this.GameIconPictureBox.Size = new System.Drawing.Size(66, 66);
            this.GameIconPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GameIconPictureBox.TabIndex = 29;
            this.GameIconPictureBox.TabStop = false;
            this.GameIconPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GameIconPictureBox_MouseClick);
            // 
            // GameCDDirBrowseButton
            // 
            this.GameCDDirBrowseButton.Image = global::AmpShell.Properties.Resources.SearchFolderHS;
            this.GameCDDirBrowseButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.GameCDDirBrowseButton.Location = new System.Drawing.Point(390, 265);
            this.GameCDDirBrowseButton.Name = "GameCDDirBrowseButton";
            this.GameCDDirBrowseButton.Size = new System.Drawing.Size(24, 23);
            this.GameCDDirBrowseButton.TabIndex = 19;
            this.GameCDDirBrowseButton.Text = "...";
            this.GameCDDirBrowseButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.GameCDDirBrowseButton.UseVisualStyleBackColor = true;
            this.GameCDDirBrowseButton.Click += new System.EventHandler(this.GameCDDirBrowseButton_Click);
            // 
            // GameSetupBrowseButton
            // 
            this.GameSetupBrowseButton.Image = global::AmpShell.Properties.Resources.search;
            this.GameSetupBrowseButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.GameSetupBrowseButton.Location = new System.Drawing.Point(390, 155);
            this.GameSetupBrowseButton.Name = "GameSetupBrowseButton";
            this.GameSetupBrowseButton.Size = new System.Drawing.Size(24, 23);
            this.GameSetupBrowseButton.TabIndex = 11;
            this.GameSetupBrowseButton.Text = "...";
            this.GameSetupBrowseButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.GameSetupBrowseButton.UseVisualStyleBackColor = true;
            this.GameSetupBrowseButton.Click += new System.EventHandler(this.GameSetupBrowseButton_Click);
            // 
            // GameSetupLabel
            // 
            this.GameSetupLabel.AutoSize = true;
            this.GameSetupLabel.Image = ((System.Drawing.Image)(resources.GetObject("GameSetupLabel.Image")));
            this.GameSetupLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GameSetupLabel.Location = new System.Drawing.Point(0, 142);
            this.GameSetupLabel.Name = "GameSetupLabel";
            this.GameSetupLabel.Size = new System.Drawing.Size(226, 13);
            this.GameSetupLabel.TabIndex = 9;
            this.GameSetupLabel.Text = "     Game setup executable location (optional) :";
            this.GameSetupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GameDirectoryBrowseButton
            // 
            this.GameDirectoryBrowseButton.Image = global::AmpShell.Properties.Resources.search;
            this.GameDirectoryBrowseButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.GameDirectoryBrowseButton.Location = new System.Drawing.Point(390, 117);
            this.GameDirectoryBrowseButton.Name = "GameDirectoryBrowseButton";
            this.GameDirectoryBrowseButton.Size = new System.Drawing.Size(24, 23);
            this.GameDirectoryBrowseButton.TabIndex = 8;
            this.GameDirectoryBrowseButton.Text = "...";
            this.GameDirectoryBrowseButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.GameDirectoryBrowseButton.UseVisualStyleBackColor = true;
            this.GameDirectoryBrowseButton.Click += new System.EventHandler(this.GameDirectoryBrowseButton_Click);
            // 
            // GameDirectoryLabel
            // 
            this.GameDirectoryLabel.AutoSize = true;
            this.GameDirectoryLabel.Image = global::AmpShell.Properties.Resources.Folder_Open;
            this.GameDirectoryLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GameDirectoryLabel.Location = new System.Drawing.Point(0, 103);
            this.GameDirectoryLabel.Name = "GameDirectoryLabel";
            this.GameDirectoryLabel.Size = new System.Drawing.Size(187, 13);
            this.GameDirectoryLabel.TabIndex = 6;
            this.GameDirectoryLabel.Text = "     Directory mounted as C: (optional) :";
            this.GameDirectoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GameAdditionalCommandsLabel
            // 
            this.GameAdditionalCommandsLabel.AutoSize = true;
            this.GameAdditionalCommandsLabel.Image = global::AmpShell.Properties.Resources.cmd;
            this.GameAdditionalCommandsLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GameAdditionalCommandsLabel.Location = new System.Drawing.Point(-1, 335);
            this.GameAdditionalCommandsLabel.Name = "GameAdditionalCommandsLabel";
            this.GameAdditionalCommandsLabel.Size = new System.Drawing.Size(298, 13);
            this.GameAdditionalCommandsLabel.TabIndex = 24;
            this.GameAdditionalCommandsLabel.Text = "      Additional DOSBox commands (-c \"command\") (optional) :";
            this.GameAdditionalCommandsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GameCDPathBrowseButton
            // 
            this.GameCDPathBrowseButton.Image = global::AmpShell.Properties.Resources.search;
            this.GameCDPathBrowseButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.GameCDPathBrowseButton.Location = new System.Drawing.Point(390, 243);
            this.GameCDPathBrowseButton.Name = "GameCDPathBrowseButton";
            this.GameCDPathBrowseButton.Size = new System.Drawing.Size(24, 23);
            this.GameCDPathBrowseButton.TabIndex = 18;
            this.GameCDPathBrowseButton.Text = "...";
            this.GameCDPathBrowseButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.GameCDPathBrowseButton.UseVisualStyleBackColor = true;
            this.GameCDPathBrowseButton.Click += new System.EventHandler(this.GameCDPathBrowseButton_Click);
            // 
            // GameCDPathLabel
            // 
            this.GameCDPathLabel.AutoSize = true;
            this.GameCDPathLabel.Image = global::AmpShell.Properties.Resources.CD_ROM;
            this.GameCDPathLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GameCDPathLabel.Location = new System.Drawing.Point(0, 243);
            this.GameCDPathLabel.Name = "GameCDPathLabel";
            this.GameCDPathLabel.Size = new System.Drawing.Size(266, 13);
            this.GameCDPathLabel.TabIndex = 16;
            this.GameCDPathLabel.Text = "      CD image file or directory mounted as D: (optional) :";
            this.GameCDPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GameNameLabel
            // 
            this.GameNameLabel.AutoSize = true;
            this.GameNameLabel.Image = global::AmpShell.Properties.Resources.Rename;
            this.GameNameLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GameNameLabel.Location = new System.Drawing.Point(-1, 25);
            this.GameNameLabel.Name = "GameNameLabel";
            this.GameNameLabel.Size = new System.Drawing.Size(91, 13);
            this.GameNameLabel.TabIndex = 1;
            this.GameNameLabel.Text = "      Game name : ";
            this.GameNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GameCustomConfigurationBrowseButton
            // 
            this.GameCustomConfigurationBrowseButton.Image = global::AmpShell.Properties.Resources.search;
            this.GameCustomConfigurationBrowseButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.GameCustomConfigurationBrowseButton.Location = new System.Drawing.Point(390, 194);
            this.GameCustomConfigurationBrowseButton.Name = "GameCustomConfigurationBrowseButton";
            this.GameCustomConfigurationBrowseButton.Size = new System.Drawing.Size(24, 23);
            this.GameCustomConfigurationBrowseButton.TabIndex = 14;
            this.GameCustomConfigurationBrowseButton.Text = "...";
            this.GameCustomConfigurationBrowseButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.GameCustomConfigurationBrowseButton.UseVisualStyleBackColor = true;
            this.GameCustomConfigurationBrowseButton.Click += new System.EventHandler(this.GameCustomConfigurationBrowseButton_Click);
            // 
            // GameCustomCofigurationLabel
            // 
            this.GameCustomCofigurationLabel.AutoSize = true;
            this.GameCustomCofigurationLabel.Image = global::AmpShell.Properties.Resources.Configuration;
            this.GameCustomCofigurationLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GameCustomCofigurationLabel.Location = new System.Drawing.Point(0, 181);
            this.GameCustomCofigurationLabel.Name = "GameCustomCofigurationLabel";
            this.GameCustomCofigurationLabel.Size = new System.Drawing.Size(213, 13);
            this.GameCustomCofigurationLabel.TabIndex = 12;
            this.GameCustomCofigurationLabel.Text = "     Custom configuration location (optional) :";
            this.GameCustomCofigurationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GameLocationBrowseButton
            // 
            this.GameLocationBrowseButton.Image = global::AmpShell.Properties.Resources.search;
            this.GameLocationBrowseButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.GameLocationBrowseButton.Location = new System.Drawing.Point(390, 77);
            this.GameLocationBrowseButton.Name = "GameLocationBrowseButton";
            this.GameLocationBrowseButton.Size = new System.Drawing.Size(24, 23);
            this.GameLocationBrowseButton.TabIndex = 5;
            this.GameLocationBrowseButton.Text = "...";
            this.GameLocationBrowseButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.GameLocationBrowseButton.UseVisualStyleBackColor = true;
            this.GameLocationBrowseButton.Click += new System.EventHandler(this.GameLocationBrowseButton_Click);
            // 
            // GameLocationLabel
            // 
            this.GameLocationLabel.AutoSize = true;
            this.GameLocationLabel.Image = global::AmpShell.Properties.Resources.GameEditExecutableLabelImage;
            this.GameLocationLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GameLocationLabel.Location = new System.Drawing.Point(0, 64);
            this.GameLocationLabel.Name = "GameLocationLabel";
            this.GameLocationLabel.Size = new System.Drawing.Size(341, 13);
            this.GameLocationLabel.TabIndex = 3;
            this.GameLocationLabel.Text = "      Game executable location (optional if a directory is mounted as C:) :";
            this.GameLocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GameForm
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(414, 441);
            this.Controls.Add(this.MountingOptionsGroupBox);
            this.Controls.Add(this.ResetIconButton);
            this.Controls.Add(this.GameIconPictureBox);
            this.Controls.Add(this.GameCDDirBrowseButton);
            this.Controls.Add(this.GameSetupBrowseButton);
            this.Controls.Add(this.GameSetupLabel);
            this.Controls.Add(this.GameSetupTextBox);
            this.Controls.Add(this.GameDirectoryBrowseButton);
            this.Controls.Add(this.GameDirectoryLabel);
            this.Controls.Add(this.GameDirectoryTextbox);
            this.Controls.Add(this.OtherOptionsGroupBox);
            this.Controls.Add(this.GameAdditionalCommandsLabel);
            this.Controls.Add(this.GameAdditionalCommandsTextBox);
            this.Controls.Add(this.NoConfigCheckBox);
            this.Controls.Add(this.GameCDPathBrowseButton);
            this.Controls.Add(this.GameCDPathLabel);
            this.Controls.Add(this.GameCDPathTextBox);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.GameNameTextbox);
            this.Controls.Add(this.GameNameLabel);
            this.Controls.Add(this.GameCustomConfigurationBrowseButton);
            this.Controls.Add(this.GameCustomCofigurationLabel);
            this.Controls.Add(this.GameCustomConfigurationTextbox);
            this.Controls.Add(this.GameLocationBrowseButton);
            this.Controls.Add(this.GameLocationLabel);
            this.Controls.Add(this.GameLocationTextbox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add a game...";
            this.OtherOptionsGroupBox.ResumeLayout(false);
            this.OtherOptionsGroupBox.PerformLayout();
            this.MountingOptionsGroupBox.ResumeLayout(false);
            this.MountingOptionsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GameIconPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox GameLocationTextbox;
        private System.Windows.Forms.Label GameLocationLabel;
        private System.Windows.Forms.Button GameLocationBrowseButton;
        private System.Windows.Forms.Button GameCustomConfigurationBrowseButton;
        private System.Windows.Forms.Label GameCustomCofigurationLabel;
        private System.Windows.Forms.TextBox GameCustomConfigurationTextbox;
        private System.Windows.Forms.Label GameNameLabel;
        private System.Windows.Forms.TextBox GameNameTextbox;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button GameCDPathBrowseButton;
        private System.Windows.Forms.Label GameCDPathLabel;
        private System.Windows.Forms.TextBox GameCDPathTextBox;
        private System.Windows.Forms.CheckBox NoConsoleCheckBox;
        private System.Windows.Forms.CheckBox NoConfigCheckBox;
        private System.Windows.Forms.CheckBox FullscreenCheckBox;
        private System.Windows.Forms.CheckBox QuitOnExitCheckBox;
        private System.Windows.Forms.Label GameAdditionalCommandsLabel;
        private System.Windows.Forms.TextBox GameAdditionalCommandsTextBox;
        private System.Windows.Forms.GroupBox OtherOptionsGroupBox;
        private System.Windows.Forms.Button GameDirectoryBrowseButton;
        private System.Windows.Forms.Label GameDirectoryLabel;
        private System.Windows.Forms.TextBox GameDirectoryTextbox;
        private System.Windows.Forms.Button GameSetupBrowseButton;
        private System.Windows.Forms.Label GameSetupLabel;
        private System.Windows.Forms.TextBox GameSetupTextBox;
        private System.Windows.Forms.Button GameCDDirBrowseButton;
        private System.Windows.Forms.PictureBox GameIconPictureBox;
        private System.Windows.Forms.Button ResetIconButton;
        private System.Windows.Forms.RadioButton UseIOCTLRadioButton;
        private System.Windows.Forms.RadioButton IsAFloppyDiskRadioButton;
        private System.Windows.Forms.GroupBox MountingOptionsGroupBox;
        private System.Windows.Forms.RadioButton NoneRadioButton;
    }
}