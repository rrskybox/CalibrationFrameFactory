using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;

namespace CalFrameFactory
{
    public enum LightSource
    {
        lsNone,
        lsFlatMan,
        lsDawn,
        lsDusk
    }

    public partial class FormCalFrameFactory : Form
    {
        public bool abortflag = false;
        public int totalreps;
        public List<int> dExpList;
        public int[] dCount;

        //Keep track of the imaging application objects
        private bool useTSX;
        private ImagingTheSky tsxApp;
        private ImagingMDL mdlApp;

        // Save folder structure pointer
        private CalibrationFileManagement CalDB;
        private FlatMan FlatControl;

        public static LogEvent StatusReportEvent;

        #region Form Initialization

        public FormCalFrameFactory()
        {
            InitializeComponent();
            // 
            CalDB = new CalibrationFileManagement();
            // Determine which application to use to take images
            Configuration cfg = new Configuration();

            // Prep the form title
            try
            {
                Text = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch
            {
                Text = " in Debug";  // probably In debug, no version info available
            }
            Text = "Calibration Factory V " + Text;

            useTSX = (cfg.ImagingApplication == Configuration.ImagingApp.TS);
            if (useTSX)
            {
                //try { tsxApp = new ImagingTheSky(); }
                //catch (Exception ex) { MessageBox.Show("TSX initialization error: " + ex.Message)}
                TSXButton.Checked = true;
                MDLButton.Checked = false;
            }
            else
            {
                //mdlApp = new ImagingMDL();
                MDLButton.Checked = true;
                TSXButton.Checked = false;
            }
            //Fill in Binnning choice
            switch (cfg.Binning)
            {
                case "1X1":
                    {
                        binningButton1x1.Checked = true;
                        break;
                    }
                case "2X2":
                    {
                        binningButton2x2.Checked = true;
                        break;
                    }
                case "3X3":
                    {
                        binningButton3x3.Checked = true;
                        break;
                    }
                case "4X4":
                    {
                        binningButton4x4.Checked = true;
                        break;
                    }
            }
            ;
            //Fill in Flat light source choice
            switch (cfg.FlatSource)
            {
                case LightSource.lsNone:
                    {

                        break;
                    }
                case LightSource.lsFlatMan:
                    {
                        break;
                    }
                case LightSource.lsDawn:
                    {
                        break;
                    }
                case LightSource.lsDusk:
                    {
                        break;
                    }
            }
            ;
            //Fill in dark exposure choices
            foreach (int exp in cfg.DarkExposures)
                switch (exp)
                {
                    case 1:
                        {
                            Check1.Checked = true;
                            break;
                        }
                    case 3:
                        {
                            Check3.Checked = true;
                            break;
                        }
                    case 10:
                        {
                            Check10.Checked = true;
                            break;
                        }
                    case 30:
                        {
                            Check30.Checked = true;
                            break;
                        }
                    case 60:
                        {
                            Check60.Checked = true;
                            break;
                        }
                    case 120:
                        {
                            Check120.Checked = true;
                            break;
                        }
                    case 180:
                        {
                            Check180.Checked = true;
                            break;
                        }
                    case 240:
                        {
                            Check240.Checked = true;
                            break;
                        }
                    case 300:
                        {
                            Check300.Checked = true;
                            break;
                        }
                    case 360:
                        {
                            Check360.Checked = true;
                            break;
                        }
                    case 480:
                        {
                            Check480.Checked = true;
                            break;
                        }
                    case 540:
                        {
                            Check540.Checked = true;
                            break;
                        }
                    case 600:
                        {
                            Check600.Checked = true;
                            break;
                        }
                    default:
                        {
                            CheckOther.Checked = true;
                            OtherExposureBox.Value = (decimal)exp;
                            break;
                        }
                }
            FlatsSubform();
            StayOnTopBox.Checked = cfg.StayOnTop;
            DateTime? latest = CalDB.FindMostRecentCalibration();
            if (latest == null)
                LibraryDateSelectionBox.Value = DateTime.Now;
            else
                LibraryDateSelectionBox.Value = (DateTime)latest;
            //Add log event generator
            StatusReportEvent = new LogEvent();
            StatusReportEvent.LogEventHandler += LogReportUpdate_Handler;

            ImagePathField.Text = cfg.ReductionGroupDirectoryPath;
            BiasCountBox.Value = cfg.BiasCount;
            DarksCountBox.Value = cfg.DarkCount;
            FlatsCountBox.Value = cfg.FlatCount;
            CCDTempBox.Value = (decimal)cfg.Temperature;
            ReferencedCheckBox.Checked = cfg.HasReferencePosition;

        }

        private void FlatsSubform()
        {
            Configuration cfg = new Configuration();
            //Fill in filter selection
            FilterFill();
            List<Filters.ActiveFilter> chkList = cfg.FlatFilters;
            //Fill in filter choices
            if (chkList.Count > 0)
            {
                if (Filters.FilterNameSet().Count > 0)
                    foreach (string f in Filters.FilterNameSet())
                        FlatFilterListBox.Items.Add(f, chkList.Exists(x => x.FilterName == f));
            }


            switch (cfg.FlatSource)
            {
                case LightSource.lsNone:
                    break;
                case LightSource.lsDawn:
                    SkyDawnSelect.Checked = true;
                    break;
                case LightSource.lsDusk:
                    SkyDuskSelect.Checked = true;
                    break;
                case LightSource.lsFlatMan:
                    //FlatControl = new FlatMan();
                    DeviceIdLabel.Text = cfg.FlatPanelDeviceName;
                    PanelSelect.Checked = true;
                    break;
                default:
                    break;
            }
            if (CheckToolKitApp("Reference Point") == null)
                ReferencePointButton.BackColor = Color.Gray;

            if (cfg.FlatTargetADU != 0) { FlatsTargetADU.Value = cfg.FlatTargetADU; }
            else cfg.FlatTargetADU = (int)FlatsTargetADU.Value;
            if (cfg.FlatCount != 0)
                FlatsCountBox.Value = cfg.FlatCount;
            else
                cfg.FlatCount = (int)FlatsCountBox.Value;
            if (cfg.FlatInitialBrightness != 0)
                FlatManBrightnessNum.Value = cfg.FlatInitialBrightness;
            else
                cfg.FlatInitialBrightness = (int)FlatManBrightnessNum.Value;
            if (cfg.FlatInitialExposure != 0)
                FlatManExposureNum.Value = (decimal)cfg.FlatInitialExposure;
            else
                cfg.FlatInitialExposure = (int)FlatManExposureNum.Value;
            if (cfg.IsPortableFlatMan)
                FlatManManualSetupCheckbox.Checked = cfg.IsPortableFlatMan;
            else
                cfg.IsPortableFlatMan = FlatManManualSetupCheckbox.Checked;
        }

        #endregion

        #region commands

        private void StartButton_Click(object sender, EventArgs e)
        {
            //Prompt with message about dome and telescope pre initialization
            Configuration cfg = new Configuration();
            LogEvent lg = new LogEvent();
            if (cfg.FlatSource != LightSource.lsFlatMan)
                MessageBox.Show("For sky flats, if a dome is in use then the shutter should be open " +
                                "and dome tracking connected to telescope.");
            else
                MessageBox.Show("For panel flats, dome should be closed and dome tracking off or disconnected.");
            StartButton.BackColor = Color.DarkRed;
            //Change session date to today, if needed
            CalDB.SetCalibrationDate(DateTime.Now);
            double camTemp = cfg.Temperature;
            lg.LogIt("Setting camera temperature to " + camTemp.ToString() + " degrees C");
            if (useTSX)
            {
                tsxApp.Connect();
                tsxApp.SetCCDTemperature(camTemp);
                double near = Math.Abs(camTemp * 0.9);
                if (near == 0)
                    near = .5;
                while ((Math.Abs(tsxApp.GetCCDTemperature() - camTemp)) > near)
                {
                    CCDTempBox.ForeColor = Color.DarkRed;
                    CCDTempBox.Value = (decimal)tsxApp.GetCCDTemperature();
                    System.Threading.Thread.Sleep(1000);
                }
            }
            else
            {
                mdlApp.Connect();
                mdlApp.SetCCDTemperature(camTemp);
                double near = Math.Abs(camTemp * 0.9);
                if (near == 0)
                    near = .5;
                while ((Math.Abs(mdlApp.GetCCDTemperature() - camTemp)) > near)
                {
                    CCDTempBox.ForeColor = Color.DarkRed;
                    CCDTempBox.Value = (decimal)mdlApp.GetCCDTemperature();
                    System.Threading.Thread.Sleep(1000);
                }

            }
            CCDTempBox.Value = (decimal)camTemp;
            CCDTempBox.ForeColor = Color.Green;

            RunExposures();
            StartButton.BackColor = Color.SpringGreen;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (useTSX)
            {
                tsxApp.CloseUp();
                //Close TheSky
                Process[] PWIFind = Process.GetProcessesByName("TheSky64");
                Thread.Sleep(1000);
                if (PWIFind.Length > 0) PWIFind[0].Kill();
            }
            else
            {
                mdlApp.CloseUp();
                //Close MDL
                Process[] PWIFind = Process.GetProcessesByName("MDL");
                Thread.Sleep(1000);
                if (PWIFind.Length > 0) PWIFind[0].Kill();
            }
            //close flatman, if enabled
            if (FlatControl != null)
            {
                FlatControl.Light = false;
                FlatControl = null;
            }
            Close();
        }

        private void AbortButton_Click(object sender, EventArgs e)
        {
            // Set abort flag
            abortflag = true;
            return;
        }

        private void CreateLibraryButton_Click(object sender, EventArgs e)
        {
            //Set calibration date from picked date
            //First close TheSky
            Process[] test = Process.GetProcesses();
            Process[] PWIFind = Process.GetProcessesByName("TheSky64");
            Thread.Sleep(1000);
            if (PWIFind.Length > 0) PWIFind[0].Kill();

            CreateLibraryButton.BackColor = Color.DarkRed;
            Show(); System.Windows.Forms.Application.DoEvents();
            CalDB.SetCalibrationDate(LibraryDateSelectionBox.Value);
            ReductionGroup rgl = new ReductionGroup();
            rgl.Generate(CalDB);
            Thread.Sleep(1000);

            //Reopen ImagingApp
            Configuration cfg = new Configuration();
            bool useTSX = (cfg.ImagingApplication == Configuration.ImagingApp.TS);
            ImagingTheSky tsxApp = new ImagingTheSky();
            ImagingMDL mdlApp = new ImagingMDL();
            tsxApp = null;
            mdlApp = null;

            CreateLibraryButton.BackColor = Color.SpringGreen;
        }

        #endregion

        #region Imaging Operations

        private void RunExposures()
        {
            Configuration cfg = new Configuration();
            LogEvent lg = new LogEvent();
            // Determine which application to use to take images
            bool useTSX = (cfg.ImagingApplication == Configuration.ImagingApp.TS);
            //ImagingTheSky tsxApp = new ImagingTheSky();
            //ImagingMDL imagingMDL = new ImagingMDL();
            if (useTSX)
                tsxApp.SetBinning(cfg.Binning);
            else
                mdlApp.SetBinning(cfg.Binning);

            //Run a loop such that bias/darks are captured until the sky flats (dawn/dusk) start time.
            //  Then run the flats as long as possible,
            //  Then return to finish Bias and Darks

            //Make list for dark exposures and their count
            dExpList = cfg.DarkExposures;
            dCount = new int[dExpList.Count];
            for (int i = 0; i < dExpList.Count; i++)
                dCount[i] = (int)DarksCountBox.Value;
            do
            {
                //Bias Frames if some left to do
                if ((int)BiasCountBox.Value > 0)
                {
                    binningButton1x1.ForeColor = Color.Red;
                    BiasFrameLoop();
                    if (BiasCountBox.Value == 0) lg.LogIt("All Bias frames completed");
                    binningButton1x1.ForeColor = Color.Green;
                }
                //Dark Frames if some left to do
                DarksCountBox.ForeColor = Color.Red;
                for (int i = 0; i < dExpList.Count; i++)
                {
                    DarkCheckBoxToggle(dExpList[i], true);
                    DarkFrameLoop(i);
                    DarkCheckBoxToggle(dExpList[i], false);
                    lg.LogIt("Dark frame + " + dExpList[i].ToString("0") + "seconds completed");
                }
                DarksCountBox.Value = 0;
                lg.LogIt("All Dark frames completed");

                //Flat Frames until done or exposure too long (i.e. count may not go to zero)
                if (FlatsCountBox.Value > 0)
                {
                    FlatsCountBox.ForeColor = Color.Red;
                    FlatMan FlatControl = new FlatMan(cfg.FlatPanelDeviceName);

                    if (ReferencedCheckBox.Checked)
                    {
                        lg.LogIt("Slewing telescope to MyFlatField reference point and parking");
                        FlatControl.FlatManStage();
                    }

                    if (PanelSelect.Checked)
                    {
                        PanelFlatFrameLoop((int)FlatsCountBox.Value, cfg.FlatFilters);
                    }
                    else
                        SkyFlatFrameLoop((int)FlatsCountBox.Value, cfg.FlatFilters);
                    if (FlatsCountBox.Value == 0) lg.LogIt("All Flat frames completed");
                }
                else
                    lg.LogIt("No Flat frames to image");

            } while (BiasCountBox.Value > 0 || DarksCountBox.Value > 0 || FlatsCountBox.Value >0);

            FlatsCountBox.ForeColor = Color.Green;
        }

        private void BiasFrameLoop()
        {
            // This is the repeat loop for a given exposure repetitions
            Configuration cfg = new Configuration();
            LogEvent lg = new LogEvent();
            // Determine which application to use to take images
            bool useTSX = (cfg.ImagingApplication == Configuration.ImagingApp.TS);

            const double biasexposure = 0.001d;
            // Change the form count box color
            BiasCountBox.ForeColor = Color.Red;
            // Set the count on the form
            for (int i = 0; i < (int)BiasCountBox.Value; i++)
            {
                lg.LogIt("Imaging Bias # " + i.ToString() + " at " + cfg.Binning.ToString() + " binning");
                if (useTSX)
                    tsxApp.ImageBias(biasexposure, CalDB);
                else
                    mdlApp.ImageBias(biasexposure, CalDB);
                if (abortflag)
                {
                    return;
                }
                // Decrement count
                BiasCountBox.Value -= 1;
                if (CheckSkyFlatStart(SkyTimePicker.Value))
                    break;
            }
            //BiasCountBox.Value = (decimal)totalreps;
            // Change the form count box color
            BiasCountBox.ForeColor = Color.Green;
            return;
        }

        private void DarkFrameLoop(int dIndex)
        {
            // This is the repeat loop for a given exposure repetitions
            Configuration cfg = new Configuration();
            // Determine which application to use to take images
            bool useTSX = (cfg.ImagingApplication == Configuration.ImagingApp.TS);

            int reps = dCount[dIndex];
            // Change the form count box color
            DarksCountBox.ForeColor = Color.DarkRed;
            // Set the count on the form
            for (int i = 0; i < reps; i++)
            {
                LogEvent lg = new LogEvent();
                lg.LogIt("Imaging Dark # " + i.ToString() + " at " + cfg.Binning.ToString() + " binning for " + dExpList[dIndex].ToString() + " seconds");
                if (useTSX)
                    tsxApp.ImageDark(dExpList[dIndex], CalDB);
                else
                    mdlApp.ImageDark(dExpList[dIndex], CalDB);
                dCount[dIndex] -= 1;
                if (abortflag)
                {
                    return;
                }
                // Decrement count
                if (CheckSkyFlatStart(SkyTimePicker.Value))
                    break;
            }
            //DarksCountBox.Value = (decimal)totalreps;
            // Change the form count box color
            DarksCountBox.ForeColor = Color.Green;
            return;
        }

        private void PanelFlatFrameLoop(int reps, List<Filters.ActiveFilter> afList)
        {
            // This is the repeat loop for a given exposure repetitions
            int MaxBrightness = 100;
            int MinBrightness = 0;

            Configuration cfg = new Configuration();
            LogEvent lg = new LogEvent();
            // Determine which application to use to take images
            bool useTSX = (cfg.ImagingApplication == Configuration.ImagingApp.TS);

            FlatControl = new FlatMan(cfg.FlatPanelDeviceName);
            if (FlatControl == null)
            {
                lg.LogIt("Attempt to open flat panel device failed.");
                return;
            }
            totalreps = 0;
            if (reps <= 0)
                return;
            // Change the form count box color
            FlatsCountBox.ForeColor = Color.DarkRed;
            //Turn on Fltaman
            FlatControl.Light = true;
            FlatControl.Bright = cfg.FlatInitialBrightness;
            foreach (Filters.ActiveFilter af in afList)
            {
                //Determine exposure
                lg.LogIt("Adjusting flat panel brightness to achieve " + cfg.FlatTargetADU.ToString() + " at " + cfg.Binning.ToString() + " binning for " + Filters.LookUpFilterName(af.FilterIndex) + " filter");
                double brightness = FlatManBrightnessCalibration(af.FilterIndex, cfg.FlatInitialExposure, cfg.FlatInitialBrightness, cfg.Binning, cfg.FlatTargetADU);
                if ((brightness >= MaxBrightness) || (brightness <= MinBrightness))
                {
                    lg.LogIt("Necessary exposure is too short or too long.  Aborting Flat imaging");
                    break;
                }
                cfg.FlatInitialBrightness = (int)brightness;
                FlatsCountBox.Value = reps;
                // Set the count on the form
                for (int i = 0; i < reps; i++)
                {
                    lg.LogIt("Imaging Flat # " + i.ToString() + " at " + cfg.Binning.ToString() + " binning for " + Filters.LookUpFilterName(af.FilterIndex) + " filter");
                    if (useTSX)
                        tsxApp.ImageFlat(cfg.FlatInitialExposure, af.FilterIndex, CalDB);
                    else
                        mdlApp.ImageFlat(cfg.FlatInitialExposure, af.FilterIndex, CalDB);
                    if (abortflag)
                    {
                        return;
                    }
                    if (useTSX)
                        CalDB.FlatImageStoreTSX(tsxApp.tsx_image, af.FilterName);
                    else
                        CalDB.FlatImageStoreMDL(mdlApp.mdl_app, af.FilterName);
                    // Decrement count
                    --FlatsCountBox.Value;
                    ++totalreps;
                }
            }
            lg.LogIt("**** Generated " + totalreps.ToString() + " panel flat frames ****");
            //FlatsCountBox.Value = (decimal)reps;
            //If FlatMan has been chosen for flats, make sure the panel is turned off
            if (cfg.FlatSource == LightSource.lsFlatMan)
                FlatControl.Light = false;
            // Change the form count box color
            FlatsCountBox.ForeColor = Color.Green;
            return;
        }

        private void SkyFlatFrameLoop(int reps, List<Filters.ActiveFilter> afList)
        {
            // This is the repeat loop for a given adu repetitions
            Configuration cfg = new Configuration();
            LogEvent lg = new LogEvent();
            // Determine which application to use to take images
            bool useTSX = (cfg.ImagingApplication == Configuration.ImagingApp.TS);

            int MinExpTime = 1;
            int MaxExpTime = 60;
            double tgtADU = cfg.FlatTargetADU;
            int MinADUVal = (int)(tgtADU * 0.8);
            int MaxADUVal = (int)(tgtADU * 1.2);

            // Change the form count box color
            FlatsCountBox.ForeColor = Color.DarkRed;

            foreach (Filters.ActiveFilter af in afList)
            {
                //Determine exposure
                lg.LogIt("Adjusting exposure to achieve " + cfg.FlatTargetADU.ToString() + " at " + cfg.Binning.ToString() + " binning for " + Filters.LookUpFilterName(af.FilterIndex) + " filter");
                double exposure = SkyExposureCalibration(af.FilterIndex, cfg.FlatInitialExposure, cfg.Binning, cfg.FlatTargetADU);
                //Check for insufficient or excessive exposure time
                //  depending on dawn or dusk
                //  opt out if so
                if ((exposure >= MaxExpTime) || (exposure <= MinExpTime))
                {
                    lg.LogIt("Necessary exposure is too short or too long.  Aborting Flat imaging");
                    break;
                }
                //Set initial exposure to current exposure
                cfg.FlatInitialExposure = (int)exposure;
                FlatsCountBox.Value = reps;
                // Set the count on the form
                for (int i = 0; i < reps; i++)
                {
                    if (useTSX)
                        tsxApp.ImageFlat(exposure, af.FilterIndex, CalDB);
                    else
                        mdlApp.ImageFlat(exposure, af.FilterIndex, CalDB);
                    if (abortflag)
                    {
                        break;
                    }
                    if (useTSX)
                        CalDB.FlatImageStoreTSX(tsxApp.tsx_image, af.FilterName);
                    else
                        CalDB.FlatImageStoreMDL(mdlApp.mdl_app, af.FilterName);
                    // Decrement count
                    --FlatsCountBox.Value;
                    ++totalreps;
                }
            }
            lg.LogIt("**** Generated " + totalreps.ToString() + " sky flat frames ****");
            // Change the form count box color
            FlatsCountBox.ForeColor = Color.Green;
            return;
        }

        private bool CheckSkyFlatStart(DateTime skystarttime)
        {
            if (PanelSelect.Checked)
                return false;
            else if ((skystarttime > DateTime.Now) && (skystarttime < (DateTime.Now + TimeSpan.FromHours(3))))
                return true;
            else
                return false;
        }

        private int FlatManBrightnessCalibration(int filter, double exposure, int startingBrightness, string binning, int targetADU)
        {
            //Looks for brightness setting that produces something close (80%) to the target ADU at the given exposure
            //This algoritm assumes that any ADU above the target ADU is in a non-linear curve, but is linear below the target ADU.
            //So, it tries to approach the optimum brightness from below using a linear calculation.  It also assumes that the 
            //maximum brightness level is 255.
            //
            //The brightness setting starts with the currently configured brightness.
            //The exposure setting is fixed at the curently configured flats exposure setting.
            //1. Take flat image with given filter at exposure and initial brightness level
            //2. It the currentADU is within 20% of the targetADU, and it is less than the targetADU, then return that brightness level
            //3. Otherwise, 

            Configuration cfg = new Configuration();
            // Determine which application to use to take images
            bool useTSX = (cfg.ImagingApplication == Configuration.ImagingApp.TS);
            LogEvent lg = new LogEvent();

            int currentADU = 0;
            int currentBrightness = startingBrightness;

            //
            //Neither the exposure nor the brightness is linear -- this is a problem
            lg.LogIt("Calibrating FlatMan brightness for Filter " + filter.ToString());
            //Try no more than 8 times to get a good brightness
            for (int i = 0; i < 8; i++)
            {
                lg.LogIt("Brightness set to " + currentBrightness.ToString("0"));
                //initially set brightness to the starting brightness, and wait a second for the FlatMan
                FlatControl.Bright = currentBrightness;
                System.Threading.Thread.Sleep(500);
                //Get the ADU of a sample image (subframe)
                if (useTSX)
                    currentADU = tsxApp.TakeFlatSample(filter, exposure, binning);
                else
                    currentADU = mdlApp.TakeFlatSample(filter, exposure, binning);
                //If ADU is not close enough (greater than 20%) 
                //  increase or decrease the brightness accordingly
                //  Otherwise, we're done with it
                if (!(CloseEnough(targetADU, currentADU, 20.0)))
                {
                    currentBrightness = AdjustedBrightness(targetADU, currentADU, currentBrightness);
                }
                else { break; }
            }
            lg.LogIt("FlatMan brightness calibrated to " + currentBrightness.ToString() + " at " + currentADU.ToString() + " ADU");
            return (currentBrightness);
        }

        private double SkyExposureCalibration(int filter, double startingExposure, string binning, int targetADU)
        {
            //Looks for exposure setting that produces something close (80%) to the ta
            // Determine which application to use to take images
            Configuration cfg = new Configuration();
            bool useTSX = (cfg.ImagingApplication == Configuration.ImagingApp.TS);
            LogEvent lg = new LogEvent();

            int currentADU = 0;
            double currentExposure = startingExposure;
            //
            //Neither the exposure nor the brightness is linear -- this is a problem
            lg.LogIt("Calibrating FlatMan exposure for Filter " + filter.ToString());
            //Try no more than 8 times to get a good brightness
            for (int i = 0; i < 8; i++)
            {
                lg.LogIt("Exposure set to " + currentExposure.ToString("0.00"));
                //Get the ADU of a sample image (subframe)
                if (useTSX)
                    currentADU = tsxApp.TakeFlatSample(filter, currentExposure, binning);
                else
                    currentADU = mdlApp.TakeFlatSample(filter, currentExposure, binning);
                //If ADU is not close enough (greater than 20%) or is greater than target then
                //  increase or decrease the brightness accordingly
                //  Otherwise, we're done with it
                if (!(CloseEnough(targetADU, currentADU, 20.0)) || (currentADU > targetADU))
                {
                    currentExposure = AdjustedExposure(targetADU, currentADU, currentExposure);
                }
                else { break; }
            }
            lg.LogIt("FlatMan brightness calibrated to " + currentExposure.ToString() + " at " + currentADU.ToString() + " ADU");
            return (currentExposure);
        }

        private int AdjustedBrightness(double targetADU, double currentADU, int currentBrightness)
        {
            //Calculates a new brightness level based on the current ADU and current Brightness  
            //  that would produce the targetADU assuming linearity below the target ADU and nonlinearity above.
            //If tested ADU is greater than target ADU, then return half the brightness.
            //  Otherwise compute an adjusted brightness based on linear slope
            int maxBrightness = 255;
            if (currentADU > targetADU)
                return currentBrightness / 2;
            else
                // return (int)Math.Min(maxBrightness, (currentBrightness * (targetADU / currentADU)) * .9);
                return (int)Math.Min(maxBrightness, (currentBrightness * Math.Sqrt(targetADU / currentADU)));
        }

        private int AdjustedExposure(double targetADU, double currentADU, double currentExposure)
        {
            //Calculates a new exposure level based on the current ADU and current exposure
            //  that would produce the targetADU assuming linearity.
            //Linearity should be true if the current ADU is less than the target ADU.  If it is not
            //  then the result will probably overshoot the target (in the negative direction) but a
            //  test should be close.
            //Maxes out at 100, I think

            return (int)Math.Min(60, (currentExposure * (targetADU / currentADU)));
        }

        public bool CloseEnough(double testval, double targetval, double percentnear)
        {
            //Cute little method for determining if a value is withing a certain percentatge of
            // another value.
            //testval is the value under consideration
            //targetval is the value to be tested against
            //npercentnear is how close (in percent of target val, i.e. x100) the two need to be within to test true
            // otherwise returns false

            if ((Math.Abs(targetval - testval)) <= (Math.Abs((targetval * percentnear / 100))))
            { return true; }
            else
            { return false; }
        }


        #endregion

        #region general configuration

        //private void ImageFolderButton_Click(object sender, EventArgs e)
        //{
        //    using (FolderBrowserDialog fbd = new FolderBrowserDialog())
        //    {
        //        DialogResult dr = fbd.ShowDialog();
        //        {
        //            if (dr == DialogResult.OK)
        //            {
        //                Configuration cfg = new Configuration();
        //                cfg.ImageDirectoryPath = fbd.SelectedPath;
        //                ImagePathField.Text = fbd.SelectedPath;
        //            }
        //        }
        //    }
        //}

        private void CCDTempBox_ValueChanged(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            cfg.Temperature = (int)CCDTempBox.Value;
        }

        private void StayOnTopBox_CheckedChanged(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            if (StayOnTopBox.Checked)
                TopMost = true;
            else
                TopMost = false;
            cfg.StayOnTop = TopMost;
        }

        #endregion

        #region binning

        private void binningButton1x1_CheckedChanged(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            if (binningButton1x1.Checked)
                cfg.Binning = "1X1";
        }

        private void binningButton2x2_CheckedChanged(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            if (binningButton2x2.Checked)
                cfg.Binning = "2X2";
        }

        private void binningButton3x3_CheckedChanged(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            if (binningButton3x3.Checked)
                cfg.Binning = "3X3";
        }

        private void binningButton4x4_CheckedChanged(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            if (binningButton4x4.Checked)
                cfg.Binning = "4X4";
        }

        #endregion

        #region bias frames

        private void BiasCountBox_ValueChanged(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            cfg.BiasCount = (int)BiasCountBox.Value;
        }

        #endregion

        #region dark frames

        public void SaveDarksExposures(int exp)
        {
            Configuration cfg = new Configuration();
            List<int> dList = cfg.DarkExposures;

            if (!dList.Contains(exp))
            {
                dList.Add(exp);
                cfg.DarkExposures = dList;
            }
        }

        public void ClearDarksExposures(int exp)
        {
            Configuration cfg = new Configuration();
            List<int> dList = cfg.DarkExposures;

            if (dList.Contains(exp))
            {
                dList.Remove(exp);
                cfg.DarkExposures = dList;
            }
        }

        private void DarksCountBox_ValueChanged(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            cfg.DarkCount = (int)DarksCountBox.Value;
        }

        private void Check1_CheckedChanged(object sender, EventArgs e) => CacheDarkSettings();

        private void Check3_CheckedChanged(object sender, EventArgs e) => CacheDarkSettings();

        private void Check10_CheckedChanged(object sender, EventArgs e) => CacheDarkSettings();

        private void Check30_CheckedChanged(object sender, EventArgs e) => CacheDarkSettings();

        private void Check60_CheckedChanged(object sender, EventArgs e) => CacheDarkSettings();

        private void Check90_CheckedChanged(object sender, EventArgs e) => CacheDarkSettings();

        private void Check120_CheckedChanged(object sender, EventArgs e) => CacheDarkSettings();

        private void Check180_CheckedChanged(object sender, EventArgs e) => CacheDarkSettings();

        private void Check240_CheckedChanged(object sender, EventArgs e) => CacheDarkSettings();

        private void Check300_CheckedChanged(object sender, EventArgs e) => CacheDarkSettings();

        private void Check360_CheckedChanged(object sender, EventArgs e) => CacheDarkSettings();

        private void Check480_CheckedChanged(object sender, EventArgs e) => CacheDarkSettings();

        private void Check540_CheckedChanged(object sender, EventArgs e) => CacheDarkSettings();

        private void Check600_CheckedChanged(object sender, EventArgs e) => CacheDarkSettings();

        private void CheckOther_CheckedChanged(object sender, EventArgs e) => CacheDarkSettings();

        public void CacheDarkSettings()
        {
            if (Check1.Checked) SaveDarksExposures(1);
            else ClearDarksExposures(1);
            if (Check3.Checked) SaveDarksExposures(3);
            else ClearDarksExposures(3);
            if (Check10.Checked) SaveDarksExposures(10);
            else ClearDarksExposures(10);
            if (Check30.Checked) SaveDarksExposures(30);
            else ClearDarksExposures(30);
            if (Check60.Checked) SaveDarksExposures(60);
            else ClearDarksExposures(60);
            if (Check120.Checked) SaveDarksExposures(120);
            else ClearDarksExposures(120);
            if (Check180.Checked) SaveDarksExposures(180);
            else ClearDarksExposures(180);
            if (Check240.Checked) SaveDarksExposures(240);
            else ClearDarksExposures(240);
            if (Check300.Checked) SaveDarksExposures(300);
            else ClearDarksExposures(300);
            if (Check360.Checked) SaveDarksExposures(360);
            else ClearDarksExposures(360);
            if (Check480.Checked) SaveDarksExposures(480);
            else ClearDarksExposures(480);
            if (Check540.Checked) SaveDarksExposures(540);
            else ClearDarksExposures(540);
            if (Check600.Checked) SaveDarksExposures(600);
            else ClearDarksExposures(600);
            if (CheckOther.Checked) SaveDarksExposures((int)OtherExposureBox.Value);
            else ClearDarksExposures((int)OtherExposureBox.Value);
        }

        #endregion

        #region flat frames

        private void ReferencePointButton_Click(object sender, EventArgs e)
        {
            if (!LaunchToolKitApp("Reference Point"))
            {
                DialogResult mbResult = MessageBox.Show("Reference Point tool not installed. \r\n " +
                    "To install, download and extract the zip file (32 or 64 bit) from \r\n " +
                    "https://github.com/rrskybox/ReferencePoint/tree/master/publish" + "\r\n" +
                    "and run the setup.exe file.\r\n" +
                    "Would you like to open that link now?", "Tool Not Installed", MessageBoxButtons.YesNo);
                if (mbResult == DialogResult.Yes)
                    System.Diagnostics.Process.Start("https://github.com/rrskybox/ReferencePoint/tree/master/publish");
            }
        }

        private void ChooseButton_Click(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            cfg.FlatPanelDeviceName = FlatMan.ChooseFlatManDevice();
            DeviceIdLabel.Text = cfg.FlatPanelDeviceName;
        }

        private void FlatManBrightnessNum_ValueChanged(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            cfg.FlatInitialBrightness = (int)FlatManBrightnessNum.Value;
        }

        private void FlatManExposureNum_ValueChanged(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            cfg.FlatInitialExposure = (int)FlatManExposureNum.Value;
        }

        private void FlatsTargetADU_ValueChanged(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            cfg.FlatTargetADU = (int)FlatsTargetADU.Value;
        }

        private void FlatsCountBox_ValueChanged(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            cfg.FlatCount = (int)FlatsCountBox.Value;
        }

        private void FlatManManualSetupCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            cfg.IsPortableFlatMan = FlatManManualSetupCheckbox.Checked;
        }

