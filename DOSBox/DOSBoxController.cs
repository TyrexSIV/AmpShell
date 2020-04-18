﻿/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2020 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

namespace AmpShell.DOSBox
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;
    using AmpShell.DAL;
    using AmpShell.Model;

    /// <summary> Used to start DOSBox with a game in it. </summary>
    public static class DOSBoxController
    {
        public static void AskForDOSBox()
        {
            //if DOSBoxPath is still empty, say to the user that dosbox's executable cannot be found
            if (string.IsNullOrEmpty(UserDataAccessor.UserData.DBPath))
            {
                switch (MessageBox.Show("AmpShell cannot find DOSBox, do you want to indicate DOSBox's executable location now ? Choose 'Cancel' to quit.", "Cannot find DOSBox", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Cancel:
                        Environment.Exit(0);
                        break;

                    case DialogResult.Yes:
                        {
                            using (var dosboxExeFileDialog = new OpenFileDialog())
                            {
                                dosboxExeFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                                dosboxExeFileDialog.Title = "Please indicate DOSBox's executable location...";
                                dosboxExeFileDialog.Filter = "DOSBox executable (dosbox*)|dosbox*";
                                if (dosboxExeFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    UserDataAccessor.UserData.DBPath = dosboxExeFileDialog.FileName;
                                }
                                else
                                {
                                    Environment.Exit(0);
                                }
                            }
                        }
                        break;

                    case DialogResult.No:
                        UserDataAccessor.UserData.DBPath = string.Empty;
                        break;
                }
            }
        }

        /// <summary> Builds the argument line in order to start DOSBox. </summary>
        /// <param name="selectedGame"> Game the user wants to start. </param>
        /// <param name="forSetupExe">
        /// Whether or not we are starting the game's setup utility or the game itself.
        /// </param>
        /// <param name="dosBoxExePath"> The path to DOSBox.exe. </param>
        /// <param name="dosboxDefaultConfFilePath"> The .conf file to use for DOSBox. </param>
        /// <param name="dosboxDefaultLangFilePath"> The .lng file to use for DOSBox. </param>
        /// <returns> . </returns>
        public static string BuildArgs(Game selectedGame, bool forSetupExe, string dosBoxExePath, string dosboxDefaultConfFilePath, string dosboxDefaultLangFilePath)
        {
            if (selectedGame == null)
            {
                return string.Empty;
            }
            var configFile = new DOSBoxConfigFile(selectedGame.DBConfPath);

            //Arguments string for DOSBox.exe
            string dosboxArgs = string.Empty;
            if (string.IsNullOrEmpty(dosBoxExePath) == true || dosBoxExePath == "dosbox.exe isn't is the same directory as AmpShell.exe!" || File.Exists(dosBoxExePath) == false)
            {
                throw new FileNotFoundException("DOSBox not found!");
            }

            dosboxArgs += AddCustomConfigFile(selectedGame, dosboxDefaultConfFilePath);

            dosboxArgs += AddPrefsLangFile(dosboxDefaultLangFilePath);

            dosboxArgs += AddAdditionalCommands(selectedGame, forSetupExe, configFile);

            //corresponds to the Fullscreen checkbox in GameForm
            if (selectedGame.InFullScreen == true)
            {
                dosboxArgs += " -fullscreen";
            }

            //corresponds to the "no console" checkbox in the GameForm
            if (selectedGame.NoConsole == true)
            {
                dosboxArgs += " -noconsole";
            }

            //corresponds to the "quit on exit (only for .exe)" checkbox in the GameForm
            if (selectedGame.QuitOnExit == true)
            {
                dosboxArgs += " -exit";
            }

            return dosboxArgs;
        }

        /// <summary> Run DOSBox itself, without any game. </summary>
        /// <param name="dosboxPath"> Path to DOSBox.exe. </param>
        /// <param name="dosboxDefaultConfFilePath"> Path to DOSBox.conf. </param>
        /// <param name="dosboxDefaultLangFilePath"> Path to DOSBox.lng. </param>
        /// <returns> The DOSBox process if it started successfully, null otherwise. </returns>
        public static Process RunOnlyDOSBox(string dosboxPath, string dosboxDefaultConfFilePath, string dosboxDefaultLangFilePath)
        {
            if (string.IsNullOrEmpty(dosboxPath) == true)
            {
                return null;
            }

            //check first for the lang file
            string languageFile = string.Empty;
            if (string.IsNullOrEmpty(dosboxDefaultConfFilePath) == false)
            {
                languageFile = $" -lang \"{dosboxDefaultConfFilePath}\"";
            }

            //then for the conf file
            if (string.IsNullOrEmpty(dosboxDefaultLangFilePath) == false)
            {
                return StartDOSBox(dosboxPath, $" -conf \"{dosboxDefaultConfFilePath}\"{languageFile}");
            }
            else
            {
                return StartDOSBox(dosboxPath, languageFile);
            }
        }

        /// <summary> Starts DOSBox, and returns whether it was successful. </summary>
        /// <param name="dosboxPath"> Path to DOSBox.exe. </param>
        /// <param name="args"> Command line args passed to DOSBox. </param>
        /// <param name="customConfFilePath"> DOSBox config file to use (optional). </param>
        /// <returns> True if DOSBox started, false if it did not. </returns>
        public static Process StartDOSBox(string dosboxPath, string args, string customConfFilePath = "")
        {
            var psi = new ProcessStartInfo(dosboxPath);

            if (string.IsNullOrEmpty(customConfFilePath) == false)
            {
                psi.WorkingDirectory = Path.GetDirectoryName(customConfFilePath);
            }

            if (string.IsNullOrEmpty(args) == false)
            {
                psi.Arguments = args;
            }
            Process dosboxProcess = Process.Start(psi);
            if (dosboxProcess != null)
            {
                dosboxProcess.EnableRaisingEvents = true;
            }
            return dosboxProcess;
        }

        private static string AddAdditionalCommands(Game selectedGame, bool forSetupExe, DOSBoxConfigFile configFile)
        {
            string dosboxArgs = string.Empty;
            if (configFile.IsAutoExecSectionUsed() == true)
            {
                return dosboxArgs;
            }

            //The arguments for DOSBox begins with the game executable (.exe, .bat, or .com)
            if (string.IsNullOrEmpty(selectedGame.DOSEXEPath) == false)
            {
                if (!forSetupExe)
                {
                    dosboxArgs = $"\"{selectedGame.DOSEXEPath}\"";
                }
                else
                {
                    dosboxArgs = $"\"{selectedGame.SetupEXEPath}\"";
                }
            }

            //the game directory mounted as C (if the DOSEXEPath is specified, the DOSEXEPath parent directory will be mounted as C: by DOSBox
            //hence the "else if" instead of "if".
            else if (string.IsNullOrEmpty(selectedGame.Directory) == false)
            {
                dosboxArgs = $" -c \"mount c '{selectedGame.Directory}'\"";
            }

            //Path for the game's CD image (.bin, .cue, or .iso) mounted as D:
            if (string.IsNullOrEmpty(selectedGame.CDPath) == false)
            {
                //put ' and not " after imgmount (or else the path will be misunderstood by DOSBox).
                if (selectedGame.CDIsAnImage == true)
                {
                    dosboxArgs += $" -c \"imgmount";
                    if (selectedGame.MountAsFloppy == true)
                    {
                        dosboxArgs += $" a '{selectedGame.CDPath}' -t floppy\"";
                    }
                    else
                    {
                        dosboxArgs += $" d '{selectedGame.CDPath}' -t iso\"";
                    }
                }
                else
                {
                    bool addedMountOptions;
                    if (selectedGame.UseIOCTL == true)
                    {
                        addedMountOptions = true;
                        dosboxArgs += $" -c \"mount d '{selectedGame.CDPath}' -t cdrom -usecd 0 -ioctl";
                    }
                    else if (selectedGame.MountAsFloppy == true)
                    {
                        addedMountOptions = true;
                        dosboxArgs += $" -c \"mount a '{selectedGame.CDPath}' -t floppy";
                    }
                    else
                    {
                        addedMountOptions = true;
                        dosboxArgs += $" -c \"mount d '{selectedGame.CDPath}'";
                    }
                    if (string.IsNullOrEmpty(selectedGame.CDLabel) == false && addedMountOptions)
                    {
                        dosboxArgs += $" -label {selectedGame.CDLabel}";
                    }
                    if (addedMountOptions)
                    {
                        dosboxArgs += '"';
                    }
                }
            }

            //Additional user commands for the game
            if (string.IsNullOrEmpty(selectedGame.AdditionalCommands) == false)
            {
                dosboxArgs += $" {selectedGame.AdditionalCommands}";
            }

            return dosboxArgs;
        }

        private static string AddCustomConfigFile(Game selectedGame, string dosboxDefaultConfFilePath)
        {
            string gameConfigFilePath = string.Empty;

            //if the "do not use any config file at all" has not been checked
            if (selectedGame.NoConfig == false)
            {
                //use at first the game's custom config file
                if (string.IsNullOrEmpty(selectedGame.DBConfPath) == false)
                {
                    gameConfigFilePath = selectedGame.DBConfPath;
                }

                //if not, use the default dosbox.conf file
                else if (string.IsNullOrEmpty(dosboxDefaultConfFilePath) == false && dosboxDefaultConfFilePath != "No configuration file (*.conf) found in AmpShell's directory.")
                {
                    gameConfigFilePath = dosboxDefaultConfFilePath;
                }
            }
            string dosboxArgs = string.Empty;
            if (string.IsNullOrEmpty(gameConfigFilePath) == false)
            {
                dosboxArgs += $" -conf \"{gameConfigFilePath}\"";
            }

            return dosboxArgs;
        }

        /// <summary>
        /// Path for the default language file used for DOSBox and specified by the user in the
        /// Tools menu.
        /// </summary>
        /// <param name="dosboxDefaultLangFilePath"> . </param>
        private static string AddPrefsLangFile(string dosboxDefaultLangFilePath)
        {
            string dosboxArgs = string.Empty;
            if (string.IsNullOrEmpty(dosboxDefaultLangFilePath) == false && dosboxDefaultLangFilePath != "No language file (*.lng) found in AmpShell's directory.")
            {
                dosboxArgs += $" -lang \"{dosboxDefaultLangFilePath}\"";
            }

            return dosboxArgs;
        }
    }
}