﻿#pragma checksum "C:\Users\Milos\source\repos\Teretana\AddProductMenu.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8E1C36ADDEB1B9FA63CD086FDBE85033"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Teretana
{
    partial class AddProductMenu : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // AddProductMenu.xaml line 43
                {
                    this.addImageButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.addImageButton).Click += this.addImageButton_Click;
                }
                break;
            case 3: // AddProductMenu.xaml line 44
                {
                    this.cancelButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.cancelButton).Click += this.cancelButton_Click;
                }
                break;
            case 4: // AddProductMenu.xaml line 45
                {
                    this.okButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.okButton).Click += this.okButton_Click;
                }
                break;
            case 5: // AddProductMenu.xaml line 41
                {
                    this.productImage = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 6: // AddProductMenu.xaml line 27
                {
                    this.backButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.backButton).Click += this.backButton_Click;
                }
                break;
            case 7: // AddProductMenu.xaml line 30
                {
                    this.nameBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8: // AddProductMenu.xaml line 31
                {
                    this.nameBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 9: // AddProductMenu.xaml line 32
                {
                    this.priceBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 10: // AddProductMenu.xaml line 33
                {
                    this.priceBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 11: // AddProductMenu.xaml line 34
                {
                    this.typeBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 12: // AddProductMenu.xaml line 36
                {
                    this.equipmentButton = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                }
                break;
            case 13: // AddProductMenu.xaml line 37
                {
                    this.supplementButton = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

