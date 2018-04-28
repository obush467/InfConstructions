using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using InfConstractionsDX.Common;
using System;

namespace InfConstractionsDX.Modules.ViewModels
{
    public class ModuleViewModel : IDocumentModule, ISupportState<ModuleViewModel.Info>
    {
        public virtual string Caption { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string Content { get; set; }

        public static ModuleViewModel Create(string caption, string content)
        {
            return ViewModelSource.Create(() => new ModuleViewModel()
            {
                Caption = caption,
                Content = content
            });
        }
        protected ModuleViewModel() { }

        #region Serialization
        [Serializable]
        public class Info
        {
            public string Content { get; set; }
            public string Caption { get; set; }
        }
        Info ISupportState<Info>.SaveState()
        {
            return new Info()
            {
                Content = this.Content,
                Caption = this.Caption,
            };
        }
        void ISupportState<Info>.RestoreState(Info state)
        {
            this.Content = state.Content;
            this.Caption = state.Caption;
        }
        #endregion
    }
}
