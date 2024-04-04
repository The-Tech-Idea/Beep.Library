using TheTechIdea.Beep.MVVM;
using CommunityToolkit.Mvvm.ComponentModel;
using TheTechIdea.Beep;
using TheTechIdea.Beep.Editor;
using DataManagementModels.Editor;
using TheTechIdea.Beep.Library.Model;
using Beep.Vis.Module;
using System.Collections.Generic;

namespace Beep.Library.VM
{ 
    public partial class LibraryPrivilegeViewModel : BaseViewModel
    {
        [ObservableProperty]
        LIBRARIES currentLibrary;
        //[ObservableProperty]
        //List<BUSINESSUNITS_HDR> bUSINESSUNITs;
        public UnitofWork<LIBRARIES_PRIVILEGES> UnitofWork { get; set; }
        public ObservableBindingList<LIBRARIES_PRIVILEGES> LIBRARIES_PRIVILEGES { get { return UnitofWork.Units; } set { UnitofWork.Units = value; } }

        public LibraryPrivilegeViewModel(IDMEEditor dMEEditor, IVisManager visManager) : base(dMEEditor, visManager)
    {
            UnitofWork = new UnitofWork<LIBRARIES_PRIVILEGES>(DMEEditor, "dhubdb", "LIBRARIES_PRIVILEGES", "ID");
            UnitofWork.PostCreate += UnitofWork_PostCreate;
            UnitofWork.Sequencer = "LIBRARIES_PRIVILEGES_SEQ";
           // bUSINESSUNITs= (List<BUSINESSUNITS_HDR>)DhubConfig.DataRepo.LoadData<BUSINESSUNITS_HDR>("select * from BUSINESSUNITS_HDR", null).Result;
        }
        public void GetLibraryPrivileges(LIBRARIES library)
        {
            CurrentLibrary = library;
            UnitofWork.Get(new List<TheTechIdea.Beep.Report.AppFilter>() { new TheTechIdea.Beep.Report.AppFilter() { FieldName= "LIBRARY_ID", Operator="=", FilterValue= $"{library.ID}" } });
        }
        public void GetLibraryPrivileges(double id)
        {
          //  CurrentLibrary = DhubConfig.DataRepo.LoadDataFirst<LIBRARIES>($"select * from LIBRARIES where id={id}", null).Result;
          
            UnitofWork.Get(new List<TheTechIdea.Beep.Report.AppFilter>() { new TheTechIdea.Beep.Report.AppFilter() { FieldName = "LIBRARY_ID", Operator = "=", FilterValue = $"{id}" } });
        }
        private void UnitofWork_PostCreate(object? sender, UnitofWorkParams e)
        {
            LIBRARIES_PRIVILEGES r = (LIBRARIES_PRIVILEGES)sender;
           // Status = RecordStatus.New;
            r.CANADD = "N";
            r.CANDELETE = "N";
            r.CANUPDATE = "N";
            r.CANVIEW = "Y";
            r.LIBRARY_ID = CurrentLibrary.ID;
            
        }
    }
}
