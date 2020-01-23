Imports System.Threading
Imports System.Threading.Tasks

Public Class clsAsync

   Public Delegate Function Test3Delegate(i As Integer) As Integer
   Public Delegate Function Test4Delegate(i As Integer, cancelationtoken As CancellationToken, Progress As IProgress(Of Integer)) As Integer

   Public Shared Async Sub Run(mainControl As Control, Aktion As Action, Optional waittext As String = Nothing)
      ' Zweck:    Das gegebene mainControl mit der gegebenen Aktion füllen. Bei Bedarf kann eine Wartemeldung angezeigt werden.
      Try
         If mainControl IsNot Nothing Then
            Dim sc As SynchronizationContext = SynchronizationContext.Current

            ' waitControl einrichten
            Dim waitControl As Label = waitControlEinrichten(mainControl, waittext)

            ' Task starten
            Await Task.Run(Aktion).ConfigureAwait(False)
            sc.Post(New SendOrPostCallback(Sub()
                                              fertig(mainControl, waitControl)
                                           End Sub), Nothing)
         End If
      Catch ex As Exception
         Stop
      End Try
   End Sub
   'Public Shared Async Function RunWithCancelationAsync(Routine As Func(Of Integer, CancellationToken, IProgress(Of Integer)),
   '                                                     Optional mainControl As Control = Nothing,
   '                                                     Optional waittext As String = Nothing) As Task(Of Integer)
   '   ' Zweck:    Das gegebene mainControl mit der gegebenen Aktion füllen. Bei Bedarf kann eine Wartemeldung angezeigt werden.
   '   '           Die Aktion kann abgebrochen werden und gibt einen Rückgabewert zurück.
   '   Dim i As Integer = 0
   '   Try
   '      ' waitControl einrichten
   '      Dim waitControl As Label = waitControlEinrichten(mainControl, waittext)

   '      ' Task starten
   '      Dim T As Task(Of Integer)
   '      T = Await Task.Run(Of Integer)(Routine)
   '      'i = Await Task.FromResult(Of Object)(Aktion.Invoke(Anzahl, cancelationtoken, Nothing))

   '      fertig(mainControl, waitControl)

   '   Catch ex As Exception
   '      Stop
   '   End Try
   '   Return i
   'End Function
   Public Shared Async Sub RunWithCancelationAndReturnAsync(mainControl As Control, Anzahl As Integer, cancelationtoken As CancellationToken,
                                                            Optional waittext As String = Nothing, Optional Progress As IProgress(Of Integer) = Nothing)
      ' Zweck:    Das gegebene mainControl mit der gegebenen Aktion füllen. Bei Bedarf kann eine Wartemeldung angezeigt werden.
      '           Die Aktion kann abgebrochen werden und gibt einen Rückgabewert zurück.
      Try
         Dim sc As SynchronizationContext = SynchronizationContext.Current

         ' waitControl einrichten
         Dim waitControl As Label = Nothing
         If waittext IsNot Nothing Then
            waitControl = waitControlEinrichten(mainControl, waittext)
         End If

         ' Task starten
         Dim i As Integer = Await Task.Run(Function()
                                              Return Test4(Anzahl, cancelationtoken, Progress)
                                           End Function).ConfigureAwait(False)

         ' waitControl wieder entfernen, und das mainControl mit den neuen Werten versehen
         sc.Post(New SendOrPostCallback(Sub()
                                           fertig(mainControl, waitControl)
                                           mainControl.Text = i.ToString
                                        End Sub), Nothing)

      Catch ex As Exception
         Stop
      End Try
   End Sub
   Public Shared Async Function RunTest5(mainControl As Control, Anzahl As Integer, cancelationtoken As CancellationToken, Optional waittext As String = Nothing) As Task
      Try
         Dim sc As SynchronizationContext = SynchronizationContext.Current

         ' waitControl einrichten
         Dim waitControl As Label = waitControlEinrichten(mainControl, waittext)

         ' Task starten
         Dim i As Integer = Await Task.Run(Function()
                                              Return Test5(Anzahl, waitControl, sc, cancelationtoken)
                                           End Function)
         sc.Post(New SendOrPostCallback(Sub()
                                           fertig(mainControl, waitControl)
                                           mainControl.Text = i.ToString
                                        End Sub), Nothing)

      Catch ex As Exception
         Stop
      End Try
   End Function

   Public Shared Async Function RunFunctionAsync(Routine As Func(Of Integer, CancellationToken, IProgress(Of Integer), Integer),
                                                 Anzahl As Integer,
                                                 cancelationtoken As Threading.CancellationToken,
                                                 Progress As IProgress(Of Integer),
                                                 mainControl As Control) As Task(Of Integer)
      Try
         'Dim sc As SynchronizationContext = SynchronizationContext.Current

         ' waitControl einrichten
         'Dim waitControl As Label = waitControlEinrichten(mainControl, "wird geladen")

         ' Task starten
         Dim T As Task(Of Integer) = Task.Run(Function()
                                                 Return Routine(Anzahl, cancelationtoken, Progress)
                                              End Function)
         Dim i As Integer = Await T



         'sc.Post(New SendOrPostCallback(Sub()
         '                                  fertig(mainControl, waitControl)
         '                                  mainControl.Text = i.ToString
         '                               End Sub), Nothing)

         Return i
      Catch ex As Exception
         Stop
      End Try
   End Function
   Public Shared Async Function RunFunctionAsync2(Routine As Func(Of Task(Of Integer)),
                                                  mainControl As Control) As Task(Of Integer)
      Try
         Dim sc As SynchronizationContext = SynchronizationContext.Current

         ' waitControl einrichten
         Dim waitControl As Label = waitControlEinrichten(mainControl, "wird geladen")

         ' Task starten
         Dim T As Task(Of Integer) = Task.Run(Routine)
         Dim i As Integer = Await T



         sc.Post(New SendOrPostCallback(Sub()
                                           fertig(mainControl, waitControl)
                                        End Sub), Nothing)

         Return i
      Catch ex As Exception
         Stop
      End Try
   End Function
   Public Shared Async Sub RunFunctionAsync3(Routine As Func(Of Task(Of Integer)),
                                                  mainControl As Control)
      Try
         Dim sc As SynchronizationContext = SynchronizationContext.Current

         ' waitControl einrichten
         Dim waitControl As Label = waitControlEinrichten(mainControl, "wird geladen")

         ' Task starten
         Dim T As Task(Of Integer) = Task.Run(Routine)
         Dim i As Integer = Await T



         sc.Post(New SendOrPostCallback(Sub()
                                           fertig(mainControl, waitControl)
                                           mainControl.Text = i.ToString
                                        End Sub), Nothing)

      Catch ex As Exception
         Stop
      End Try
   End Sub

   Private Shared Function waitControlEinrichten(mainControl As Control, waittext As String) As Control
      ' Zweck:    Wenn ein waitControl angezeigt werden soll, wird es hier erzeugt
      Dim waitControl As Label = Nothing
      Try
         If waittext IsNot Nothing Then

            mainControl.Invoke(Sub()
                                  mainControl.Visible = False
                               End Sub)
            waitControl = New Label
            With waitControl
               .Top = mainControl.Top
               .Left = mainControl.Left
               .Width = mainControl.Width
               .Height = mainControl.Height
               .TextAlign = ContentAlignment.MiddleCenter
               .Font = New Font("Arial", CSng(.Width / 20), FontStyle.Bold)

               ' Ladetext anzeigen, wenn das waitControl groß genug dafür ist
               If .Font.SizeInPoints >= 6 Then
                  .Text = waittext
               End If

            End With
            mainControl.Invoke(Sub()
                                  mainControl.Parent.Controls.Add(waitControl)
                               End Sub)
            Application.DoEvents()
         End If
      Catch ex As Exception
         Stop
      End Try
      Return waitControl
   End Function
   Private Shared Sub fertig(mainControl As Control, waitControl As Control)
      ' Zweck:    Wenn ein waitControl vorhanden war, wird es wieder entfernt
      Try
         mainControl.Invoke(Sub()
                               mainControl.Visible = True
                            End Sub)
         If waitControl IsNot Nothing Then
            mainControl.Invoke(Sub()
                                  mainControl.Parent.Controls.Remove(waitControl)
                                  waitControl.Dispose()
                               End Sub)
         End If
         Application.DoEvents()
      Catch ex As Exception
         Stop
      End Try
   End Sub

End Class