        private void FlatFilterListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterFill();
        }

        private void FilterFill()
            {
            
            Configuration cfg = new Configuration();
            //If the list is empty, try to fill in filter selection
            if (FlatFilterListBox.Items.Count == 0)
            {
                List<Filters.ActiveFilter> chkList = cfg.FlatFilters;
                //Fill in filter choices

                if (Filters.FilterNameSet().Count > 0)
                    foreach (string f in Filters.FilterNameSet())
                        FlatFilterListBox.Items.Add(f, chkList.Exists(x => x.FilterName == f));
                else
                {
                    MessageBox.Show("No Filters have been configured in TSX/MDL.  " +
                    "Set up filters and restart Calibration Frame Factory.  " +
                    "Calibration Frame Factory will exit.",
                    "Initialization Error");
                    return;
                }
            }
            else
            {
                List<Filters.ActiveFilter> fList = new List<Filters.ActiveFilter>();
                foreach (string fName in FlatFilterListBox.CheckedItems)
                {
                    int i = (int)Filters.LookUpFilterIndex(fName);
                    fList.Add(new Filters.ActiveFilter { FilterName = fName, FilterIndex = (int)Filters.LookUpFilterIndex(fName) });
                }
                cfg.FlatFilters = fList;
            }
        }

        private void SkyDawnSelect_CheckedChanged(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            cfg.FlatSource = LightSource.lsDawn;
            SkyTimePicker.Value = cfg.AstroTwilightEnd();
            DeviceIdLabel.Text = "ASCOM Device ID";
        }

