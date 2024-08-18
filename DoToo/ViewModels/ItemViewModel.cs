using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoToo.Repositories;


namespace DoToo.ViewModels
{
    public class ItemViewModel : ViewModel
    {
        private readonly ITodoItemRepository repository;

        public ItemViewModel(ITodoItemRepository repository)
        {
            this.repository = repository;
        }
    }
}
