/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2020 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

using AmpShell.Enums;

using System;
using System.Xml.Serialization;

namespace AmpShell.Models
{
    using System.Windows.Forms;
    using System.Xml.Serialization;

    public class Category : ModelWithChildren
    {
        public Category()
            : base()
        {
        }

        public Category(string categoryTitle, string categorySignature)
        {
            this.Title = categoryTitle;
            this.Signature = categorySignature;
        }

        [XmlAttribute("Title")]
        public string Title { get; set; }

        [XmlAttribute("Signature")]
        public string Signature { get; set; }

        public int Id => Convert.ToInt32(Signature);

        private int _nameColumnWidth = 150;

        public int NameColumnWidth
        {
            get => _nameColumnWidth;
            set => this.RaiseAndSetIfChanged(ref _nameColumnWidth, value);
        }

        private int releaseDateColumnWidth = 150;

        public int ReleaseDateColumnWidth
        {
            get => releaseDateColumnWidth;
            set { Set(ref releaseDateColumnWidth, value); }
        }

        private int executableColumnWith = 150;

        public int ExecutableColumnWidth
        {
            get => _executableColumnWith;
            set => base.RaiseAndSetIfChanged(ref _executableColumnWith, value);
        }

        private int cMountColumnWidth = 150;

        public int CMountColumnWidth
        {
            get => _cMountColumnWidth;
            set => this.RaiseAndSetIfChanged(ref _cMountColumnWidth, value);
        }

        private int setupExecutableColumnWidth = 150;

        public int SetupExecutableColumnWidth
        {
            get => _setupExecutableColumnWidth;
            set => this.RaiseAndSetIfChanged(ref _setupExecutableColumnWidth, value);
        }

        private int customConfigurationColumnWidth = 150;

        public int CustomConfigurationColumnWidth
        {
            get => _customConfigurationColumnWidth;
            set => this.RaiseAndSetIfChanged(ref _customConfigurationColumnWidth, value);
        }

        private int dMountColumnWidth = 150;

        public int DMountColumnWidth
        {
            get => _dMountColumnWidth;
            set => this.RaiseAndSetIfChanged(ref _dMountColumnWidth, value);
        }

        private int mountingOptionsColumnWidth = 100;

        public int MountingOptionsColumnWidth
        {
            get => _mountingOptionsColumnWidth;
            set => this.RaiseAndSetIfChanged(ref _mountingOptionsColumnWidth, value);
        }

        private int _additionnalCommandsColumnWidth = 150;

        public int AdditionnalCommandsColumnWidth
        {
            get => _additionnalCommandsColumnWidth;
            set => this.RaiseAndSetIfChanged(ref _additionnalCommandsColumnWidth, value);
        }

        private int noConsoleColumnWidth = 100;

        public int NoConsoleColumnWidth
        {
            get => _noConsoleColumnWidth;
            set => this.RaiseAndSetIfChanged(ref _noConsoleColumnWidth, value);
        }

        private int fullscreenColumnWidth = 100;

        public int FullscreenColumnWidth
        {
            get => _fullscreenColumnWidth;
            set => this.RaiseAndSetIfChanged(ref _fullscreenColumnWidth, value);
        }

        private int quitOnExitColumnWidth = 100;

        public int QuitOnExitColumnWidth
        {
            get => _quitOnExitColumnWidth;
            set => this.RaiseAndSetIfChanged(ref _quitOnExitColumnWidth, value);
        }

        private ViewMode _viewMode = ViewMode.LargeIcon;

        public ViewMode ViewMode
        {
            get => _viewMode;
            set => this.RaiseAndSetIfChanged(ref _viewMode, value);
        }
    }
}