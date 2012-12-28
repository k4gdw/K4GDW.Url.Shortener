Imports System.Runtime.CompilerServices
Imports System.Text
Imports NLog


Module Helpers

    ''' <summary>
    ''' Gets the formatted exception.
    ''' </summary>
    ''' <param name="ex">The <see cref="System.Exception" /> to be formated.</param>
    ''' <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
    ''' <returns>A formated representation of the exception including any inner exceptions.</returns>
    ''' <remarks>
    ''' <para>
    ''' This function recursively iterates through an exception and all of its innerexceptions to
    ''' get a formated version of all of it's messages.
    ''' </para>
    ''' <para>
    ''' Created By:  Bryan Johns<br />
    ''' On:  3/11/2011 at 11:10 AM
    ''' </para>
    ''' </remarks>
    <Extension()>
    Public Function GetFormattedException(ByRef ex As Exception,
            Optional ByVal isRecursive As Boolean = False, Optional ShowData As Boolean = True) As String
        Dim sb As New StringBuilder

        If isRecursive Then
            sb.AppendFormat("*********************** Inner Exception ************************{0}", Environment.NewLine)
        Else
            sb.AppendFormat("************************* Exception ***************************{0}", Environment.NewLine)
        End If

        sb.AppendFormat("Exception Message:{0}{1}{2}", Environment.NewLine, ex.Message, Environment.NewLine)
        sb.AppendFormat("Stack Trace:{0}{1}{2}", Environment.NewLine, ex.StackTrace, Environment.NewLine)
        sb.AppendFormat("Source:{0}{1}{2}", Environment.NewLine, ex.Source, Environment.NewLine)

        ' recurse into inner exceptions
        If ex.InnerException IsNot Nothing Then
			sb.AppendFormat("{0}{1}",
							ex.InnerException.GetFormattedException(True), Environment.NewLine)
        End If

        If ShowData AndAlso ex.Data.Count > 0 Then
            Dim x As Integer
            sb.AppendLine("Exception.Data {")
            For Each i As DictionaryEntry In ex.Data
                x += 1
                sb.AppendFormat("{0}{1}:{2}{3}",
                                          vbTab,
                                          i.Key.ToString(),
                                          IIf(IsNumeric(i.Value),
                                              i.Value,
                                              String.Format("""{0}""",
                                                            i.Value)),
                                          IIf((x < ex.Data.Count),
                                              ",",
                                              ""))
            Next
            sb.Append("}")
        End If

        Dim msg As String = sb.ToString
        Return msg
    End Function

    <Extension()>
    Public Function PluralOrSingular(ByRef count As Integer, singular As String, plural As String) As String
        Return If(count = 1, singular, plural)
    End Function

    ''' <summary>
    ''' Logs the exception.
    ''' </summary>
    ''' <param name="ex">The <see cref="Exception"/> to be logged.</param>
    <Extension()>
    Public Sub LogException(ByRef ex As Exception)
        Dim logger As Logger = LogManager.GetCurrentClassLogger
		logger.ErrorException(ex.GetFormattedException(False), ex)
		' ReSharper disable SuspiciousTypeConversion.Global
		Dim exception = TryCast(ex, ILoggable)
		' ReSharper restore SuspiciousTypeConversion.Global
		If (exception IsNot Nothing) Then
			logger.Info(exception.Info)
		End If
    End Sub

    <Extension()>
    Public Sub LogItem(ByRef item As ILoggable)
        Dim logger As Logger = LogManager.GetCurrentClassLogger()
        logger.Info(item.Info())
    End Sub
    
    ''' <summary>
    ''' To require that an object provides a params property.
    ''' </summary>
    ''' <remarks>
    ''' <para>
    ''' Created:  11/18/2011 at 6:39 PM.<br />
    ''' By: Bryan Johns, K4GDW<br />
    ''' Email:  bjohns@greendragonweb.com
    ''' </para>
    ''' </remarks>
	Public Interface ILoggable

		Function Info() As String

	End Interface

End Module
