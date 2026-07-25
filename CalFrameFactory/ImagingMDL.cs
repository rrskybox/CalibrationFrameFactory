using CalFrameFactory.Properties;
using MaxIm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CalFrameFactory
{
    internal class ImagingMDL
    {
        public MaxIm.Application mdl_app;
        public MaxIm.Document mdl_doc;
        public MaxIm.CCDCamera mdl_cam;

        const sbyte sbyteTrue = 1;
        const sbyte sbyteFalse = 0;
        const sbyte sbyteLight = 1;
        const sbyte sbyteDark = 0;
        const sbyte sbyteBias = 0;
        const sbyte sbyteHighGainStackPro = 3;

        private bool abortflag = false;

        public double delaystate;
        public short binningXstate;
        public short binningYstate;
        public short exposurestate;
        public double settempstate;
        //public int autosavestate;

        public short framestate;

        public ImagingMDL()
        {
            mdl_app = new MaxIm.Application();
            mdl_cam = new CCDCamera();

            mdl_cam.LinkEnabled = sbyteTrue;
            System.Threading.Thread.Sleep(10000);  //Wait for camera to initialize

            mdl_cam.ReadoutMode = sbyteHighGainStackPro;
            mdl_cam.SetFullFrame();
            //store the current settings
            binningXstate = mdl_cam.BinX;
            binningYstate = mdl_cam.BinY;
            exposurestate = mdl_cam.ExposureTime;
            settempstate = mdl_cam.TemperatureSetpoint;
            //framestate = mdl_cam.Frame;

        }

        public bool SetAbort => abortflag;

        public void Connect()
        {
            // Connect to the camera

            try
            {
                mdl_cam.LinkEnabled = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to camera: " + ex.Message);
                return;
            }
        }

        public void CloseUp()
        {
            //restore current tsx camera settings
            //mdl_cam.Delay = delaystate;
            mdl_cam.BinX = binningXstate;
            mdl_cam.BinY = binningYstate;
            mdl_cam.ExposureTime = exposurestate;
            mdl_cam.TemperatureSetpoint = settempstate;
            //mdl_cam.Frame = framestate;
            mdl_cam.LinkEnabled = sbyteFalse; // Disconnect the camera
            return;
        }

        public void SetBinning(string binning)
        {
            // Method to set TSX CAO binning state
            mdl_cam.BinX = (short)Configuration.DecodeBinningX(binning);
            mdl_cam.BinY = (short)Configuration.DecodeBinningY(binning);
        }

        public double GetCCDTemperature()
        {
            Thread.Sleep(1000);
            return mdl_cam.Temperature;
        }



        public void SetCCDTemperature(double setTemp)
        {
            LogEvent lg = new LogEvent();
            lg.LogIt("Cooling camera to " + setTemp.ToString("0.0"));

            mdl_cam.TemperatureSetpoint = setTemp;
            mdl_cam.CoolerOn = sbyteTrue;
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

            
            mdl_cam.Expose(exposure, sbyteBias);
            // Save the using the PreStack manager if checked,
            // Otherwise TSX will do what TSX does.
            if (WaitImaging())
                CalDB.BiasImageStoreMDL(mdl_app);
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
            mdl_cam.Expose(exposure, sbyteDark);
            if (WaitImaging())
                CalDB.DarkImageStoreMDL(mdl_app);
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

            LogEvent lg = new();
            Configuration cfg = new();
            mdl_cam.Expose(exposure, sbyteLight, filter);
            if (WaitImaging())
            {
                Document mdl_doc = mdl_app.CurrentDocument;
                var imageInfo = mdl_doc.CalcAreaInfo(0, 0, 4096, 4096);
                int avgADU = imageInfo[2];
                lg.LogIt("Flat Imaged " + Filters.LookUpFilterName(filter) + " filter at " + cfg.Binning + " binning for " + avgADU.ToString() + " average ADU");
            }
            return;
        }

        public int TakeFlatSample(int fltr, double exposure, string binning)
        {
            //Take a small subframed flat image and return the average pixel value
            const double subframeFactor = .1;  //fraction of frame that will be subframed
            LogEvent lg = new();
            Configuration cfg = new();
            lg.LogIt("Taking Flat Sample Frame");

            //Take full image just to start and make sure we have the height and width correct
            lg.LogIt("- Imaging Flat Frame at " + exposure.ToString("0.00") + "sec");
            mdl_cam.BinX = (short)Configuration.DecodeBinningX(binning);
            mdl_cam.BinY = (short)Configuration.DecodeBinningY(binning);

            mdl_cam.Expose(exposure, sbyteLight, fltr);

            int width = mdl_cam.CameraXSize;
            int height = mdl_cam.CameraYSize;

            //Set subframe centered on full image of height and width scaled down to the subframe factor
            // The width center is
            mdl_cam.StartX = (short)((width / 2) - (int)(width * subframeFactor / 2));
            mdl_cam.StartY = (short)((height / 2) - (int)(width * subframeFactor / 2));
            short subframeBottom = (short)((height / 2) + (int)(width * subframeFactor / 2));
            short subframeRight = (short)((width / 2) + (int)(width * subframeFactor / 2));
            mdl_cam.NumX = (short)(subframeRight - mdl_cam.StartX);
            mdl_cam.NumY = (short)(subframeBottom - mdl_cam.StartY);

            mdl_cam.Expose(exposure, sbyteLight, fltr);
            if (WaitImaging())
            {
                Document mdl_doc = mdl_app.CurrentDocument;
                var imageInfo = mdl_doc.CalcAreaInfo(
                    (short)mdl_cam.StartX,
                    (short)mdl_cam.StartY,
                    (short)(mdl_cam.StartX + mdl_cam.NumX),
                    (short)(mdl_cam.StartY + mdl_cam.NumY),
                    0);
                int avgADU = imageInfo[2];
                lg.LogIt("Flat Imaged " + Filters.LookUpFilterName(fltr) + " filter at " + cfg.Binning + " binning for " + avgADU.ToString() + " average ADU");
                lg.LogIt("Sample Flat Sample Done: Average ADU = " + avgADU.ToString("0"));
                return avgADU;
            }
            else
            {
                lg.LogIt("Sample Flat Sample Failed");
                return -1; // Indicate an error or abort
            }
        }

        private bool WaitImaging()
        {
            while (mdl_cam.ImageReady == sbyteFalse)
            {
                System.Windows.Forms.Application.DoEvents();
                if (abortflag)
                {
                    mdl_cam.AbortExposure();
                    return false;
                }
                System.Threading.Thread.Sleep(1000);
            }
            return true;
        }
    }
}
