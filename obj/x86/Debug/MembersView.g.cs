﻿#pragma checksum "C:\Users\Milos\source\repos\Teretana\MembersView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "65CA4F977BB678B7883105C2BF56BF02"
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
    partial class MembersView : 
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
            case 2: // MembersView.xaml line 69
                {
                    this.membersList = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 4: // MembersView.xaml line 79
                {
                    global::Windows.UI.Xaml.Controls.Button element4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element4).Click += this.DetailsButton_Click;
                }
                break;
            case 5: // MembersView.xaml line 66
                {
                    this.searchBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.searchBox).TextChanged += this.searchBox_TextChanged;
                }
                break;
            case 6: // MembersView.xaml line 34
                {
                    this.addMemberButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.addMemberButton).Click += this.AddMember_Click;
                }
                break;
            case 7: // MembersView.xaml line 35
                {
                    this.editMemberButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.editMemberButton).Click += this.editMemberButton_Click;
                }
                break;
            case 8: // MembersView.xaml line 36
                {
                    this.prolongMembershipButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.prolongMembershipButton).Click += this.prolongMembershipButton_Click;
                }
                break;
            case 9: // MembersView.xaml line 37
                {
                    this.activateButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.activateButton).Click += this.activateButton_Click;
                }
                break;
            case 10: // MembersView.xaml line 38
                {
                    this.deactivateButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.deactivateButton).Click += this.deactivateButton_Click;
                }
                break;
            case 11: // MembersView.xaml line 39
                {
                    this.addSessionButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.addSessionButton).Click += this.addSessionButton_Click;
                }
                break;
            case 12: // MembersView.xaml line 40
                {
                    this.endSessionButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.endSessionButton).Click += this.endSessionButton_Click;
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

