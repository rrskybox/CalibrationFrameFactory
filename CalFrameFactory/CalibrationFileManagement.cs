using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheSky64Lib;
using MaxIm;
using System.Diagnostics.Eventing.Reader;

namespace CalFrameFactory
{
    public class CalibrationFileManagement
    {
        // Class for managing the storage and retrieval (future) calibration files, including darks, flats and bias
        // 
        // The dark files are stored in a directory tree.  The root is a directory in the user document directory
        // called "PreStack", which I've used for other TSX miniapps.  Within this directory, the darks library
        // directory named "Darks" (created if doesn't exist).  
        // 
        // The darks library is structured by duration (exposure), then binning (1x1, 2x2, etc), then date.
        // This overall structure is created and/or verified upon launch.  The date directory associated with each
        // selected checkbox is created or verified when that dark is created and stored.
        // 
        // Individaul FITS filename is composed of a string "Dark"."B"<binning>."E"<exposure in sec>."T"<temperature in degrees celsius>.a sequence number
        // The file management functions are broken into three methods.  a New creation of this class verifies and/or creates the
        // base file tree structure down to the Darks directory, populating the public variable CalPath with that path.  
        // DarkFilePath() figures out the path to a target directory given the
        // exposure, binning and date.  DarkFileStore() stores a file given the path and a filename.

        private const string DarksDirectory = @"\Darks";
        private const string BiasDirectory = @"\Bias";
        private const string FlatDirectory = @"\Flats";

        private sbyte FitsFileType = 3;
        private sbyte PixelDateType16 = 1;

        private int SeqNum;

        private bool useTSX;

        private string DarkCalPath;
        private string BiasCalPath;
        private string FlatCalPath;
        private string LocalBinPath;
        private string LocalExpPath;
        //private string LocalDatePath;
        private string ImageCCDTemp;

        // Enumeration are here to ease binning
        public enum CalBin : int
        {
            cb1x1,
            cb2x2,
            cb3x3,
            cb4x4
        }

        public string SessionDateString { get; set; } = null;

        public string SessionCalibrationPath { get; set; } = null;

        public CalibrationFileManagement()
        {
            Configuration cfg = new Configuration();
            if (cfg.ImagingApplication == Configuration.ImagingApp.TS)
                useTSX = true;
            else
                useTSX = false;
            bool existresult;
            //Set todays date as default
            string groupPath = cfg.ReductionGroupDirectoryPath;
            SessionDateString = DateTime.Now.ToString("ddMMMyyyy");
            // The base directory must exist with Configuration object creation
            //Create session date directory for calibration files
            SessionCalibrationPath = groupPath + @"\" + "Calibration_" + SessionDateString;
            //  if it exists then delete it
            existresult = Directory.Exists(SessionCalibrationPath);
            if (!existresult)
                Directory.CreateDirectory(SessionCalibrationPath);

            // Create the darks directory, if none exists
            // Set the Darks base directory
            DarkCalPath = SessionCalibrationPath + DarksDirectory;
            existresult = Directory.Exists(DarkCalPath);
            if (!existresult)
            {
                Directory.CreateDirectory(DarkCalPath);
            }

            // Create the bias directory, if none exists
            // Set the Bias base directory
            BiasCalPath = SessionCalibrationPath + BiasDirectory;
            existresult = Directory.Exists(BiasCalPath);
            if (!existresult)
            {
                Directory.CreateDirectory(BiasCalPath);
            }

            // Create the flats directory, if none exists
            // Set the Flat base directory
            FlatCalPath = SessionCalibrationPath + FlatDirectory;
            existresult = Directory.Exists(FlatCalPath);
            if (!existresult)
            {
                Directory.CreateDirectory(FlatCalPath);
            }
            // Set the sequence number
            SeqNum = 0;
            // All done, ready to go.
            return;
        }

