using System.ComponentModel;
using System.Windows.Controls;

namespace DoitForFree.UI
{
    /// <summary>
    /// 用于只存在图片的按钮
    /// </summary>
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
