using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm.UI;
using System.Windows;
using InfConstractions.Models;
using InfConstractions.ViewModels;
using InfConstractions.Services;
using DevExpress.Mvvm;
using DevExpress.Utils.MVVM.Services;
using DevExpress.Mvvm.POCO;

namespace InfConstractions.Services
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
