using TheTechIdea.Beep.Editor;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace  TheTechIdea.Beep.Library.Model 
{ 
public class LIBRARIES_PRIVILEGES :  Entity 
{ 
public  LIBRARIES_PRIVILEGES (){}

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

 private  double  _BU_IDValue ;

 public  double BU_ID
    {
        get
        {
            return this._BU_IDValue;
        }

        set
        {
       SetProperty(ref _BU_IDValue, value);
    }
    }

 private System.String  _CANVIEWValue ;

 public System.String CANVIEW
    {
        get
        {
            return this._CANVIEWValue;
        }

        set
        {
       SetProperty(ref _CANVIEWValue, value);
    }
    }

 private System.String  _CANDELETEValue ;

 public System.String CANDELETE
    {
        get
        {
            return this._CANDELETEValue;
        }

        set
        {
       SetProperty(ref _CANDELETEValue, value);
    }
    }

 private System.String  _CANUPDATEValue ;

 public System.String CANUPDATE
    {
        get
        {
            return this._CANUPDATEValue;
        }

        set
        {
       SetProperty(ref _CANUPDATEValue, value);
    }
    }

 private System.String  _CANADDValue ;

 public System.String CANADD
    {
        get
        {
            return this._CANADDValue;
        }

        set
        {
       SetProperty(ref _CANADDValue, value);
    }
    }
} 


} 

