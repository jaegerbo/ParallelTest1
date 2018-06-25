Module LongRunner


   Public Sub Test1(DauerInSekunden As Integer)
      Try
         Threading.Thread.Sleep(DauerInSekunden * 1000)
         Application.DoEvents()
      Catch ex As Exception
         Stop
      End Try
   End Sub
   Public Sub Test2()
      Try
         Threading.Thread.Sleep(4 * 1000)
         Application.DoEvents()
      Catch ex As Exception
         Stop
      End Try
   End Sub
   Public Sub Test3()
      Try
         Dim i As Integer
         For i = 0 To 4000000

         Next

      Catch ex As Exception
         Stop
      End Try
   End Sub
   Public Function Test4(Anzahl As Integer, cancelationtoken As Threading.CancellationToken) As Integer
      Dim i As Integer
      Try
         For i = 0 To Anzahl - 1

            ' prüfen, ob abgebrochen werden soll
            If cancelationtoken.IsCancellationRequested Then
               Exit For
            End If

            Application.DoEvents()
         Next
      Catch ex As Exception
         Stop
      End Try
      Return i
   End Function
   Public Function Test5(Anzahl As Integer, control As Control, sc As Threading.SynchronizationContext, cancelationtoken As Threading.CancellationToken) As Integer
      Dim i As Integer
      Try
         Dim timeNow As DateTime = DateTime.Now

         For i = 0 To Anzahl - 1

            ' prüfen, ob abgebrochen werden soll
            If cancelationtoken.IsCancellationRequested Then
               Exit For
            End If

            ' alle 50 Millisekunden den bildschirm aktualisieren
            If (DateTime.Now - timeNow).Milliseconds >= 50 Then
               sc.Post(New Threading.SendOrPostCallback(Sub()
                                                           control.Text = i.ToString
                                                        End Sub), Nothing)
               timeNow = DateTime.Now
            End If

            Application.DoEvents()
         Next
      Catch ex As Exception
         Stop
      End Try
      Return i
   End Function

End Module
