Imports System.Threading

Public Class Form1

   Private _tokenSource As New CancellationTokenSource

   Private Sub btnStart1_Click(sender As Object, e As EventArgs) Handles btnStart1.Click
      lblStatus.Text = Nothing
      Try
         ' UI au Grundstellung
         ProgressBar.Visible = False
         lblOk.Text = "in Ruhe"

         ' Prozess starten
         lblOk.Text = Test4(5, cancelationtoken:=Nothing, Progress:=Nothing).ToString
      Catch ex As Exception
         Stop
      End Try
   End Sub
   Private Async Sub btnStartMitAwait_Click(sender As Object, e As EventArgs) Handles btnStartMitAwait.Click
      lblStatus.Text = Nothing
      Try
         Dim sc As SynchronizationContext = SynchronizationContext.Current

         ' UI au Grundstellung
         ProgressBar.Visible = False
         lblOk.Text = "in Ruhe"

         ' Prozess starten
         Dim Anzahl As Integer = 5
         Dim cancelationtoken As CancellationToken = _tokenSource.Token
         Dim Progress As New Progress(Of Integer)(Sub(result As Integer)
                                                     ProgressBar.Visible = True
                                                     ProgressBar.Value = result
                                                  End Sub)
         Dim i As Integer = Await Task.Run(Function() Test4(Anzahl, cancelationtoken, Progress)).ConfigureAwait(continueOnCapturedContext:=True)
         lblOk.Text = i.ToString
         'sc.Post(New SendOrPostCallback(Sub()
         '                                  lblOk.Text = i.ToString
         '                               End Sub), Nothing)

      Catch ex As Exception
         Stop
      End Try
   End Sub

   Private Sub btnStart2MitReturn_Click(sender As Object, e As EventArgs) Handles btnStart2MitReturn.Click
      lblStatus.Text = Nothing
      Try
         ' UI au Grundstellung
         ProgressBar.Visible = False
         lblOk.Text = "in Ruhe"

         _tokenSource = New CancellationTokenSource

         ' Prozess starten
         'clsAsync.RunWithCancelationAndReturnAsync(lblOk, 5, _tokenSource.Token, "wird geladen",
         '                                          New Progress(Of Integer)(Sub(result As Integer)
         '                                                                      ProgressBar.Visible = True
         '                                                                      ProgressBar.Value = result
         '                                                                   End Sub))
         clsAsync.RunWithCancelationAndReturnAsync(lblOk, 5, _tokenSource.Token, "wird geladen",
                                                   New Progress(Of Integer)(Sub(result As Integer)
                                                                               ProgressBar.Visible = True
                                                                               ProgressBar.Value = result
                                                                            End Sub))
      Catch ex As Exception
         Stop
      End Try
      lblStatus.Text = $"btnStart2MitReturn_Click beendet um {Now.ToLongTimeString}"
   End Sub

   Private Sub btnStart3_Click(sender As Object, e As EventArgs) Handles btnStart3.Click
      Try
         Dim i As Integer

         'clsAsync.RunFunctionAsync3(Function()
         '                              Return Test4Async(5,
         '                                                _tokenSource.Token,
         '                                                New Progress(Of Integer)(Sub(result As Integer)
         '                                                                            ProgressBar.Visible = True
         '                                                                            ProgressBar.Value = result
         '                                                                         End Sub))
         '                           End Function,
         '                           lblOk)



         Dim T As Task(Of Integer) = Test4Async(5, _tokenSource.Token, New Progress(Of Integer)(Sub(result As Integer)
                                                                                                   ProgressBar.Visible = True
                                                                                                   ProgressBar.Value = result
                                                                                                End Sub))
         T.Wait()
         i = T.Result
         'i = Await T
         lblOk.Text = i.ToString
      Catch ex As Exception
         Stop
      End Try
   End Sub

   Private Sub btnStart4_Click(sender As Object, e As EventArgs) Handles btnStart4.Click
      lblStatus.Text = Nothing
      Try
         ' UI au Grundstellung
         ProgressBar.Visible = False
         lblOk.Text = "in Ruhe"

         ' Prozess starten
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
      _tokenSource.Cancel()
   End Sub

   Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
      lblUhrzeit.Text = Now.ToLongTimeString
   End Sub

End Class
