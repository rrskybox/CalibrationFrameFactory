using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheSky64Lib;

namespace CalFrameFactory
{
    internal class ImagingTheSky
    {
        public ccdsoftCamera tsx_cc;
        public ccdsoftImage tsx_image;

        private bool abortflag = false;

        public double delaystate;
        public int binningXstate;
        public int binningYstate;
        public double exposurestate;
        public double settempstate;
        public int autosavestate;

        public ccdsoftImageFrame framestate;

        public ImagingTheSky()
        {
            tsx_cc = new ccdsoftCamera();
            tsx_image = new ccdsoftImage();
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

        }

        public bool SetAbort => abortflag;

        public void Connect()
        {
            // Connect to the camera

            ccdsoftCamera tsx_cc = new ccdsoftCamera();
            try
            {
                tsx_cc.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to camera: " + ex.Message);
                return;
            }
        }

        public void CloseUp()
        {
            //ccdsoftCamera tsx_cc = new ccdsoftCamera();
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

            return;
        }

        public void SetBinning(string binning)
        {
            // Method to set TSX CAO binning state
            //ccdsoftCamera tsx_cc = new ccdsoftCamera();
            tsx_cc.BinX = Configuration.DecodeBinningX(binning);
            tsx_cc.BinY = Configuration.DecodeBinningY(binning);
        }

        public double GetCCDTemperature()
        {
            //ccdsoftCamera tsx_cc = new ccdsoftCamera();
            try
            {
                tsx_cc.Connect();
            }
            catch (Exception ex)
            {
                return (0);
            }
            return tsx_cc.Temperature;
        }



        public void SetCCDTemperature(double setTemp)
        {
            LogEvent lg = new LogEvent();
            //ccdsoftCamera tsx_cc = new ccdsoftCamera();
            try
            {
                tsx_cc.Connect();
            }
            catch (Exception ex)
            {
                return;
            }
            lg.LogIt("Cooling camera to " + setTemp.ToString("0.0"));

            tsx_cc.TemperatureSetPoint = setTemp;
            tsx_cc.RegulateTemperature = 1;
            //double near = Math.Abs(tsx_cc.TemperatureSetPoint * 0.9);
            //if (near == 0)
            //    near = .5;
            //while ((Math.Abs(tsx_cc.Temperature - tsx_cc.TemperatureSetPoint)) > near)
            //{
            //    CCDTempBox.ForeColor = Color.DarkRed;
            //    CCDTempBox.Value = (decimal)tsx_cc.Temperature;
            //    System.Threading.Thread.Sleep(1000);
            //}
            //CCDTempBox.Value = (decimal)tsx_cc.TemperatureSetPoint;
            //CCDTempBox.ForeColor = Color.Green;
        }

        public void ImageBias(double exposure, CalibrationFileManagement CalDB)
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

            //ccdsoftCamera tsx_cc = new ccdsoftCamera()

            tsx_cc.ExposureTime = exposure;
            tsx_cc.Frame = TheSky64Lib.ccdsoftImageFrame.cdBias;
            tsx_cc.Delay = 0;
            tsx_cc.Asynchronous = 0;
            tsx_cc.ImageReduction = TheSky64Lib.ccdsoftImageReduction.cdNone;
            tsx_cc.Subframe = 0;
            //Wait for all the camera settings to be applied before taking the image
            System.Threading.Thread.Sleep(2000);
            //Take the image and wait for it to complete
            tsx_cc.TakeImage();
            while (tsx_cc.State == TheSky64Lib.ccdsoftCameraState.cdStateTakePicture)
            {
                System.Windows.Forms.Application.DoEvents();
                if (abortflag)
                {
                    tsx_cc.Abort();
                    return;
                }
                System.Threading.Thread.Sleep(2000);
            }
            // Save the using the PreStack manager if checked,
            // Otherwise TSX will do what TSX does.
            CalDB.BiasImageStoreTSX(tsx_image);
            return;
        }

        public void ImageDark(double exposure, CalibrationFileManagement CalDB)
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
            //ccdsoftCamera tsx_cc = new ccdsoftCamera()

