using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using TheSky64Lib;

namespace CalFrameFactory
{
    internal class Configuration
    {

        //Directories and files
        const string BisqueDirectoryPath = "\\Software Bisque\\TheSky Professional Edition 64";
        const string PrestackDirectoryName = "\\PreStack";
        const string ConfigurationFilename = "Calibration.xml";
        const string ReductionGroupDirectoryName = "\\CalibrationSets";
        const string AppSettingsFilename = "AppSettings.ini";
        const string OutAppSettingsFilename = "AppSettings.ini";

        //xml name strings
        const string xReductionGroupPath = "ImageDirectoryPath";
        const string xRoot = "ReductionGroup";
        const string xBiasCount = "BiasFrameCount";
        const string xDarkCount = "DarkFrameCount";
        const string xFlatCount = "FlatFrameCount";
        const string xBinning = "Binning";
        const string xTemperature = "Temperature";
        const string xDarkExposures = "DarkExposures";
        const string xExposure = "Exposure";
        const string xFlatFilters = "FlatFilters";
        const string xFilter = "Filter";
        const string xColor = "Color";
        const string xIndex = "Index";
        const string xFlatSource = "FlatSource";
        const string xFlatTargetADU = "FlatTargetADU";
        const string xFlatInitialExposure = "FlatInitialExposure";
        const string xFlatInitialBrightness = "FlatInitialBrightness";
        const string xIsPortableFlatMan = "IsPortableFlatMan";
        const string xTopMostWindow = "TopMostWindow";
        const string xHasReferencePosition = "HasReferencePosition";

        const string xFlatPanelDeviceName = "FlatPanelDeviceName";

        string cfgDir;  //Directory that contains the calibration configuration xml file, default user/Prestack
        string grpDir;  //Directory that contains the date-named calibration folders, default user/Prestack/CalibrationFrames
        string tsxDir;  //Software Bisque root directory

        public Configuration()
        {
            // tsxDir is the software bisque directory path
            tsxDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + BisqueDirectoryPath;

            // Documents/Prestack is always the configuration file folder
            //Check to see if this directory exists,
            //  if not, create the Calibration Frame Directory folder path in the Documents folder
            //      and populate it with a default configuration file
            cfgDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + PrestackDirectoryName;
            if (!Directory.Exists(cfgDir))
            {
                Directory.CreateDirectory(cfgDir);
            }
            grpDir = cfgDir + ReductionGroupDirectoryName;
            //The configuration file exists in the Calibration Frame Directory directory, if not there than create it with defaults.
            if (!File.Exists(cfgDir + "\\" + ConfigurationFilename))
            {
                //New set up
                XElement cDefaultX = new XElement(xRoot,
                              new XElement(xReductionGroupPath, grpDir),  //default path for image storage
                              new XElement(xBiasCount, "40"),
                              new XElement(xDarkCount, "20"),
                              new XElement(xFlatCount, "10"),
                              new XElement(xBinning, "1X1"),
                              new XElement(xTemperature, "-30"),
                              new XElement(xFlatSource, LightSource.lsNone.ToString()), //FlatMan none default
                              new XElement(xFlatTargetADU, "30000"),
                              new XElement(xFlatInitialExposure, "3"),
                              new XElement(xFlatInitialBrightness, "40"),
                              new XElement(xIsPortableFlatMan, Convert.ToString(true)),
                              new XElement(xDarkExposures, ""),
                              new XElement(xFlatPanelDeviceName, ""),
                              new XElement(xFlatFilters, ""),
                              new XElement(xHasReferencePosition, "false"));

                cDefaultX.Save(cfgDir + "\\" + ConfigurationFilename);
            }
            //Create the image directory if it doesn't exist
            if (!Directory.Exists(grpDir))
                Directory.CreateDirectory(grpDir);
        }

        public string TSXDirectoryPath
        {
            get { return tsxDir; }
        }

        public string ReductionGroupDirectoryPath  //return default if empty
        {
            //The default image directory path is "CalibrationFrames" inside the "PreStack" directory.'
            //The user can change it to something else after intitialization, but the default will always
            //be created first time around.
            get
            {
                return GetConfig(xReductionGroupPath, "");
            }
            //set
            //{
            //    string idp = value + ImageSetDirectory;
            //    if (!Directory.Exists(idp))
            //        Directory.CreateDirectory(idp);
            //    SetConfig(xImageDirectoryPath, idp);
            //}
        }

        public bool StayOnTop
        {
            get { return Convert.ToBoolean(GetConfig(xTopMostWindow, Convert.ToString(false))); }
            set { SetConfig(xTopMostWindow, value.ToString()); }
        }

