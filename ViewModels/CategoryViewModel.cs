/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

using AmpShell.DAL;
using AmpShell.Models;

using Avalonia.Diagnostics.ViewModels;

namespace AmpShell.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        private string _name = "";
        private readonly int _categoryId;

        public CategoryViewModel()
        {
        }

        public CategoryViewModel(int categoryId)
        {
            this._categoryId = categoryId;
            this.Name = UserDataAccessor.GetCategory(categoryId).Title;
        }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public void CreateCategory()
        {
            Category category;
            if (_categoryId == 0)
            {
                UserDataAccessor.CreateCategory();
            }
            category = UserDataAccessor.GetCategory(_categoryId);
            category.Title = Name;
            UserDataAccessor.UpdateCategory(category);
        }

        public bool IsDataValid()
        {
            return string.IsNullOrWhiteSpace(Name) == false;
        }
    }
}