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
    public interface IPassportService
    {
        void Show(Guid passportID, IDocumentManagerService docService);
        void Show(GUPassport passport, IDocumentManagerService docService);
        ucPassportViewModel Passport(Guid passportID);
        Dictionary<Guid, ucPassportViewModel> Passports { get; }

    }
    public class svPassports : ServiceBase, IPassportService
    {
        private Dictionary<Guid, ucPassportViewModel> _passports = new Dictionary<Guid, ucPassportViewModel>();

        public svPassports(IDocumentManagerService docServise)
        { }
        public svPassports()
        { }
        void IPassportService.Show(Guid passportID, IDocumentManagerService docService)
        {
            IDocument doc1;
            doc1 = docService.CreateDocument("ucPassport", ViewModelSource.Create(() => new ucPassportViewModel(new Entities(App.mainConnection), passportID)));
            doc1.Id = docService.Documents.Count<IDocument>();
            doc1.Title = "Паспорт "+Passport(passportID).Passport.UNOM;
            doc1.Show();
        }
        void IPassportService.Show(GUPassport passport, IDocumentManagerService docService)
        {}
        public ucPassportViewModel Passport(Guid passportID)
        {
            if (!_passports.ContainsKey(passportID))
            {
                _passports.Add(passportID, OpenPassport(passportID));
            }
                   return _passports[passportID];
        }

        ucPassportViewModel OpenPassport(Guid passportID)
        { return new ucPassportViewModel(new Entities(App.mainConnection), passportID); }
        Dictionary<Guid, ucPassportViewModel> IPassportService.Passports
        { get { return _passports; } }
    }
}