        public int BiasCount
        {
            get { return Convert.ToInt16(GetConfig(xBiasCount, "0")); }
            set { SetConfig(xBiasCount, value.ToString()); }
        }

        public int DarkCount
        {
            get { return Convert.ToInt16(GetConfig(xDarkCount, "0")); }
            set { SetConfig(xDarkCount, value.ToString()); }
        }

        public int FlatCount
        {
            get { return Convert.ToInt16(GetConfig(xFlatCount, "0")); }
            set { SetConfig(xFlatCount, value.ToString()); }
        }

        public string Binning
        {
            get { return (GetConfig(xBinning, "1x1")); }
            set { SetConfig(xBinning, value.ToString()); }
        }

        public int Temperature
        {
            get { return Convert.ToInt16(GetConfig(xTemperature, "0")); }
            set { SetConfig(xTemperature, value.ToString()); }
        }

        public List<int> DarkExposures
        {
            get { return GetDarksList(); }
            set { SetDarksList(value); }
        }

        public List<Filters.ActiveFilter> FlatFilters
        {
            get { return GetFilterList(); }
            set { SetFilterList(value); }
        }

        public LightSource FlatSource
        {
            get { return GetLightSource(); }
            set { SetConfig(xFlatSource, value.ToString()); }
        }

        public int FlatTargetADU
        {
            get { return Convert.ToInt16(GetConfig(xFlatTargetADU, "30000")); }
            set { SetConfig(xFlatTargetADU, value.ToString()); }
        }

        public int FlatInitialExposure
        {
            get { return Convert.ToInt16(GetConfig(xFlatInitialExposure, "3")); }
            set { SetConfig(xFlatInitialExposure, value.ToString()); }
        }

        public int FlatInitialBrightness
        {
            get { return Convert.ToInt16(GetConfig(xFlatInitialBrightness, "40")); }
            set { SetConfig(xFlatInitialBrightness, value.ToString()); }
        }

        public string FlatPanelDeviceName
        {
            get { return GetConfig(xFlatPanelDeviceName, null); }
            set { SetConfig(xFlatPanelDeviceName, value); }
        }

        public bool IsPortableFlatMan
        {
            get { return Convert.ToBoolean(GetConfig(xIsPortableFlatMan, Convert.ToString(false))); }
            set { SetConfig(xIsPortableFlatMan, value.ToString()); }
        }

        public bool HasReferencePosition
        {
            get { return Convert.ToBoolean(GetConfig(xHasReferencePosition, Convert.ToString(false))); }
            set { SetConfig(xHasReferencePosition, value.ToString()); }
        }

        private string GetConfig(string elementName, string defaultValue)
        {
            //single value xelements
            string cfgfilename = cfgDir + "\\" + ConfigurationFilename;
            XElement cfgXf = XElement.Load(cfgfilename);
            if (cfgXf.Element(elementName) == null)
            {
                cfgXf.Add(new XElement(elementName, defaultValue));
                cfgXf.Save(cfgfilename);
                return defaultValue;
            }
            else if (cfgXf.Element(elementName).Value == "")
            {
                cfgXf.Element(elementName).ReplaceWith(new XElement(elementName, defaultValue));
                cfgXf.Save(cfgfilename);
                return defaultValue;
            }
            else return (cfgXf.Element(elementName).Value);
        }

        public LightSource GetLightSource()
        {
            string ls_str = GetConfig(xFlatSource, LightSource.lsNone.ToString());
            switch (ls_str)
            {
                case "lsNone":
                    return LightSource.lsNone;
                case "lsDawn":
                    return LightSource.lsDawn;
                case "lsDusk":
                    return LightSource.lsDusk;
                case "lsFlatMan":
                    return LightSource.lsFlatMan;
                default:
                    return LightSource.lsNone;
            }
        }

        private List<Filters.ActiveFilter> GetFilterList()
        {
            //single value list of xelements
            string cfgfilename = cfgDir + "\\" + ConfigurationFilename;
            List<Filters.ActiveFilter> fList = new List<Filters.ActiveFilter>();
            XElement cfgXf = XElement.Load(cfgfilename);
            XElement sublistX = cfgXf.Element(xFlatFilters);
            if (sublistX.Elements(xFilter).Count() == 0)
            {
                return fList;
            }
            else
            {
                foreach (XElement x in sublistX.Elements(xFilter))
                {
                    Filters.ActiveFilter af = new Filters.ActiveFilter();
                    af.FilterName = x.Element(xColor).Value;
                    af.FilterIndex = Convert.ToInt16(x.Element(xIndex).Value);
                    fList.Add(af);
                }
                return fList;
            }
        }

