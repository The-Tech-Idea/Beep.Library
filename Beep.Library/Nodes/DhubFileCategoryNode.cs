using Beep.Library.VM;
using Beep.Vis.Module;

using TheTechIdea;
using TheTechIdea.Beep;
using TheTechIdea.Beep.DataBase;
using TheTechIdea.Beep.Library.Model;
using TheTechIdea.Beep.Vis;
using TheTechIdea.Util;

namespace Beep.Library.Nodes
{
    [Addin(Caption = "Library Folder", Name = "DhubFileCategoryNode.Dhub", misc = "Dhub", iconimage = "category.png", menu = "Dhub", ObjectType = "Dhub")]
    public class DhubFileCategoryNode : IBranch
    {

        public DhubFileCategoryNode(LIB_FOLDERS folder, LIBRARIES lib, int pID, LibrariesViewModel viewmodel, ITree pTreeEditor, IDMEEditor pDMEEditor, IBranch pParentNode, string pBranchText, EnumPointType pBranchType, string pimagename)
        {
            Folder = folder;
            LIBRARy = lib;


            TreeEditor = pTreeEditor;
            DMEEditor = pDMEEditor;
            ParentBranchID = pParentNode.ID;
            BranchText = pBranchText;
            BranchType = pBranchType;
            IconImageName = pimagename;

            if (pID != 0)

            {
                ID = pID;
                BranchID = pID;
            }

            ViewModel = viewmodel;
        }

        #region "Properties"
        public bool IsDataSourceNode { get; set; } = true;
        public string GuidID { get; set; } = Guid.NewGuid().ToString();
        public string ParentGuidID { get; set; }
        public string DataSourceConnectionGuidID { get; set; }
        public string EntityGuidID { get; set; }
        public string MiscStringID { get; set; }
        public IBranch ParentBranch { get; set; }
        public string ObjectType { get; set; } = "Beep";
        public int ID { get; set; }
        public EntityStructure EntityStructure { get; set; }
        public string Name { get; set; }
        public string BranchText { get; set; } = "Library Folder";
        public IDMEEditor DMEEditor { get; set; }
        public IDataSource DataSource { get; set; }
        public string DataSourceName { get; set; }
        public int Level { get; set; }
        public EnumPointType BranchType { get; set; } = EnumPointType.DataPoint;
        public int BranchID { get; set; }
        public string IconImageName { get; set; } = "category.png";
        public string BranchStatus { get; set; }
        public int ParentBranchID { get; set; }
        public string BranchDescription { get; set; } = "Category node for File Library";
        public string BranchClass { get; set; } = "Dhub3.File.Category";
        public List<IBranch> ChildBranchs { get; set; } = new List<IBranch>();
        public LIB_FOLDERS Folder { get; }
        public LIBRARIES LIBRARy { get; private set; }
        public ITree TreeEditor { get; set; }
        public List<string> BranchActions { get; set; } = new List<string>();
        public object TreeStrucure { get; set; }
        public IVisManager Visutil { get; set; }
        public int MiscID { get; set; }
       //
        LibrariesViewModel ViewModel;
        // public event EventHandler<PassedArgs> BranchSelected;
        // public event EventHandler<PassedArgs> BranchDragEnter;
        // public event EventHandler<PassedArgs> BranchDragDrop;
        // public event EventHandler<PassedArgs> BranchDragLeave;
        // public event EventHandler<PassedArgs> BranchDragClick;
        // public event EventHandler<PassedArgs> BranchDragDoubleClick;
        // public event EventHandler<PassedArgs> ActionNeeded;
        #endregion "Properties"
        #region "Interface Methods"
        public IErrorsInfo CreateChildNodes()
        {

            try
            {


                //if (ViewModel != null)
                // {

                //     foreach (var item in ViewModel.Files)
                //     {
                //         if (!ChildBranchs.Any(p => p.BranchText == item.DOCNAME))
                //         {
                //             DhubFileEntityNode file = new DhubFileEntityNode(ViewModel, TreeEditor, DMEEditor, this, item.DOCNAME, TreeEditor.SeqID, EnumPointType.Function, $"{item.DOCTYPE}.png");
                //             TreeEditor.treeBranchHandler.AddBranch(this, file);
                //             ChildBranchs.Add(file);
                //             file.CreateChildNodes();
                //         }

                //     }

                // }
                DMEEditor.AddLogMessage("Success", "Added Child Nodes", DateTime.Now, 0, null, Errors.Ok);
            }
            catch (Exception ex)
            {
                string mes = "Could not Child Nodes";
                DMEEditor.AddLogMessage(ex.Message, mes, DateTime.Now, -1, mes, Errors.Failed);
            };
            return DMEEditor.ErrorObject;

        }

        public IErrorsInfo ExecuteBranchAction(string ActionName)
        {
            throw new NotImplementedException();
        }

        public IErrorsInfo MenuItemClicked(string ActionNam)
        {
            throw new NotImplementedException();
        }

