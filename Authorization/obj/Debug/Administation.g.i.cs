﻿#pragma checksum "..\..\Administation.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "74A77BC52ECF27EF85851A80CD89137F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Authorization {
    
    
    /// <summary>
    /// Administation
    /// </summary>
    public partial class Administation : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 5 "..\..\Administation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridHome;
        
        #line default
        #line hidden
        
        
        #line 6 "..\..\Administation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListViewGroup;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Administation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnAddGroup;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Administation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnDeleteGroup;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\Administation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnAddUserToGroup;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\Administation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtGroupName;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\Administation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListViewUsers;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\Administation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListViewAction;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\Administation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnSaveEditAction;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\Administation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnExit;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\Administation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnDeleteUser;
        
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
            System.Uri resourceLocater = new System.Uri("/Authorization;component/administation.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Administation.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            
            #line 4 "..\..\Administation.xaml"
            ((Authorization.Administation)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.GridHome = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.ListViewGroup = ((System.Windows.Controls.ListView)(target));
            
            #line 7 "..\..\Administation.xaml"
            this.ListViewGroup.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListViewGroup_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BtnAddGroup = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\Administation.xaml"
            this.BtnAddGroup.Click += new System.Windows.RoutedEventHandler(this.BtnAddGroup_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BtnDeleteGroup = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\Administation.xaml"
            this.BtnDeleteGroup.Click += new System.Windows.RoutedEventHandler(this.BtnDeleteGroup_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BtnAddUserToGroup = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\Administation.xaml"
            this.BtnAddUserToGroup.Click += new System.Windows.RoutedEventHandler(this.BtnAddUserToGroup_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.TxtGroupName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.ListViewUsers = ((System.Windows.Controls.ListView)(target));
            return;
            case 9:
            this.ListViewAction = ((System.Windows.Controls.ListView)(target));
            return;
            case 10:
            this.BtnSaveEditAction = ((System.Windows.Controls.Button)(target));
            
            #line 98 "..\..\Administation.xaml"
            this.BtnSaveEditAction.Click += new System.Windows.RoutedEventHandler(this.BtnSaveEditAction_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.BtnExit = ((System.Windows.Controls.Button)(target));
            
            #line 99 "..\..\Administation.xaml"
            this.BtnExit.Click += new System.Windows.RoutedEventHandler(this.BtnExit_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 100 "..\..\Administation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.BtnDeleteUser = ((System.Windows.Controls.Button)(target));
            
            #line 102 "..\..\Administation.xaml"
            this.BtnDeleteUser.Click += new System.Windows.RoutedEventHandler(this.BtnDeleteUser_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

