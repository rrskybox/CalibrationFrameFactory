Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Net.Mime.MediaTypeNames
Imports System.Windows.Forms.AxHost
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip

Namespace CalFrameFactory
    Public Partial Class FormCalFrameFactory
        Inherits Form
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        ' TODO: Skipped BadDirectiveTrivia
        Private [Class] As [Public]

        ' TODO: Error SkippedTokensTrivia ''This application uses TSX to take a series of dark frames for a dark frame library'
    ' TODO: Error SkippedTokensTrivia '''
    ' TODO: Error SkippedTokensTrivia ''The RunDarks procedure launches TSX (if not already open), connects to the camera and'
    ' TODO: Error SkippedTokensTrivia ''  sets up the temperature and autosave'
    ' TODO: Error SkippedTokensTrivia '''
    ' TODO: Error SkippedTokensTrivia ''The private method "ImageDark" takes the dark image, waits for completion while monitoring'
    ' TODO: Error SkippedTokensTrivia ''  for an abort.  If aborted, the frame is aborted and the sequence ends.'
    ' TODO: Error SkippedTokensTrivia '''
    ' TODO: Error SkippedTokensTrivia ''The dark files are stored in a folder tree.  The root is a folder in the user document folder'
    ' TODO: Error SkippedTokensTrivia ''  called "PreStack", which is used for other TsxToolKit Miniapps.  Within this folder, a folder'
    ' TODO: Error SkippedTokensTrivia ''  "Calibrations" is used (created if doesn''                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'FormDarksKnight' at character 805
