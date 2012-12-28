Imports System.IO
Imports System.Net
Imports System.Threading.Tasks

''' <summary>
''' A class to handle comms with the TinyURL longUrl shortening service.
''' </summary>
''' <remarks>
''' <para>
''' Created By:  Bryan Johns<br />
''' On:  3/18/2010 at 3:21 PM
''' </para>
''' </remarks>
Public Class TinyUrl

	''' <summary>
	''' Shortens the url asynchronously.
	''' </summary>
	''' <param name="longUrl">The long URL.</param>
	''' <returns>Task{System.String}.</returns>
	''' <exception cref="System.Net.WebException"></exception>
	''' <exception cref="System.ApplicationException"></exception>
	Public Shared Async Function ShortenUrlAsync(ByVal longUrl As String) As Task(Of String)
		Try
			If Not longUrl.ToLower().StartsWith("http") AndAlso Not longUrl.ToLower().StartsWith("ftp") Then
				longUrl = String.Format("http://{0}", longUrl)
			End If
			If longUrl.Length <= 30 Then
				Return longUrl
			End If
			Dim req As WebRequest = WebRequest.Create(String.Format("http://tinyurl.com/api-create.php?url={0}", longUrl))
			Using rsp As WebResponse = Await req.GetResponseAsync()
				Dim txt As String = longUrl
				Using rdr As StreamReader = New StreamReader(rsp.GetResponseStream())
					txt = rdr.ReadToEnd()
				End Using
				Return txt
			End Using
		Catch ex As WebException
			Throw New WebException("There was an error communicating with the TinyURL.com service.", ex)
		Catch ex As Exception
			Throw New ApplicationException("There was an error getting the tiny url from TinyUrl.com.", ex)
		End Try
	End Function
	
End Class
