using TheTechIdea.Beep.Editor;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TheTechIdea.Beep.Library.Model
{ 
public class LIB_FILES :  Entity 
{ 
public  LIB_FILES (){}

 private System.String  _TEAMCODEValue ;

 public System.String TEAMCODE
    {
        get
        {
            return this._TEAMCODEValue;
        }

        set
        {
       SetProperty(ref _TEAMCODEValue, value);
    }
    }

 private  double  _IDValue ;

 public  double ID
    {
        get
        {
            return this._IDValue;
        }

        set
        {
       SetProperty(ref _IDValue, value);
    }
    }

 private  double  _REF_IDValue ;

 public  double REF_ID
    {
        get
        {
            return this._REF_IDValue;
        }

        set
        {
       SetProperty(ref _REF_IDValue, value);
    }
    }



 private  double  _REFNOValue ;

 public  double REF_NO
    {
        get
        {
            return this._REFNOValue;
        }

        set
        {
       SetProperty(ref _REFNOValue, value);
    }
    }

 private System.DateTime  _DOCDATEValue ;

 public System.DateTime DOCDATE
    {
        get
        {
            return this._DOCDATEValue;
        }

        set
        {
       SetProperty(ref _DOCDATEValue, value);
    }
    }

 private System.String  _DOCPATHValue ;

 public System.String DOCPATH
    {
        get
        {
            return this._DOCPATHValue;
        }

        set
        {
       SetProperty(ref _DOCPATHValue, value);
    }
    }

 private System.String  _DOCTYPEValue ;

 public System.String DOCTYPE
    {
        get
        {
            return this._DOCTYPEValue;
        }

        set
        {
       SetProperty(ref _DOCTYPEValue, value);
    }
    }

 private System.String  _DOCDESCRIPTIONValue ;

 public System.String DOCDESCRIPTION
    {
        get
        {
            return this._DOCDESCRIPTIONValue;
        }

        set
        {
       SetProperty(ref _DOCDESCRIPTIONValue, value);
    }
    }

 private System.DateTime  _INSERTDATEValue ;

 public System.DateTime INSERTDATE
    {
        get
        {
            return this._INSERTDATEValue;
        }

        set
        {
       SetProperty(ref _INSERTDATEValue, value);
    }
    }

 private System.String  _INSERTBYValue ;

 public System.String INSERTBY
    {
        get
        {
            return this._INSERTBYValue;
        }

        set
        {
       SetProperty(ref _INSERTBYValue, value);
    }
    }

 private  double  _REVIEW_IDValue ;

 public  double REVIEW_ID
    {
        get
        {
            return this._REVIEW_IDValue;
        }

        set
        {
       SetProperty(ref _REVIEW_IDValue, value);
    }
    }

 private  double  _TICKET_IDValue ;

 public  double TICKET_ID
    {
        get
        {
            return this._TICKET_IDValue;
        }

        set
        {
       SetProperty(ref _TICKET_IDValue, value);
    }
    }

 private  double  _LIBRARY_IDValue ;

 public  double LIBRARY_ID
    {
        get
        {
            return this._LIBRARY_IDValue;
        }

        set
        {
       SetProperty(ref _LIBRARY_IDValue, value);
    }
    }

 private System.String  _DOCNAMEValue ;

 public System.String DOCNAME
    {
        get
        {
            return this._DOCNAMEValue;
        }

        set
        {
       SetProperty(ref _DOCNAMEValue, value);
    }
    }

 private  double  _FOLDER_IDValue ;

 public  double FOLDER_ID
    {
        get
        {
            return this._FOLDER_IDValue;
        }

        set
        {
       SetProperty(ref _FOLDER_IDValue, value);
    }
    }
} 


} 

