using Beep.Library.VM;
using Beep.Vis.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using TheTechIdea;
using TheTechIdea.Beep;
using TheTechIdea.Beep.Addin;
using TheTechIdea.Beep.DataBase;
using TheTechIdea.Beep.Library.Model;
using TheTechIdea.Beep.Vis;
using TheTechIdea.Util;

namespace Beep.Library.Nodes
{
    [Addin(Caption = "Libraries", Name = "Libraries.Dhub", misc = "Dhub", iconimage = "Library.png", menu = "Dhub", ObjectType = "Beep")]
    [AddinVisSchema(BranchType = EnumPointType.Root, BranchClass = "Dhub3.File")]
    public class DhubFileLibraryRootNode : IBranch
    {
        public bool IsDataSourceNode { get; set; } = false;
        public string GuidID { get; set; } = Guid.NewGuid().ToString();
        public string ParentGuidID { get; set; }
        public string DataSourceConnectionGuidID { get; set; }
        public string EntityGuidID { get; set; }
        public string MiscStringID { get; set; }
        public DhubFileLibraryRootNode()
        {


        }
        public IBranch ParentBranch { get; set; }
        public string ObjectType { get; set; } = "Dhub";
        public int ID { get; set; }
       // public IDhubMainConfig DhubConfig { get; set; }
        public IDMEEditor DMEEditor { get; set; }
        public IDataSource DataSource { get; set; }
        public string DataSourceName { get; set; }
        public List<IBranch> ChildBranchs { get; set; } = new List<IBranch>();
        public ITree TreeEditor { get; set; }
        public IVisManager Visutil { get; set; }
        public List<string> BranchActions { get; set; } = new List<string>();
        public EntityStructure EntityStructure { get; set; }
        public int MiscID { get; set; }
        public string Name { get; set; }
        public string BranchText { get; set; } = "Libraries";
        public int Level { get; set; } = 0;
        public EnumPointType BranchType { get; set; } = EnumPointType.Root;
        public int BranchID { get; set; }
        public string IconImageName { get; set; } = "Library.png";
        public string BranchStatus { get; set; }
        public int ParentBranchID { get; set; }
        public string BranchDescription { get; set; } = "Root node for File Library";
        public string BranchClass { get; set; } = "Dhub3.File.Root";

        LibrariesViewModel ViewModel;

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
                //categoryBranch = new DhubFileCategoryNode(ViewModel, TreeEditor, DMEEditor, parent, p.FolderName, TreeEditor.SeqID, EnumPointType.Category, "category.png");
                //TreeEditor.treeBranchHandler.AddBranch(parent, categoryBranch);
                //ChildBranchs.Add(categoryBranch);
                //categoryBranch.CreateChildNodes();


            }
            catch (Exception ex)
            {
                DMEEditor.Logger.WriteLog($"Error Creating Category  View Node ({ex.Message}) ");
                DMEEditor.ErrorObject.Flag = Errors.Failed;
                DMEEditor.ErrorObject.Ex = ex;
            }
            return categoryBranch;
        }

        #region "Interface Methods"
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
                //if (ViewModel == null)
                //{
                //    ViewModel = new LibrariesViewModel(DMEEditor,Visutil);
                //}
                //-------------------------------------------------------------------------------------
                ViewModel.GetLibraries();
                List<LIBRARIES> Libs = ViewModel.AllLibraries;
                foreach (var item in Libs)
                {
                    DhubLibraryNode categoryBranch = (DhubLibraryNode)ChildBranchs.FirstOrDefault(p => p.BranchText == item.LIBRARY_NAME);
                    if (categoryBranch == null)
                    {
                        DhubLibraryNode br = new DhubLibraryNode(item, TreeEditor.SeqID, ViewModel, TreeEditor, DMEEditor, this, item.LIBRARY_NAME, $"{item.ICONNAME}");
                        TreeEditor.treeBranchHandler.AddBranch(this, br);
                        br.CreateChildNodes();
                        ChildBranchs.Add(br);
                    }
                    //  categoryBranch.CreateChildNodes();
                }


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
        #endregion "Interface Methods"
        #region "Exposed Interface"

        [Command(Caption = "Create Library", Hidden = false, iconimage = "add.png")]
        public IErrorsInfo CreateLib()
        {

            try
            {

                //    }   
                DMEEditor.Passedarguments.CurrentEntity = "LibrariesViewModel";
                DMEEditor.Passedarguments.ObjectName = BranchText;
                DMEEditor.Passedarguments.ObjectType = "LibrariesViewModel";


                if (DMEEditor.Passedarguments.Objects.Any(p => p.Name != "LibrariesViewModel"))
                {
                    DMEEditor.Passedarguments.Objects.Add(new ObjectItem { Name = "LibrariesViewModel", obj = ViewModel });
                }
                Visutil.ShowPage("uc_library", (PassedArgs)DMEEditor.Passedarguments);

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