        private void SkyDuskSelect_CheckedChanged(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            cfg.FlatSource = LightSource.lsDusk;
            SkyTimePicker.Value = cfg.AstroTwilightStart();
            DeviceIdLabel.Text = "ASCOM Device ID";
        }

        private void PanelSelectButton_CheckedChanged(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            cfg.FlatSource = LightSource.lsFlatMan;
            DeviceIdLabel.Text = cfg.FlatPanelDeviceName;
        }

        private void ReferencedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //Use MyFlatField
            Configuration cfg = new Configuration();
            //Check to see if MyFlatField has been created in TSX,
            //  if not, then don't allow check, otherwise save accordingly
            if (ReferencedCheckBox.Checked)
            {
                FlatMan.Target target = FlatMan.FindTarget("MyFlatField");
                if (target == null)
                    ReferencedCheckBox.Checked = false;
            }
            cfg.HasReferencePosition = ReferencedCheckBox.Checked;
        }

        private void DarkCheckBoxToggle(int checkBoxNumber, bool red)
        {
            switch (checkBoxNumber)
            {
                case 1:
                    {
                        if (red) Check1.ForeColor = Color.DarkRed;
                        else Check1.ForeColor = Color.LightGreen;
                        break;
                    }
                case 3:
                    {
                        if (red) Check3.ForeColor = Color.DarkRed;
                        else Check3.ForeColor = Color.LightGreen;
                        break;
                    }
                case 10:
                    {
                        if (red) Check10.ForeColor = Color.DarkRed;
                        else Check10.ForeColor = Color.LightGreen;
                        break;
                    }
                case 30:
                    {
                        if (red) Check30.ForeColor = Color.DarkRed;
                        else Check30.ForeColor = Color.LightGreen;
                        break;
                    }
                case 60:
                    {
                        if (red) Check60.ForeColor = Color.DarkRed;
                        else Check60.ForeColor = Color.LightGreen;
                        break;
                    }
                case 90:
                    {
                        if (red) Check90.ForeColor = Color.DarkRed;
                        else Check90.ForeColor = Color.LightGreen;
                        break;
                    }
                case 120:
                    {
                        if (red) Check1.ForeColor = Color.DarkRed;
                        else Check1.ForeColor = Color.LightGreen;
                        break;
                    }
                case 180:
                    {
                        if (red) Check180.ForeColor = Color.DarkRed;
                        else Check180.ForeColor = Color.LightGreen;
                        break;
                    }
                case 240:
                    {
                        if (red) Check240.ForeColor = Color.DarkRed;
                        else Check240.ForeColor = Color.LightGreen;
                        break;
                    }
                case 300:
                    {
                        if (red) Check300.ForeColor = Color.DarkRed;
                        else Check300.ForeColor = Color.LightGreen;
                        break;
                    }
                case 360:
                    {
                        if (red) Check360.ForeColor = Color.DarkRed;
                        else Check360.ForeColor = Color.LightGreen;
                        break;
                    }
                case 480:
                    {
                        if (red) Check480.ForeColor = Color.DarkRed;
                        else Check300.ForeColor = Color.LightGreen;
                        break;
                    }
                case 540:
                    {
                        if (red) Check1.ForeColor = Color.DarkRed;
                        else Check480.ForeColor = Color.LightGreen;
                        break;
                    }
                case 600:
                    {
                        if (red) Check600.ForeColor = Color.DarkRed;
                        else Check600.ForeColor = Color.LightGreen;
                        break;
                    }
                default:
                    {
                        if (red) CheckOther.ForeColor = Color.DarkRed;
                        else CheckOther.ForeColor = Color.LightGreen;
                        break;
                    }
            }
        }

        #endregion

        #region Utility

        public void LogReportUpdate_Handler(object sender, LogEvent.LogEventArgs e)
        {
            StatusBox.AppendText(e.LogEntry + "\r\n");
            this.Show();
            return;
        }

        private bool LaunchToolKitApp(string toolName)
        {
            //Launches the specified toolName 
            //  returns true if successful, false otherwise
            string toolPath = CheckToolKitApp(toolName);
            if (toolPath != null)
            {
                //Save state and turn on OnTop if on
                StayOnTopBox.Checked = false;
                Process pSystemExe = new Process();
                pSystemExe.StartInfo.FileName = toolPath;
                pSystemExe.Start();
                return true;
            }
            else return false;
        }

        private string CheckToolKitApp(string toolName)
        {
            //builds file path to toolName.  returns empty if tool isn't installed
            string ttdir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\Start Menu\\Programs\\TSXToolkit\\TSXToolkit";
            string ifbPath = ttdir + "\\" + toolName + ".appref-ms";
            if (System.IO.File.Exists(ifbPath))
                return ifbPath;
            else
                return null;
        }


        #endregion

        private void TSXButton_CheckedChanged(object sender, EventArgs e)
        {
            // If TSX is selected, then set the imaging application to TSX
            //  else set the imaging application to MDL
            if (TSXButton.Checked)
            {
                Configuration cfg = new Configuration();
                cfg.ImagingApplication = Configuration.ImagingApp.TS;
                MDLButton.Checked = false;
                try
                {
                    tsxApp = null;
                    tsxApp = new ImagingTheSky();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error initializing TheSky application: " + ex.Message);
                    TSXButton.Checked = false;
                    return;
                }
            }
        }

        private void MDLButton_CheckedChanged(object sender, EventArgs e)
        {
            // If MDL is selected, then set the imaging application to MDL
            //  else set the imaging application to TSX
            if (MDLButton.Checked)
            {
                Configuration cfg = new Configuration();
                cfg.ImagingApplication = Configuration.ImagingApp.MDL;
                TSXButton.Checked = false;
                try
                {
                    mdlApp = null;
                    mdlApp = new ImagingMDL();
                    //Wait for filter wheel -- it's slow
                    Thread.Sleep(5000);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error initializing MDL application: " + ex.Message);
                    MDLButton.Checked = false;
                    return;
                }
            }
        }
    }
}