using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DoitForFree.UI
{
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

    class PwdButton : Button, INotifyPropertyChanged
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