        public IErrorsInfo RemoveChildNodes()
        {
            throw new NotImplementedException();
        }
        public IBranch CreateCategoryNode(CategoryFolder p)
        {
            DhubFileCategoryNode categoryBranch = null;
            try
            {
                //IBranch parent = TreeEditor.Branches.FirstOrDefault(x => x.ID == p.ID);
                //if (parent == null)
                //{
                //    parent = this;
                //}
                //categoryBranch = new FileCategoryNode(TreeEditor, DMEEditor, parent, p.FolderName, TreeEditor.SeqID, EnumPointType.Category, "category.png");
                //TreeEditor.treeBranchHandler.AddBranch(parent, categoryBranch);
                //ChildBranchs.Add(categoryBranch);
                //categoryBranch.CreateChildNodes();
                //categoryBranch= (DhubFileCategoryNode)NodesHelpers.CreateCategoryNode(p, this, TreeEditor, DMEEditor, Visutil);

            }
            catch (Exception ex)
            {
                DMEEditor.Logger.WriteLog($"Error Creating Category  View Node ({ex.Message}) ");
                DMEEditor.ErrorObject.Flag = Errors.Failed;
                DMEEditor.ErrorObject.Ex = ex;
            }
            return categoryBranch;

        }
        public IErrorsInfo SetConfig(ITree pTreeEditor, IDMEEditor pDMEEditor, IBranch pParentNode, string pBranchText, int pID, EnumPointType pBranchType, string pimagename)
        {
            try
            {
                TreeEditor = pTreeEditor;
                DMEEditor = pDMEEditor;
                ParentBranchID = pParentNode.ID;
                BranchText = pBranchText;
                BranchType = pBranchType;
                IconImageName = pimagename;
                if (pID != 0)
                {
                    ID = pID;
                }

                //   DMEEditor.AddLogMessage("Success", "Set Config OK", DateTime.Now, 0, null, Errors.Ok);
            }
            catch (Exception ex)
            {
                string mes = "Could not Set Config";
                DMEEditor.AddLogMessage(ex.Message, mes, DateTime.Now, -1, mes, Errors.Failed);
            };
            return DMEEditor.ErrorObject;
        }
        #endregion "Interface Methods"
        #region "Exposed Interface"

        [Command(Caption = "Edit Folder Privilege", Hidden = false, iconimage = "privilege.png")]
        public IErrorsInfo FolderPrivilege()
        {

            try
            {
                DhubConfig = DMEEditor.GetDhub();
                if (DhubConfig == null)
                {
                    DMEEditor.AddLogMessage("Beep", "Error Could Find DhubConfig Instance", DateTime.Now, -1, null, Errors.Failed);
                    return DMEEditor.ErrorObject;
                }
                if (DhubConfig != null)
                {
                    if (DhubConfig.userManager.User.KOCNO == LIBRARy.OWNER_KOCNO)
                    {
                        DMEEditor.Passedarguments.CurrentEntity = "LIBRARY";
                        DMEEditor.Passedarguments.ObjectName = BranchText;
                        DMEEditor.Passedarguments.ObjectType = "LIBRARY";
                        if (DMEEditor.Passedarguments.Objects.Any(p => p.Name != "LIBRARY"))
                        {
                            DMEEditor.Passedarguments.Objects.Add(new ObjectItem { Name = "LIBRARY", obj = LIBRARy });
                        }
                        DMEEditor.Passedarguments.Parameterdouble1 = LIBRARy.ID;
                        DMEEditor.Passedarguments.Parameterdouble2 = LIBRARy.ID;
                        Visutil.ShowPage("uc_library_privileges", (PassedArgs)DMEEditor.Passedarguments);
                    }
                    else
                        Visutil.Controlmanager.ShowMessege("Dhub", "Your not the Library Owner", null);
                }


                //NodesHelpers.AddFolder(this, TreeEditor, DMEEditor, Visutil);
            }
            catch (Exception ex)
            {
                string mes = "Could not Add Database Connection";
                DMEEditor.AddLogMessage(ex.Message, mes, DateTime.Now, -1, mes, Errors.Failed);
            };
            return DMEEditor.ErrorObject;
        }

        [Command(Caption = "Folder Files", Hidden = false, iconimage = "files.png")]
        public IErrorsInfo FolderGetFiles()
        {

            try
            {
                DhubConfig = DMEEditor.GetDhub();
                if (DhubConfig == null)
                {
                    DMEEditor.AddLogMessage("Beep", "Error Could Find DhubConfig Instance", DateTime.Now, -1, null, Errors.Failed);
                    return DMEEditor.ErrorObject;
                }
                if (DhubConfig != null)
                {
                    if (DhubConfig.userManager.User.KOCNO == LIBRARy.OWNER_KOCNO)
                    {
                        DMEEditor.Passedarguments.CurrentEntity = "LIBRARY";
                        DMEEditor.Passedarguments.ObjectName = BranchText;
                        DMEEditor.Passedarguments.ObjectType = "LIBRARY";
                        if (DMEEditor.Passedarguments.Objects.Any(p => p.Name != "LIBRARY"))
                        {
                            DMEEditor.Passedarguments.Objects.Add(new ObjectItem { Name = "LIBRARY", obj = LIBRARy });
                        }
                        DMEEditor.Passedarguments.Parameterdouble1 = LIBRARy.ID;
                        DMEEditor.Passedarguments.Parameterdouble2 = Folder.ID;
                        DMEEditor.Passedarguments.EventType = "FOLDER";
                        Visutil.ShowPage("uc_library_files", (PassedArgs)DMEEditor.Passedarguments);
                    }
                    else
                        Visutil.Controlmanager.ShowMessege("Dhub", "Your not the Library Owner", null);
                }


                //NodesHelpers.AddFolder(this, TreeEditor, DMEEditor, Visutil);
            }
            catch (Exception ex)
            {
                string mes = "Could not Add Database Connection";
                DMEEditor.AddLogMessage(ex.Message, mes, DateTime.Now, -1, mes, Errors.Failed);
            };
            return DMEEditor.ErrorObject;
        }
        #endregion Exposed Interface"


    }
}
