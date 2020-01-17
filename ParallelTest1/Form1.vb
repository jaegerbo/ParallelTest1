Imports System.Threading

Public Class Form1

   Private _Task As Task = Nothing
   Private _previousTime As DateTime = DateTime.Now
   Private _sc As SynchronizationContext = SynchronizationContext.Current
   Private _tokenSource As New CancellationTokenSource
   Private _cancelationtoken As CancellationToken = Nothing

   Private Sub btnStart1_Click(sender As Object, e As EventArgs) Handles btnStart1.Click
      lblStatus.Text = Nothing
      Try
         lblOk.Text = Test4(5, cancelationtoken:=Nothing, Progress:=Nothing).ToString
         'clsAsync.Run(lblOk, Sub() Test1(4), "wird geladen")
      Catch ex As Exception
         Stop
      End Try
   End Sub
   Private Async Sub btnStartMitAwait_Click(sender As Object, e As EventArgs) Handles btnStartMitAwait.Click
      lblStatus.Text = Nothing
      Try
         lblOk.Text = Await Test4Async(5, cancelationtoken:=Nothing, Progress:=Nothing).ToString
      Catch ex As Exception
         Stop
      End Try
   End Sub

   Private Sub btnStart2MitReturn_Click(sender As Object, e As EventArgs) Handles btnStart2MitReturn.Click
      lblStatus.Text = Nothing
      Try
         _tokenSource = New CancellationTokenSource

         clsAsync.RunWithCancelationAndReturnAsync(lblOk, 5, _tokenSource.Token, "wird geladen",
                                                   New Progress(Of Integer)(Sub(result As Integer) ProgressBar.Value = result))
      Catch ex As Exception
         Stop
      End Try
      lblStatus.Text = $"btnStart2MitReturn_Click beendet um {Now.ToLongTimeString}"
   End Sub

   Private Sub btnStart4_Click(sender As Object, e As EventArgs) Handles btnStart4.Click
      lblStatus.Text = Nothing
      Try
         Debug.WriteLine($"Start4 gestartet um {Now.ToLongTimeString}")

         Task.Run(Sub()
                     ' langen Prozess starten
                     Dim Parameterliste As New Hashtable
                     Parameterliste.Add("Anzahl", 5)
                     Parameterliste.Add("mainControl", lblOk)
                     Parameterliste.Add("waitControlText", $"wird geladen {Now.ToLongTimeString}")

                     Dim Ergebnis As clsAufgabenErgebnis = __AufgabenManager.starteAufgabe(AddressOf Test6, Parameterliste).Result
                     lblOk.Invoke(Sub()
                                     If Ergebnis.abgebrochen = False Then
                                        lblOk.Text = CInt(Ergebnis.WertObjekt).ToString
                                     End If
                                  End Sub)
                  End Sub)

      Catch ex As Exception
         Stop
      End Try
      Debug.WriteLine($"Start4 beendet um {Now.ToLongTimeString}")
   End Sub

   Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
      Try
         Debug.WriteLine($"aktueller Prozess gecancelt um {Now.ToLongTimeString}")
         _tokenSource.Cancel()
      Catch ex As Exception
         Stop
      End Try
   End Sub

   Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
      lblUhrzeit.Text = Now.ToLongTimeString
   End Sub

End Class
