﻿#pragma checksum "C:\Users\user\documents\visual studio 2010\Projects\iTVOD_WindowPhone7\iTVOD_WindowPhone7\List_Video_Follow_Child_Category.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B4B073D288C4901936463DD86F8DCC17"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace iTVOD_WindowPhone7 {
    
    
    public partial class List_Video_Follow_Child_Category : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock lblCategory_Name;
        
        internal System.Windows.Controls.Button btnNewest;
        
        internal System.Windows.Controls.Button btnMostView;
        
        internal System.Windows.Controls.StackPanel ContentPanel;
        
        internal System.Windows.Controls.ListBox lstVideo_Follow_Category;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/iTVOD_WindowPhone7;component/List_Video_Follow_Child_Category.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.lblCategory_Name = ((System.Windows.Controls.TextBlock)(this.FindName("lblCategory_Name")));
            this.btnNewest = ((System.Windows.Controls.Button)(this.FindName("btnNewest")));
            this.btnMostView = ((System.Windows.Controls.Button)(this.FindName("btnMostView")));
            this.ContentPanel = ((System.Windows.Controls.StackPanel)(this.FindName("ContentPanel")));
            this.lstVideo_Follow_Category = ((System.Windows.Controls.ListBox)(this.FindName("lstVideo_Follow_Category")));
        }
    }
}

