Imports Caliburn.Micro
Imports System.ComponentModel.Composition

Namespace ViewModels

	Public Class AboutViewModel
		Inherits PropertyChangedBase
		Implements IViewAware

		Private _dialogWindow As Window
		Private _events As IEventAggregator

		<ImportingConstructor()>
		Public Sub New(events As IEventAggregator)
			_events = events
			_events.Subscribe(Me)
			AboutText = My.Resources.AboutText
		End Sub

		Private _AboutText As String

		''' <summary>
		''' Gets or sets the AboutText property and raises the PropertyChanged event.
		''' </summary>
		''' <remarks>
		''' <para>
		''' This template works with the Caliburn.Micro MVVM framework.
		''' </para>
		''' </remarks>
		Public Property AboutText As String
			Get
				Return _AboutText
			End Get
			Private Set(ByVal value As String)
				If Not _AboutText = value Then
					_AboutText = value
					NotifyOfPropertyChange(Function() AboutText)
				End If
			End Set
		End Property

		Public Sub OkButton()
			_dialogWindow.Close()
		End Sub

		Public Sub AttachView(view As Object, Optional context As Object = Nothing) Implements IViewAware.AttachView
			_dialogWindow = CType(view, Window)

			RaiseEvent ViewAttached(Me,
									New ViewAttachedEventArgs() With {.Context = context, .View = view})

		End Sub

		Public Function GetView(Optional context As Object = Nothing) As Object Implements IViewAware.GetView
			Return _dialogWindow
		End Function

		Public Event ViewAttached(sender As Object, e As ViewAttachedEventArgs) Implements IViewAware.ViewAttached

	End Class

End Namespace
