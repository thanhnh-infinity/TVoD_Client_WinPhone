﻿#pragma checksum "C:\Users\user\documents\visual studio 2010\Projects\iTVOD_WindowPhone7\iTVOD_WindowPhone7\Live_Channel_Player.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C12B9CA05C0365DEB9429C9EC5901532"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Web.Media.SmoothStreaming;
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
    
    
    public partial class Live_Channel_Player : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Web.Media.SmoothStreaming.SmoothStreamingMediaElement liveChannelPlayer;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/iTVOD_WindowPhone7;component/Live_Channel_Player.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.liveChannelPlayer = ((Microsoft.Web.Media.SmoothStreaming.SmoothStreamingMediaElement)(this.FindName("liveChannelPlayer")));
        }
    }
}
