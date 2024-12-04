using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using TheSky64Lib;

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
        public int autosavestate;
        public ccdsoftImageFrame framestate;
        public double delaystate;
        public int binningXstate;
        public int binningYstate;
        public double exposurestate;
        public double settempstate;
        public List<int> dExpList;
        public int[] dCount;

        // Save folder structure pointer
        public CalibrationFileManagement CalDB;
        public FlatMan FlatControl;

        public static LogEvent StatusReportEvent;

        #region Form Initialization

        public FormCalFrameFactory()
        {
            InitializeComponent();
            // 
            CalDB = new CalibrationFileManagement();

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
            ccdsoftCamera tsx_cc = new ccdsoftCamera();
            try
            {
                tsx_cc.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Camera has been selected in TSX.  " +
                                "Choose a camera and restart calibration.  " +
                                "Calibration Frame Factory will exit.",
                                "Initialization Error");
                Close();
                return;
            }
            // TSX camera simulator throws an exception on AutoSave so handle it
            try
            {
                autosavestate = tsx_cc.AutoSaveOn;
            }
            catch (Exception ex)
            {
                // Just breeze on by
            }
            //Save current tsx camera settings
            delaystate = tsx_cc.Delay;
            binningXstate = tsx_cc.BinX;
            binningYstate = tsx_cc.BinY;
            exposurestate = tsx_cc.ExposureTime;
            settempstate = tsx_cc.TemperatureSetPoint;
            framestate = tsx_cc.Frame;
            //Add log event generator
            StatusReportEvent = new LogEvent();
            StatusReportEvent.LogEventHandler += LogReportUpdate_Handler;

            Configuration cfg = new Configuration();
            ImagePathField.Text = cfg.ImageDirectoryPath;
            BiasCountBox.Value = cfg.BiasCount;
            DarksCountBox.Value = cfg.DarkCount;
            FlatsCountBox.Value = cfg.FlatCount;
            CCDTempBox.Value = (decimal)cfg.Temperature;
            ReferencePointBox.Checked = cfg.HasReferencePosition;

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
            };
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
            };
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
            //Fill in filter selection
            List<Filters.ActiveFilter> chkList = cfg.FlatFilters;
            //Fill in filter choices

            if (Filters.FilterNameSet().Length > 0)
                foreach (string f in Filters.FilterNameSet())
                    FlatFilterListBox.Items.Add(f, chkList.Exists(x => x.FilterName == f));
            else
            {
                MessageBox.Show("No Filters have been configured in TSX.  " +
                "Set up filters and restart calibration frames.  " +
                "Calibration Frame Factory will exit.",
                "Initialization Error");
                Close();
                return;
            }


            FlatsSubform();
            StayOnTopBox.Checked = cfg.StayOnTop;
            DateTime? latest = CalDB.FindMostRecentCalibration();
            if (latest == null)
                LibraryDateSelectionBox.Value = DateTime.Now;
            else
                LibraryDateSelectionBox.Value = (DateTime)latest;
        }

        private void FlatsSubform()
        {
            Configuration cfg = new Configuration();
            LightSource flatLightSource = cfg.FlatSource;
            FlatControl = new FlatMan();

            switch (flatLightSource)
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
                    DeviceIdLabel.Text = cfg.FlatPanelDeviceName;
                    PanelSelect.Checked = true;
                    break;
                default:
                    break;
            }
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
            MessageBox.Show("For sky flats, if a dome is in use then the shutter should be open " +
                            "and dome tracking connected to telescope.");

            StartButton.BackColor = Color.DarkRed;
            //Change session date to today, if needed
            CalDB.SetCalibrationDate(DateTime.Now);
            SetCCDTemperature();
            RunExposures();
            StartButton.BackColor = Color.SpringGreen;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            ccdsoftCamera tsx_cc = new ccdsoftCamera();
            try
            {
                tsx_cc.AutoSaveOn = autosavestate;
            }
            catch (Exception ex)
            {
                // Just breeze on by
            }
            //restore current tsx camera settings
            tsx_cc.Delay = delaystate;
            tsx_cc.BinX = binningXstate;
            tsx_cc.BinY = binningYstate;
            tsx_cc.ExposureTime = exposurestate;
            tsx_cc.TemperatureSetPoint = settempstate;
            tsx_cc.Frame = framestate;
            //close flatman, if enabled
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

            //Reopen TheSky
            ccdsoftCamera tsxc = new ccdsoftCamera();
            tsxc = null;

            CreateLibraryButton.BackColor = Color.SpringGreen;
        }

        #endregion

        #region TSX Operations

        private void SetBinning(string binning)
        {
            // Method to set TSX CAO binning state
            ccdsoftCamera tsx_cc = new ccdsoftCamera();
            tsx_cc.BinX = Configuration.DecodeBinningX(binning);
            tsx_cc.BinY = Configuration.DecodeBinningY(binning);
        }

        private void SetCCDTemperature()
        {
            ccdsoftCamera tsx_cc = new ccdsoftCamera();
            try
            {
                tsx_cc.Connect();
            }
            catch (Exception ex)
            {
                return;
            }
            StatusBox.Text += "Cooling camera to " + CCDTempBox.Value.ToString("0") + "\r\n";

            Show();

            tsx_cc.TemperatureSetPoint = Convert.ToDouble(CCDTempBox.Value);
            tsx_cc.RegulateTemperature = 1;
            double near = Math.Abs(tsx_cc.TemperatureSetPoint * 0.9);
            if (near == 0)
                near = .5;
            while ((Math.Abs(tsx_cc.Temperature - tsx_cc.TemperatureSetPoint)) > near)
            {
                CCDTempBox.ForeColor = Color.DarkRed;
                CCDTempBox.Value = (decimal)tsx_cc.Temperature;
                System.Threading.Thread.Sleep(1000);
            }
            CCDTempBox.Value = (decimal)tsx_cc.TemperatureSetPoint;
            CCDTempBox.ForeColor = Color.Green;
        }

        private void RunExposures()
        {
            Configuration cfg = new Configuration();
            LogEvent lg = new LogEvent();

            SetBinning(cfg.Binning);

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
                    binningButton1x1.ForeColor = Color.Green;
                }
                //Dark Frames if some left to do
                DarksCountBox.ForeColor = Color.Red;
                for (int i = 0; i < dExpList.Count; i++)
                {
                    DarkCheckBoxToggle(dExpList[i], true);
                    DarkFrameLoop(i);
                    DarkCheckBoxToggle(dExpList[i], false);
                }

                //Flat Frames until done or exposure too long (i.e. count may not go to zero)
                FlatsCountBox.ForeColor = Color.Red;

                if (ReferencePointBox.Checked)
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
            } while (BiasCountBox.Value > 0 && DarksCountBox.Value > 0);

            FlatsCountBox.ForeColor = Color.Green;
        }

        private void BiasFrameLoop()
        {
            // This is the repeat loop for a given exposure repetitions
            Configuration cfg = new Configuration();
            LogEvent lg = new LogEvent();
            const double biasexposure = 0.001d;
            // Change the form count box color
            BiasCountBox.ForeColor = Color.Red;
            // Set the count on the form
            for (int i = 0; i < (int)BiasCountBox.Value; i++)
            {
                lg.LogIt("Imaging Bias # " + i.ToString() + " at " + cfg.Binning.ToString() + " binning");
                ImageBias(biasexposure);
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

        private void ImageBias(double exposure)
        {
            // Take a bias image at the given exposure length and binning at the temperature
            // assumes that binning and xxx have already been set correctly

            // Image and save bias frames
            // Turn on autosave
            // Set exposure length
            // Set for Bias frame type
            // Set for 0 second delay
            // Set for no image reduction
            // Set for asynchronous execution
            // For the number of repetions:
            // Start exposure and wait until completed or aborted
            // Upon completion, store the image file in the library 
            // Clean up mess and return

            ccdsoftCamera tsx_cc = new ccdsoftCamera();
            tsx_cc.ExposureTime = exposure;
            // tsx_cc.ExposureTime = exposure
            tsx_cc.Frame = TheSky64Lib.ccdsoftImageFrame.cdBias;
            tsx_cc.Delay = 0;
            tsx_cc.Asynchronous = 0;
            tsx_cc.ImageReduction = TheSky64Lib.ccdsoftImageReduction.cdNone;
            tsx_cc.TakeImage();
            while (tsx_cc.State == TheSky64Lib.ccdsoftCameraState.cdStateTakePicture)
            {
                System.Windows.Forms.Application.DoEvents();
                if (abortflag)
                {
                    tsx_cc.Abort();
                    return;
                }
                System.Threading.Thread.Sleep(1000);
            }
            // Save the using the PreStack manager if checked,
            // Otherwise TSX will do what TSX does.
            CalDB.BiasImageStore();
            return;
        }

        private void DarkFrameLoop(int dIndex)
        {
            // This is the repeat loop for a given exposure repetitions
            Configuration cfg = new Configuration();
            int reps = dCount[dIndex];
            // Change the form count box color
            DarksCountBox.ForeColor = Color.DarkRed;
            // Set the count on the form
            for (int i = 0; i < reps; i++)
            {
                LogEvent lg = new LogEvent();
                lg.LogIt("Imaging Dark # " + i.ToString() + " at " + cfg.Binning.ToString() + " binning for " + dExpList[dIndex].ToString() + " seconds");
                ImageDark(dExpList[dIndex]);
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

        private void ImageDark(double exposure)
        {
            // Take a dark image at the given exposure length and binning at the temperature
            // assumes that binning and xxx have already been set correctly

            // Image and save dark frames
            // Turn on autosave
            // Set exposure length
            // Set for Dark frame type
            // Set for 0 second delay
            // Set for no image reduction
            // Set for asynchronous execution
            // For the number of repetions:
            // Start exposure and wait until completed or aborted
            // Upon completion, store the image file in the library 
            // Clean up mess and return
            ccdsoftCamera tsx_cc = new ccdsoftCamera();
            tsx_cc.ExposureTime = exposure;
            // tsx_cc.ExposureTime = exposure
            tsx_cc.Frame = TheSky64Lib.ccdsoftImageFrame.cdDark;
            tsx_cc.Delay = 0;
            tsx_cc.Asynchronous = 0; ;
            tsx_cc.ImageReduction = TheSky64Lib.ccdsoftImageReduction.cdNone;
            tsx_cc.TakeImage();
            while (tsx_cc.State == TheSky64Lib.ccdsoftCameraState.cdStateTakePicture)
            {
                System.Windows.Forms.Application.DoEvents();
                if (abortflag)
                {
                    tsx_cc.Abort();
                    return;
                }
                System.Threading.Thread.Sleep(1000);
            }
            CalDB.DarkImageStore();
        }

        private void PanelFlatFrameLoop(int reps, List<Filters.ActiveFilter> afList)
        {
            // This is the repeat loop for a given exposure repetitions
            int MaxBrightness = 100;
            int MinBrightness = 0;

            Configuration cfg = new Configuration();
            LogEvent lg = new LogEvent();
            totalreps = 0;
            if (reps <= 0)
                return;
            // Change the form count box color
            FlatsCountBox.ForeColor = Color.DarkRed;
            lg.LogIt("Slewing telescope to MyFlatField reference point and parking");
            FlatControl.FlatManStage();
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
                    ImageFlat(cfg.FlatInitialExposure, af.FilterIndex);
                    if (abortflag)
                    {
                        return;
                    }
                    CalDB.FlatImageStore(af.FilterName);
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
            int MinExpTime = 1;
            int MaxExpTime = 60;
            double tgtADU = cfg.FlatTargetADU;
            int MinADUVal = (int)(tgtADU * 0.8);
            int MaxADUVal = (int)(tgtADU * 1.2);

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
                    ImageFlat(exposure, af.FilterIndex);
                    if (abortflag)
                    {
                        break;
                    }
                    CalDB.FlatImageStore(af.FilterName);
                    // Decrement count
                    --FlatsCountBox.Value;
                    ++totalreps;
                }
            }
            lg.LogIt("**** Generated " + totalreps.ToString() + " sky flat frames ****");
            //FlatsCountBox.Value = (decimal)reps;
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

        private void ImageFlat(double exposure, int filter)
        {
            // Take a dark image at the given exposure length and binning at the temperature
            // assumes that binning and xxx have already been set correctly

            // Image and save dark frames
            // Turn on autosave
            // Set exposure length
            // Set for Dark frame type
            // Set for 0 second delay
            // Set for no image reduction
            // Set for asynchronous execution
            // For the number of repetions:
            // Start exposure and wait until completed or aborted
            // Upon completion, store the image file in the library 
            // Clean up mess and return

            LogEvent lg = new LogEvent();
            Configuration cfg = new Configuration();
            ccdsoftCamera tsx_cc = new ccdsoftCamera()
            {
                ExposureTime = exposure,
                FilterIndexZeroBased = filter,
                Frame = TheSky64Lib.ccdsoftImageFrame.cdFlat,
                Asynchronous = 0,
                ImageReduction = TheSky64Lib.ccdsoftImageReduction.cdNone
            };
            tsx_cc.TakeImage();
            WaitImaging();
            ccdsoftImage tsxi = new ccdsoftImage();
            tsxi.AttachToActiveImager();
            int avgADU = (int)tsxi.averagePixelValue();
            lg.LogIt("Flat Imaged " + Filters.LookUpFilterName(filter) + " filter at " + cfg.Binning + " binning for " + avgADU.ToString() + " average ADU");
            return;
        }

        private int FlatManBrightnessCalibration(int filter, double exposure, int startingBrightness, string binning, int targetADU)
        {
            //Looks for brightness setting that produces something close (80%) to the target ADU at the given exposure
            //The brightness setting starts with the currently configured brightness.
            //The exposure setting is fixed at the curently configured flats exposure setting.
            //1. Take flat image with given filter at exposure and initial brightness level
            //2. It the currentADU is within 20% of the targetADU, and it is less than the targetADU, then return that brightness level
            //3.   Otherwise, increment the brightness level up or down by 5 and try again.

            LogEvent lg = new LogEvent();

            int currentADU = 0;
            int currentBrightness = startingBrightness;
            //
            //Neither the exposure nor the brightness is linear -- this is a problem
            lg.LogIt("Calibrating FlatMan brightness for Filter " + filter.ToString());
            //Try no more than 8 times to get a good brightness
            for (int i = 0; i < 8; i++)
            {
                lg.LogIt("Brightness reset to " + currentBrightness.ToString("0"));
                //initially set brightness to the starting brightness, and wait a second for the FlatMan
                FlatControl.Bright = currentBrightness;
                System.Threading.Thread.Sleep(500);
                //Get the ADU of a sample image (subframe)
                currentADU = TakeFlatSample(filter, exposure, binning);
                //If ADU is not close enough (greater than 20%) or is greater than target then
                //  increase or decrease the brightness accordingly
                //  Otherwise, we're done with it
                if (!(CloseEnough(targetADU, currentADU, 20.0)) || (currentADU > targetADU))
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
            //Looks for exposure setting that produces something close (80%) to the target ADU at the given exposure
            //The exposure setting is fixed at the curently configured flats exposure setting.
            //1. Take flat image with given filter at exposure and initial brightness level
            //2. It the currentADU is within 20% of the targetADU, and it is less than the targetADU, then return that brightness level
            //3.   Otherwise, increment the brightness level up or down by 5 and try again.

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
                currentADU = TakeFlatSample(filter, currentExposure, binning);
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

        public int TakeFlatSample(int fltr, double exposure, string binning)
        {
            //Take a small subframed flat image and return the average pixel value
            const double subframeFactor = .1;  //fraction of frame that will be subframed
            LogEvent lg = new LogEvent();
            lg.LogIt("Taking Flat Sample Frame");

            //Take full image just to start and make sure we have the height and width correct
            lg.LogIt("- Imaging Flat Frame at " + exposure.ToString("0.00") + "sec");
            ccdsoftCamera tsxc = new ccdsoftCamera
            {
                BinX = Configuration.DecodeBinningX(binning),
                BinY = Configuration.DecodeBinningY(binning),
                FilterIndexZeroBased = fltr,
                Frame = ccdsoftImageFrame.cdFlat,
                ImageReduction = ccdsoftImageReduction.cdNone,
                Subframe = 0,
                AutoSaveOn = 1,
                ExposureTime = exposure,
                Asynchronous = 1,
            };

            int width = tsxc.WidthInPixels;
            int height = tsxc.HeightInPixels;

            //Set subframe centered on full image of height and width scaled down to the subframe factor
            // The width center is
            tsxc.SubframeLeft = (width / 2) - (int)(width * subframeFactor / 2);
            tsxc.SubframeTop = (height / 2) - (int)(width * subframeFactor / 2);
            tsxc.SubframeBottom = (height / 2) + (int)(width * subframeFactor / 2);
            tsxc.SubframeRight = (width / 2) + (int)(width * subframeFactor / 2);
            tsxc.Subframe = 1;

            tsxc.TakeImage();
            bool camResults = WaitImaging();
            if (!camResults)
            {
                lg.LogIt("- Image Subframe Flat Error: " + camResults.ToString());
                return 0;
            }
            ccdsoftImage tsxi = new ccdsoftImage();
            tsxi.AttachToActiveImager();
            int avgADU = (int)tsxi.averagePixelValue();
            lg.LogIt("Sample Flat Sample Done: Average ADU = " + avgADU.ToString("0"));
            return avgADU;
        }

        private int AdjustedBrightness(double targetADU, double currentADU, int currentBrightness)
        {
            //Calculates a new brightness level based on the current ADU and current Brightness  
            //  that would produce the targetADU assuming linearity.
            //Linearity should be true if the current ADU is less than the target ADU.  If it is not
            //  then the result will probably overshoot the target (in the negative direction) but a
            //  test should be close.
            //Maxes out at 100, I think

            return (int)Math.Min(100, (currentBrightness * (targetADU / currentADU)));

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

        private bool WaitImaging()
        {
            ccdsoftCamera tsxc = new ccdsoftCamera();
            while (tsxc.State == TheSky64Lib.ccdsoftCameraState.cdStateTakePicture)
            {
                System.Windows.Forms.Application.DoEvents();
                if (abortflag)
                {
                    tsxc.Abort();
                    return false;
                }
                System.Threading.Thread.Sleep(1000);
            }
            return true;
        }

        #endregion

        #region general configuration

        private void ImageFolderButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult dr = fbd.ShowDialog();
                {
                    if (dr == DialogResult.OK)
                    {
                        Configuration cfg = new Configuration();
                        cfg.ImageDirectoryPath = fbd.SelectedPath;
                        ImagePathField.Text = fbd.SelectedPath;
                    }
                }
            }
        }

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

        private void ChooseButton_Click(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            cfg.FlatPanelDeviceName = null;
            cfg.FlatPanelDeviceName = FlatControl.CreateFlatManDevice();
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
            Configuration cfg = new Configuration();
            List<Filters.ActiveFilter> fList = new List<Filters.ActiveFilter>();
            foreach (string fName in FlatFilterListBox.CheckedItems)
                fList.Add(new Filters.ActiveFilter { FilterName = fName, FilterIndex = (int)Filters.LookUpFilterIndex(fName) });
            cfg.FlatFilters = fList;
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
            FlatControl = new FlatMan();
            cfg.FlatSource = LightSource.lsFlatMan;
            DeviceIdLabel.Text = cfg.FlatPanelDeviceName;
        }

        private void ReferencePointBox_CheckedChanged(object sender, EventArgs e)
        {
            Configuration cfg = new Configuration();
            cfg.HasReferencePosition = ReferencePointBox.Checked;
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

        #endregion

    }
}