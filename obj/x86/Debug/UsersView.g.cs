﻿#pragma checksum "C:\Users\Milos\source\repos\Teretana\UsersView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C19AE9F499BDDB49E7DE3DBD133C023B"
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
    partial class UsersView : 
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
            case 2: // UsersView.xaml line 39
                {
                    this.usersList = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 4: // UsersView.xaml line 34
                {
                    this.addUserButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.addUserButton).Click += this.addUserButton_Click;
                }
                break;
            case 5: // UsersView.xaml line 35
                {
                    this.editUserButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.editUserButton).Click += this.editUserButton_Click;
                }
                break;
            case 6: // UsersView.xaml line 36
                {
                    this.activateUserButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.activateUserButton).Click += this.activateUserButton_Click;
                }
                break;
            case 7: // UsersView.xaml line 37
                {
                    this.deactivateUserButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.deactivateUserButton).Click += this.deactivateUserButton_Click;
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

