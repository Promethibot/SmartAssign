Imports System.IO

Public Class Form1

    Private _intArrSize As Integer = 11
    Private _strSavings(_intArrSize) As String
    Private _decBill(_intArrSize) As Decimal

    Private Sub btnDisplay_Click(sender As Object, e As EventArgs) Handles btnDisplay.Click
        lblMonth.Visible = True
        lblAvg.Visible = True
        lblSignificant.Visible = True
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim objReader As IO.StreamReader
        Dim strSavingsAmount As String
        Dim intCount As Integer = 0
        Dim intFill As Integer


        Try
            If IO.File.Exists("D:\OneDrive\Classes\Spring 2023\Graphic User Interface Design\repos\SmartAssign\SmartAssign\savings.txt") Then
                objReader = IO.File.OpenText("D:\OneDrive\Classes\Spring 2023\Graphic User Interface Design\repos\SmartAssign\SmartAssign\savings.txt")
                Do While objReader.Peek <> -1
                    _strSavings(intCount) = objReader.ReadLine
                    strSavingsAmount = objReader.ReadLine
                    _decBill(intCount) = Convert.ToDecimal(strSavingsAmount)
                    intCount += 1
                Loop

                objReader.Close()

                For intFill = 0 To (_strSavings.Length - 1)
                    cboMonths.Items.Add(_strSavings(intFill))
                Next

            Else
                MsgBox("The file could not be found, sorry!", , "Error")
            End If

        Catch Exception As IOException
            MsgBox("Something went wrong with the IO, sorry!")
        End Try



    End Sub

    Private Sub cboMonths_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMonths.SelectedIndexChanged
        Dim intSelectedCityIndex As Integer

        intSelectedCityIndex = cboMonths.SelectedIndex

        lblMonth.Text = "The electric savings for " + _strSavings(intSelectedCityIndex) + " is $" + _decBill(intSelectedCityIndex).ToString
        btnDisplay.Visible = True

        ComputeAverage()
        ComputeMaxSavings()


    End Sub

    Private Sub ComputeMaxSavings()
        Dim intMonths As Integer
        Dim intLargestSavingsValue As Integer = 0
        Dim intIndexValue As Integer = 0
        Dim strYearValue As String = ""

        For Each intMonths In _decBill
            intLargestSavingsValue = Math.Max(intLargestSavingsValue, intMonths)
            If (intMonths >= intLargestSavingsValue) Then
                strYearValue = _strSavings(intIndexValue)
            End If
            intIndexValue += 1
        Next

        lblSignificant.Text = strYearValue + " had the most significant monthly savings"

    End Sub

    Private Sub ComputeAverage()
        Dim intCountYears As Integer
        Dim intYears As Integer = 0
        Dim decTotalBill As Decimal = 0
        Dim decAverageNumberOfBill As Decimal = 0D

        For Each intCountYears In _decBill
            decTotalBill += _decBill(intYears)
            intYears += 1
        Next

        decAverageNumberOfBill = decTotalBill / Convert.ToDecimal(_decBill.Length())

        lblAvg.Text = "The average monthly savings are " + decAverageNumberOfBill.ToString("C2")

    End Sub
End Class
