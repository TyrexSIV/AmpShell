using AmpShell.Model;
using AmpShell.Notification;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmpShell.ViewModel
{
    public class GameViewModel : PropertyChangedNotifier
    {
        public Game Model { get; private set; }

        public GameViewModel() : base()
        {
            Model = new Game();
            ReplacePortableModePathsWithRealPaths();
        }

        public GameViewModel(Game model)
        {
            Model = model;
            ReplacePortableModePathsWithRealPaths();
        }

        private void ReplacePortableModePathsWithRealPaths()
        {
            Model.Icon = Model.Icon.Replace("AppPath", Application.StartupPath);
            Model.DOSEXEPath = Model.DOSEXEPath.Replace("AppPath", Application.StartupPath);
            Model.Directory = Model.Directory.Replace("AppPath", Application.StartupPath);
            Model.DBConfPath = Model.DBConfPath.Replace("AppPath", Application.StartupPath);
            Model.CDPath = Model.CDPath.Replace("AppPath", Application.StartupPath);
            Model.AdditionalCommands = Model.AdditionalCommands.Replace("AppPath", Application.StartupPath);
            Model.SetupEXEPath = Model.SetupEXEPath.Replace("AppPath", Application.StartupPath);
            Model.AlternateDOSBoxExePath = Model.AlternateDOSBoxExePath.Replace("AppPath", Application.StartupPath);
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
