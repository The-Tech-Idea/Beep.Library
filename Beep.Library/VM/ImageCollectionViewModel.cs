using CommunityToolkit.Mvvm.ComponentModel;
using Beep.Vis.Module;
using TheTechIdea.Beep.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTechIdea.Beep;
using System.IO;

namespace Beep.Library.VM
{
    public partial class ImageCollectionViewModel : BaseViewModel
    {
        [ObservableProperty]
        List<string> images = new List<string>();
        [ObservableProperty]
        string imagesLocation;
        public ImageCollectionViewModel(IDMEEditor dMEEditor, IVisManager visManager) :base (dMEEditor, visManager)
        {
            foreach(string filename in Directory.EnumerateFiles(ImagesLocation))
            {
                images.Add(filename);
            }
        }
    }
}
