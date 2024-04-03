using TheTechIdea.Beep.Editor;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TheTechIdea.Beep.Library.Model
{ 
public class LIB_FOLDERS :  Entity 
{ 
public  LIB_FOLDERS (){}

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

 private  double  _LIB_IDValue ;

 public  double LIB_ID
    {
        get
        {
            return this._LIB_IDValue;
        }

        set
        {
       SetProperty(ref _LIB_IDValue, value);
    }
    }

 private System.String  _FOLDERNAMEValue ;

 public System.String FOLDERNAME
    {
        get
        {
            return this._FOLDERNAMEValue;
        }

        set
        {
       SetProperty(ref _FOLDERNAMEValue, value);
    }
    }

 private System.String  _ICONNAMEValue ;

 public System.String ICONNAME
    {
        get
        {
            return this._ICONNAMEValue;
        }

        set
        {
       SetProperty(ref _ICONNAMEValue, value);
    }
    }
} 


} 

