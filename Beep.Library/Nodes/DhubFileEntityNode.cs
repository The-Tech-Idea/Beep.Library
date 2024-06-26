﻿using Beep.Vis.Module;

using TheTechIdea;
using TheTechIdea.Beep;
using TheTechIdea.Beep.DataBase;
using Beep.Library.VM;
using TheTechIdea.Beep.Vis;
using TheTechIdea.Util;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Beep.Library.Nodes
{
    [Addin(Caption = "Files", Name = "DhubFileEntityNode.Dhub", misc = "Dhub", iconimage = "file.png", menu = "Dhub", ObjectType = "Dhub")]
    public class DhubFileEntityNode : IBranch
    {

        public DhubFileEntityNode()
        {


        }
        public DhubFileEntityNode(LibraryFilesViewModel viewModel, ITree pTreeEditor, IDMEEditor pDMEEditor, IBranch pParentNode, string pBranchText, int pID, EnumPointType pBranchType, string pimagename)
        {



            TreeEditor = pTreeEditor;
            DMEEditor = pDMEEditor;
            ParentBranchID = pParentNode.ID;
            BranchText = pBranchText;
            BranchType = pBranchType;
            DataSourceName = pBranchText;
            //string ext = Path.GetExtension(BranchText).Remove(0, 1);
            IconImageName = pimagename;

            if (pID != 0)

            {
                ID = pID;
                BranchID = pID;
            }
            ViewModel = viewModel;
        }
        public bool Visible { get; set; } = true;
        #region "Properties"
        LibraryFilesViewModel ViewModel;
        public bool IsDataSourceNode { get; set; } = true;
        ConnectionProperties cn = new ConnectionProperties();
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
        public string BranchText { get; set; } = "Files";
        public IDMEEditor DMEEditor { get; set; }
        public IDataSource DataSource { get; set; }
        public string DataSourceName { get; set; }
        public int Level { get; set; }
        public EnumPointType BranchType { get; set; } = EnumPointType.DataPoint;
        public int BranchID { get; set; }
        public string IconImageName { get; set; } = "file.png";
        public string BranchStatus { get; set; }
        public int ParentBranchID { get; set; }
        public string BranchDescription { get; set; } = "File node for File Library";
        public string BranchClass { get; set; } = "Dhub3.File";
        public List<IBranch> ChildBranchs { get; set; } = new List<IBranch>();
        public ITree TreeEditor { get; set; }
        public List<string> BranchActions { get; set; } = new List<string>();
        public object TreeStrucure { get; set; }
        public IVisManager Visutil { get; set; }
        public int MiscID { get; set; }


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
                CreateNodes();

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
        [Command(Caption = "Get Sheets", Hidden = false, iconimage = "get_childs.png")]
        public IErrorsInfo GetSheets()
        {

            try
            {
                TreeEditor.treeBranchHandler.RemoveChildBranchs(this);
                CreateChildNodes();
                // DMEEditor.AddLogMessage("Success", "Show File", DateTime.Now, 0, null, Errors.Ok);
            }
            catch (Exception ex)
            {
                string mes = "Could not Get Sheets";
                DMEEditor.AddLogMessage(ex.Message, mes, DateTime.Now, -1, mes, Errors.Failed);
            };
            return DMEEditor.ErrorObject;
        }
        [Command(Caption = "Refresh Sheets", Hidden = false, iconimage = "refresh.png")]
        public IErrorsInfo RefreshSheets()
        {
            try
            {
                TreeEditor.treeBranchHandler.RemoveChildBranchs(this);
                int i = 0;
                DataSource = DMEEditor.GetDataSource(BranchText);
                if (DataSource != null)
                {

                    DataSource.GetEntitesList();
                    if (DataSource.Entities.Count > 0)
                    {
                        DataSource.EntitiesNames = DataSource.Entities.Select(o => o.EntityName).ToList();
                        if (DataSource.EntitiesNames.Count > 0)
                        {
                            foreach (string n in DataSource.EntitiesNames)
                            {
                                EntityStructure entity = DataSource.GetEntityStructure(n, true);
                                CreateFileItemSheetsNode(i, n);
                                i += 1;
                            }

                        }
                    }
                }
                DMEEditor.AddLogMessage("Success", "Created child Nodes", DateTime.Now, 0, null, Errors.Ok);
            }
            catch (Exception ex)
            {
                string mes = "Could not Get Sheets";
                DMEEditor.AddLogMessage(ex.Message, mes, DateTime.Now, -1, mes, Errors.Failed);
            };
            return DMEEditor.ErrorObject;
        }
        //[BranchDelegate(Caption = "Remove", Hidden = false)]
        //public IErrorsInfo Remove()
        //{

        //    try
        //    {


        //        DMEEditor.AddLogMessage("Success", "Remove File", DateTime.Now, 0, null, Errors.Ok);
        //    }
        //    catch (Exception ex)
        //    {
        //        string mes = "Could not Remove File";
        //        DMEEditor.AddLogMessage(ex.Message, mes, DateTime.Now, -1, mes, Errors.Failed);
        //    };
        //    return DMEEditor.ErrorObject;
        //}
        [Command(Caption = "Remove", iconimage = "remove.png")]
        public IErrorsInfo Remove()
        {

            try
            {
                if (Visutil.Controlmanager.InputBoxYesNo("Remove", "Area you Sure ? you want to remove File???") == DialogResult.Yes)
                {

                    try
                    {
                        // DMEEditor.viewEditor.Views.Remove(DMEEditor.viewEditor.Views.Where(x => x.ViewName == DataView.ViewName).FirstOrDefault());
                        DMEEditor.ConfigEditor.RemoveDataConnection(BranchText);
                        DMEEditor.RemoveDataDource(BranchText);
                        TreeEditor.treeBranchHandler.RemoveBranch(this);
                        TreeEditor.treeBranchHandler.RemoveEntityFromCategory("FILE", TreeEditor.treeBranchHandler.GetBranch(ParentBranchID).BranchText, BranchText);
                        DMEEditor.ConfigEditor.SaveCategoryFoldersValues();
                        DMEEditor.AddLogMessage("Success", "Removed View from Views List", DateTime.Now, 0, null, Errors.Ok);
                    }
                    catch (Exception ex)
                    {
                        string mes = "Could not Remove View from Views List";
                        DMEEditor.AddLogMessage(ex.Message, mes, DateTime.Now, -1, mes, Errors.Failed);
                    };


                    TreeEditor.treeBranchHandler.RemoveBranch(this);
                }


                DMEEditor.AddLogMessage("Success", "Remove View", DateTime.Now, 0, null, Errors.Ok);
            }
            catch (Exception ex)
            {
                string mes = "Could not Remove View";
                DMEEditor.AddLogMessage(ex.Message, mes, DateTime.Now, -1, mes, Errors.Failed);
            };
            return DMEEditor.ErrorObject;
        }
        //[CommandAttribute(Caption = "Copy Entities")]
        //public IErrorsInfo CopyEntities()
        //{

        //    try
        //    {
        //        List<string> ents = new List<string>();
        //        if (TreeEditor.SelectedBranchs.Count > 0)
        //        {
        //            if (DataSource == null)
        //            {
        //                DataSource = DMEEditor.GetDataSource(DataSourceName);
        //            }
        //            if (DataSource != null)
        //            {
        //                foreach (int item in TreeEditor.SelectedBranchs)
        //                {
        //                    IBranch br = TreeEditor.treeBranchHandler.GetBranch(item);
        //                    ents.Add(br.BranchText);
        //                    // EntityStructure = DataSource.GetEntityStructure(br.BranchText, true);

        //                }
        //                IBranch pbr = TreeEditor.treeBranchHandler.GetBranch(ParentBranchID);
        //                List<ObjectItem> ob = new List<ObjectItem>(); ;
        //                ObjectItem it = new ObjectItem();
        //                it.obj = pbr;
        //                it.Name = "ParentBranch";
        //                ob.Add(it);

        //                PassedArgs args = new PassedArgs
        //                {
        //                    ObjectName = "DATABASE",
        //                    ObjectType = "TABLE",
        //                    EventType = "COPYENTITIES",
        //                    ParameterString1 = "COPYENTITIES",
        //                    DataSource = DataSource,
        //                    DatasourceName = DataSource.DatasourceName,
        //                    CurrentEntity = BranchText,
        //                    EntitiesNames = ents,
        //                    Objects = ob
        //                };

        //                DMEEditor.Passedarguments = args;
        //            }
        //            else
        //            {
        //                DMEEditor.AddLogMessage("Fail", "Could not get DataSource", DateTime.Now, -1, null, Errors.Failed);
        //            }

        //        }

        //        // TreeEditor.SendActionFromBranchToBranch(pbr, this, "Create View using Table");

        //    }
        //    catch (Exception ex)
        //    {
        //        string mes = "Could not Copy Entites";
        //        DMEEditor.AddLogMessage(ex.Message, mes, DateTime.Now, -1, mes, Errors.Failed);
        //    };
        //    return DMEEditor.ErrorObject;
        //}
        #endregion Exposed Interface"
        #region "Other Methods"
        public IErrorsInfo CreateNodes()
        {
            //  TxtXlsCSVFileSource fs = null; ;
            try
            {
                int i = 0;
                DataSource = DMEEditor.GetDataSource(BranchText);

                if (DataSource != null)
                {
                    DataSource.Openconnection();
                    if (DataSource.ConnectionStatus == System.Data.ConnectionState.Open)
                    {
                        DataSource.GetEntitesList();


                        if (DataSource.Entities.Count > 0)
                        {
                            DataSource.EntitiesNames = DataSource.Entities.Select(o => o.EntityName).ToList();
                            if (DataSource.EntitiesNames.Count > 0)
                            {
                                foreach (string n in DataSource.EntitiesNames)
                                {
                                    CreateFileItemSheetsNode(i, n);
                                    i += 1;
                                }

                            }
                        }
                    }
                    else
                    {
                        string mes = "Error : Could Not Find File";
                        DMEEditor.AddLogMessage("Beep", mes, DateTime.Now, -1, mes, Errors.Failed);
                        Visutil.Controlmanager.MsgBox("Beep", mes);
                    }


                }
                else
                {
                    string mes = "Error : Could Not Find File DataSource";
                    DMEEditor.AddLogMessage("Beep", mes, DateTime.Now, -1, mes, Errors.Failed);
                    Visutil.Controlmanager.MsgBox("Beep", mes);
                }

                DMEEditor.AddLogMessage("Success", "Created child Nodes", DateTime.Now, 0, null, Errors.Ok);
            }
            catch (Exception ex)
            {
                string mes = "Could not Create child Nodes";
                DMEEditor.AddLogMessage(ex.Message, mes, DateTime.Now, -1, mes, Errors.Failed);
            };
            return DMEEditor.ErrorObject;

        }
        private IErrorsInfo CreateFileItemSheetsNode(int id, string Sheetname)
        {

            try
            {
                DhubFileEntitySheetNode fileitemsheet = new DhubFileEntitySheetNode(ViewModel, TreeEditor, DMEEditor, this, Sheetname, TreeEditor.SeqID, EnumPointType.Entity, IconImageName, BranchText);
                fileitemsheet.DataSource = DataSource;
                fileitemsheet.DataSourceName = DataSourceName;

                // ChildBranchs.Add(fileitemsheet);
                TreeEditor.treeBranchHandler.AddBranch(this, fileitemsheet);

                DMEEditor.AddLogMessage("Success", "Added sheet", DateTime.Now, 0, null, Errors.Ok);
            }
            catch (Exception ex)
            {
                string mes = "Could not Add sheet";
                DMEEditor.AddLogMessage(ex.Message, mes, DateTime.Now, -1, mes, Errors.Failed);
            };
            return DMEEditor.ErrorObject;


        }

        public IBranch CreateCategoryNode(CategoryFolder p)
        {
            throw new NotImplementedException();
        }
        #endregion"Other Methods"

    }
}
