using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using TheSky64Lib;

namespace CalFrameFactory

{
    public partial class ReductionGroup
    {
        const string AppSettingsFilename = "AppSettings.ini";
        const string OutAppSettingsFilename = "AppSettings.ini";

        public List<XElement> CalibrationLibs = new List<XElement>();

        public void Generate(CalibrationFileManagement sessionCFM)
        {
            Configuration cfg = new Configuration();
            LogEvent lg = new LogEvent();
            //Build Calibration libraries
            lg.LogIt("Creating library listings for calibration files on " + sessionCFM.SessionDateString);
            FrameCatalog fc = new FrameCatalog();
            List<FrameCatalog.ReductionLibrary> frcl = new List<FrameCatalog.ReductionLibrary>();
            fc.AcquireAndSort(sessionCFM.SessionCalibrationPath);
            lg.LogIt(fc.FlatGroupFilterList.Count.ToString() + " different filter sets for flat images");
            lg.LogIt(fc.DarkGroupExposureList.Count.ToString() + " different exposure lengths for dark images");
            lg.LogIt(fc.BiasGroupBinningList.Count.ToString() + " different binnings for bias images");
            foreach (string filter in fc.FlatGroupFilterList)
            {
                foreach (int exposure in fc.DarkGroupExposureList)
                {
                    //Note: Temperature converted to integer value
                    frcl.Add(fc.CompileLibrary(exposure, filter, Convert.ToInt16(cfg.Temperature.ToString()), cfg.Binning.ToString()));
                }
            }
            lg.LogIt("Created " + frcl.Count.ToString() + " reduction sets for this session date");
            //Write calibration libraries to appsettings.ini
            AppSettings.InsertAppSettings(frcl, cfg.TSXDirectoryPath + "\\" + AppSettingsFilename, cfg.TSXDirectoryPath + "\\" + OutAppSettingsFilename);
            return;
        }

        public static class AppSettings
        {
            //Group_TESTGROUP, Bias frames, 1, 1, 0, 0, 1, 0, Dark frames, 1, 1, 0, 0, 1, 0, Dark frames for flats only, 1, 1, 0, 0, 1, 0, Flat frames, 1, 1, 0, 0, 1, 0
            const string ReductionGroupTag = "APP_qslCalibrationGroups=";
            const string GroupHeaderTag = "Group_";
            const string BiasHeaderTag = "Bias frames";
            const string DarkHeaderTag = "Dark frames";
            const string DarkFlatHeaderTag = "Dark frames for flats only";
            const string FlatHeaderTag = "Flat frames";

            public static bool InsertAppSettings(List<FrameCatalog.ReductionLibrary> fcrlList, string appSettingsInPath, string appSettingsOutPath)
            {
                int[] groupPrefix = new int[] { 1, 1, 0, 0, 1, 0 };
                string appSettingsText = "";
                string lineText = null;
                //Open the AppSettings.ini file
                //Read each line and save until the calibration header is found
                TextReader tRead = File.OpenText(appSettingsInPath);
                do
                {
                    lineText = tRead.ReadLine();
                    if (lineText == null)
                    {
                        MessageBox.Show("AppSettings.ini read failure:\nMake sure the Calibration Library window (Input->Image Calibration Library) has been opened at least once.");
                        return false;
                    }
                    if (lineText.Contains(ReductionGroupTag))
                        break;
                    appSettingsText += lineText + "\n";
                } while (lineText != null);

                //We either have the current calibration set in the buffer
                // or null, meaning that no calibration set was found.
                //  if not null then suck out the Imager and Guider calibrations, if any
                if (lineText != null)
                {
                    //remove the calibration header tag and add it to the store
                    lineText = lineText.Remove(0, ReductionGroupTag.Length);
                    appSettingsText += ReductionGroupTag;
                    string[] calLibs = lineText.Split(new[] { GroupHeaderTag }, StringSplitOptions.RemoveEmptyEntries);
                    //if the first calibration is for the imager, then move whole thing to store
                    if (calLibs[0].Contains("Imager"))
                        appSettingsText += GroupHeaderTag + calLibs[0];
                    //same for second calibration for autoguider
                    if (calLibs.Length > 1 && calLibs[1].Contains("Autoguider"))
                        appSettingsText += GroupHeaderTag + calLibs[0];
                    //For each set in the reduction library, we will generate a group
                    foreach (FrameCatalog.ReductionLibrary rl in fcrlList)
                    {    //  each group has starts with a group tag with a name.
                         //  the name has already been generated from the Binning, Exposure, Filter and Temperature values for the library
                        appSettingsText += GroupHeaderTag + rl.LibraryName + ", ";
                        //Bias files -- tag
                        appSettingsText += BiasHeaderTag + ", ";
                        //  update the prefix with the file count in the 6th index
                        groupPrefix[5] = rl.BiasLibrary.Count;
                        for (int i = 0; i < groupPrefix.Length; i++)
                        { appSettingsText += groupPrefix[i].ToString("0") + ", "; }
                        //add File paths
                        foreach (string s in rl.BiasLibrary)
                        { appSettingsText += s + ", "; }

                        //Dark -- tag
                        appSettingsText += DarkHeaderTag + ", ";
                        //  update the prefix with the file count in the 6th index
                        groupPrefix[5] = rl.DarkLibrary.Count;
                        for (int i = 0; i < groupPrefix.Length; i++)
                        { appSettingsText += groupPrefix[i].ToString("0") + ", "; }
                        //add File paths
                        foreach (string s in rl.DarkLibrary)
                        { appSettingsText += s + ", "; }

                        //Dark Flat -- tag
                        appSettingsText += DarkFlatHeaderTag + ", ";
                        //  update the prefix with the file count in the 6th index
                        groupPrefix[5] = rl.DarkFlatLibrary.Count;
                        for (int i = 0; i < groupPrefix.Length; i++)
                        { appSettingsText += groupPrefix[i].ToString("0") + ", "; }
                        //add File paths
                        foreach (string s in rl.DarkFlatLibrary)
                        { appSettingsText += s + ", "; }

                        //Flat  -- tag
                        appSettingsText += FlatHeaderTag + ", ";
                        //  update the prefix with the file count in the 6th index
                        groupPrefix[5] = rl.FlatLibrary.Count;
                        for (int i = 0; i < groupPrefix.Length; i++)
                        { appSettingsText += groupPrefix[i].ToString("0") + ", "; }
                        //add File paths
                        foreach (string s in rl.FlatLibrary)
                        { appSettingsText += s + ", "; }
                    }
                    //remove last comma and space
                    appSettingsText = appSettingsText.TrimEnd(' ');
                    appSettingsText = appSettingsText.TrimEnd(',');
                    //add new line
                    appSettingsText += "\n";
                    //Continue copying the .ini until the end
                    do
                    {
                        lineText = tRead.ReadLine();
                        appSettingsText += lineText + "\n";
                    } while (lineText != null);
                    tRead.Close();
                    //Write text to new app file
                    string newAppSettingsFilePath = appSettingsOutPath;
                    File.WriteAllText(newAppSettingsFilePath, appSettingsText);
                    return true;
                }
                return false;
            }

        }

    }
}
