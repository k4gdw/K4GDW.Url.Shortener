Namespace Messages

	''' <summary>
	''' This class just provides an event for the preferences view model to
	''' stuff into the event aggregator when it has saved the preferences.  It
	''' doesn't need any properties or anything.
	''' </summary>
	Public Class PreferencesSavedEvent

		''' <summary>
		''' Initializes a new instance of the <see cref="PreferencesSavedEvent" /> class.
		''' </summary>
		Public Sub New()
			EventName = "Preferences Saved"
		End Sub

		Private _EventName As String

		Public Property EventName As String
			Get
				Return _EventName
			End Get
			Private Set(ByVal value As String)
				_EventName = value
			End Set
		End Property

	End Class

End Namespace
