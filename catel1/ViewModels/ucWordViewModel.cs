using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace InfConstractions.ViewModels

{
    public class UcWordViewModel : ViewModelBase
    {
        public UcWordViewModel() { }
        public static UcWordViewModel Create()
        { return ViewModelSource.Create(() => new UcWordViewModel()); }
    }
}