﻿#pragma checksum "..\..\OrderDetails.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "595596547DDA1354CC941A1E789AEC58"
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


namespace AirVentsOrderManager {
    
    
    /// <summary>
    /// OrderDetails
    /// </summary>
    public partial class OrderDetails : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\OrderDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox РежимРедактированияЗаказа;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\OrderDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ВнутреннийНомерЗаказа;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\OrderDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox НомерЗаказа;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\OrderDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Конструктор;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\OrderDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker ДатаПоступленияЗаказа;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\OrderDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker ДатаОтгрузкиЗаказа;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\OrderDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker ПланируемаяСдачаКд;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\OrderDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker ФактическаяСдачаКд;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\OrderDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Ok;
        
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
            System.Uri resourceLocater = new System.Uri("/AirVentsOrderManager;component/orderdetails.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\OrderDetails.xaml"
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
            this.РежимРедактированияЗаказа = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 2:
            this.ВнутреннийНомерЗаказа = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.НомерЗаказа = ((System.Windows.Controls.TextBox)(target));
            
            #line 27 "..\..\OrderDetails.xaml"
            this.НомерЗаказа.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.НомерЗаказа_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Конструктор = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.ДатаПоступленияЗаказа = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 6:
            this.ДатаОтгрузкиЗаказа = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 7:
            this.ПланируемаяСдачаКд = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 8:
            this.ФактическаяСдачаКд = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 9:
            this.Ok = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\OrderDetails.xaml"
            this.Ok.Click += new System.Windows.RoutedEventHandler(this.Ok_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