        private List<int> GetDarksList()
        {
            //single value list of xelements
            string cfgfilename = cfgDir + "\\" + ConfigurationFilename;
            List<int> xList = new List<int>();
            XElement cfgXf = XElement.Load(cfgfilename);
            XElement cfgDarksX = cfgXf.Element(xDarkExposures);
            if (cfgDarksX.Elements(xExposure).Count() == 0)
            {
                return xList;
            }
            else
            {
                foreach (XElement x in cfgDarksX.Elements(xExposure))
                    if (x.Value != "")
                        xList.Add(Convert.ToInt16(x.Value));
                return xList;
            }
        }

        private void SetConfig(string elementName, string value)
        {
            string cfgfilename = cfgDir + "\\" + ConfigurationFilename;
            XElement cfgXf = XElement.Load(cfgfilename);
            XElement cfgXel = cfgXf.Element(elementName);
            if (cfgXel == null)
                cfgXf.Add(new XElement(elementName, value));
            else
                cfgXel.ReplaceWith(new XElement(elementName, value));
            cfgXf.Save(cfgfilename);
            return;
        }

        private void SetDarksList(List<int> xList)
        {
            XElement subListX = new XElement(xDarkExposures);
            foreach (int x in xList)
                subListX.Add(new XElement(xExposure, x));
            string cfgfilename = cfgDir + "\\" + ConfigurationFilename;
            XElement cfgXf = XElement.Load(cfgfilename);
            XElement cfgDarksX = cfgXf.Element(xDarkExposures);
            if (cfgDarksX == null)
                cfgDarksX.Add(subListX);
            else
                cfgDarksX.ReplaceWith(subListX);
            cfgXf.Save(cfgfilename);
            return;
        }

        private void SetFilterList(List<Filters.ActiveFilter> fList)
        {
            //Replaces active filter list in configuratiion file

            string cfgfilename = cfgDir + "\\" + ConfigurationFilename;
            XElement cfgXf = XElement.Load(cfgfilename);
            XElement cfgXel = cfgXf.Element(xFlatFilters);
            XElement sublistX = new XElement(xFlatFilters);
            foreach (Filters.ActiveFilter af in fList)
            {
                XElement fX = new XElement(xFilter);
                fX.Add(new XElement(xColor, af.FilterName));
                fX.Add(new XElement(xIndex, af.FilterIndex.ToString()));
                sublistX.Add(fX);
            }
            if (cfgXel == null)
                cfgXf.Add(sublistX);
            else
                cfgXel.ReplaceWith(sublistX);
            cfgXf.Save(cfgfilename);
            return;

        }

        public static int DecodeBinningX(string binning)
        {
            return Convert.ToInt16(binning.Substring(0, 1));
        }

        public static int DecodeBinningY(string binning)
        {
            return Convert.ToInt16(binning.Substring(2, 1));
        }

        //Sky flats support functions

        public DateTime AstroTwilightStart()
        {
            return LookUpTwilight(Sk6ObjectInformationProperty.sk6ObjInfoProp_TWIL_ASTRON_START);
        }

        public DateTime AstroTwilightEnd()
        {
            return LookUpTwilight(Sk6ObjectInformationProperty.sk6ObjInfoProp_TWIL_ASTRON_END);
        }

        public DateTime CivilTwilightStart()
        {
            return LookUpTwilight(Sk6ObjectInformationProperty.sk6ObjInfoProp_TWIL_CIVIL_START);
        }

        public DateTime CivilTwilightEnd()
        {
            return LookUpTwilight(Sk6ObjectInformationProperty.sk6ObjInfoProp_TWIL_CIVIL_END);
        }

        private static DateTime LookUpTwilight(Sk6ObjectInformationProperty oProp)
        {
            sky6StarChart sc = new sky6StarChart();
            sky6ObjectInformation ob = new sky6ObjectInformation();
            //Cache current Find target
            ob.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_NAME1);
            string tName = ob.ObjInfoPropOut;
            sc.Find("Sun");
            ob.Property(oProp);
            DateTime tOut = DateTime.Now.Date + TimeSpan.FromHours(ob.ObjInfoPropOut);
            //Check for situation where the set time is in the morning of the next day, e.g. now + timespan = tomorrow, but not less than 6 hours ago.
            if (tOut < (DateTime.Now - TimeSpan.FromHours(6)))
                tOut += TimeSpan.FromDays(1);
            //Restore current target
            sc.Find(tName);
            return tOut;
        }

    }
}
