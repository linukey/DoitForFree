using System.ComponentModel;
using System.Windows.Controls;

namespace DoitForFree.UI
{
    class ImageButton : Button, INotifyPropertyChanged
    {
        private string imagePath;

        public string ImagePath
        {
            get
            {
                return imagePath;
            }

            set
            {
                imagePath = value;
                if(PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ImagePath"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
