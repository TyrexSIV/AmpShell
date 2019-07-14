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
using AmpShell.Models;
using AmpShell.Serialization;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AmpShell.DAL
{
    public static class UserDataAccessor
    {
        static UserDataAccessor()
        {
            UserData = LoadUserDataAsync().Result;
        }

        public static void CreateCategory(Category category)
        {
            category.Signature = GetAUniqueSignature();
            UserData.AddChild(category);
            _ = SaveUserDataAsync();
        }

        public static Category GetCategory(int id)
        {
            return UserData.ListChildren.Cast<Category>().FirstOrDefault(x => x.Id == id);
        }

        public static void UpdateCategory(Category updatedCategory)
        {
            var selectedCategory = GetCategory(updatedCategory.Id);
            if (selectedCategory != null)
            {
                UserData.RemoveChild(selectedCategory);
                int index = UserData.ListChildren.Cast<Category>().ToList().IndexOf(selectedCategory);
                UserData.AddChild(updatedCategory);
                UserData.MoveChildToPosition(updatedCategory, index);
                _ = SaveUserDataAsync();
            }
        }

        public static void DeleteCategory(int id)
        {
            var selectedCategory = GetCategory(id);
            UserData.RemoveChild(UserData.ListChildren.Cast<Category>().FirstOrDefault(x => x.Id == id));
            _ = SaveUserDataAsync();
        }

        public static List<Category> GetAllCategories()
        {
            return UserDataAccessor.UserData.ListChildren.Cast<Category>().ToList();
        }

        public static Game CreateGame(int categoryId, Game game)
        {
            var selectedCategory = GetCategory(categoryId);
            game.Signature = GetAUniqueSignature();
            if (selectedCategory != null)
            {
                selectedCategory.AddChild(game);
                _ = SaveUserDataAsync();
            }
            return game;
        }

        public static Game GetGame(int id)
        {
            return UserData.ListChildren.Cast<Category>().SelectMany(x => x.ListChildren.Cast<Game>()).FirstOrDefault(x => x.Id == id);
        }

        public static void UpdateGame(Game updatedGame)
        {
            var selectedGame = GetGame(updatedGame.Id);
            var category = GetParentCategory(selectedGame.Id);
            int index = category.ListChildren.Cast<Game>().ToList().IndexOf(selectedGame);
            category.RemoveChild(selectedGame);
            category.AddChild(updatedGame);
            category.MoveChildToPosition(updatedGame, index);
            _ = SaveUserDataAsync();
        }

        public static void DeleteGame(int gameId)
        {
            var selectedGame = GetGame(gameId);
            if (selectedGame == null)
            {
                return;
            }
            Category parentCategory = GetParentCategory(gameId);
            if (parentCategory == null)
            {
                return;
            }
            parentCategory.RemoveChild(selectedGame);
            _ = SaveUserDataAsync();
        }

        private static Category GetParentCategory(int gameId)
        {
            return GetAllCategories().FirstOrDefault(x => x.ListChildren.Cast<Game>().Any(y => y.Id == gameId));
        }

        /// <summary>
        /// Object to load and save user data through XML (de)serialization. Root Object.
        /// </summary>
        public static Preferences UserData { get; private set; }

        private static async Task<bool> SaveUserDataAsync()
        {
            return await Task.Run(async () =>
            {
                if (!UserData.PortableMode)
                {
                    return await ObjectSerializer.SerializeToDiskFileAsync(GetDataFilePath(), UserData, typeof(ModelWithChildren));
                }
                else
                {
                    foreach (Category category in UserData.ListChildren)
                    {
                        foreach (Game game in category.ListChildren)
                        {
                            game.DOSEXEPath = game.DOSEXEPath.Replace(PathFinder.GetStartupPath(), "AppPath");
                            game.DBConfPath = game.DBConfPath.Replace(PathFinder.GetStartupPath(), "AppPath");
                            game.AdditionalCommands = game.AdditionalCommands.Replace(PathFinder.GetStartupPath(), "AppPath");
                            game.Directory = game.Directory.Replace(PathFinder.GetStartupPath(), "AppPath");
                            game.CDPath = game.CDPath.Replace(PathFinder.GetStartupPath(), "AppPath");
                            game.SetupEXEPath = game.SetupEXEPath.Replace(PathFinder.GetStartupPath(), "AppPath");
                            game.Icon = game.Icon.Replace(PathFinder.GetStartupPath(), "AppPath");
                        }
                    }
                    UserData.DBDefaultConfFilePath = UserData.DBDefaultConfFilePath.Replace(PathFinder.GetStartupPath(), "AppPath");
                    UserData.DBDefaultLangFilePath = UserData.DBDefaultLangFilePath.Replace(PathFinder.GetStartupPath(), "AppPath");
                    UserData.DBPath = UserData.DBPath.Replace(PathFinder.GetStartupPath(), "AppPath");
                    UserData.ConfigEditorPath = UserData.ConfigEditorPath.Replace(PathFinder.GetStartupPath(), "AppPath");
                    UserData.ConfigEditorAdditionalParameters = UserData.ConfigEditorAdditionalParameters.Replace(PathFinder.GetStartupPath(), "AppPath");
                    return await ObjectSerializer.SerializeToDiskFileAsync(PathFinder.GetStartupPath() + "\\AmpShell.xml", UserData, typeof(ModelWithChildren));
                }
            });
        }

        private static async Task<Preferences> LoadUserDataAsync()
        {
            object userDataObject = await ObjectSerializer.DeserializeFromFileAsync(GetDataFilePath(), typeof(ModelWithChildren));
            var userData = (Preferences)userDataObject;
            await Task.Run(() =>
            {
                foreach (Category concernedCategory in userData.ListChildren)
                {
                    foreach (Game concernedGame in concernedCategory.ListChildren)
                    {
                        concernedGame.DOSEXEPath = concernedGame.DOSEXEPath.Replace("AppPath", PathFinder.GetStartupPath());
                        concernedGame.DBConfPath = concernedGame.DBConfPath.Replace("AppPath", PathFinder.GetStartupPath());
                        concernedGame.AdditionalCommands = concernedGame.AdditionalCommands.Replace("AppPath", PathFinder.GetStartupPath());
                        concernedGame.Directory = concernedGame.Directory.Replace("AppPath", PathFinder.GetStartupPath());
                        concernedGame.CDPath = concernedGame.CDPath.Replace("AppPath", PathFinder.GetStartupPath());
                        concernedGame.SetupEXEPath = concernedGame.SetupEXEPath.Replace("AppPath", PathFinder.GetStartupPath());
                        concernedGame.Icon = concernedGame.Icon.Replace("AppPath", PathFinder.GetStartupPath());
                    }
                }
                userData.DBDefaultConfFilePath = UserData.DBDefaultConfFilePath.Replace("AppPath", PathFinder.GetStartupPath());
                userData.DBDefaultLangFilePath = UserData.DBDefaultLangFilePath.Replace("AppPath", PathFinder.GetStartupPath());
                userData.DBPath = UserData.DBPath.Replace("AppPath", PathFinder.GetStartupPath());
                userData.ConfigEditorPath = UserData.ConfigEditorPath.Replace("AppPath", PathFinder.GetStartupPath());
                userData.ConfigEditorAdditionalParameters = UserData.ConfigEditorAdditionalParameters.Replace("AppPath", PathFinder.GetStartupPath());

                if (string.IsNullOrWhiteSpace(userData.DBPath))
                {
                    userData.DBPath = FileFinder.SearchDOSBox(GetDataFilePath(), UserData.PortableMode);
                }
                else if (File.Exists(userData.DBPath) == false)
                {
                    userData.DBPath = FileFinder.SearchDOSBox(GetDataFilePath(), UserData.PortableMode);
                }
                if (string.IsNullOrWhiteSpace(userData.ConfigEditorPath))
                {
                    userData.ConfigEditorPath = FileFinder.SearchCommonTextEditor();
                }
                else if (File.Exists(userData.ConfigEditorPath) == false)
                {
                    userData.ConfigEditorPath = FileFinder.SearchCommonTextEditor();
                }

                if (string.IsNullOrWhiteSpace(userData.DBDefaultConfFilePath))
                {
                    userData.DBDefaultConfFilePath = FileFinder.SearchDOSBoxConf(GetDataFilePath(), userData.DBPath);
                }
                else if (File.Exists(UserData.DBDefaultConfFilePath) == false)
                {
                    userData.DBDefaultConfFilePath = FileFinder.SearchDOSBoxConf(GetDataFilePath(), userData.DBPath);
                }

                if (string.IsNullOrWhiteSpace(userData.DBDefaultLangFilePath) == false)
                {
                    userData.DBDefaultLangFilePath = FileFinder.SearchDOSBoxLanguageFile(userData.DBPath);
                }
                else if (File.Exists(UserData.DBDefaultLangFilePath) == false)
                {
                    userData.DBDefaultLangFilePath = FileFinder.SearchDOSBoxLanguageFile(userData.DBPath);
                }
            });
            return userData;
        }

        public static void SetCategoriesOrder(ObservableCollection<Category> categories)
        {
            var currentCategories = GetAllCategories();
            foreach (Category category in categories)
            {
                UserData.MoveChildToPosition(category, currentCategories.IndexOf(currentCategories.FirstOrDefault(x => x.Signature == category.Signature)));
            }
        }

        /// <summary>
        /// Returns the path to the user data file (AmpShell.xml)
        /// </summary>
        private static string GetDataFilePath()
        {
            var path = "";
            //If the file named AmpShell.xml doesn't exists inside the directory AmpShell uses the one in the user's profile Application Data directory
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AmpShell\\AmpShell.xml") == false && File.Exists(PathFinder.GetStartupPath() + "\\AmpShell.xml") == false)
            {
                //Setup the whole directory path
                if (Directory.GetDirectoryRoot(PathFinder.GetStartupPath()) == Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) || Directory.GetDirectoryRoot(PathFinder.GetStartupPath()) == Environment.SystemDirectory.Substring(0, 3) + "Program Files (x86)")
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AmpShell";
                    //create the directory
                    if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AmpShell") == false)
                    {
                        Directory.CreateDirectory(path);
                        path += "\\AmpShell.xml";
                    }
                }
                else
                {
                    path = PathFinder.GetStartupPath() + "\\AmpShell.xml";
                }
            }
            //if the file named AmpShell.xml exists inside that directory
            else
            {
                //then, deserialize it in Amp.
                if (File.Exists(PathFinder.GetStartupPath() + "\\AmpShell.xml"))
                {
                    path = PathFinder.GetStartupPath() + "\\AmpShell.xml";
                }
                else if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AmpShell\\AmpShell.xml"))
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AmpShell\\AmpShell.xml";
                }
            }
            return path;
        }

        private static string GetAUniqueSignature()
        {
            string newSignature;
            do
            {
                Random randNumber = new Random();
                newSignature = randNumber.Next(1048576).ToString();
            }
            while (IsItAUniqueSignature(newSignature) == false);
            return newSignature;
        }

        /// <summary>
        /// Used when a new Category or Game is created : it's signature must be unique
        /// so AmpShell can recognize it instantly
        /// </summary>
        /// <param name="signatureToTest">A Category's or Game's signature</param>
        /// <returns>Whether the signature equals none of the other ones, or not</returns>
        private static bool IsItAUniqueSignature(string signatureToTest)
        {
            foreach (Category otherCat in UserData.ListChildren)
            {
                if (otherCat.Signature != signatureToTest)
                {
                    if (otherCat.ListChildren.Length != 0)
                    {
                        foreach (Game otherGame in otherCat.ListChildren)
                        {
                            if (otherGame.Signature == signatureToTest)
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}