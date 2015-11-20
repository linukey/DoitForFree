using System.ComponentModel;
using System.Windows.Controls;

namespace DoitForFree.UI
{
    class MenuButton : Button, INotifyPropertyChanged
    {
        private string imagePath;
        private string text;
        //private string parendName; //当MenuButton为另一个容器的元素时，记录该容器的姓名

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