﻿#pragma checksum "C:\Users\Milos\source\repos\Teretana\TrainerMenu.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "454F78F08029CD8CE93A783ADFB032CB"
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
    partial class TrainerMenu : 
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
            case 2: // TrainerMenu.xaml line 21
                {
                    this.PivotTab = (global::Windows.UI.Xaml.Controls.Pivot)(target);
                    ((global::Windows.UI.Xaml.Controls.Pivot)this.PivotTab).SelectionChanged += this.trainerPivotSelection;
                }
                break;
            case 3: // TrainerMenu.xaml line 56
                {
                    this.currentFrame = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            case 4: // TrainerMenu.xaml line 51
                {
                    this.Logout = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)this.Logout).Click += this.Logout_Click;
                }
                break;
            case 5: // TrainerMenu.xaml line 48
                {
                    this.English = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)this.English).Click += this.englishButtonClick;
                }
                break;
            case 6: // TrainerMenu.xaml line 49
                {
                    this.Serbian = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)this.Serbian).Click += this.serbianButtonClick;
                }
                break;
            case 7: // TrainerMenu.xaml line 43
                {
                    this.DarkThemeOption = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)this.DarkThemeOption).Click += this.DarkThemeOption_Click;
                }
                break;
            case 8: // TrainerMenu.xaml line 44
                {
                    this.LightThemeOption = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)this.LightThemeOption).Click += this.LightThemeOption_Click;
                }
                break;
            case 9: // TrainerMenu.xaml line 45
                {
                    this.BlueThemeOption = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)this.BlueThemeOption).Click += this.BlueThemeOption_Click;
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
