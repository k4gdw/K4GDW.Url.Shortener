Imports Caliburn.Micro
Imports System.ComponentModel.Composition
Imports K4GDW.Infrastructure.Logging
Imports K4GDW.Url.Shortener.Messages


Namespace ViewModels

	<Export(GetType(PreferencesViewModel))>
	Public Class PreferencesViewModel
		Inherits PropertyChangedBase
		Implements IViewAware

		Private ReadOnly _logger As ILogger
		Private ReadOnly _config As AppConfiguration
		Private _events As IEventAggregator

		<ImportingConstructor()>
		Public Sub New(logger As ILogger, config As AppConfiguration, events As IEventAggregator)
			_logger = logger
			_config = config
			BitLyApiKey = _config.BitLyKey
			BitLyApiLogin = _config.BitLyLogin
			_events = events
			_events.Subscribe(Me)
		End Sub

		Private _BitLyApiKey As String

		''' <summary>
		''' Gets or sets the BitLyApiKey property and raises the PropertyChanged event.
		''' </summary>
		''' <remarks>
		''' <para>
		''' This template works with the Caliburn.Micro MVVM framework.
		''' </para>
		''' </remarks>
		Public Property BitLyApiKey As String
			Get
				Return _BitLyApiKey
			End Get
			Set(ByVal value As String)
				If Not _BitLyApiKey = value Then
					_BitLyApiKey = value
					NotifyOfPropertyChange(Function() BitLyApiKey)
					NotifyOfPropertyChange(Function() CanSave)
				End If
			End Set
		End Property

		Private _BitLyApiLogin As String

		''' <summary>
		''' Gets or sets the BitLyApiLogin property and raises the PropertyChanged event.
		''' </summary>
		''' <remarks>
		''' <para>
		''' This template works with the Caliburn.Micro MVVM framework.
		''' </para>
		''' </remarks>
		Public Property BitLyApiLogin As String
			Get
				Return _BitLyApiLogin
			End Get
			Set(ByVal value As String)
				If Not _BitLyApiLogin = value Then
					_BitLyApiLogin = value
					NotifyOfPropertyChange(Function() BitLyApiLogin)
					NotifyOfPropertyChange(Function() CanSave)
				End If
			End Set
		End Property

		''' <summary>
		''' Gets a value indicating whether this instance can save bit ly credentials.
		''' </summary>
		''' <value><c>true</c> if this instance can save bit ly credentials; otherwise, <c>false</c>.</value>
		''' <remarks></remarks>
		Public ReadOnly Property CanSave As Boolean
			Get
				Return BitLyCredsNotEmpty()
			End Get
		End Property

		''' <summary>
		''' Bits the ly creds not empty.
		''' </summary>
		''' <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
		Private Function BitLyCredsNotEmpty() As Boolean
			If String.IsNullOrEmpty(BitLyApiLogin) OrElse String.IsNullOrEmpty(BitLyApiKey) Then
				Return False
			End If
			Return True
		End Function

		Public Sub Save()
			_config.BitLyKey = BitLyApiKey
			_config.BitLyLogin = BitLyApiLogin
			_config.Write()
			_events.Publish(New PreferencesSavedEvent)
			dialogWindow.Close()
		End Sub

		Public Sub Cancel()
			dialogWindow.Close()
		End Sub

		Private dialogWindow As Window

		Public Sub AttachView(view As Object, Optional context As Object = Nothing) Implements IViewAware.AttachView
			dialogWindow = CType(view, Window)

			RaiseEvent ViewAttached(Me,
									New ViewAttachedEventArgs() With {.Context = context, .View = view})

		End Sub

		Public Function GetView(Optional context As Object = Nothing) As Object Implements IViewAware.GetView
			Return dialogWindow
		End Function

		Public Event ViewAttached(sender As Object, e As ViewAttachedEventArgs) Implements IViewAware.ViewAttached
	End Class
End Namespace
