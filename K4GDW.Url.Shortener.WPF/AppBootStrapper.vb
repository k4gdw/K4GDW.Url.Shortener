Imports System.ComponentModel.Composition
Imports System.ComponentModel.Composition.Hosting
Imports System.IO
Imports Westwind.Utilities.Configuration
Imports K4GDW.Url.Shortener.ViewModels
Imports Caliburn.Micro
Imports System.ComponentModel.Composition.Primitives
Imports K4GDW.Infrastructure.Logging

''' <summary>
''' Class AppBootStrapper
''' </summary>
''' <remarks></remarks>
Public Class AppBootStrapper
	Inherits Bootstrapper(Of MainViewModel)

	Private container As CompositionContainer

	Protected Overrides Sub Configure()
		container = New CompositionContainer(
						New AggregateCatalog(
							AssemblySource.Instance.Select(Function(x) New AssemblyCatalog(x)) _
							.OfType(Of ComposablePartCatalog)()))
		Dim batch As New CompositionBatch
		batch.AddExportedValue(Of IWindowManager)(New WindowManager)
		batch.AddExportedValue(Of IEventAggregator)(New EventAggregator)
		batch.AddExportedValue(Of ILogger)(New NLogger)
		batch.AddExportedValue(Of AppConfiguration)(Config())
		container.Compose(batch)
	End Sub

	Private Function Config() As AppConfiguration
		Dim configFolder As String = String.Format("{0}/K4GDW Software/Url.Shortener", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData))
		If Not Directory.Exists(configFolder) Then
			Directory.CreateDirectory(configFolder)
		End If
		Dim configProvider As New ConfigurationFileConfigurationProvider(Of AppConfiguration) With {
				.ConfigurationSection = "Settings",
				.ConfigurationFile = String.Format("{0}\UrlShortenerConfig.xml", configFolder),
				.PropertiesToEncrypt = "BitLyKey,BitLyLogin",
				.EncryptionKey = "8cA_5yT&"}
		Dim cnf As New AppConfiguration
		cnf.Initialize(configProvider)
		Return cnf
	End Function

	Protected Overrides Function GetInstance(serviceType As Type,
											 key As String) As Object
		Dim contract As String = If(String.IsNullOrEmpty(key),
									AttributedModelServices.GetContractName(serviceType),
									key)
		Dim exports As IEnumerable(Of Object) = container.GetExportedValues(Of Object)(contract)

		If exports.Count > 0 Then
			Return exports.First
		End If
		Throw New Exception(String.Format("Could not located any instances of contract {0}.",
										  contract))
	End Function
End Class