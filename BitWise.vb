''' <summary>
''' Exercise on bitwise operators in VB.NET
''' 
''' There are six groups of things each group is consists of five.
''' The six groups are represented as one unsigned integer (activePartLeft)
''' The lowest five bits represent the first group
''' The highest five bits represent the sixth group
''' 
''' Each bit is called sensor.
''' 
''' We can turn off a sensor as long as it's not the last sensor on in its group (five bits (sensors) comprise a group as mentioned)
''' 
''' </summary>
''' <remarks></remarks>
Module Module1
    Dim MaskG1 As UInteger = 31
    Dim MaskG2 As UInteger = 992
    Dim MaskG3 As UInteger = 31744
    Dim MaskG4 As UInteger = 1015808
    Dim MaskG5 As UInteger = 32505856
    Dim MaskG6 As UInteger = 1040187392

    Sub Main()
        Dim maxRandom = CInt(Math.Pow(2, 30)) + 1
        Dim activePartLeftMask As UInteger = Convert.ToUInt32((New Random()).Next(1, maxRandom))
        While True
            Dim bits = Dec2Bin(activePartLeftMask)
            'Console.WriteLine("Number {0} is Binary: {1}", activePartLeftMask, bits)
            Console.Write("Number {0} ", activePartLeftMask)
            PrintBits(bits)

            Dim group As UInteger = ReadGroupNumber()
            Dim sensor As UInteger = ReadSensorNumber()
            activePartLeftMask = CanSensorBeTurnedOff(group, sensor, activePartLeftMask)
        End While
    End Sub
    Function ReadGroupNumber() As UInteger
        Console.Write("Enter the group number to operate on:")
        Dim line = Console.ReadLine()
        Return UInteger.Parse(line)
    End Function
    Function ReadSensorNumber() As UInteger
        Console.Write("Enter the sensor number to toggle turn off:")
        Dim line = Console.ReadLine()
        Return UInteger.Parse(line)
    End Function
    Function CanSensorBeTurnedOff(ByVal groupNumber As UInteger, ByVal sensor As UInteger, ByVal activePartLeftRep As UInteger) As UInteger
        Dim mask As UInteger = 0
        Select Case groupNumber
            Case 1
                mask = MaskG1
            Case 2
                mask = MaskG2
            Case 3
                mask = MaskG3
            Case 4
                mask = MaskG4
            Case 5
                mask = MaskG5
            Case 6
                mask = MaskG6
            Case Else
                Console.Error.WriteLine("Invalid group number")
                Return activePartLeftRep
        End Select
        If (sensor < 1 OrElse sensor > 5) Then
            Console.Error.WriteLine("Invalid sensor; must be between 1 to 5")
            Return activePartLeftRep
        End If
        Dim groupMasked = activePartLeftRep And mask
        If (groupMasked = 0) Then
            Console.Error.WriteLine("Whole group is already off.")
            Return activePartLeftRep
        End If
        Dim alreadyOff = IsSensorAlreadyOff(groupMasked, sensor, groupNumber)
        If alreadyOff Then
            Console.Error.WriteLine("Sensor is already off.")
            Return activePartLeftRep
        End If
        Dim groupRepAfterSensorTurnedOff = TurnOffSensor(groupMasked, sensor, groupNumber)
        Dim canTurnOffSensor = False
        If groupRepAfterSensorTurnedOff > 0 Then
            Console.WriteLine("Yes sensor can be turned off")
            canTurnOffSensor = True
        Else
            Console.WriteLine("No this is the last sensor standing and cannot be turned off")
        End If
        If canTurnOffSensor Then
            Dim newActivePartLeftRep = UInteger.MaxValue Xor groupMasked 'turn on all bits except those of the group
            newActivePartLeftRep = newActivePartLeftRep Or groupRepAfterSensorTurnedOff 'turn on the bits of the group that are left on after turning off sensor
            newActivePartLeftRep = (newActivePartLeftRep And activePartLeftRep) 'turn off bits that were not on in the original rep
            'PrintBits(Dec2Bin(newActivePartLeftRep))
            Return newActivePartLeftRep
        End If
        Return activePartLeftRep
    End Function
    ''' <summary>
    ''' we basically do a clear-bit operation. 
    ''' We start with 1 (bit pattern = 0x00000001)
    ''' we shift the bits left (using the shift operator &lt;&lt;)  
    ''' we then negate and do an AND operation on the result.
    ''' </summary>
    ''' <param name="groupRep">The group of five bits we are interested in</param>
    ''' <param name="sensor">the sensor number we need to set from 1 to 0. The sensor number must be between 1 and 5</param>
    ''' <param name="groupNumber">the group number; needed to know how far to shift to the right</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function TurnOffSensor(ByVal groupRep As UInteger, ByVal sensor As UInteger, ByVal groupNumber As UInteger) As UInteger
        'Dim mask = Not (1 << CInt((sensor - 1) + (groupNumber - 1) * 5))
        Dim mask = Not ShiftLeft(1, CInt((sensor - 1) + (groupNumber - 1) * 5))
        Return CUInt(groupRep And mask)
    End Function
    ''' <summary>
    ''' Test to see if the senor bit is alread 0 (off).
    ''' </summary>
    ''' <param name="groupRep"></param>
    ''' <param name="sensor"></param>
    ''' <param name="groupNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsSensorAlreadyOff(ByVal groupRep As UInteger, ByVal sensor As UInteger, ByVal groupNumber As UInteger) As Boolean
        'Dim mask = (1 << CInt((sensor - 1) + (groupNumber - 1) * 5))
        Dim mask = ShiftLeft(1, CInt((sensor - 1) + (groupNumber - 1) * 5))
        Return (groupRep And mask) = 0
    End Function
    ''' <summary>
    ''' Some languages do not have shift operators like vbscript
    ''' </summary>
    ''' <param name="pattern"></param>
    ''' <param name="places"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ShiftLeft(ByVal pattern As Integer, ByVal places As Integer) As UInteger
        If places <= 0 Then
            Return CUInt(pattern) 'no shifting 
        End If
        Return CUInt(pattern * CInt(Math.Pow(2, places)))
    End Function
    Sub PrintBits(ByVal bits As String)
        For index As Integer = 2 To bits.Length - 1
            If (index - 2) Mod 5 = 0 Then
                Console.Write(" ")
            End If
            Console.Write(bits(index))
        Next
        Console.WriteLine()
    End Sub
    Function Dec2Bin(ByVal value As UInteger) As String
        Dim bits(31) As Char
        For index As Integer = 31 To 0 Step -1
            If (value And 1) > 0 Then
                bits(index) = "1"c
            Else
                bits(index) = "0"c
            End If
            value = value >> 1
        Next
        Return bits
    End Function
End Module
