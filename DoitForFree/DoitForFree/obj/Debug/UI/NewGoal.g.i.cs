﻿#pragma checksum "..\..\..\UI\NewGoal.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BE03FCCA0C20C9495C935E59730C2D0D"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using DoitForFree.UI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace DoitForFree.UI {
    
    
    /// <summary>
    /// NewGoal
    /// </summary>
    public partial class NewGoal : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\..\UI\NewGoal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DoitForFree.UI.NewGoal NewGoalWindow;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\UI\NewGoal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbx标题;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\UI\NewGoal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbx描述;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\UI\NewGoal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DoitForFree.UI.MenuButton MenuButton截止时间;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\UI\NewGoal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContextMenu MenuCalendar;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\UI\NewGoal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Calendar Calendar截止时间;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\UI\NewGoal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn确定;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\UI\NewGoal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn取消;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DoitForFree;component/ui/newgoal.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UI\NewGoal.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.NewGoalWindow = ((DoitForFree.UI.NewGoal)(target));
            
            #line 7 "..\..\..\UI\NewGoal.xaml"
            this.NewGoalWindow.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tbx标题 = ((System.Windows.Controls.TextBox)(target));
            
            #line 25 "..\..\..\UI\NewGoal.xaml"
            this.tbx标题.GotFocus += new System.Windows.RoutedEventHandler(this.TextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 25 "..\..\..\UI\NewGoal.xaml"
            this.tbx标题.LostFocus += new System.Windows.RoutedEventHandler(this.TextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbx描述 = ((System.Windows.Controls.TextBox)(target));
            
            #line 28 "..\..\..\UI\NewGoal.xaml"
            this.tbx描述.GotFocus += new System.Windows.RoutedEventHandler(this.TextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 28 "..\..\..\UI\NewGoal.xaml"
            this.tbx描述.LostFocus += new System.Windows.RoutedEventHandler(this.TextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 4:
            this.MenuButton截止时间 = ((DoitForFree.UI.MenuButton)(target));
            return;
            case 5:
            this.MenuCalendar = ((System.Windows.Controls.ContextMenu)(target));
            return;
            case 6:
            this.Calendar截止时间 = ((System.Windows.Controls.Calendar)(target));
            
            #line 34 "..\..\..\UI\NewGoal.xaml"
            this.Calendar截止时间.SelectedDatesChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.Calendar_SelectedDatesChanged);
            
            #line default
            #line hidden
            
            #line 34 "..\..\..\UI\NewGoal.xaml"
            this.Calendar截止时间.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Calendar截止时间_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btn确定 = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\UI\NewGoal.xaml"
            this.btn确定.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btn取消 = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\UI\NewGoal.xaml"
            this.btn取消.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

