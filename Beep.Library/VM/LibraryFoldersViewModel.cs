
using CommunityToolkit.Mvvm.ComponentModel;
using TheTechIdea.Beep;
using TheTechIdea.Beep.Editor;
using DataManagementModels.Editor;
using TheTechIdea.Beep.Report;
using Beep.Vis.Module;
using TheTechIdea.Beep.MVVM;
using TheTechIdea.Beep.Library.Model;

namespace Beep.Library.VM
{
    public partial class LibraryFoldersViewModel : BaseViewModel
    {
        [ObservableProperty]
        double library_id;

        [ObservableProperty]
        LIB_FILES currentFile;
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
        LIB_FOLDERS currentFolder;
        [ObservableProperty]
        LIBRARIES currentLibrary;
        [ObservableProperty]
        string gfxPath;
        public double CurrentID { get; set; }
        public UnitofWork<LIB_FOLDERS> UnitofWork { get; set; }
        public ObservableBindingList<LIB_FOLDERS> Folders { get { return UnitofWork.Units; } set { UnitofWork.Units = value; } }
        public LibraryFoldersViewModel(IDMEEditor dMEEditor, IVisManager visManager) : base(dMEEditor, visManager)
        {
            
           
            UnitofWork = new UnitofWork<LIB_FOLDERS>(DMEEditor, "dhubdb", "LIB_FOLDERS", "ID");
            UnitofWork.PostCreate += UnitofWork_PostCreate;
            UnitofWork.Sequencer = "LIB_FOLDERS_SEQ";
         
            GlobalPath = dhubConfig.Library.GlobalPath  ;
            TeamPath = dhubConfig.Library.GetTeamPath();
            GfxPath = dhubConfig.Library.GFXPath;
        }
        public string GetGlobalLibraryPath(string libraryname)
        {
            return System.IO.Path.Combine(GlobalPath, libraryname);
        }
        public string GetMyLibraryPath(string libraryname)
        {
            return System.IO.Path.Combine(DhubConfig.Library.GetMyPath(), libraryname);
        }
        public string GetTicketsPath(string libraryname)
        {
            return System.IO.Path.Combine(GlobalPath, libraryname);
        }
        public string GetReviewPath(string libraryname)
        {
            return System.IO.Path.Combine(GlobalPath, libraryname);
        }
        public string GetTeamLibraryPath(string libraryname)
        {
            return System.IO.Path.Combine(DhubConfig.Library.GetTeamPath(), libraryname);
        }
        public void GetLibraryFolders(double LibID)
        {
            UnitofWork.Get(new List<AppFilter>() { new AppFilter() { FieldName = "LIB_ID", Operator = "=", FilterValue = LibID.ToString() } });
            CurrentLibrary = Repo.LoadDataFirst<LIBRARIES>($"select * from libraries where id={LibID}", null).Result;
        }
        public void CreateFolder(double libID)
        {
            CurrentLibrary = Repo.LoadDataFirst<LIBRARIES>($"select * from libraries where id={libID}", null).Result;
            if(CurrentLibrary != null)
            {
                UnitofWork.Create(new LIB_FOLDERS());
            }
        }
        public IEnumerable<LIB_FILES> GetMyFiles(double folderid)
        {

            return Repo.LoadData<LIB_FILES>($"select * from Lib_files where folder_id={folderid}", null).Result;
        }
       
        private void UnitofWork_PostCreate(object? sender, UnitofWorkParams e)
        {
            LIB_FOLDERS r = (LIB_FOLDERS)sender;
          //  Status = RecordStatus.New;
            if (CurrentLibrary != null) {
                r.LIB_ID = CurrentLibrary.ID;
            }
            CurrentFolder = r;
        }
       
    
    }
}
