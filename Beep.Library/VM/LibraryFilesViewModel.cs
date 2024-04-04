using CommunityToolkit.Mvvm.ComponentModel;
using TheTechIdea.Beep;
using TheTechIdea.Beep.Editor;
using DataManagementModels.Editor;
using TheTechIdea.Beep.Report;
using TheTechIdea.Util;
using TheTechIdea.Beep.MVVM;
using TheTechIdea.Beep.Library.Model;
using Beep.Vis.Module;




using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;

namespace Beep.Library.VM
{
    public partial class LibraryFilesViewModel : BaseViewModel
    {
      
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
        LIBRARIES currentLibrary;
        [ObservableProperty]
        string gfxPath;
        [ObservableProperty]
        double folder_id;
        public double CurrentID { get; set; }
        public UnitofWork<LIB_FILES> UnitofWork { get; set; }
        public ObservableBindingList<LIB_FILES> Files { get { return UnitofWork.Units; } set { UnitofWork.Units = value; } }
        public LibraryFilesViewModel(IDMEEditor dMEEditor, IVisManager visManager) : base(dMEEditor, visManager)
        {

            UnitofWork = new UnitofWork<LIB_FILES>(DMEEditor, "dhubdb", "LIB_FILES", "ID");
            UnitofWork.PostCreate += UnitofWork_PostCreate;
            UnitofWork.Sequencer = "LIB_FILES_SEQ";
         
            //GlobalPath = dhubConfig.Library.GlobalPath  ;
            //TeamPath = dhubConfig.Library.GetTeamPath();
            //GfxPath = dhubConfig.Library.GFXPath;
        }
        public string GetGlobalLibraryPath(string libraryname)
        {
            return System.IO.Path.Combine(GlobalPath, libraryname);
        }
        public string GetMyLibraryPath(string libraryname)
        {
            return System.IO.Path.Combine(LibraryPath, libraryname);
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
            return System.IO.Path.Combine(TeamPath, libraryname);
        }
        public void GetLibraryFiles(double LibID)
        {
            UnitofWork.Get(new List<AppFilter>() { new AppFilter() { FieldName = "LIBRARY_ID", Operator = "=", FilterValue = LibID.ToString() } });
            //CurrentLibrary = Repo.LoadDataFirst<LIBRARIES>($"select * from libraries where id={LibID}", null).Result;
        }
        public void GetLibraryFiles(double LibID,double folderid)
        {
            Folder_id = folderid;
            UnitofWork.Get(new List<AppFilter>() { new AppFilter() { FieldName = "LIBRARY_ID", Operator = "=", FilterValue = LibID.ToString() },new AppFilter() { FieldName = "FOLDER_ID", Operator = "=", FilterValue = folderid.ToString() } });
           // CurrentLibrary = Repo.LoadDataFirst<LIBRARIES>($"select * from libraries where id={LibID}", null).Result;
        }
        public List<LIB_FILES> GetMyFiles()
        {
            UnitofWork.Get(new List<AppFilter>() { new AppFilter() { FieldName= "INSERTBY", Operator="=", FilterValue = VisManager.User.Email } });
            return UnitofWork.Units.Where(x => x.INSERTBY ==VisManager.User.Email).ToList();
        }
        public List<string> GetFileTypes()
        {
            return UnitofWork.Units.Select(x => x.DOCTYPE).Distinct().ToList();
        }
        private void UnitofWork_PostCreate(object? sender, UnitofWorkParams e)
        {
            LIB_FILES r = (LIB_FILES)sender;
            //Status = RecordStatus.New;
            r.INSERTDATE = DateTime.Now;
            r.INSERTBY = VisManager.User.Email;
            r.TEAMCODE=VisManager.User.Email;
            r.DOCDATE = DateTime.Now;
            if (CurrentLibrary != null) {
                r.LIBRARY_ID = CurrentLibrary.ID;
            }
            //if (CurrentReview != null)
            //{
            //    r.REVIEW_ID = CurrentReview.ID;
            //}
            //if (CurrentTicket != null)
            //{
            //    r.TICKET_ID = CurrentTicket.ID;
            //}
            if (Folder_id > 0)
            {
                r.FOLDER_ID= Folder_id;
            }
        }
        public LIB_FILES CreateFileForReview(string filename,string filepath,double reviewid,string desription=null)
        {
            // Create lib_Files for Review
            LIB_FILES lIB_FILES = CreateLibFileRecord(filename, filepath, desription);
            lIB_FILES.REVIEW_ID = reviewid;
            UnitofWork.Create(lIB_FILES);
            return lIB_FILES;
        }
      
