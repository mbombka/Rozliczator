﻿#ExternalChecksum("..\..\..\View\DelegacjeView.xaml","{ff1816ec-aa5e-4d10-87f7-6f4963833460}","CD204780A068D9A3C3BEA532F9B976FF6C266D87")
'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports Rozliczator
Imports System
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Automation
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Effects
Imports System.Windows.Media.Imaging
Imports System.Windows.Media.Media3D
Imports System.Windows.Media.TextFormatting
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Shell


'''<summary>
'''DelegacjeView
'''</summary>
<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
Partial Public Class DelegacjeView
    Inherits System.Windows.Controls.UserControl
    Implements System.Windows.Markup.IComponentConnector
    
    
    #ExternalSource("..\..\..\View\DelegacjeView.xaml",27)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents DpFltrujOd As System.Windows.Controls.DatePicker
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\View\DelegacjeView.xaml",31)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents DpFltrujDo As System.Windows.Controls.DatePicker
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\View\DelegacjeView.xaml",34)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents DelegowanyComboBox As System.Windows.Controls.ComboBox
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\View\DelegacjeView.xaml",37)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents ZaplaconoComboBox As System.Windows.Controls.ComboBox
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\View\DelegacjeView.xaml",46)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents DataGridDelegacje As System.Windows.Controls.DataGrid
    
    #End ExternalSource
    
    Private _contentLoaded As Boolean
    
    '''<summary>
    '''InitializeComponent
    '''</summary>
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")>  _
    Public Sub InitializeComponent() Implements System.Windows.Markup.IComponentConnector.InitializeComponent
        If _contentLoaded Then
            Return
        End If
        _contentLoaded = true
        Dim resourceLocater As System.Uri = New System.Uri("/Rozliczator;component/view/delegacjeview.xaml", System.UriKind.Relative)
        
        #ExternalSource("..\..\..\View\DelegacjeView.xaml",1)
        System.Windows.Application.LoadComponent(Me, resourceLocater)
        
        #End ExternalSource
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes"),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")>  _
    Sub System_Windows_Markup_IComponentConnector_Connect(ByVal connectionId As Integer, ByVal target As Object) Implements System.Windows.Markup.IComponentConnector.Connect
        If (connectionId = 1) Then
            
            #ExternalSource("..\..\..\View\DelegacjeView.xaml",21)
            AddHandler CType(target,System.Windows.Controls.Button).Click, New System.Windows.RoutedEventHandler(AddressOf Me.Button_Delegacje)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 2) Then
            
            #ExternalSource("..\..\..\View\DelegacjeView.xaml",22)
            AddHandler CType(target,System.Windows.Controls.Button).Click, New System.Windows.RoutedEventHandler(AddressOf Me.Button_Wczytaj)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 3) Then
            Me.DpFltrujOd = CType(target,System.Windows.Controls.DatePicker)
            Return
        End If
        If (connectionId = 4) Then
            Me.DpFltrujDo = CType(target,System.Windows.Controls.DatePicker)
            Return
        End If
        If (connectionId = 5) Then
            Me.DelegowanyComboBox = CType(target,System.Windows.Controls.ComboBox)
            Return
        End If
        If (connectionId = 6) Then
            Me.ZaplaconoComboBox = CType(target,System.Windows.Controls.ComboBox)
            Return
        End If
        If (connectionId = 7) Then
            
            #ExternalSource("..\..\..\View\DelegacjeView.xaml",40)
            AddHandler CType(target,System.Windows.Controls.Button).Click, New System.Windows.RoutedEventHandler(AddressOf Me.Button_Reset)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 8) Then
            Me.DataGridDelegacje = CType(target,System.Windows.Controls.DataGrid)
            
            #ExternalSource("..\..\..\View\DelegacjeView.xaml",52)
            AddHandler Me.DataGridDelegacje.MouseDoubleClick, New System.Windows.Input.MouseButtonEventHandler(AddressOf Me.DataGrid_MouseDoubleClick)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 9) Then
            
            #ExternalSource("..\..\..\View\DelegacjeView.xaml",69)
            AddHandler CType(target,System.Windows.Controls.MenuItem).Click, New System.Windows.RoutedEventHandler(AddressOf Me.DataGrid_RClick_Edytuj)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 10) Then
            
            #ExternalSource("..\..\..\View\DelegacjeView.xaml",70)
            AddHandler CType(target,System.Windows.Controls.MenuItem).Click, New System.Windows.RoutedEventHandler(AddressOf Me.DataGrid_RClick_Dodaj)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 11) Then
            
            #ExternalSource("..\..\..\View\DelegacjeView.xaml",72)
            AddHandler CType(target,System.Windows.Controls.MenuItem).Click, New System.Windows.RoutedEventHandler(AddressOf Me.DataGrid_RClick_Usun)
            
            #End ExternalSource
            Return
        End If
        Me._contentLoaded = true
    End Sub
End Class

