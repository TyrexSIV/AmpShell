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
using ReactiveUI;

namespace AmpShell.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        public Category Model { get; private set; }

        public CategoryViewModel()
        {
            Model = new Category();
            InitializeCommands();
        }

        public CategoryViewModel(Category model)
        {
            this.Model = model;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            this.CreateCategory = ReactiveCommand.Create(this.CreateCategory_Execute, this.WhenAnyValue(x => x.CreateCategory_CanExecute()));
            this.EditCategory = ReactiveCommand.Create(this.EditCategory_Execute, this.WhenAnyValue(x => x.IsDataValid()));

        }

        public ReactiveCommand EditCategory { get; private set; }

        public ReactiveCommand CreateCategory { get; private set; }

        private string _name = "";

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private void CreateCategory_Execute()
        {
            UserDataAccessor.CreateCategory(this.Model);
        }

        private bool CreateCategory_CanExecute()
        {
            return Model.Id == 0 && IsDataValid();
        }

        private void EditCategory_Execute()
        {
            UserDataAccessor.UpdateCategory(this.Model);
        }

        private bool IsDataValid()
        {
            return string.IsNullOrWhiteSpace(Name) == false;
        }
    }
}