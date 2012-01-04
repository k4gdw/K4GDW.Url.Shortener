Imports System.IO
Imports System.Net

''' <summary>
''' A class to handle comms with the TinyURL URL shortening service.
''' </summary>
''' <remarks>
''' <para>
''' Created By:  Bryan Johns<br />
''' On:  3/18/2010 at 3:21 PM
''' </para>
''' </remarks>
Public Class TinyUrl

	''' <summary>
	''' Gets the tiny URL.
	''' </summary>
	''' <param name="URL">The URL.</param>
	''' <returns></returns>
	''' <remarks>
	''' Created: 6/30/2009 at 12:12 PM
	''' By: bjohns.
	''' </remarks>
	Public Shared Function GetTinyUrl(ByVal URL As String) As String
		Try
			If Not URL.ToLower().StartsWith("http") AndAlso Not URL.ToLower().StartsWith("ftp") Then
				URL = "http://" + URL
			End If
			If URL.Length <= 30 Then
				Return URL
			End If
			Dim req As WebRequest = WebRequest.Create("http://tinyurl.com/api-create.php?url=" + URL)
			Dim rsp As WebResponse = req.GetResponse()
			Dim txt As String
			Using rdr As StreamReader = New StreamReader(rsp.GetResponseStream())
				txt = rdr.ReadToEnd()
			End Using
			Return txt
		Catch ex As Net.WebException
			Throw New ApplicationException("There was an error communicating with the TinyURL.com service.", ex)
		Catch ex As Exception
			Throw New ApplicationException("There was an error getting the tiny url.", ex)
		End Try
	End Function


End Class
