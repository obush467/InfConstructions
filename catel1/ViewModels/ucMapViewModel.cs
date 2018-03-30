using System;
using DevExpress.Mvvm;

namespace InfConstractions.ViewModels
{
    public class ucMapViewModel : ViewModelBase
    {
        public ucMapViewModel()
        { dProvider = new DemoValuesProvider(); }
        public DemoValuesProvider dProvider { get ; set; }
    }
}