        public string BiasImageStoreTSX(ccdsoftImage tsxi)
        {
            // Stores the current active TSX image as designated bias file
            // Step A:  attach the active image and generate directory and file name strings from the FITS information
            // Two strings are needed:  one for one for the binning, one for temperature
            /* TODO ERROR: Skipped IfDirectiveTrivia
            #If (TSX_32) Then
            *//* TODO ERROR: Skipped DisabledTextTrivia
                    Dim tsxi = CreateObject("TheSkyX.ccdsoftImage")
            *//* TODO ERROR: Skipped ElseDirectiveTrivia
            #Else
            */
            LogEvent Status = new LogEvent();
            /* TODO ERROR: Skipped EndIfDirectiveTrivia
            #End If
            */
            var attachresult = ((dynamic)tsxi).AttachToActiveImager();
            LocalBinPath = Convert.ToString(((dynamic)tsxi).FITSKeyword("XBINNING")) + "X" + Convert.ToString(((dynamic)tsxi).FITSKeyword("YBINNING"));
            ImageCCDTemp = Convert.ToString(((dynamic)tsxi).FITSKeyword("SET-TEMP"));
            // Step B:  make sure the directory tree exists, create it if it doesn't
            bool existresult = Directory.Exists(BiasCalPath + @"\" + LocalBinPath);
            if (!existresult)
            {
                Directory.CreateDirectory(BiasCalPath + @"\" + LocalBinPath);
            }
            existresult = Directory.Exists(BiasCalPath + @"\" + LocalBinPath);
            if (!existresult)
            {
                Directory.CreateDirectory(BiasCalPath + @"\" + LocalBinPath);
            }   // Open TSX object
            // Step
            string biasfilename = "Bias." + "B" + LocalBinPath + ".T" + ImageCCDTemp + "." + SeqNum.ToString();
            // Tell TSX what the filepath is going to be
            string tsxPath = BiasCalPath + @"\" + LocalBinPath + @"\" + biasfilename + ".FITS";
            ((dynamic)tsxi).path = tsxPath;
            string result = "Writing: " + tsxPath;
            var savestatus = tsxi.Save();
            // increment sequence number
            SeqNum += 1;
            Status.LogIt(result);
            return result;
        }

        public string BiasImageStoreMDL(MaxIm.Application mdl_app)
        {
            // Stores the current active TSX image as designated bias file
            // Step A:  attach the active image and generate directory and file name strings from the FITS information
            // Two strings are needed:  one for one for the binning, one for temperature
            LogEvent Status = new LogEvent();
            MaxIm.Document image = mdl_app.CurrentDocument;
            //LocalBinPath = Convert.ToString(((dynamic)image).GetFITSKey("XBINNING")) + "X" + Convert.ToString(((dynamic)image).GetFITSKey("YBINNING"));
            //ImageCCDTemp = Convert.ToString(((dynamic)image).GetFITSKey("SET-TEMP"));
            string xBinStr = image.GetFITSKey("XBINNING").ToString();
            string yBinStr = image.GetFITSKey("YBINNING").ToString();
            string tempStr = image.GetFITSKey("SET-TEMP").ToString("0"); ;
            LocalBinPath = xBinStr + "X" + yBinStr;
            ImageCCDTemp = tempStr;
            // Step B:  make sure the directory tree exists, create it if it doesn't
            bool existresult = Directory.Exists(BiasCalPath + @"\" + LocalBinPath);
            if (!existresult)
            {
                Directory.CreateDirectory(BiasCalPath + @"\" + LocalBinPath);
            }
            existresult = Directory.Exists(BiasCalPath + @"\" + LocalBinPath);
            if (!existresult)
            {
                Directory.CreateDirectory(BiasCalPath + @"\" + LocalBinPath);
            }   // Open TSX object
            // Step
            string biasfilename = "Bias." + "B" + LocalBinPath + ".T" + ImageCCDTemp + "." + SeqNum.ToString();
            // Tell TSX what the filepath is going to be
            string tsxPath = BiasCalPath + @"\" + LocalBinPath + @"\" + biasfilename + ".FITS";
            image.SaveFile(tsxPath, MaxIm.ImageFormatType.mxFITS, false, MaxIm.PixelDataFormatType.mx16BitPF);
            string result = "Writing: " + tsxPath;
            // increment sequence number
            SeqNum += 1;
            Status.LogIt(result);
            return result;
        }

        public string DarkImageStoreTSX(ccdsoftImage tsxi)
        {
            // Stores the current active TSX image as designated dark file
            // Step A:  attach the active image and generate directory and file name strings from the FITS information
            // Three strings are needed:  one for exposure, one for the binning, one for temperature
            LogEvent Status = new LogEvent();
            var attachresult = ((dynamic)tsxi).AttachToActiveImager();
            LocalExpPath = Convert.ToString(((dynamic)tsxi).FITSKeyword("EXPTIME"));
            LocalBinPath = Convert.ToString(((dynamic)tsxi).FITSKeyword("XBINNING")) + "X" + Convert.ToString(((dynamic)tsxi).FITSKeyword("YBINNING"));
            ImageCCDTemp = Convert.ToString(((dynamic)tsxi).FITSKeyword("SET-TEMP"));
            // Step B:  make sure the directory tree exists, create it if it doesn't
            bool existresult = Directory.Exists(DarkCalPath + @"\" + LocalBinPath);
            if (!existresult)
            {
                Directory.CreateDirectory(DarkCalPath + @"\" + LocalBinPath);
            }
            existresult = Directory.Exists(DarkCalPath + @"\" + LocalBinPath + @"\" + LocalExpPath);
            if (!existresult)
            {
                Directory.CreateDirectory(DarkCalPath + @"\" + LocalBinPath + @"\" + LocalExpPath);
            }   // Open TSX object
            // Step
            string darkfilename = "Dark." + "B" + LocalBinPath + ".E" + LocalExpPath + ".T" + ImageCCDTemp + "." + SeqNum.ToString();
            // Tell TSX what the filepath is going to be
            string tsxPath = DarkCalPath + @"\" + LocalBinPath + @"\" + LocalExpPath + @"\" + darkfilename + ".FITS";
            tsxi.Path = tsxPath;
            string result = "Writing: " + tsxPath;
            var savestatus = tsxi.Save();
            // increment sequence number
            SeqNum += 1;
            Status.LogIt(result);

            return result;
        }

        public string DarkImageStoreMDL(MaxIm.Application mdl_app)
        {
            // Stores the current active TSX image as designated dark file
            // Step A:  attach the active image and generate directory and file name strings from the FITS information
            // Three strings are needed:  one for exposure, one for the binning, one for temperature
            LogEvent Status = new LogEvent();
            MaxIm.Document image = mdl_app.CurrentDocument;
            //LocalExpPath = Convert.ToString(((dynamic)image).GetFITSKey("EXPTIME"));
            //LocalBinPath = Convert.ToString(((dynamic)image).GetFITSKey("XBINNING")) + "X" + Convert.ToString(((dynamic)image).FITSKeyword("YBINNING"));
            //ImageCCDTemp = Convert.ToString(((dynamic)image).FITSKeyword("SET-TEMP"));
            string expTimeStr = image.GetFITSKey("EXPTIME").ToString("0");
            string xBinStr = image.GetFITSKey("XBINNING").ToString();
            string yBinStr = image.GetFITSKey("YBINNING").ToString();
            string tempStr = image.GetFITSKey("SET-TEMP").ToString("0");
            LocalExpPath = expTimeStr;
            LocalBinPath = xBinStr + "X" + yBinStr;
            ImageCCDTemp = tempStr;

            // Step B:  make sure the directory tree exists, create it if it doesn't
            bool existresult = Directory.Exists(DarkCalPath + @"\" + LocalBinPath);
            if (!existresult)
            {
                Directory.CreateDirectory(DarkCalPath + @"\" + LocalBinPath);
            }
            existresult = Directory.Exists(DarkCalPath + @"\" + LocalBinPath + @"\" + LocalExpPath);
            if (!existresult)
            {
                Directory.CreateDirectory(DarkCalPath + @"\" + LocalBinPath + @"\" + LocalExpPath);
            }   // Open TSX object
            // Step
            string darkfilename = "Dark." + "B" + LocalBinPath + ".E" + LocalExpPath + ".T" + ImageCCDTemp + "." + SeqNum.ToString();
            // Tell TSX what the filepath is going to be
            string tsxPath = DarkCalPath + @"\" + LocalBinPath + @"\" + LocalExpPath + @"\" + darkfilename + ".FITS";
            image.SaveFile(tsxPath, MaxIm.ImageFormatType.mxFITS, false, MaxIm.PixelDataFormatType.mx16BitPF);
            string result = "Writing: " + tsxPath;
            // increment sequence number
            SeqNum += 1;
            Status.LogIt(result);

            return result;
        }

        public string FlatImageStoreTSX(ccdsoftImage tsxi, string filterName)
        {
            // Stores the current active TSX image as designated dark file
            // Step A:  attach the active image and generate directory and file name strings from the FITS information
            // Three strings are needed:  one for exposure, one for the binning, one for temperature
            LogEvent Status = new LogEvent();
            var attachresult = ((dynamic)tsxi).AttachToActiveImager();
            // Step B:  make sure the directory tree exists, create it if it doesn't
            // Create path strings
            string flatDatePath = FlatCalPath + @"\" + SessionDateString;

            bool existresult = Directory.Exists(flatDatePath);
            if (!existresult)
            {
                Directory.CreateDirectory(flatDatePath);
            }
            // Step
            string flatFilename = filterName + ".Flat." + SeqNum.ToString();
            // Tell TSX what the filepath is going to be
            string tsxPath = FlatCalPath + @"\" + SessionDateString + @"\" + flatFilename + ".FITS";
            tsxi.Path = tsxPath;
            string result = "Writing: " + tsxPath;
            var savestatus = tsxi.Save();
            // increment sequence number
            SeqNum += 1;
            Status.LogIt(result);
            return result;
        }

        public string FlatImageStoreMDL(MaxIm.Application mdl_app, string filterName)
        {
            // Stores the current active TSX image as designated dark file
            // Step A:  attach the active image and generate directory and file name strings from the FITS information
            // Three strings are needed:  one for exposure, one for the binning, one for temperature
            LogEvent Status = new LogEvent();
            MaxIm.Document image = mdl_app.CurrentDocument;
            var attachresult = ((dynamic)image).AttachToActiveImager();
            // Step B:  make sure the directory tree exists, create it if it doesn't
            // Create path strings
            string flatDatePath = FlatCalPath + @"\" + SessionDateString;

            bool existresult = Directory.Exists(flatDatePath);
            if (!existresult)
            {
                Directory.CreateDirectory(flatDatePath);
            }
            // Step
            string flatFilename = filterName + ".Flat." + SeqNum.ToString();
            // Tell TSX what the filepath is going to be
            string tsxPath = FlatCalPath + @"\" + SessionDateString + @"\" + flatFilename + ".FITS";
            image.SaveFile(tsxPath, MaxIm.ImageFormatType.mxFITS, false, MaxIm.PixelDataFormatType.mx16BitPF);
            string result = "Writing: " + tsxPath;
            // increment sequence number
            SeqNum += 1;
            Status.LogIt(result);
            return result;
        }

        public void SetCalibrationDate(DateTime newDate)
        {
            Configuration cfg = new Configuration();
            // Set date string for file directory
            SessionDateString = newDate.ToString("ddMMMyyyy");
            // Set Calibration file path, creating necessary directories, as you go.
            SessionCalibrationPath = cfg.ReductionGroupDirectoryPath + @"\" + "Calibration_" + SessionDateString;
        }

        public DateTime? FindMostRecentCalibration()
        {
            //Search the list of dated calibration subdirectories for most recent set
            //  return null if no prestack folder or no calibration folders
            Configuration cfg = new Configuration();
            string imageDir = cfg.ReductionGroupDirectoryPath;
            if (Directory.Exists(imageDir))
            {
                DirectoryInfo di = new DirectoryInfo(imageDir);
                List<DirectoryInfo> calDirs = di.GetDirectories("Calibration_*").ToList();
                if (!(calDirs.Count > 0))
                    return null;
                DateTime? mostRecent = Convert.ToDateTime(calDirs[0].Name.Split('_')[1]);
                foreach (DirectoryInfo calDir in calDirs)
                {
                    if (calDir.GetFiles("*.FITS", SearchOption.AllDirectories).Count() > 0)
                        if (Convert.ToDateTime(calDir.Name.Split('_')[1]) > mostRecent)
                            mostRecent = Convert.ToDateTime(calDir.Name.Split('_')[1]);
                }
                return mostRecent;
            }
            return null;
        }
    }
}
