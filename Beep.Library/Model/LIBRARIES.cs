using TheTechIdea.Beep.Editor;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TheTechIdea.Beep.Library.Model
{
    public class LIBRARIES : Entity
    {
        public LIBRARIES() { }

        private double _IDValue;

        public double ID
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

        private System.String _LIBRARY_NAMEValue;

        public System.String LIBRARY_NAME
        {
            get
            {
                return this._LIBRARY_NAMEValue;
            }

            set
            {
                SetProperty(ref _LIBRARY_NAMEValue, value);
            }
        }

        private System.String _OWNER_TEAMIDValue;

        public System.String OWNER_TEAMID
        {
            get
            {
                return this._OWNER_TEAMIDValue;
            }

            set
            {
                SetProperty(ref _OWNER_TEAMIDValue, value);
            }
        }

        private System.String _OWNER_NOValue;

        public System.String OWNER_NO
        {
            get
            {
                return this._OWNER_NOValue;
            }

            set
            {
                SetProperty(ref _OWNER_NOValue, value);
            }
        }

        private System.String _LIBRARY_DESCRIPTIONValue;

        public System.String LIBRARY_DESCRIPTION
        {
            get
            {
                return this._LIBRARY_DESCRIPTIONValue;
            }

            set
            {
                SetProperty(ref _LIBRARY_DESCRIPTIONValue, value);
            }
        }

        private System.String _ROW_CREATE_BYValue;

        public System.String ROW_CREATE_BY
        {
            get
            {
                return this._ROW_CREATE_BYValue;
            }

            set
            {
                SetProperty(ref _ROW_CREATE_BYValue, value);
            }
        }

        private System.DateTime _ROW_CREATE_DATEValue;

        public System.DateTime ROW_CREATE_DATE
        {
            get
            {
                return this._ROW_CREATE_DATEValue;
            }

            set
            {
                SetProperty(ref _ROW_CREATE_DATEValue, value);
            }
        }

        private System.String _ICONNAMEValue;

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

        private System.String _TEAMCODEValue;

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
        private System.String _GROUPCODEValue;

        public System.String GROUPCODE
        {
            get
            {
                return this._GROUPCODEValue;
            }

            set
            {
                SetProperty(ref _GROUPCODEValue, value);
            }
        }

        // GROUPCODE VARCHAR2(20)

    }
}

