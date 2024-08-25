using DoToo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoToo.Views;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using DoToo.Models;

namespace DoToo.ViewModels
{
    public partial class MainViewModel: ViewModel
    {
        private readonly ITodoItemRepository repository;
        private readonly IServiceProvider services;

        [ObservableProperty]
        ObservableCollection<TodoItemViewModel> items;

        [ObservableProperty]
        TodoItemViewModel selectedItem;

        [ObservableProperty]
        bool showAll;

        [RelayCommand]
        public async Task AddItemAsync() => await Navigation.PushAsync(services.GetRequiredService<ItemView>());

        [RelayCommand]
        private async Task ToggleFilterAsync()
        {
            ShowAll = !ShowAll;
            await LoadDataAsync();
        }

        public MainViewModel(ITodoItemRepository repository, IServiceProvider services)
        {
            repository.OnItemAdded += (sender, item) => items.Add(CreateTodoItemViewModel(item));
            repository.OnItemUpdated += (sender, item) => Task.Run(async () => await LoadDataAsync());

            this.repository = repository;
            Task.Run(async () => await LoadDataAsync());
            this.services = services;
        }

        partial void OnSelectedItemChanging(TodoItemViewModel value)
        {
            if (value == null)
            {
                return;
            }
            MainThread.BeginInvokeOnMainThread(async () => {
                await NavigateToItemAsync(value);
            });
        }
        private async Task NavigateToItemAsync(TodoItemViewModel item)
        {
            var itemView = services.GetRequiredService<ItemView>();
            var vm = itemView.BindingContext as ItemViewModel;
            vm.Item = item.Item;
            itemView.Title = "Edit todo item";
            await Navigation.PushAsync(itemView);
        }

        private async Task LoadDataAsync()
        {
            var items = await repository.GetItemsAsync();
            if (!ShowAll)
            {
                items = items.Where(x => x.Completed == false).ToList();
            }

            var itemViewModels = items.Select(i => CreateTodoItemViewModel(i));
            Items = new ObservableCollection<TodoItemViewModel>(itemViewModels);
        }

        private TodoItemViewModel CreateTodoItemViewModel(TodoItem item)
        {
            var itemViewModel = new TodoItemViewModel(item);
            itemViewModel.ItemStatusChanged += ItemStatusChanged;
            return itemViewModel;
        }


        private void ItemStatusChanged(object sender, EventArgs e)
        {
            if(sender is TodoItemViewModel item)
            {
                if(!showAll && item.Item.Completed)
                {
                    Items.Remove(item);
                }
                Task.Run(async () => await repository.UpdateItemAsync(item.Item));
            }
        }
    }
}