''' 
''' 
''' Input:
''' FormDarksKnight
''' 
'''     'This application uses TSX to take a series of dark frames for a dark frame library
'''     '
'''     'The RunDarks procedure launches TSX (if not already open), connects to the camera and
'''     '  sets up the temperature and autosave
'''     '
'''     'The private method "ImageDark" takes the dark image, waits for completion while monitoring
'''     '  for an abort.  If aborted, the frame is aborted and the sequence ends.
'''     '
'''     'The dark files are stored in a folder tree.  The root is a folder in the user document folder
'''     '  called "PreStack", which is used for other TsxToolKit Miniapps.  Within this folder, a folder
'''     '  "Calibrations" is used (created if doesn'
''' 
        Private exist As t

        Private ReadOnly Property Item(will As folder, the As be, library As darks, exist As t) As Within
        ' TODO: Error SkippedTokensTrivia ''  folder named "Darks" (created if doesn''
        ' TODO: Error SkippedTokensTrivia ''Save folder structure pointer'
        Private [Object], e As (sender, _)
        Private EventArgs As [As]

        ' TODO: Skipped BadDirectiveTrivia        ' TODO: Skipped BadDirectiveTrivia        ' TODO: Skipped BadDirectiveTrivia
        Private Function Load(My As [Try], Optional _ As Forms.FormDarksKnight.Text = Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(), My As [Catch], Optional _ As Forms.FormDarksKnight.Text = " in Debug", [Try] As [End], Optional _ As [MyBase].Text = "Darks Knight V " & My.Forms.FormDarksKnight.Text.ToString(), Optional tsx_cc As [Dim] = CreateObject("TheSkyX.ccdsoftcamera"), Optional tsx_cc As [Dim] = CreateObject("TheSky64.ccdsoftcamera"), tsx_cc As [Try], _ As Connect, [Catch] As (_, _), [As] As ex, MsgBox As Exception, _ As (_, _)) As [Handles]  ' TODO: Error SkippedTokensTrivia ''probably In debug, no version info available'


        ' TODO: Error SkippedTokensTrivia ''Save the current camera state,'
        ' TODO: Error SkippedTokensTrivia ''Then get the camera cooling and set up the file structure'
        Private TryField As [End]
        ' TODO: Error SkippedTokensTrivia ''TSX camera simulator throws an exception on AutoSave so handle it'
        Private autosavestate As [Try] = tsx_cc.AutoSave()
        Private exField As [Catch]
        Private ExceptionField As [As]
        ' TODO: Error SkippedTokensTrivia ''Just breeze on by'
        Private [Try] As [End]
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'delaystate' at character 4170
''' 
''' 
''' Input:
'''         delaystate = 
''' 
        Private binningXstate As tsx_cc.Delay = tsx_cc.BinX()
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'binningYstate' at character 4249
''' 
''' 
''' Input:
'''         binningYstate = 
''' 
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'tsx_cc.BinY' at character 4265
''' 
''' 
''' Input:
''' tsx_cc.BinY
''' 
        Private exposurestate As (_, _) = tsx_cc.ExposureTime()
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'settempstate' at character 4340
''' 
''' 
''' Input:
'''         settempstate = 
''' 
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'tsx_cc.TemperatureSetPoint' at character 4355
''' 
''' 
''' Input:
''' tsx_cc.TemperatureSetPoint
''' 
        Private framestate As (_, _) = tsx_cc.Frame
        ' TODO: Error SkippedTokensTrivia ''Get folder path defaults set up, if necessary'
        ' TODO: Error SkippedTokensTrivia ''           ssdir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + ScanFolderName;'
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'tsx_cc' at character 4433
''' 
''' 
''' Input:
'''         tsx_cc = 
''' 
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Nothing' at character 4442
''' 
''' 
''' Input:
''' Nothing
'''         'Get folder path defaults set up, if necessary
'''         '           ssdir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + ScanFolderName;
''' 
''' 

        Private Sub New(Optional _ As My.Settings.DestinationDir = "")
        End Sub

        Private Function DestinationDir([If] As [End], [End] As [Return], [Private] As [Sub], CloseButton_Click As [Sub], [Object] As (sender, _), [As] As e, _ As EventArgs) As [Then]
        ' TODO: Skipped BadDirectiveTrivia        ' TODO: Skipped BadDirectiveTrivia
        Private Function Click(Optional tsx_cc As [Dim] = CreateObject("TheSkyX.ccdsoftcamera"), Optional tsx_cc As [Dim] = CreateObject("TheSky64.ccdsoftcamera"), tsx_cc As [Try], _ As AutoSave, Optional _ As (_, _) = autosavestate, ex As [Catch], Exception As [As], [Try] As [End], Optional _ As tsx_cc.Delay = delaystate, _ As tsx_cc.BinX, Optional _ As (_, _) = binningXstate, _ As tsx_cc.BinY, Optional _ As (_, _) = binningYstate, _ As tsx_cc.ExposureTime, Optional _ As (_, _) = exposurestate, _ As tsx_cc.TemperatureSetPoint, Optional _ As (_, _) = settempstate, Optional _ As tsx_cc.Frame = framestate, Optional _ As tsx_cc = [Nothing], _ As Close, [End] As (_, _), [Private] As [Sub], AbortButton_Click As [Sub], [Object] As (sender, _), [As] As e, _ As EventArgs) As [Handles]
        ' TODO: Skipped BadDirectiveTrivia        ' TODO: Error SkippedTokensTrivia ''TSX camera simulator throws an exception on AutoSave so handle it'

        ' TODO: Error SkippedTokensTrivia ''Just breeze on by'

        Private Function Click(Optional _ As abortflag = [True], [End] As [Return], [Private] As [Sub], StartButton_Click As [Sub], [Object] As (sender, _), [As] As e, _ As EventArgs) As [Handles]


        Private Function Click(Optional _ As StartButton.BackColor = Color.LightSalmon, SaveTSXCheckBox As [If], [Then] As Checked, _ As UseAutoSave, SaveTSXCheckBox As (_, _), Optional _ As ForeColor = Color.LightGreen, _ As [ElseIf], [Then] As (SavePreStackCheckBox.Checked, _), Optional _ As CalDB = [New], _ As CalibrationFileManagement, SavePreStackCheckBox As ([True], _), Optional _ As ForeColor = Color.LightGreen, Optional CalDB As [Else] = [New], _ As CalibrationFileManagement, OtherDirRadioButton As ([False], _), Optional _ As ForeColor = Color.LightGreen, [If] As [End], _ As SetCCDTemperature, [If] As (_, _), [Then] As CheckBox1x1.Checked, Optional _ As CheckBox1x1.ForeColor = Color.Red, _ As SetBinning, _ As (_, _), _ As _) As [Handles]

        Private Sub New()
        End Sub

                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'CheckBox1x1.ForeColor' at character 7225
''' 
''' 
''' Input:
'''             CheckBox1x1.ForeColor = 
''' 
        Private EndField24 As Color.LightGreen
        Private IfField18 As [If]
        Private ThenField17 As CheckBox2x2.Checked
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'CheckBox2x2.ForeColor' at character 7368
''' 
''' 
''' Input:
'''             CheckBox2x2.ForeColor = 
''' 
        Private Function SetBinningMethod1(_ As _, _ As _) As Color.Red

        Private Sub New()
        End Sub

                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'CheckBox2x2.ForeColor' at character 7495
''' 
''' 
''' Input:
'''             CheckBox2x2.ForeColor = 
''' 
        Private EndField23 As Color.LightGreen
        Private IfField17 As [If]
        Private ThenField16 As CheckBox3x3.Checked
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'CheckBox3x3.ForeColor' at character 7638
''' 
''' 
''' Input:
'''             CheckBox3x3.ForeColor = 
''' 
        Private Function SetBinningMethod(_ As _, _ As _) As Color.Red

        Private Sub New()
        End Sub

                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'CheckBox3x3.ForeColor' at character 7764
''' 
''' 
''' Input:
'''             CheckBox3x3.ForeColor = 
''' 
        Private EndField22 As Color.LightGreen
        Private IfField16 As [If]
        Private ThenField15 As CheckBox4x4.Checked
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'CheckBox4x4.ForeColor' at character 7907
''' 
''' 
''' Input:
'''             CheckBox4x4.ForeColor = 
''' 
        Private Function SetBinning(_ As _, _ As _) As Color.Red

        Private Sub New()
        End Sub

                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'CheckBox4x4.ForeColor' at character 8027
''' 
''' 
''' Input:
'''             CheckBox4x4.ForeColor = 
''' 
        Private EndField21 As Color.LightGreen

        Private Function BackColor([Return] As Color.LightGreen, [Sub] As [End], [Sub] As [Private], _ As SetBinning, [Integer] As (binx, _), [As] As biny, _ As [Integer]) As [If]
        ' TODO: Skipped BadDirectiveTrivia        Private tsx_ccField10 As [Dim] = CreateObject("TheSkyX.ccdsoftcamera")
        ' TODO: Skipped BadDirectiveTrivia        Private tsx_ccField9 As [Dim] = CreateObject("TheSky64.ccdsoftcamera")
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'tsx_cc.BinX' at character 8486
''' 
''' 
''' Input:
''' #End If
'''         tsx_cc.BinX = 
''' 
        ' TODO: Skipped BadDirectiveTrivia

        Private Function BinY(Optional tsx_cc As biny = [Nothing], [End] As [Return], [Private] As [Sub], RepeatDark As [Sub], _ As (reps, exposure), Optional _ As totalreps = Me.DarksCountBox.Value, i As [Dim], [Integer] As [As], Optional _ As DarksCountBox.ForeColor = Color.Red, Optional i As [For] = reps - 1, _ As [To], _ As [Step], _ As ImageDark, [If] As (exposure, _), [Then] As abortflag, [End] As [Return], _ As [If], _ As DarksCountBox.Value, DarksCountBox As [Next], Optional _ As Value = totalreps, Optional _ As DarksCountBox.ForeColor = Color.LightGreen, [End] As [Return], [Private] As [Sub], ImageDark As [Sub], [Double] As (exposure, _)) As binx
        ' TODO: Error SkippedTokensTrivia ''Change the form count box color'

        ' TODO: Error SkippedTokensTrivia ''Set the count on the form'

        ' TODO: Error SkippedTokensTrivia ''Change the form count box color'

        ' TODO: Skipped BadDirectiveTrivia        Private tsx_ccField8 As [Dim] = CreateObject("TheSkyX.ccdsoftcamera")
        ' TODO: Skipped BadDirectiveTrivia                Private tsx_ccField7 As [Dim] = CreateObject("TheSky64.ccdsoftcamera")
        ' TODO: Skipped BadDirectiveTrivia
        ' TODO: Error SkippedTokensTrivia ''turn off TSX autosave if using the PreStack file management'

        Private Function Checked(tsx_cc As [Then], Optional _ As autosaveon = 0, [If] As [End], Optional _ As tsx_cc.ExposureTime = exposure, Optional _ As tsx_cc.Frame = TheSky64Lib.ccdsoftImageFrame.cdDark, Optional _ As tsx_cc.Delay = 0, Optional _ As tsx_cc.Asynchronous = [True], Optional _ As tsx_cc.ImageReduction = TheSky64Lib.ccdsoftImageReduction.cdNone, _ As tsx_cc.TakeImage, [Do] As (_, _), tsx_cc As [While], Optional _ As state = TheSky64Lib.ccdsoftCameraState.cdStateTakePicture, _ As Application.DoEvents, [If] As (_, _), [Then] As abortflag, _ As tsx_cc.Abort, Optional tsx_cc As (_, _) = [Nothing], [End] As [Return], System As [If], _ As Threading.Thread.Sleep, _ As (_, _)) As [If]
        ' TODO: Error SkippedTokensTrivia '' tsx_cc.ExposureTime = exposure'

        ' TODO: Error SkippedTokensTrivia ''Save the using the PreStack manager if checked,'
        ' TODO: Error SkippedTokensTrivia ''  Otherwise TSX will do what TSX does.'
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Loop' at character 10966
''' 
''' 
''' Input:
'''         Loop
'''         'Save the using the PreStack manager if checked,
'''         '  Otherwise TSX will do what TSX does.
''' 
''' 
        Private NotField As [If]
        Private ThenField14 As SaveTSXCheckBox.Checked
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'CalDB.DarkImageStore' at character 11148
''' 
''' 
''' Input:
'''             CalDB.DarkImageStore
''' 
        Private EndField20 As (_, _)
        Private tsx_ccField6 As [If] = [Nothing]
        Private EndField19 As [Return]

        Private PrivateField2 As [Sub]
        Private Function RepeatBias(_ As reps) As [Sub]
        Private biasexposure As [Const] = 0.001
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'totalreps' at character 11416
''' 
''' 
''' Input:
'''         totalreps = 
''' 
        Private [Dim] As BiasCountBox.Value
        Private [As] As i
        ' TODO: Error SkippedTokensTrivia ''Change the form count box color'

        ' TODO: Error SkippedTokensTrivia ''Set the count on the form'
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Integer' at character 11465
''' 
''' 
''' Input:
''' Integer
'''         'Change the form count box color
''' 
''' 
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'BiasCountBox.ForeColor' at character 11524
''' 
''' 
''' Input:
'''         BiasCountBox.ForeColor = 
''' 
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'System.Drawing.Color.Red' at character 11549
''' 
''' 
''' Input:
''' System.Drawing.Color.Red
'''         'Set the count on the form
''' 
''' 
        Private iField As [For] = reps - 1 ' TODO: Error SkippedTokensTrivia '-'' TODO: Error SkippedTokensTrivia '1'
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'To' at character 11643
''' 
''' 
''' Input:
''' To 0 
''' 
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Step' at character 11648
''' 
''' 
''' Input:
''' Step -1
''' 
''' 
        Private Sub New(_ As biasexposure)
        End Sub

        Private abortflag As [If]
        Private [Return] As [Then]
        Private IfField15 As [End]
        ' TODO: Error SkippedTokensTrivia ''Decrement count'
        ' TODO: Error SkippedTokensTrivia '-=' ' TODO: Error SkippedTokensTrivia '1'
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'BiasCountBox.Value' at character 11818
''' 
''' 
''' Input:
'''             BiasCountBox.Value -= 1
''' 
''' 

        Private Function Value(_ As totalreps, Optional _ As BiasCountBox.ForeColor = Color.LightGreen, [End] As [Return], [Private] As [Sub], ImageBias As [Sub], [Double] As (exposure, _)) As [Next]

        ' TODO: Skipped BadDirectiveTrivia        Private tsx_ccField5 As [Dim] = CreateObject("TheSkyX.ccdsoftcamera")
        ' TODO: Skipped BadDirectiveTrivia                Private tsx_ccField4 As [Dim] = CreateObject("TheSky64.ccdsoftcamera")
        ' TODO: Skipped BadDirectiveTrivia
        ' TODO: Error SkippedTokensTrivia ''turn off TSX autosave if using the PreStack file management'

        Private Function Checked(tsx_cc As [Then], Optional _ As autosaveon = 0, [If] As [End], Optional _ As tsx_cc.ExposureTime = exposure, Optional _ As tsx_cc.Frame = TheSky64Lib.ccdsoftImageFrame.cdBias, Optional _ As tsx_cc.Delay = 0, Optional _ As tsx_cc.Asynchronous = [True], Optional _ As tsx_cc.ImageReduction = TheSky64Lib.ccdsoftImageReduction.cdNone, _ As tsx_cc.TakeImage, [Do] As (_, _), tsx_cc As [While], Optional _ As state = TheSky64Lib.ccdsoftCameraState.cdStateTakePicture, _ As Application.DoEvents, [If] As (_, _), [Then] As abortflag, _ As tsx_cc.Abort, Optional tsx_cc As (_, _) = [Nothing], [End] As [Return], System As [If], _ As Threading.Thread.Sleep, _ As (_, _)) As [If]
        ' TODO: Error SkippedTokensTrivia '' tsx_cc.ExposureTime = exposure'

        ' TODO: Error SkippedTokensTrivia ''Save the using the PreStack manager if checked,'
        ' TODO: Error SkippedTokensTrivia ''  Otherwise TSX will do what TSX does.'
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Loop' at character 13698
''' 
''' 
''' Input:
'''         Loop
'''         'Save the using the PreStack manager if checked,
'''         '  Otherwise TSX will do what TSX does.
''' 
''' 
        Private [Not] As [If]
        Private ThenField13 As SaveTSXCheckBox.Checked
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'CalDB.BiasImageStore' at character 13875
''' 
''' 
''' Input:
'''             CalDB.BiasImageStore
''' 
        Private EndField18 As (_, _)
        Private tsx_ccField3 As [If] = [Nothing]
        Private EndField17 As [Return]

        Private PrivateField1 As [Sub]
        Private Function SetCCDTemperature() As [Sub]
        ' TODO: Skipped BadDirectiveTrivia        Private tsx_ccField2 As [Dim] = CreateObject("TheSkyX.ccdsoftcamera")
        ' TODO: Skipped BadDirectiveTrivia        Private tsx_ccField1 As [Dim] = CreateObject("TheSky64.ccdsoftcamera")
        ' TODO: Skipped BadDirectiveTrivia                            Private Function Connect() As [Try]
        Private ex As [Catch]
        Private Exception As [As]
        Private EndField16 As [Return]
        Private Function Text(_ As CCDTempBox.Value.ToString, _ As (_, _)) As [Try]
        Private Function ShowMethod() As vbCrLf
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'tsx_cc.TemperatureSetPoint' at character 14538
''' 
''' 
''' Input:
''' 
'''         tsx_cc.TemperatureSetPoint = 
''' 
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'System.Convert.ToDouble' at character 14567
''' 
''' 
''' Input:
''' System.Convert.ToDouble
''' 

        Private Function RegulateTemperature([Dim] As [True], Optional _ As near = Math.Abs(tsx_cc.TemperatureSetPoint() * 0.9), [While] As [Do], _ As (Math.Abs, _), _ As (tsx_cc.Temperature, _), _ As (_, _), _ As tsx_cc.TemperatureSetPoint) As (CCDTempBox.Value, _) ' TODO: Error SkippedTokensTrivia ')'
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'near' at character 14817
''' 
''' 
''' Input:
''' near)
''' 
''' 
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'CCDTempBox.ForeColor' at character 14836
''' 
''' 
''' Input:
'''             CCDTempBox.ForeColor = 
''' 
        Private Function Value(_ As tsx_cc.Temperature, System As (_, _), _ As Threading.Thread.Sleep, _ As (_, _)) As Color.Red
        ' TODO: Error SkippedTokensTrivia ''Wait one minute for the temperature to settle'
        ' TODO: Error SkippedTokensTrivia ''Blink the temperature number will waiting'
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Loop' at character 14995
''' 
''' 
''' Input:
'''         Loop
'''         'Wait one minute for the temperature to settle
'''         'Blink the temperature number will waiting
''' 
''' 
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'StatusBox.Text' at character 15117
''' 
''' 
''' Input:
'''         StatusBox.Text += "At temperature. Pausing for 1 minute to settle down" & 
''' 
        Private Function Show() As vbCrLf
        Private i As [For]
        Private [Integer] As [As] = 1 ' TODO: Error SkippedTokensTrivia '30'
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'To' at character 15244
''' 
''' 
''' Input:
''' To 30
''' 
''' 
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'CCDTempBox.ForeColor' at character 15263
''' 
''' 
''' Input:
'''             CCDTempBox.ForeColor = 
''' 
        Private Function Sleep() As Color.Yellow
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'CCDTempBox.ForeColor' at character 15376
''' 
''' 
''' Input:
'''             CCDTempBox.ForeColor = 
''' 
        Private Function Sleep() As Color.Green

        Private Function Value(_ As tsx_cc.TemperatureSetPoint, CCDTempBox As (_, _), Optional _ As ForeColor = Color.LightGreen, Optional _ As tsx_cc = [Nothing], [End] As [Return], [Private] As [Sub], RunExposures As [Sub], [If] As (_, _), [Then] As Check1.Checked, Optional _ As Check1.ForeColor = Color.Red, _ As RepeatDark, _ As (DarksCountBox.Value, _)) As [Next]
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check1.ForeColor' at character 15849
''' 
''' 
''' Input:
'''             Check1.ForeColor = 
''' 
        Private EndField15 As Color.LightGreen
        Private IfField14 As [If]
        Private ThenField12 As Check3.Checked
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check3.ForeColor' at character 15982
''' 
''' 
''' Input:
'''             Check3.ForeColor = 
''' 
        Private Function RepeatDarkMethod11(_ As DarksCountBox.Value, _ As _) As Color.Red
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check3.ForeColor' at character 16095
''' 
''' 
''' Input:
'''             Check3.ForeColor = 
''' 
        Private EndField14 As Color.LightGreen
        Private IfField13 As [If]
        Private ThenField11 As Check10.Checked
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check10.ForeColor' at character 16229
''' 
''' 
''' Input:
'''             Check10.ForeColor = 
''' 
        Private Function RepeatDarkMethod10(_ As DarksCountBox.Value, _ As _) As Color.Red
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check10.ForeColor' at character 16344
''' 
''' 
''' Input:
'''             Check10.ForeColor = 
''' 
        Private EndField13 As Color.LightGreen
        Private IfField12 As [If]
        Private ThenField10 As Check30.Checked
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check30.ForeColor' at character 16479
''' 
''' 
''' Input:
'''             Check30.ForeColor = 
''' 
        Private Function RepeatDarkMethod9(_ As DarksCountBox.Value, _ As _) As Color.Red
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check30.ForeColor' at character 16593
''' 
''' 
''' Input:
'''             Check30.ForeColor = 
''' 
        Private EndField12 As Color.LightGreen
        Private IfField11 As [If]
        Private ThenField9 As Check60.Checked
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check60.ForeColor' at character 16727
''' 
''' 
''' Input:
'''             Check60.ForeColor = 
''' 
        Private Function RepeatDarkMethod8(_ As DarksCountBox.Value, _ As _) As Color.Red
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check60.ForeColor' at character 16841
''' 
''' 
''' Input:
'''             Check60.ForeColor = 
''' 
        Private EndField11 As Color.LightGreen
        Private IfField10 As [If]
        Private ThenField8 As Check90.Checked
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check90.ForeColor' at character 16975
''' 
''' 
''' Input:
'''             Check90.ForeColor = 
''' 
        Private Function RepeatDarkMethod7(_ As DarksCountBox.Value, _ As _) As Color.Red
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check90.ForeColor' at character 17089
''' 
''' 
''' Input:
'''             Check90.ForeColor = 
''' 
        Private EndField10 As Color.LightGreen
        Private IfField9 As [If]
        Private ThenField7 As Check120.Checked
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check120.ForeColor' at character 17223
''' 
''' 
''' Input:
'''             Check120.ForeColor = 
''' 
        Private Function RepeatDarkMethod6(_ As DarksCountBox.Value, _ As _) As Color.Red
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check120.ForeColor' at character 17339
''' 
''' 
''' Input:
'''             Check120.ForeColor = 
''' 
        Private EndField9 As Color.LightGreen
        Private IfField8 As [If]
        Private ThenField6 As Check180.Checked
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check180.ForeColor' at character 17473
''' 
''' 
''' Input:
'''             Check180.ForeColor = 
''' 
        Private Function RepeatDarkMethod5(_ As DarksCountBox.Value, _ As _) As Color.Red
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check180.ForeColor' at character 17589
''' 
''' 
''' Input:
'''             Check180.ForeColor = 
''' 
        Private EndField8 As Color.LightGreen
        Private IfField7 As [If]
        Private ThenField5 As Check240.Checked
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check240.ForeColor' at character 17723
''' 
''' 
''' Input:
'''             Check240.ForeColor = 
''' 
        Private Function RepeatDarkMethod4(_ As DarksCountBox.Value, _ As _) As Color.Red
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check240.ForeColor' at character 17839
''' 
''' 
''' Input:
'''             Check240.ForeColor = 
''' 
        Private EndField7 As Color.LightGreen
        Private IfField6 As [If]
        Private ThenField4 As Check300.Checked
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check300.ForeColor' at character 17973
''' 
''' 
''' Input:
'''             Check300.ForeColor = 
''' 
        Private Function RepeatDarkMethod3(_ As DarksCountBox.Value, _ As _) As Color.Red
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check300.ForeColor' at character 18089
''' 
''' 
''' Input:
'''             Check300.ForeColor = 
''' 
        Private EndField6 As Color.LightGreen
        Private IfField5 As [If]
        Private ThenField3 As Check360.Checked
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check360.ForeColor' at character 18223
''' 
''' 
''' Input:
'''             Check360.ForeColor = 
''' 
        Private Function RepeatDarkMethod2(_ As DarksCountBox.Value, _ As _) As Color.Red
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check360.ForeColor' at character 18339
''' 
''' 
''' Input:
'''             Check360.ForeColor = 
''' 
        Private EndField5 As Color.LightGreen
        Private IfField4 As [If]
        Private ThenField2 As Check480.Checked
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check480.ForeColor' at character 18473
''' 
''' 
''' Input:
'''             Check480.ForeColor = 
''' 
        Private Function RepeatDarkMethod1(_ As DarksCountBox.Value, _ As _) As Color.Red
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check480.ForeColor' at character 18589
''' 
''' 
''' Input:
'''             Check480.ForeColor = 
''' 
        Private EndField4 As Color.LightGreen
        Private IfField3 As [If]
        Private ThenField1 As Check540.Checked
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check540.ForeColor' at character 18723
''' 
''' 
''' Input:
'''             Check540.ForeColor = 
''' 
        Private Function RepeatDarkMethod(_ As DarksCountBox.Value, _ As _) As Color.Red
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check540.ForeColor' at character 18838
''' 
''' 
''' Input:
'''             Check540.ForeColor = 
''' 
        Private EndField3 As Color.LightGreen
        Private IfField2 As [If]
        Private ThenField As Check600.Checked
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check600.ForeColor' at character 18971
''' 
''' 
''' Input:
'''             Check600.ForeColor = 
''' 
        Private Function RepeatDark(_ As DarksCountBox.Value, _ As _) As Color.Red
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'Check600.ForeColor' at character 19080
''' 
''' 
''' Input:
'''             Check600.ForeColor = 
''' 
        Private EndField2 As Color.LightGreen
        Private IfField1 As [If]
        Private [Then] As CheckOther.Checked
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'CheckOther.ForeColor' at character 19210
''' 
''' 
''' Input:
'''             CheckOther.ForeColor = 
''' 
        Private Function RepeatDark(_ As DarksCountBox.Value, _ As Int, _ As (OtherExposureBox.Value, _)) As Color.Red
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'CheckOther.ForeColor' at character 19345
''' 
''' 
''' Input:
'''             CheckOther.ForeColor = 
''' 
        Private EndField1 As Color.LightGreen
        Private IfField As [If]
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'BiasCountBox.Value' at character 19439
''' 
''' 
''' Input:
''' BiasCountBox.Value > 0 
''' 
        Private Function RepeatBias(_ As BiasCountBox.Value) As [Then]
        Private [If] As [End]

        Private EndField As [Return]

        Private PrivateField As [Sub]
        Private Function UseAutoSave() As [Sub]
        ' TODO: Skipped BadDirectiveTrivia        Private tsx_ccField As [Dim] = CreateObject("TheSkyX.ccdsoftcamera")
        ' TODO: Skipped BadDirectiveTrivia        ' TODO: Skipped BadDirectiveTrivia        Private tsx_cc As [Dim] = CreateObject("TheSky64.ccdsoftcamera") ' TODO: Error SkippedTokensTrivia '=' ' TODO: Error SkippedTokensTrivia '1'
                ''' Cannot convert IncompleteMemberSyntax, CONVERSION ERROR: Conversion for IncompleteMember not implemented, please report this issue in 'tsx_cc.AutoSaveOn' at character 19769
''' 
''' 
''' Input:
''' #End If
'''         tsx_cc.AutoSaveOn = 1
''' 
''' 
        Private [End] As [Return]

        Private [Private] As [Sub]
        Private Function DDriveRadioButton_CheckedChanged([As] As sender, _ As Object, [As] As e, _ As EventArgs) As [Sub]

        Private Function CheckedChanged(_ As [If], [Then] As (OtherDirRadioButton.Checked, _), _ As [If], _ As (DestinationFolderDialog, _), _ As (_, _), _ As ShowDialog, Optional _ As (_, _) = DialogResult.OK) As [Handles]

        Private Function DestinationDir(_ As DestinationFolderDialog, _ As (_, _), [End] As SelectedPath, [End] As [If], [Return] As [If], [Sub] As [End], [Class] As [End]) As [Then]







    End Class
End Namespace
