
using CommunityToolkit.Mvvm.ComponentModel;
using TheTechIdea.Beep;
using TheTechIdea.Beep.Editor;
using DataManagementModels.Editor;
using TheTechIdea.Beep.MVVM;
using TheTechIdea.Beep.Library.Model;
using Beep.Vis.Module;
using System.Collections.Generic;
using System;


namespace Beep.Library.VM
{
    public partial class LibrariesViewModel : BaseViewModel
    {
        [ObservableProperty]
        LIBRARIES currentLibrary;
        [ObservableProperty]
        string myLibraryPath;
        [ObservableProperty]
        string globalPath;
        [ObservableProperty]
        string teamPath;
        [ObservableProperty]
        string libraryName;
        [ObservableProperty]
        string libraryDescription;
        [ObservableProperty]
        string libraryType;
        [ObservableProperty]
        string libraryPath;

        [ObservableProperty]
        List<LIBRARIES> myLibraries;
        [ObservableProperty]
        List<LIBRARIES> globalLibraries;
        [ObservableProperty]
        List<LIBRARIES> teamLibraries;
        [ObservableProperty]
        List<LIBRARIES> allLibraries;
        [ObservableProperty]
        List<LIBRARIES>grantedLibraries;

      
        public UnitofWork<LIBRARIES> UnitofWork { get; set; }
        public ObservableBindingList<LIBRARIES> LIBRARIES { get { return UnitofWork.Units; } set { UnitofWork.Units = value; } }
        public LibrariesViewModel(IDMEEditor dMEEditor, IVisManager visManager) : base(dMEEditor, visManager)
        {
            UnitofWork = new UnitofWork<LIBRARIES>(Editor, "dhubdb", "LIBRARIES", "ID");
            UnitofWork.Sequencer = "LIBRARIES_SEQ";
            UnitofWork.PostCreate += UnitofWork_PostCreate;

            //MyLibraryPath = dhubConfig.Library.GetMyPath();
            //GlobalPath = dhubConfig.Library.GlobalPath;
            //TeamPath = dhubConfig.Library.GetTeamPath();
        }
        private void UnitofWork_PostCreate(object? sender, UnitofWorkParams e)
        {
            LIBRARIES r = (LIBRARIES)sender;
            //Status = RecordStatus.New;
            r.ROW_CREATE_DATE = DateTime.Now;
            //r.OWNER_KOCNO = DhubConfig.userManager.User.KOCNO;
            //r.OWNER_TEAMID = DhubConfig.userManager.User.TEAM;
            //r.GROUPCODE = DhubConfig.userManager.User.GRP;
            //r.TEAMCODE = DhubConfig.userManager.User.TEAM;
        }
        public void GetLibraries()
        {
            GetMyLibraries();
            GetGrantedLibrariesToME();
            AllLibraries=new List<LIBRARIES>();
            AllLibraries.Clear();
            AllLibraries.AddRange(MyLibraries);
            AllLibraries.AddRange(GrantedLibraries);
        }
        public void CreateLibrary(string libname,string description,string iconfile)
        {
            LIBRARIES r = new LIBRARIES();
            r.LIBRARY_NAME = libname;
            r.LIBRARY_DESCRIPTION = description;
            r.ICONNAME = iconfile;
            UnitofWork.Create(r);
            CurrentLibrary = r;

        }
        public void CreateLibrary(string libname)
        {
            LIBRARIES r = new LIBRARIES();
            r.LIBRARY_NAME = libname;
            UnitofWork.Create(r);
            CurrentLibrary = r;

        }
        public void CreateLibrary(string libname, string description)
        {
            LIBRARIES r = new LIBRARIES();
            r.LIBRARY_NAME = libname;
            r.LIBRARY_DESCRIPTION = description;
            UnitofWork.Create(r);
            CurrentLibrary = r;

        }
        public void CreateLibrary()
        {
            LIBRARIES r = new LIBRARIES();
            UnitofWork.Create(r);
            CurrentLibrary = r;

        }
        public void GetLibraryFiles()
        {

        }
        public void GetMyLibraries()

        {
            try
            {
             //   MyLibraries = (List<LIBRARIES>)Repo.LoadData<LIBRARIES>($" select * from LIBRARIES where  OWNER_KOCNO='{DhubConfig.userManager.User.KOCNO}'", null).Result;
            }
            catch (Exception ex)
            {
                Editor.AddLogMessage("Dhub", $"Error getting my libraries {ex.Message}", DateTime.Now, -1, ex.StackTrace,TheTechIdea.Util.Errors.Failed);
                
            }
            
        }
        public void GetGrantedLibrariesToME()
        {
            try
            {
              //  GrantedLibraries = (List<LIBRARIES>)Repo.LoadData<LIBRARIES>($" select c.* from libraries c where c.id in (select a.library_id from LIBRARIES_PRIVILEGES a where a.BU_ID  in (select b.id from businessunits_members b where b.kocno='{DhubConfig.userManager.User.KOCNO}'))", null).Result;
            }
            catch (Exception ex)
            {
                Editor.AddLogMessage("Dhub", $"Error getting my Granted libraries {ex.Message}", DateTime.Now, -1, ex.StackTrace, TheTechIdea.Util.Errors.Failed);
                
            }
            
        }
        public void GetLibraryUsers(LIBRARIES lib)
        {


        }
    }
}
