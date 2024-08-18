using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using DoToo.Models;

namespace DoToo.ViewModels
{
    public partial class TodoItemViewModel : ViewModel
    {
        public TodoItemViewModel(TodoItem item) => Item = item;

        public event EventHandler ItemStatusChanged;

        [ObservableProperty]
        TodoItem item;

        public string StatusText => Item.Completed ? "Reactivate" : "Completed";
    }
}
