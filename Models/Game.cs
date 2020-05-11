/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2020 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

using Avalonia.Diagnostics.ViewModels;

using System;
using System.Xml.Serialization;

namespace AmpShell.Models
{
    public class Game : ViewModelBase
    {
        [XmlAttribute("Signature")]
        public string Signature { get; set; }

        public int Id => Convert.ToInt32(Id);

        private string _name;

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private string _directory;

        public DateTime ReleaseDate
        {
            get => releaseDate;
            set { Set(ref releaseDate, value); }
        }

        private string directory = string.Empty;

        /// <summary>
        /// Gets or sets game's directory mounted as C:.
        /// </summary>
        public string Directory
        {
            get => _directory;
            set => this.RaiseAndSetIfChanged(ref _directory, value);
        }

        private string _cdPath;

        /// <summary>
        /// Gets or sets game's CD image / CD directory (like 'D:\') location.
        /// </summary>
        public string CDPath
        {
            get => _cdPath;
            set => this.RaiseAndSetIfChanged(ref _cdPath, value);
        }

        private string _setupEEXEPath;

        /// <summary>
        /// Optional, user-specified CD LABEL (only when it is not an image)
        /// </summary>
        public string CDLabel
        {
            get => _cdLabel;
            set { this.RaiseAndSetIfChanged(ref _cdLabel, value); }
        }

        private string _setupEXEPath = "";

        /// <summary>
        /// Game's setup executable location
        /// </summary>
        public string SetupEXEPath
        {
            get => _setupEEXEPath;
            set => this.RaiseAndSetIfChanged(ref _setupEEXEPath, value);
        }

        private string _dbConfPath;

        /// <summary>
        /// Gets or sets game's custom DOSBox .conf file path.
        /// </summary>
        public string DBConfPath
        {
            get => _dbConfPath;
            set => this.RaiseAndSetIfChanged(ref _dbConfPath, value);
        }

        private string _additionalCommands;

        /// <summary>
        /// Gets or sets game's additional commands for DOSBox.
        /// </summary>
        public string AdditionalCommands
        {
            get => _additionalCommands;
            set => this.RaiseAndSetIfChanged(ref _additionalCommands, value);
        }

        private bool useIOCTL;

        /// <summary>
        /// Gets or sets a value indicating whether option to use IOCTL (only available for optical drives).
        /// </summary>
        public bool UseIOCTL
        {
            get => _useIOCTL;
            set => this.RaiseAndSetIfChanged(ref _useIOCTL, value);
        }

        private bool mountAsFloppy;

        /// <summary>
        /// Gets or sets a value indicating whether option to use the image file as a floppy (A:).
        /// </summary>
        public bool MountAsFloppy
        {
            get => _mountAsFloppy;
            set => this.RaiseAndSetIfChanged(ref _mountAsFloppy, value);
        }

        private bool noConfig;

        /// <summary>
        /// Gets or sets a value indicating whether boolean if no config is used ("Don't use any
        /// config file at all" checkbox in GameForm) Legacy 0.72 or older DOSBox option.
        /// </summary>
        public bool NoConfig
        {
            get => _noConfig;
            set => this.RaiseAndSetIfChanged(ref _noConfig, value);
        }

        private bool inFullScreen;

        public bool InFullScreen
        {
            get => _inFullScreen;
            set => this.RaiseAndSetIfChanged(ref _inFullScreen, value);
        }

        private bool noConsole;

        /// <summary>
        /// Gets or sets a value indicating whether boolean for displaying DOSBox's console.
        /// </summary>
        public bool NoConsole
        {
            get => _noConsole;
            set => this.RaiseAndSetIfChanged(ref _noConsole, value);
        }

        private bool quitOnExit;

        /// <summary>
        /// Gets or sets a value indicating whether boolean for the -exit switch for DOSBox (if set
        /// to true, DOSBox closes when the game exits).
        /// </summary>
        public bool QuitOnExit
        {
            get => _quitOnExit;
            set => this.RaiseAndSetIfChanged(ref _quitOnExit, value);
        }

        private string _dosExePath;

        /// <summary>
        /// Gets or sets game's main executable location.
        /// </summary>
        public string DOSEXEPath
        {
            get => _dosExePath;
            set => this.RaiseAndSetIfChanged(ref _dosExePath, value);
        }

        private bool cdIsAnImage;

        /// <summary>
        /// Gets or sets a value indicating whether if GameCDPath points to a CD image file (false
        /// if it points to a directory).
        /// </summary>
        public bool CDIsAnImage
        {
            get => _cdIsAnImage;
            set => this.RaiseAndSetIfChanged(ref _cdIsAnImage, value);
        }

        private string _icon;

        public string Icon
        {
            get => _icon;
            set => this.RaiseAndSetIfChanged(ref _icon, value);
        }

        private string _alternateDOSBoxExePath;

        /// <summary>
        /// Gets or sets if we want to use DOSBox Daum, ECE, SVN, or other instead of the one set in
        /// the global preferences.
        /// </summary>
        public string AlternateDOSBoxExePath
        {
            get => _alternateDOSBoxExePath;
            set => this.RaiseAndSetIfChanged(ref _alternateDOSBoxExePath, value);
        }
    }
}