        public LIB_FILES CreateFileForTicket(string filename, string filepath, double ticketid, string desription = null)
        {
            // Create lib_Files for Review
            LIB_FILES lIB_FILES = CreateLibFileRecord(filename, filepath, desription);
            lIB_FILES.TICKET_ID = ticketid;
         
            UnitofWork.Create(lIB_FILES);
            return lIB_FILES;
        }
        private LIB_FILES CreateLibFileRecord(string filename, string filepath, string description = null)
        {
            LIB_FILES lIB_FILES = new LIB_FILES();
            lIB_FILES.DOCNAME = filename;
            lIB_FILES.DOCPATH = filepath;
            
            lIB_FILES.DOCDESCRIPTION = description;
            lIB_FILES.INSERTDATE = DateTime.Now;
          //  lIB_FILES.TEAMCODE = DhubConfig.userManager.User.TEAM;
          //  lIB_FILES.INSERTBY = DhubConfig.userManager.User.KOCNO;
            // get the file type
            string ext = System.IO.Path.GetExtension(filename).ToLower();
            if (ext == ".doc" || ext == ".docx")
                lIB_FILES.DOCTYPE = "WORD";
            else if (ext == ".xls" || ext == ".xlsx")
                lIB_FILES.DOCTYPE = "EXCEL";
            else if (ext == ".pdf")
                lIB_FILES.DOCTYPE = "PDF";
            else if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                lIB_FILES.DOCTYPE = "IMAGE";
            else if (ext == ".txt")
                lIB_FILES.DOCTYPE = "TEXT";
            else if (ext == ".zip" || ext == ".rar")
                lIB_FILES.DOCTYPE = "ZIP";
            else if (ext == ".mp4" || ext == ".avi" || ext == ".wmv" || ext == ".mov")
                lIB_FILES.DOCTYPE = "VIDEO";
            else if (ext == ".mp3" || ext == ".wav" || ext == ".wma")
                lIB_FILES.DOCTYPE = "AUDIO";
            else if (ext == ".ppt" || ext == ".pptx")
                lIB_FILES.DOCTYPE = "POWERPOINT";
            else if (ext == ".csv")
                lIB_FILES.DOCTYPE = "CSV";
            else if (ext == ".xml")
                lIB_FILES.DOCTYPE = "XML";
            else if (ext == ".json")
                lIB_FILES.DOCTYPE = "JSON";
            else if (ext == ".html" || ext == ".htm")
                lIB_FILES.DOCTYPE = "HTML";
            else if (ext == ".dll")
                lIB_FILES.DOCTYPE = "DLL";
            else if (ext == ".cs")
                lIB_FILES.DOCTYPE = "CS";
            else if (ext == ".vb")
                lIB_FILES.DOCTYPE = "VB";
            else if (ext == ".py")
                lIB_FILES.DOCTYPE = "PYTHON";
            else if (ext == ".js")
                lIB_FILES.DOCTYPE = "JAVASCRIPT";
            else if (ext == ".css")
                lIB_FILES.DOCTYPE = "CSS";
            else if (ext == ".sql")
                lIB_FILES.DOCTYPE = "SQL";
            else if (ext == ".r")
                lIB_FILES.DOCTYPE = "R";
            else if (ext == ".java")
                lIB_FILES.DOCTYPE = "JAVA";
            else if (ext == ".cshtml")
                lIB_FILES.DOCTYPE = "CSHTML";
            else if (ext == ".vbhtml")
                lIB_FILES.DOCTYPE = "VBHTML";
            else if (ext == ".aspx")
                lIB_FILES.DOCTYPE = "ASPX";
            else                 
                lIB_FILES.DOCTYPE = "FILE";
            return lIB_FILES;
        }
        public double MoveFileToMyLibrary(string filename, string filepath,string Libname,bool overwrite=true)
        {
            // move file to another folder
            try
            {
                libraryName = Libname;
                string MyLibraryPath = GetMyLibraryPath(LibraryName);
                if (!Directory.Exists(MyLibraryPath))
                {
                    Directory.CreateDirectory(MyLibraryPath);
                }
                string srcfile= System.IO.Path.Combine(filepath, filename);
                string destfile = System.IO.Path.Combine(MyLibraryPath, filename);
                System.IO.File.Move(srcfile, destfile, overwrite);
                // update the record
                LIB_FILES lIB_FILES = UnitofWork.Units.Where(x => x.DOCNAME == filename && x.DOCPATH == MyLibraryPath).FirstOrDefault();
                if (lIB_FILES == null)
                {
                    lIB_FILES= CreateLibFileRecord(filename, MyLibraryPath);
                    UnitofWork.Create(lIB_FILES);
                }
                else
                {
                    lIB_FILES.DOCPATH = MyLibraryPath;
                    UnitofWork.Update(lIB_FILES);
                }

                IErrorsInfo er= UnitofWork.Commit().Result;
                if(er.Flag== Errors.Ok)
                {
                    return lIB_FILES.ID;
                }else
                {
                    DMEEditor.AddLogMessage("Dhub", $"Error moving file {filename} to {MyLibraryPath}- {er.Message}", DateTime.Now, 0, null, TheTechIdea.Util.Errors.Failed);
                    return -1;
                }
              
            }
            catch (Exception ex)
            {
                DMEEditor.AddLogMessage("Dhub", $"Error moving file {filename} to {MyLibraryPath}", DateTime.Now, 0,null, TheTechIdea.Util.Errors.Failed);
                return -1;
            }
        }
        public double MoveFileToGlobleLibrary(string filename, string filepath,string libname, bool overwrite = true)
        {
            libraryName = libname;
            // move file to another folder
            string MyLibraryPath = GetGlobalLibraryPath(libname);
            try
            {
                if (!Directory.Exists(MyLibraryPath))
                {
                    Directory.CreateDirectory(MyLibraryPath);
                }
                string srcfile = System.IO.Path.Combine(filepath, filename);
                string destfile = System.IO.Path.Combine(MyLibraryPath, filename);
                System.IO.File.Move(srcfile, destfile, overwrite);
                // update the record
                LIB_FILES lIB_FILES = UnitofWork.Units.Where(x => x.DOCNAME == filename && x.DOCPATH == MyLibraryPath).FirstOrDefault();
                if (lIB_FILES == null)
                {
                    lIB_FILES = CreateLibFileRecord(filename, MyLibraryPath);
                    UnitofWork.Create(lIB_FILES);
                }
                else
                {
                    lIB_FILES.DOCPATH = MyLibraryPath;
                    UnitofWork.Update(lIB_FILES);
                }
                UnitofWork.Commit();
                lIB_FILES = UnitofWork.Units.Where(x => x.DOCNAME == filename && x.DOCPATH == MyLibraryPath).FirstOrDefault();
                return lIB_FILES.ID;
            }
            catch (Exception ex)
            {
                DMEEditor.AddLogMessage("Dhub", $"Error moving file {filename} to {MyLibraryPath}", DateTime.Now, 0, null, TheTechIdea.Util.Errors.Failed);
                return -1;
            }
        }
        public double MoveFileToTeamLibrary(string filename, string filepath, string libname, bool overwrite = true)
        {
            // move file to another folder
            try
            {
                libraryName = libname;
                string MyLibraryPath = GetTeamLibraryPath(libname);
                if (!Directory.Exists(MyLibraryPath))
                {
                    Directory.CreateDirectory(MyLibraryPath);
                }
                string srcfile = System.IO.Path.Combine(filepath, filename);
                string destfile = System.IO.Path.Combine(MyLibraryPath, filename);
                System.IO.File.Move(srcfile, destfile, overwrite);
                // update the record
                LIB_FILES lIB_FILES = UnitofWork.Units.Where(x => x.DOCNAME == filename && x.DOCPATH == MyLibraryPath).FirstOrDefault();
                if (lIB_FILES == null)
                {
                    lIB_FILES = CreateLibFileRecord(filename, MyLibraryPath);
                    UnitofWork.Create(lIB_FILES);
                }
                else
                {
                    lIB_FILES.DOCPATH = MyLibraryPath;
                    UnitofWork.Update(lIB_FILES);
                }
                UnitofWork.Commit();
                return lIB_FILES.ID;
            }
            catch (Exception ex)
            {
                DMEEditor.AddLogMessage("Dhub", $"Error moving file {filename} to {MyLibraryPath}", DateTime.Now, 0, null, TheTechIdea.Util.Errors.Failed);
                return -1;
            }
        }
        public double MoveTicketFileToLibrary(string filename, string filepath,double ticketid,string uwi=null, bool overwrite = true)
        {
            // move file to another folder
            try
            {
               
                string MyLibraryPath = GetTicketsPath(ticketid.ToString());
                if (!Directory.Exists(MyLibraryPath))
                {
                    Directory.CreateDirectory(MyLibraryPath);
                }
                string srcfile = System.IO.Path.Combine(filepath, filename);
                string destfile = System.IO.Path.Combine(MyLibraryPath, filename);
                System.IO.File.Move(srcfile, destfile, overwrite);
                
                // update the record
                LIB_FILES lIB_FILES = UnitofWork.Units.Where(x => x.DOCNAME == filename && x.DOCPATH == MyLibraryPath).FirstOrDefault();
                if (lIB_FILES == null)
                {
                    lIB_FILES = CreateFileForTicket(filename, MyLibraryPath, ticketid, uwi);
                    UnitofWork.Create(lIB_FILES);
                }
                else
                {
                    lIB_FILES.DOCPATH = MyLibraryPath;
                    UnitofWork.Update(lIB_FILES);
                }
                UnitofWork.Commit();
                return lIB_FILES.ID;
            }
            catch (Exception ex)
            {
                DMEEditor.AddLogMessage("Dhub", $"Error moving file {filename} to {myLibraryPath}", DateTime.Now, 0, null, TheTechIdea.Util.Errors.Failed);
                return -1;
            }
        }
        public double MoveReviewFileToLibrary(string filename, string filepath, double reviewid, string entityname = null, bool overwrite = true)
        {
            // move file to another folder
            try
            {
                string MyLibraryPath = GetReviewPath(reviewid.ToString());
                if (!Directory.Exists(MyLibraryPath))
                {
                    Directory.CreateDirectory(MyLibraryPath);
                }
                string srcfile = System.IO.Path.Combine(filepath, filename);
                string destfile = System.IO.Path.Combine(MyLibraryPath, filename);
                System.IO.File.Move(srcfile, destfile, overwrite);
                // update the record
                LIB_FILES lIB_FILES = UnitofWork.Units.Where(x => x.DOCNAME == filename && x.DOCPATH == MyLibraryPath).FirstOrDefault();
                if (lIB_FILES == null)
                {
                    lIB_FILES= CreateFileForReview(filename, MyLibraryPath, reviewid, entityname);

                }
                else
                {
                    lIB_FILES.DOCPATH = MyLibraryPath;
                    UnitofWork.Update(lIB_FILES);
                }
                UnitofWork.Commit();
                return lIB_FILES.ID;
            }
            catch (Exception ex)
            {
                DMEEditor.AddLogMessage("Dhub", $"Error moving file {filename} to {MyLibraryPath}", DateTime.Now, 0, null, TheTechIdea.Util.Errors.Failed);
                return -1;
            }
        }
        public void GetFilesForReview(double reviewid)
        {
            UnitofWork.Get(new List<TheTechIdea.Beep.Report.AppFilter>() { new TheTechIdea.Beep.Report.AppFilter() { FieldName = "REVIEW_ID", FilterValue = reviewid.ToString(), Operator="=" } });
        }
        public void GetFilesForTicket(double ticketid)
        {
            UnitofWork.Get(new List<TheTechIdea.Beep.Report.AppFilter>() { new TheTechIdea.Beep.Report.AppFilter() { FieldName = "TICKET_ID", FilterValue = ticketid.ToString(), Operator = "=" } });
        }
        public  void CopyFileUsingBinaryStream(string sourcePath, string destinationPath)
        {
            try
            {
                using (FileStream sourceStream = File.Open(sourcePath, FileMode.Open))
                {
                    using (FileStream destinationStream = File.Create(destinationPath))
                    {
                        byte[] buffer = new byte[1024]; // Buffer size can be adjusted
                        int bytesRead;

                        while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            destinationStream.Write(buffer, 0, bytesRead);
                        }
                    }
                }

                Console.WriteLine("File copied successfully.");
            }
            catch (Exception ex)
            {
                DMEEditor.AddLogMessage("Dhub", $"Error moving file {sourcePath} to {destinationPath}", DateTime.Now, 0, null, TheTechIdea.Util.Errors.Failed);
                Console.WriteLine("Error during file copy: " + ex.Message);
            }
        }
    }
}
