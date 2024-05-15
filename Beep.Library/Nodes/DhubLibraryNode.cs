using Beep.Vis.Module;

using Beep.Library.VM;
using TheTechIdea;
using TheTechIdea.Beep;
using TheTechIdea.Beep.DataBase;
using TheTechIdea.Beep.Vis;
using TheTechIdea.Util;

using TheTechIdea.Beep.Library.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Beep.Library.Nodes
{
    [Addin(Caption = "Library", Name = "DhubLibraryNode.Dhub", misc = "Dhub", iconimage = "library.png", menu = "Dhub", ObjectType = "Dhub")]
    public class DhubLibraryNode : IBranch
    {
        public DhubLibraryNode(LIBRARIES lib, int pID, LibrariesViewModel viewModel, ITree pTreeEditor, IDMEEditor pDMEEditor, IBranch pParentNode, string pBranchText, string iconname)
        {


            ViewModel = viewModel;
            TreeEditor = pTreeEditor;
            DMEEditor = pDMEEditor;
            ParentBranchID = pParentNode.ID;
            BranchText = pBranchText;
            BranchType = EnumPointType.Function;
            DataSourceName = pParentNode.DataSourceName;
            IconImageName = iconname;
            LIBRARy = lib;
            if (pID != 0)

            {
                ID = pID;
                BranchID = pID;
            }
        }
        public bool Visible { get; set; } = true;
        LIBRARIES LIBRARy;
        LibrariesViewModel ViewModel;
        //IDhubMainConfig DhubConfig;
        #region "Properties"
        public IBranch ParentBranch { get; set; }
        public string ObjectType { get; set; } = "Beep";
        public int ID { get; set; }
        public EntityStructure EntityStructure { get; set; }
        public string Name { get; set; }
        public string BranchText { get; set; } = "Libraries";
        public IDMEEditor DMEEditor { get; set; }
        public IDataSource DataSource { get; set; }
        public string DataSourceName { get; set; }
        public int Level { get; set; }
        public EnumPointType BranchType { get; set; } = EnumPointType.Function;
        public int BranchID { get; set; }
        public string IconImageName { get; set; } = "library.png";
        public string BranchStatus { get; set; }
        public int ParentBranchID { get; set; }

        public string BranchDescription { get; set; } = "Folder node for File Library";
        public string BranchClass { get; set; } = "Dhub3.File.Library";
        public List<IBranch> ChildBranchs { get; set; } = new List<IBranch>();
        public ITree TreeEditor { get; set; }
        public List<string> BranchActions { get; set; } = new List<string>();
        public object TreeStrucure { get; set; }
        public IVisManager Visutil { get; set; }
        public int MiscID { get; set; }

        List<LIB_FOLDERS> folders = new List<LIB_FOLDERS>();
        // public event EventHandler<PassedArgs> BranchSelected;
        // public event EventHandler<PassedArgs> BranchDragEnter;
        // public event EventHandler<PassedArgs> BranchDragDrop;
        // public event EventHandler<PassedArgs> BranchDragLeave;
        // public event EventHandler<PassedArgs> BranchDragClick;
        // public event EventHandler<PassedArgs> BranchDragDoubleClick;
        // public event EventHandler<PassedArgs> ActionNeeded;
        #endregion "Properties"
        public bool IsDataSourceNode { get; set; } = true;
        public string GuidID { get; set; } = Guid.NewGuid().ToString();
        public string ParentGuidID { get; set; }
        public string DataSourceConnectionGuidID { get; set; }
        public string EntityGuidID { get; set; }
        public string MiscStringID { get; set; }
        public IBranch CreateCategoryNode(CategoryFolder p)
        {
            return null;
        }

        public IErrorsInfo CreateChildNodes()
        {
            try
            {
                //DhubConfig = DMEEditor.GetDhub();
                //if (DhubConfig == null)
                //{
                //    DMEEditor.AddLogMessage("Beep", "Error Could Find DhubConfig Instance", DateTime.Now, -1, null, Errors.Failed);
                //    return DMEEditor.ErrorObject;
                //}
                //folders = (List<LIB_FOLDERS>)DhubConfig.DataRepo.LoadData<LIB_FOLDERS>($"select * from lib_folders where lib_id={LIBRARy.ID}", null).Result;
                //foreach (var item in folders)
                //{
                //    if (!ChildBranchs.Any(p => p.BranchText == item.FOLDERNAME))
                //    {
                //        DhubFileCategoryNode foldernode = new DhubFileCategoryNode(item, LIBRARy, TreeEditor.SeqID, ViewModel, TreeEditor, DMEEditor, this, item.FOLDERNAME, EnumPointType.Function, item.ICONNAME);
                //        TreeEditor.treeBranchHandler.AddBranch(this, foldernode);
                //        ChildBranchs.Add(foldernode);
                //        // foldernode.CreateChildNodes();
                //    }
                //}
            }
            catch (Exception ex)
            {


            }
            return DMEEditor.ErrorObject;

        }

        public IErrorsInfo ExecuteBranchAction(string ActionName)
        {
            return DMEEditor.ErrorObject;
        }

        public IErrorsInfo MenuItemClicked(string ActionNam)
        {
            return DMEEditor.ErrorObject;
        }

        public IErrorsInfo RemoveChildNodes()
        {
            return DMEEditor.ErrorObject;
        }

        public IErrorsInfo SetConfig(ITree pTreeEditor, IDMEEditor pDMEEditor, IBranch pParentNode, string pBranchText, int pID, EnumPointType pBranchType, string pimagename)
        {
            try
            {
                TreeEditor = pTreeEditor;
                DMEEditor = pDMEEditor;

            }
            catch (Exception ex)
            {
                string mes = "Could not Set Config";
                DMEEditor.AddLogMessage(ex.Message, mes, DateTime.Now, -1, mes, Errors.Failed);
            };
            return DMEEditor.ErrorObject;
        }
        #region "Exposed Interface"

        [Command(Caption = "Edit Privilege", Hidden = false, iconimage = "privilege.png")]
        public IErrorsInfo Privilege()
        {

            try
            {
                //DhubConfig = DMEEditor.GetDhub();
                //if (DhubConfig == null)
                //{
                //    DMEEditor.AddLogMessage("Beep", "Error Could Find DhubConfig Instance", DateTime.Now, -1, null, Errors.Failed);
                //    return DMEEditor.ErrorObject;
                //}
                //if (DhubConfig != null)
                //{
                //    if (DhubConfig.userManager.User.KOCNO == LIBRARy.OWNER_KOCNO)
                //    {
                //        DMEEditor.Passedarguments.CurrentEntity = "LIBRARY";
                //        DMEEditor.Passedarguments.ObjectName = BranchText;
                //        DMEEditor.Passedarguments.ObjectType = "LIBRARY";
                //        if (DMEEditor.Passedarguments.Objects.Any(p => p.Name != "LIBRARY"))
                //        {
                //            DMEEditor.Passedarguments.Objects.Add(new ObjectItem { Name = "LIBRARY", obj = LIBRARy });
                //        }
                //        DMEEditor.Passedarguments.Parameterdouble1 = LIBRARy.ID;
                //        Visutil.ShowPage("uc_library_privileges", (PassedArgs)DMEEditor.Passedarguments);
                //    }
                //    else
                //        Visutil.Controlmanager.ShowMessege("Dhub", "Your not the Library Owner", null);
                //}


                //NodesHelpers.AddFolder(this, TreeEditor, DMEEditor, Visutil);
            }
            catch (Exception ex)
            {
                string mes = "Could not Add Database Connection";
                DMEEditor.AddLogMessage(ex.Message, mes, DateTime.Now, -1, mes, Errors.Failed);
            };
            return DMEEditor.ErrorObject;
        }

        [Command(Caption = "Files", Hidden = false, iconimage = "files.png")]
        public IErrorsInfo GetFiles()
        {

            try
            {
                //DhubConfig = DMEEditor.GetDhub();
                //if (DhubConfig == null)
                //{
                //    DMEEditor.AddLogMessage("Beep", "Error Could Find DhubConfig Instance", DateTime.Now, -1, null, Errors.Failed);
                //    return DMEEditor.ErrorObject;
                //}
                //if (DhubConfig != null)
                //{
                //    if (DhubConfig.userManager.User.KOCNO == LIBRARy.OWNER_KOCNO)
                //    {
                //        DMEEditor.Passedarguments.CurrentEntity = "LIBRARY";
                //        DMEEditor.Passedarguments.ObjectName = BranchText;
                //        DMEEditor.Passedarguments.ObjectType = "LIBRARY";
                //        if (DMEEditor.Passedarguments.Objects.Any(p => p.Name != "LIBRARY"))
                //        {
                //            DMEEditor.Passedarguments.Objects.Add(new ObjectItem { Name = "LIBRARY", obj = LIBRARy });
                //        }
                //        DMEEditor.Passedarguments.Parameterdouble1 = LIBRARy.ID;
                //        DMEEditor.Passedarguments.EventType = "LIB";
                //        Visutil.ShowPage("uc_library_files", (PassedArgs)DMEEditor.Passedarguments);
                //    }
                //    else
                //        Visutil.Controlmanager.ShowMessege("Dhub", "Your not the Library Owner", null);
                //}


                //NodesHelpers.AddFolder(this, TreeEditor, DMEEditor, Visutil);
            }
            catch (Exception ex)
            {
                string mes = "Could not Add Database Connection";
                DMEEditor.AddLogMessage(ex.Message, mes, DateTime.Now, -1, mes, Errors.Failed);
            };
            return DMEEditor.ErrorObject;
        }
        [Command(Caption = "Create Category", Hidden = false, iconimage = "category.png")]
        public IErrorsInfo createcategory()
        {

            try
            {
                //DhubConfig = DMEEditor.GetDhub();
                //if (DhubConfig == null)
                //{
                //    DMEEditor.AddLogMessage("Beep", "Error Could Find DhubConfig Instance", DateTime.Now, -1, null, Errors.Failed);
                //    return DMEEditor.ErrorObject;
                //}
                //if (DhubConfig != null)
                //{
                //    if (DhubConfig.userManager.User.KOCNO == LIBRARy.OWNER_KOCNO)
                //    {
                //        DMEEditor.Passedarguments.CurrentEntity = "LIBRARY";
                //        DMEEditor.Passedarguments.ObjectName = BranchText;
                //        DMEEditor.Passedarguments.ObjectType = "LIBRARY";
                //        if (DMEEditor.Passedarguments.Objects.Any(p => p.Name != "LIBRARY"))
                //        {
                //            DMEEditor.Passedarguments.Objects.Add(new ObjectItem { Name = "LIBRARY", obj = LIBRARy });
                //        }
                //        DMEEditor.Passedarguments.Parameterdouble1 = LIBRARy.ID;
                //        Visutil.ShowPage("uc_library_folders", (PassedArgs)DMEEditor.Passedarguments);
                //    }
                //    else
                //        Visutil.Controlmanager.ShowMessege("Dhub", "Your not the Library Owner", null);
                //}


                //NodesHelpers.AddFolder(this, TreeEditor, DMEEditor, Visutil);
            }
            catch (Exception ex)
            {
                string mes = "Could not Add Database Connection";
                DMEEditor.AddLogMessage(ex.Message, mes, DateTime.Now, -1, mes, Errors.Failed);
            };
            return DMEEditor.ErrorObject;
        }
        #endregion
    }
}
