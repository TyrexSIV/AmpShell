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

using System.Collections.Generic;
using System.Xml.Serialization;

namespace AmpShell.Models
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType(TypeName = "Window")]
    public class Preferences : ModelWithChildren
    {
        public static readonly List<int> LargeViewModeSizes = new List<int> { 48, 64, 80, 96, 112, 128, 144, 160, 176, 192, 208, 224, 240, 256 };

        private bool _exitOnGameLaunch = false;

        public bool ExitOnGameLaunch
        {
            get => _exitOnGameLaunch;
            set => this.RaiseAndSetIfChanged(ref _exitOnGameLaunch, value);
        }

        private bool _goToTaskBarOnGameLaunch = true;

        public bool GoToTaskBarOnGameLaunch
        {
            get => _goToTaskBarOnGameLaunch;
            set => this.RaiseAndSetIfChanged(ref _goToTaskBarOnGameLaunch, value);
        }

        private bool _portableMode = false;

        public bool PortableMode
        {
            get => _portableMode;
            set => this.RaiseAndSetIfChanged(ref _portableMode, value);
        }

        private bool defaultIconViewOverride = false;

        public bool DefaultIconViewOverride
        {
            get => _defaultIconViewOverride;
            set => this.RaiseAndSetIfChanged(ref _defaultIconViewOverride, value);
        }

        public int X { get; set; }

        public int Y { get; set; }

        private ViewMode _categoriesDefaultViewMode = ViewMode.LargeIcon;

        public ViewMode CategoriesDefaultViewMode
        {
            get => _categoriesDefaultViewMode;
            set => this.RaiseAndSetIfChanged(ref _categoriesDefaultViewMode, value);
        }

        private bool rememberWindowPosition = true;

        public bool RememberWindowPosition
        {
            get => _rememberWindowPosition;
            set => this.RaiseAndSetIfChanged(ref _rememberWindowPosition, value);
        }

        private bool rememberWindowSize = true;

        public bool RememberWindowSize
        {
            get => _rememberWindowSize;
            set => this.RaiseAndSetIfChanged(ref _rememberWindowSize, value);
        }

        private bool gameDeletePrompt = true;

        public bool GameDeletePrompt
        {
            get => _gameDeletePrompt;
            set => this.RaiseAndSetIfChanged(ref _gameDeletePrompt, value);
        }

        private bool categoryDeletePrompt = true;

        public bool CategoryDeletePrompt
        {
            get => _categoryDeletePrompt;
            set => this.RaiseAndSetIfChanged(ref _categoryDeletePrompt, value);
        }

        private bool gamesNoConsole = true;

        public bool GamesNoConsole
        {
            get => _gamesNoConsole;
            set => this.RaiseAndSetIfChanged(ref _gamesNoConsole, value);
        }

        private bool gamesInFullScreen = true;

        public bool GamesInFullScreen
        {
            get => _gamesInFullScreen;
            set => this.RaiseAndSetIfChanged(ref _gamesInFullScreen, value);
        }

        private bool gamesQuitOnExit = true;

        public bool GamesQuitOnExit
        {
            get => _gamesQuitOnExit;
            set => this.RaiseAndSetIfChanged(ref _gamesQuitOnExit, value);
        }

        private string _gamesAdditionnalCommands;

        public string GamesAdditionalCommands
        {
            get => _gamesAdditionnalCommands;
            set => this.RaiseAndSetIfChanged(ref _gamesAdditionnalCommands, value);
        }

        private string _gamesDefaultDir;

        public string GamesDefaultDir
        {
            get => _gamesDefaultDir;
            set => this.RaiseAndSetIfChanged(ref _gamesDefaultDir, value);
        }

        private string _cdsDefaultDir;

        public string CDsDefaultDir
        {
            get => _cdsDefaultDir;
            set => this.RaiseAndSetIfChanged(ref _cdsDefaultDir, value);
        }

        private string _configEditorPath;

        public string ConfigEditorPath
        {
            get => _configEditorPath;
            set => this.RaiseAndSetIfChanged(ref _configEditorPath, value);
        }

        private string _configEditorAdditionalParameters;

        public string ConfigEditorAdditionalParameters
        {
            get => _configEditorAdditionalParameters;
            set => this.RaiseAndSetIfChanged(ref _configEditorAdditionalParameters, value);
        }

        private bool fullScreen;

        public bool Fullscreen
        {
            get => _fullScreen;
            set => this.RaiseAndSetIfChanged(ref _fullScreen, value);
        }

        private bool menuBarVisible = true;

        public bool MenuBarVisible
        {
            get => _menuBarVisible;
            set => this.RaiseAndSetIfChanged(ref _menuBarVisible, value);
        }

        private bool toolBarVisible = true;

        public bool ToolBarVisible
        {
            get => _toolBarVisible;
            set => this.RaiseAndSetIfChanged(ref _toolBarVisible, value);
        }

        private bool _statusBarVisble = true;

        public bool StatusBarVisible
        {
            get => _statusBarVisble;
            set => this.RaiseAndSetIfChanged(ref _statusBarVisble, value);
        }

        private string _dbPath;

        public string DBPath
        {
            get => _dbPath;
            set => this.RaiseAndSetIfChanged(ref _dbPath, value);
        }

        private string _dbDefaultConfFilePath;

        public string DBDefaultConfFilePath
        {
            get => _dbDefaultConfFilePath;
            set => this.RaiseAndSetIfChanged(ref _dbDefaultConfFilePath, value);
        }

        private string _dbDefaultLangFilePath;

        public string DBDefaultLangFilePath
        {
            get => _dbDefaultLangFilePath;
            set => this.RaiseAndSetIfChanged(ref _dbDefaultLangFilePath, value);
        }

        private int width = 640;

        public int Width
        {
            get => _width;
            set => this.RaiseAndSetIfChanged(ref _width, value);
        }

        private int height = 400;

        public int Height
        {
            get => _height;
            set => this.RaiseAndSetIfChanged(ref _height, value);
        }

        private int largeViewModeSize = 48;

        public int LargeViewModeSize
        {
            get => _largeViewModeSize;
            set => this.RaiseAndSetIfChanged(ref _largeViewModeSize, value);
        }
    }
}