﻿#pragma checksum "C:\Users\Milos\source\repos\Teretana\MembershipTypesView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "89E569C35C0A58CEB674407D96766B15"
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
    partial class MembershipTypesView : 
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
            case 2: // MembershipTypesView.xaml line 39
                {
                    this.membershipTypesList = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 4: // MembershipTypesView.xaml line 34
                {
                    this.addMembershipTypeButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.addMembershipTypeButton).Click += this.addMembershipTypeButton_Click;
                }
                break;
            case 5: // MembershipTypesView.xaml line 35
                {
                    this.editMembershipTypeButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.editMembershipTypeButton).Click += this.editMembershipTypeButton_Click;
                }
                break;
            case 6: // MembershipTypesView.xaml line 36
                {
                    this.activateMembershipTypeButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.activateMembershipTypeButton).Click += this.activateMembershipTypeButton_Click;
                }
                break;
            case 7: // MembershipTypesView.xaml line 37
                {
                    this.deactivateMembershipTypeButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.deactivateMembershipTypeButton).Click += this.deactivateMembershipTypeButton_Click;
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

