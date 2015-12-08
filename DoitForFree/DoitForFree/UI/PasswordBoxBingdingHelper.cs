using System.Windows;
using System.Windows.Controls;

namespace DoitForFree.UI
{
    static class PasswordBoxBingdingHelper
    {
        public static readonly DependencyProperty IsPasswordBoxEnabledProperty = DependencyProperty.RegisterAttached("IsPasswordBoxEnabled",
    typeof(bool), typeof(PasswordBoxBingdingHelper), new UIPropertyMetadata(false, PasswordBoxEnabledChanged));
        public static void SetIsPasswordBoxEnabled(UIElement element, bool value)
        {
            element.SetValue(IsPasswordBoxEnabledProperty, value);
        }
        public static bool GetIsPasswordBoxEnabled(UIElement element)
        {
            return (bool)element.GetValue(IsPasswordBoxEnabledProperty);
        }
        public static void PasswordBoxEnabledChanged(object obj, DependencyPropertyChangedEventArgs arg)
        {
            PasswordBox pwd = obj as PasswordBox;
            if (pwd != null)
            {
                pwd.PasswordChanged += InfoConvert;
            }
        }

        public static void InfoConvert(object obj, RoutedEventArgs arg)
        {
            PasswordBox pwd = obj as PasswordBox;
            if (pwd != null)
            {
                pwd.SetValue(PasswordStoreProperty, pwd.Password);
            }
        }

        public static readonly DependencyProperty PasswordStoreProperty = DependencyProperty.RegisterAttached("PasswordStore",
            typeof(string), typeof(PasswordBoxBingdingHelper) /*, new PropertyMetadata(null, InitPassword)*/);
        public static void SetPasswordStore(UIElement element, string value)
        {
            element.SetValue(PasswordStoreProperty, value);
        }
        public static string GetPasswordStore(UIElement element)
        {
            return (string)element.GetValue(PasswordStoreProperty);
        }
        //public static void InitPassword(object obj, DependencyPropertyChangedEventArgs arg)
        //{
        //    PasswordBox pwd = obj as PasswordBox;
        //    if(pwd != null)
        //    {
        //        pwd.Password = (string)pwd.GetValue(PasswordStoreProperty);
        //    }
        //}
    }
}
