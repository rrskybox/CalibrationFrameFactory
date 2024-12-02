using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CalFrameFactory
{
    public partial class FrameCatalog
    {
        public enum FrameType
        {
            Light,
            Bias,
            Dark,
            Flat
        }

        private List<Frame> BiasFileList = new List<Frame>();
        private List<Frame> DarkFileList = new List<Frame>();
        private List<Frame> FlatFileList = new List<Frame>();

        public List<string> BiasGroupBinningList = new List<string>();
        public List<int> BiasGroupTemperatureList = new List<int>();
        private List<string> DarkGroupBinningList = new List<string>();
        private List<int> DarkGroupTemperatureList = new List<int>();
        public List<double> DarkGroupExposureList = new List<double>();
        private List<string> FlatGroupBinningList = new List<string>();
        public List<string> FlatGroupFilterList = new List<string>();
        private List<double> FlatGroupExposureList = new List<double>();
        private List<int> FlatGroupTemperatureList = new List<int>();

        public void AcquireAndSort(string dirPath)
        {
            //Finds all the fits files within the dirPath directory and subdirectories
            //  and sort them into the *FileLists, discarding "Light" type frames
            LogEvent logEvent = new LogEvent();
            List<string> fitsFiles = new List<string>();
            try
            {
                fitsFiles = Directory.GetFiles(dirPath, "*.fit?", SearchOption.AllDirectories).ToList<string>();
            }
            catch (Exception ex)
            {
                logEvent.LogIt(ex.Message);
                return;
            }
            //Convert all "\\" to "/" -- TSX likes it better
            for (int i = 0; i < fitsFiles.Count; i++)
            {
                fitsFiles[i] = fitsFiles[i].Replace('\\', '/');
            }
            foreach (string fitsPath in fitsFiles)
            {
                Frame fData = new Frame(fitsPath);
                switch (fData.Type)
                {
                    case FrameType.Bias:
                        {
                            BiasFileList.Add(fData);
                            break;
                        }
                    case FrameType.Dark:
                        {
                            DarkFileList.Add(fData);
                            break;
                        }
                    case FrameType.Flat:
                        {
                            FlatFileList.Add(fData);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            //Bias group by temperature
            //Darks group by exposure and temperature
            //Flats group by filter, exposure and temperature
            //A catalog is a group of groups of all three

            //Lets figure out what we have in each of the categories
            //Bias
            foreach (Frame f in BiasFileList)
            {
                if (!BiasGroupTemperatureList.Contains(f.Temperature))
                    BiasGroupTemperatureList.Add(f.Temperature);
                if (!BiasGroupBinningList.Contains(f.Binning))
                    BiasGroupBinningList.Add(f.Binning);
            }
            //Darks
            foreach (Frame f in DarkFileList)
            {
                if (!DarkGroupTemperatureList.Contains(f.Temperature))
                    DarkGroupTemperatureList.Add(f.Temperature);
                if (!DarkGroupExposureList.Contains(f.Exposure))
                    DarkGroupExposureList.Add(f.Exposure);
                if (!DarkGroupBinningList.Contains(f.Binning))
                    DarkGroupBinningList.Add(f.Binning);
            }
            //Flats
            foreach (Frame f in FlatFileList)
            {
                if (!FlatGroupTemperatureList.Contains(f.Temperature))
                    FlatGroupTemperatureList.Add(f.Temperature);
                if (!FlatGroupExposureList.Contains(f.Exposure))
                    FlatGroupExposureList.Add(f.Exposure);
                if (!FlatGroupFilterList.Contains(f.Filter))
                    FlatGroupFilterList.Add(f.Filter);
                if (!FlatGroupBinningList.Contains(f.Binning))
                    FlatGroupBinningList.Add(f.Binning);
            }
        }

        public ReductionLibrary CompileLibrary(double exposure, string filter, int temperature, string binning = "1X1")
        {
            //Creates a set of file paths for the library set -- bias, dark, flatdark, flat
            ReductionLibrary rl = new ReductionLibrary();
            //Create Library name: B<>_T<>_E<>_F<>
            rl.LibraryName = "B" + binning + "_T" + temperature.ToString("0") + "_E" + exposure.ToString("0.00") + "_F" + filter;
            //start with bias
            rl.BiasLibrary = BiasFolderList(temperature, binning);
            //Now darks
            rl.DarkLibrary = DarkFolderList(temperature, exposure, binning);
            //Now flats
            rl.FlatLibrary = FlatFolderList(temperature, filter, binning);
            //Now flatdarks
            rl.DarkFlatLibrary = DarkFlatFolderList(temperature, binning);
            return rl;
        }

        private List<string> BiasFolderList(int temperature, string binning = "1X1")
        {
            //Returns of a list of paths to bias files
            List<string> biasList = new List<string>();
            foreach (Frame f in BiasFileList.Where(x => x.Temperature == temperature && x.Binning == binning))
                biasList.Add(f.Path);
            return biasList;
        }

        private List<string> DarkFolderList(double temperature, double exposure, string binning = "1X1")
        {
            List<string> darkList = new List<string>();
            foreach (Frame f in DarkFileList.Where(x => x.Exposure == exposure && x.Temperature == temperature && x.Binning == binning))
                darkList.Add(f.Path);
            return darkList;
        }

        private List<string> FlatFolderList(double temperature, string filter, string binning = "1X1")
        {
            List<string> flatList = new List<string>();
            foreach (Frame f in FlatFileList.Where(x => x.Temperature == temperature && x.Filter == filter && x.Binning == binning))
                flatList.Add(f.Path);
            return flatList;
        }

        private List<string> DarkFlatFolderList(double temperature, string binning = "1X1")
        {
            LogEvent logEvent = new LogEvent();
            //returns exposure if all frames in the subset have nearly the same exposure, otherwise 0
            List<Frame> flatFrameList = new List<Frame>();
            List<string> darkList = new List<string>();
            //get all the flat frames that meet the criteria
            foreach (Frame f in FlatFileList.Where(x => x.Temperature == temperature && x.Binning == binning))
                flatFrameList.Add(f);
            //List<double> exposureList = new List<double>();
            //foreach (Frame f in flatFrameList)
            //    if (!exposureList.Contains(f.Exposure))
            //        exposureList.Add(f.Exposure);
            //if (exposureList.Count > 1) return null;
            //Find the nearest dark exposure to the first flat frame exposure, making the assumption that they all were exposed the same amount for this
            //  temperature/binning/filter set
            if (flatFrameList.Count == 0)
            {
                logEvent.LogIt("No flats matching temperature and/or binning.");
                return darkList;
            }
            double flatExposure = flatFrameList[0].Exposure;
            double bestDarkExposure = double.MaxValue;
            foreach (Frame dark in DarkFileList)
            {
                if (Math.Abs(flatExposure - dark.Exposure) < Math.Abs(flatExposure - bestDarkExposure))
                    bestDarkExposure = dark.Exposure;
            }
            foreach (Frame f in DarkFileList.Where(x => x.Exposure == bestDarkExposure && x.Temperature == temperature && x.Binning == binning))
                darkList.Add(f.Path);
            return darkList;
        }


        public class Frame
        {
            public string Path { get; set; }
            public FrameType Type { get; set; }
            public string Binning { get; set; }
            public double Exposure { get; set; }
            public int Temperature { get; set; }
            public string Filter { get; set; }

            private FitsFile fitsData;

            public Frame(string fitsImagePath)
            {
                //Header dump the fits file and parse the header
                Path = fitsImagePath;
                fitsData = new FitsFile(Path);
                //figure out the frame type
                //Test Code
                if (CompareString(fitsData.ImageType, "Light"))
                    Type = FrameType.Light;
                else if (CompareString(fitsData.ImageType, "Bias"))
                    Type = FrameType.Bias;
                else if (CompareString(fitsData.ImageType, "Dark"))
                    Type = FrameType.Dark;
                else if (CompareString(fitsData.ImageType, "Flat"))
                    Type = FrameType.Flat;
                Binning = fitsData.Binning;
                Exposure = Convert.ToDouble(fitsData.Exposure);
                Temperature = (int)Math.Round(Convert.ToDouble(fitsData.Temperature));
                //Filter = fitsData.Filter[0].ToString();
                Filter = fitsData.Filter;
            }

            private bool CompareString(string str, string wordToCheck)
            {
                CultureInfo culture = new CultureInfo("");
                if (str != null)
                    return culture.CompareInfo.IndexOf(str, wordToCheck, CompareOptions.IgnoreCase) >= 0;
                else return false;
            }
        }

        public struct ReductionLibrary
        {
            public string LibraryName;
            public List<string> BiasLibrary { get; set; }
            public List<string> DarkLibrary { get; set; }
            public List<string> DarkFlatLibrary { get; set; }
            public List<string> FlatLibrary { get; set; }
        }
    }
}