            tsx_cc.ExposureTime = exposure;
            tsx_cc.Frame = TheSky64Lib.ccdsoftImageFrame.cdDark;
            tsx_cc.Delay = 0;
            tsx_cc.Asynchronous = 0;
            tsx_cc.ImageReduction = TheSky64Lib.ccdsoftImageReduction.cdNone;
            tsx_cc.Subframe = 0;
            //Wait for all the camera settings to be applied before taking the image
            System.Threading.Thread.Sleep(2000);
            //Take the image and wait for it to complete
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
            CalDB.DarkImageStoreTSX(tsx_image);
        }

        public void ImageFlat(double exposure, int filter, CalibrationFileManagement CalDB)
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
            //ccdsoftCamera tsx_cc = new ccdsoftCamera()

            tsx_cc.ExposureTime = exposure;
            tsx_cc.FilterIndexZeroBased = filter;
            tsx_cc.Frame = TheSky64Lib.ccdsoftImageFrame.cdFlat;
            tsx_cc.Asynchronous = 0;
            tsx_cc.ImageReduction = TheSky64Lib.ccdsoftImageReduction.cdNone;
            tsx_cc.Subframe = 0;
            //Wait for all the camera settings to be applied before taking the image
            System.Threading.Thread.Sleep(1000);
            //Take the image and wait for it to complete
            tsx_cc.TakeImage();
            WaitImaging();
            ccdsoftImage tsxi = new ccdsoftImage();
            tsxi.AttachToActiveImager();
            int avgADU = (int)tsxi.averagePixelValue();
            lg.LogIt("Flat Imaged " + Filters.LookUpFilterName(filter) + " filter at " + cfg.Binning + " binning for " + avgADU.ToString() + " average ADU");
            return;
        }

        public int TakeFlatSample(int fltr, double exposure, string binning)
        {
            //Take a small subframed flat image and return the average pixel value
            const double subframeFactor = .1;  //fraction of frame that will be subframed
            LogEvent lg = new LogEvent();
            lg.LogIt("Taking Flat Sample Frame");

            //Take full image just to start and make sure we have the height and width correct
            lg.LogIt("- Imaging Flat Frame at " + exposure.ToString("0.00") + "sec");
            //ccdsoftCamera tsx_cc = new ccdsoftCamera

            tsx_cc.BinX = Configuration.DecodeBinningX(binning);
            tsx_cc.BinY = Configuration.DecodeBinningY(binning);
            tsx_cc.FilterIndexZeroBased = fltr;
            tsx_cc.Frame = ccdsoftImageFrame.cdFlat;
            tsx_cc.ImageReduction = ccdsoftImageReduction.cdNone;
            tsx_cc.Subframe = 0;
            tsx_cc.AutoSaveOn = 1;
            tsx_cc.ExposureTime = exposure;
            tsx_cc.Asynchronous = 1;            

            int width = tsx_cc.WidthInPixels;
            int height = tsx_cc.HeightInPixels;

            //Set subframe centered on full image of height and width scaled down to the subframe factor
            // The width center is
            tsx_cc.SubframeLeft = (width / 2) - (int)(width * subframeFactor / 2);
            tsx_cc.SubframeTop = (height / 2) - (int)(width * subframeFactor / 2);
            tsx_cc.SubframeBottom = (height / 2) + (int)(width * subframeFactor / 2);
            tsx_cc.SubframeRight = (width / 2) + (int)(width * subframeFactor / 2);
            tsx_cc.Subframe = 1;

            tsx_cc.TakeImage();
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

        private bool WaitImaging()
        {
            ccdsoftCamera tsx_cc = new ccdsoftCamera();
            while (tsx_cc.State == TheSky64Lib.ccdsoftCameraState.cdStateTakePicture)
            {
                System.Windows.Forms.Application.DoEvents();
                if (abortflag)
                {
                    tsx_cc.Abort();
                    return false;
                }
                System.Threading.Thread.Sleep(1000);
            }
            return true;
        }

    }
}
