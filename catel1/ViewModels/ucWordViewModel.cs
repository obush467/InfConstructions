using System;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace InfConstractions.ViewModels

{
    public class ucWordViewModel : ViewModelBase
    {
        public ucWordViewModel() { }
        public static ucWordViewModel Create()
        { return ViewModelSource.Create(() => new ucWordViewModel()); }
    }
}