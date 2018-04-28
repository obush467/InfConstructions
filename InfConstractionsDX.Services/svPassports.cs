using DevExpress.Mvvm.UI;
using System.Windows;

namespace InfConstractionsDX.Main.Services
{
    public interface ITitleService
    {
       string Title { get; set; }
    }
    public class TitleService : ServiceBase, ITitleService
    {
        public static readonly DependencyProperty TitleProperty =
                DependencyProperty.Register("Title", typeof(string), typeof(TitleService), new PropertyMetadata("Hello"));
        public TitleService()
        { }

        public string Title
        { get;set;}
    }
}
