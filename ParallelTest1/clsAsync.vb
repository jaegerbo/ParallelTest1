Imports System.Threading

Public Class clsAsync

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
   Public Shared Async Sub RunWithCancelation(mainControl As Control, Anzahl As Integer, cancelationtoken As CancellationToken,
                                              Optional waitText As String = Nothing)
      ' Zweck:    Das gegebene mainControl mit der gegebenen Aktion füllen. Bei Bedarf kann eine Wartemeldung angezeigt werden.
      '           Die Aktion kann abgebrochen werden und gibt einen Rückgabewert zurück.
      Try
         Dim sc As SynchronizationContext = SynchronizationContext.Current

         ' waitControl einrichten
         Dim waitControl As Label = waitControlEinrichten(mainControl, waitText)

         ' Task starten
         Await Task.Run(Sub() Test4(Anzahl, cancelationtoken)).ConfigureAwait(False)
         sc.Post(New SendOrPostCallback(Sub()
                                           fertig(mainControl, waitControl)
                                        End Sub), Nothing)

      Catch ex As Exception
         Stop
      End Try
   End Sub
   Public Shared Async Sub RunWithCancelationAndReturn(mainControl As Control, Anzahl As Integer, cancelationtoken As CancellationToken,
                                                            Optional waittext As String = Nothing)
      ' Zweck:    Das gegebene mainControl mit der gegebenen Aktion füllen. Bei Bedarf kann eine Wartemeldung angezeigt werden.
      '           Die Aktion kann abgebrochen werden und gibt einen Rückgabewert zurück.
      Try
         Dim sc As SynchronizationContext = SynchronizationContext.Current

         ' waitControl einrichten
         Dim waitControl As Label = waitControlEinrichten(mainControl, waittext)

         ' Task starten
         Dim i As Integer = Await Task.Run(Function() Test4(Anzahl, cancelationtoken)).ConfigureAwait(False)
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
         Dim i As Integer = Await Task.Run(Function() Test5(Anzahl, waitControl, sc, cancelationtoken))
         sc.Post(New SendOrPostCallback(Sub()
                                           fertig(mainControl, waitControl)
                                           mainControl.Text = i.ToString
                                        End Sub), Nothing)

      Catch ex As Exception
         Stop
      End Try
   End Function

   Private Shared Function waitControlEinrichten(mainControl As Control, waittext As String) As Control
      ' Zweck:    Wenn ein waitControl angezeigt werden soll, wird es hier erzeugt
      Dim waitControl As Label = Nothing
      Try
         If waittext IsNot Nothing Then

            mainControl.Visible = False
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
            mainControl.Parent.Controls.Add(waitControl)
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
         mainControl.Visible = True
         If waitControl IsNot Nothing Then
            mainControl.Parent.Controls.Remove(waitControl)
            waitControl.Dispose()
         End If
         Application.DoEvents()
      Catch ex As Exception
         Stop
      End Try
   End Sub

End Class
