Imports System.IO 'imports
Imports Excel = Microsoft.Office.Interop.Excel

Module Module1

    Dim Seniles As Decimal() = {10, 0, 0} ' array for storing Population,Birth,Death rate
    Dim Adults As Decimal() = {10, 2, 1}
    Dim Juveniles As Decimal() = {10, 1, 1}
    Dim Disease As Boolean
    Dim DiseaseTrigerStart As Decimal = 200 'declarations
    Dim DiseaseTrigerEnd As Decimal = 100
    Dim total As Decimal
    Dim generations As Decimal = 25
    Dim completed As Integer

    Sub Main()
        Console.WriteLine("Greenfly Population Simulator")
        Console.WriteLine("~~~~~MENU~~~~~")
        Console.WriteLine("")
        Console.WriteLine("1: Set the starting values ( Do this before running the simulation )") ' menu
        Console.WriteLine("2: Print the starting values")
        Console.WriteLine("3: Run the simulation")
        Console.WriteLine("4: Exit the program")
        Dim temp As String = Console.ReadLine()
        If temp = 1 Then
            Setvalues()
            Main()
        ElseIf temp = 2 Then ' menu code
            Printvalues()
            Main()
        ElseIf temp = 3 Then
            If IsNothing(Seniles(0)) Then
                Console.WriteLine("Error: No values were found, Try setting the values at the menu.") ' error messages
                Main()
            Else
                start(generations)
                Main()
            End If
        ElseIf temp = 4 Then
            Environment.Exit(0.0)
        End If

    End Sub
    Function Printvalues()
        Console.Clear()
        Console.WriteLine("Amount of Juviniles: " & Juveniles(0))
        Console.WriteLine("Amount of Adults: " & Adults(0))
        Console.WriteLine("Amount of Seniles: " & Seniles(0))
        Console.WriteLine("Adult Birthrate: " & Adults(1)) ' writes values to the screen 
        Console.WriteLine("Juvinile Survival Rate: " & Juveniles(2))
        Console.WriteLine("Adult Survival Rate: " & Adults(2))
        Console.WriteLine("Senile Survival Rate: " & Seniles(2))
        Console.WriteLine("Total Generations:" & generations)
        Console.ReadLine()
        Console.Clear()
        Return True
    End Function
    Function Setvalues()
        Console.Clear()
        Console.WriteLine("Enter the starting population of the Juviniles")
        Juveniles(0) = Console.ReadLine()
        Console.WriteLine("Enter the starting population of the Adults")
        Adults(0) = Console.ReadLine()
        Console.WriteLine("Enter the starting population of the Seniles")
        Seniles(0) = Console.ReadLine() ' set the starting values
        Console.WriteLine("Enter the Adult birthrate")
        Adults(1) = Console.ReadLine()
        Console.WriteLine("Enter the Juvinile survival rate")
        Juveniles(2) = Console.ReadLine()
        Console.WriteLine("Enter the Adults survival rate")
        Adults(2) = Console.ReadLine()
        Console.WriteLine("Enter the Senile survival rate")
        Seniles(2) = Console.ReadLine()
        Console.WriteLine("How many Generations?")
        generations = Console.ReadLine()
        Return True
    End Function
    Public Function start(generations As Integer)
        Randomize() 'initalize randomizer
        Dim Values(generations - 1)() As Decimal
        For i = 1 To generations
            If total >= DiseaseTrigerStart And Disease = False Then ' get random disease values
                Disease = True
            End If
            If total <= DiseaseTrigerEnd And Disease = True Then ' disease triggers
                Disease = False
            End If
            Juveniles(0) *= Juveniles(2)
            Adults(0) *= Adults(2) 'things being old
            Seniles(0) *= Seniles(2)
            If Disease Then
                Dim diseaseFactor As Decimal = (CInt(Math.Floor((5 - 8 + 1) * Rnd())) + 8) / 10
                Console.WriteLine(diseaseFactor) ' calulate disease
                Juveniles(0) *= diseaseFactor
                Seniles(0) *= diseaseFactor
            End If
            Dim _Adults As Decimal = Adults(0)
            Seniles(0) += Adults(0) * Adults(2)
            Adults(0) = Juveniles(0)
            Juveniles(0) = _Adults * Adults(1)
            Seniles(0) = Math.Round(Seniles(0))
            Adults(0) = Math.Round(Adults(0)) ' round the values
            Juveniles(0) = Math.Round(Juveniles(0))
            Console.Write(Math.Round(Juveniles(0)) & " ")
            Console.Write(Math.Round(Adults(0)) & " ")
            Console.Write(Math.Round(Seniles(0)) & " ")
            Try
                total = Juveniles(0) + Adults(0) + Seniles(0) ' check that no overflows happen

            Catch OverflowException As Exception
                MsgBox("Values too large to continue, aborting")
                ExcelGenerate(Values, completed) ' if so catch them and open excel
                Main()
            End Try
            Console.Write(Math.Round(total))
            Console.WriteLine()
            Values(i - 1) = New Decimal(3) {}
            Values(i - 1)(0) = Math.Round(Juveniles(0))
            Values(i - 1)(1) = Math.Round(Adults(0)) ' add the values to a 2d array
            Values(i - 1)(2) = Math.Round(Seniles(0))
            Values(i - 1)(3) = Math.Round(total)
            completed += 1
        Next
        ExcelGenerate(Values, completed)


        Return True
    End Function

    Public Function ExcelGenerate(Values As Array, Completed As Integer)
        Dim appXL As Excel.Application
        Dim wbXl As Excel.Workbook
        Dim shXL As Excel.Worksheet
        appXL = CreateObject("Excel.Application")
        appXL.Visible = True
        wbXl = appXL.Workbooks.Add 'open excel and make workbook
        shXL = wbXl.ActiveSheet
        shXL.Cells(1, 1).Value = "Juviniles" ' add titles to collums
        shXL.Cells(1, 2).Value = "Adults"
        shXL.Cells(1, 3).Value = "Seniles"
        shXL.Cells(1, 4).Value = "Total"

        For i = 1 To Completed
            shXL.Cells(i + 1, 1).Value = Values(i - 1)(0)
            shXL.Cells(i + 1, 2).Value = Values(i - 1)(1) ' add the values to the cells
            shXL.Cells(i + 1, 3).Value = Values(i - 1)(2)
            shXL.Cells(i + 1, 4).Value = Values(i - 1)(3)
        Next
        Dim chartPage As Excel.Chart
        Dim xlCharts As Excel.ChartObjects
        Dim myChart As Excel.ChartObject ' make a chart
        Dim chartRange As Excel.Range

        xlCharts = shXL.ChartObjects
        myChart = xlCharts.Add(300, 80, 300, 250)
        chartPage = myChart.Chart ' add values to chart
        chartRange = shXL.Range("A1", "d" & Completed + 1)
        chartPage.SetSourceData(Source:=chartRange)
        chartPage.ChartType = Excel.XlChartType.xlLine ' add axes titles
        chartPage.HasTitle = True
        chartPage.ChartTitle.Text = "Greenfly"
        Dim axis As Excel.Axis = CType(chartPage.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary), Excel.Axis)
        axis.HasTitle = True ' add title
        axis.AxisTitle.Text = "Population"
        Dim axis2 As Excel.Axis = CType(chartPage.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary), Excel.Axis)
        axis2.Delete() ' remove numbers to prevent huge excel being special errors
        axis2.HasTitle = True
        axis2.AxisTitle.Text = "Generations"
        Return True
    End Function

End Module
