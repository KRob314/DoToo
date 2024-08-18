using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DoToo.ViewModels
{
    [ObservableObject]
    public abstract partial class ViewModel 
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }

        //public void RaisePropertyChanged(params string[] propertyNames)
        //{
        //    foreach (var propertyName in propertyNames)
        //    {
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
        
    }
}
