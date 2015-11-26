using System.ComponentModel;
using System.Windows.Controls;

namespace DoitForFree.UI
{
    /// <summary>
    /// 用于存在图片和文字的按钮
    /// </summary>
    class MenuButton : Button, INotifyPropertyChanged
    {
        private string imagePath;
        private string text;

        public string ImagePath
        {
            get
            {
                return imagePath;
            }

            set
            {
                imagePath = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ImagePath"));
                }
            }
        }

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}