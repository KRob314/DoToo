using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoToo.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoToo.Models;



namespace DoToo.ViewModels
{
    public partial class ItemViewModel : ViewModel
    {
        private readonly ITodoItemRepository repository;

        [ObservableProperty]
        TodoItem item;

        public ItemViewModel(ITodoItemRepository repository)
        {
            this.repository = repository;
            Item = new TodoItem() { Due = DateTime.Now.AddDays(1) };
        }

        [RelayCommand]
        public async Task SaveAsync()
        {
            await repository.AddOrUpdateAsync(item);
            await Navigation.PopAsync();
        }
    }
}
