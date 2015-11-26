using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace DoitForFree.UI
{
    /// <summary>
    /// 用于右侧显示收缩栏
    /// </summary>
    public class TitleButton : Button, INotifyPropertyChanged
    {
        private DateTime date;
        private string week;
        private string month;
        private string imagePath;
        

        public event PropertyChangedEventHandler PropertyChanged;

        public string Week
        {
            get
            {
                return week;
            }

            set
            {
                week = value;
                if(PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Week"));
                }
            }
        }

        public string Month
        {
            get
            {
                return month;
            }

            set
            {
                month = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Month"));
                }
            }
        }

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

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

    }

    /// <summary>
    /// 用于右侧显示任务列表里的单个任务
    /// </summary>
    public class TitleNodeButton : Button, INotifyPropertyChanged
    {
        private string startDate;
        private string endDate;
        private string title;
        private string discription;
        private string imagePath;

        public event PropertyChangedEventHandler PropertyChanged;

        public string StartDate
        {
            get
            {
                return startDate;
            }

            set
            {
                startDate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Time"));
                }
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Title"));
                }
            }
        }

        public string Discription
        {
            get
            {
                return discription;
            }

            set
            {
                discription = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Discription"));
                }
            }
        }

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

        public string EndDate
        {
            get
            {
                return endDate;
            }

            set
            {
                endDate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Time"));
                }
            }
        }
    }
}