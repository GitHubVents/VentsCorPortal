﻿#pragma checksum "..\..\SmokeExtractionMainUC.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7845F412045DAC01CB9D14E7359E182D"
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


namespace SmokeExtraction {
    
    
    /// <summary>
    /// SmokeExtractionMainUc
    /// </summary>
    public partial class SmokeExtractionMainUc : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 46 "..\..\SmokeExtractionMainUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton Rb1;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\SmokeExtractionMainUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton Rb2;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\SmokeExtractionMainUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton Rb3;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\SmokeExtractionMainUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton Rb4;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\SmokeExtractionMainUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DataGridSmoke;
        
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
            System.Uri resourceLocater = new System.Uri("/SmokeExtraction;component/smokeextractionmainuc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SmokeExtractionMainUC.xaml"
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
            this.Rb1 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 46 "..\..\SmokeExtractionMainUC.xaml"
            this.Rb1.Click += new System.Windows.RoutedEventHandler(this.Rb1_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Rb2 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 47 "..\..\SmokeExtractionMainUC.xaml"
            this.Rb2.Click += new System.Windows.RoutedEventHandler(this.Rb2_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Rb3 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 48 "..\..\SmokeExtractionMainUC.xaml"
            this.Rb3.Click += new System.Windows.RoutedEventHandler(this.Rb3_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Rb4 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 49 "..\..\SmokeExtractionMainUC.xaml"
            this.Rb4.Click += new System.Windows.RoutedEventHandler(this.Rb4_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.DataGridSmoke = ((System.Windows.Controls.DataGrid)(target));
            
            #line 61 "..\..\SmokeExtractionMainUC.xaml"
            this.DataGridSmoke.Loaded += new System.Windows.RoutedEventHandler(this.DataGrid_Loaded);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

