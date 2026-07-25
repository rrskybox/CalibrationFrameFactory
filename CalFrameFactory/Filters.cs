// --------------------------------------------------------------------------------
// VariScan module
//
// Description:	
//
// Environment:  Windows 10 executable, 32 and 64 bit
//
// Usage:        TBD
//
// Author:		(REM) Rick McAlister, rrskybox@yahoo.com
//
// Edit Log:     Rev 1.0 Initial Version
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// 
// ---------------------------------------------------------------------------------
//

using TheSky64Lib;
using MaxIm;
using System.Collections.Generic;
using System.Linq;

namespace CalFrameFactory
{
    public partial class Filters
    {

        const sbyte sbyteTrue = 1;
        const sbyte sbyteFalse = 0;

        public class ActiveFilter
        {
            //public ColorIndexing.StandardColors JcAssign { get; set; }
            public string FilterName { get; set; }
            public int FilterIndex { get; set; }
            public bool FilterActive { get; set; }
        }

        public static List<string> FilterNameSet()
        {
            Configuration cfg = new Configuration();
            if (cfg.ImagingApplication == Configuration.ImagingApp.TS)
                return FilterNameSetTSX();
            else
                return FilterNameSetMDL();
        }


        public static List<string> FilterNameSetTSX()
        {
            //Figure out the filter mapping
            //Find the filter name for the filter filter Number
            ccdsoftCamera tsx_cc = new ccdsoftCamera();
            try { tsx_cc.Connect(); }
            catch { return null; }
            int filterCount = tsx_cc.lNumberFilters;
            string[] TSXFilterList = new string[filterCount];
            for (int f = 0; f < filterCount; f++)
                TSXFilterList[f] = (tsx_cc.szFilterName(f));
            return TSXFilterList.ToList();
        }

        public static List<string> FilterNameSetMDL()
        {
            //Figure out the filter mapping
            //Find the filter name for the filter filter Number
            CCDCamera ccdc = new CCDCamera();
            List<string> MDLFilterListStr = new List<string>();
            try { ccdc.LinkEnabled = sbyteTrue; }
            catch { return null; }
            var MDLFilterList = ccdc.FilterNames;
            foreach (var filterName in MDLFilterList)
                MDLFilterListStr.Add(filterName.ToString());
            return MDLFilterListStr;
        }

        public static string LookUpFilterName(int filterIndex)
        {
            Configuration cfg = new Configuration();
            if (cfg.ImagingApplication == Configuration.ImagingApp.TS)
                return LookUpFilterNameTSX(filterIndex);
            else
                return LookUpFilterNameMDL(filterIndex);
        }

        public static string LookUpFilterNameTSX(int filterIndex)
        {
            ccdsoftCamera tsx_cc = new ccdsoftCamera();
            return (tsx_cc.szFilterName(filterIndex));
        }

        public static string LookUpFilterNameMDL(int filterIndex)
        {
            CCDCamera ccdc = new CCDCamera();
            try { ccdc.LinkEnabled = sbyteTrue; }
            catch { return null; }
            var MDLFilterList = ccdc.FilterNames;
            return MDLFilterList[filterIndex];
        }

        public static int? LookUpFilterIndex(string filterName)
        {
            List<string> fnl = FilterNameSet();
            if (fnl == null)
                return null;
            for (int i = 0; i < fnl.Count; i++)
                if (fnl[i].Contains(filterName))
                    return i;
            return null;
        }

        //public static string? LookUpAssignment(ColorIndexing.StandardColors jcAssign)
        //{
        //    Configuration cfg = new Configuration();
        //    if (File.Exists(cfg.ColorListPath))
        //    {
        //        ColorIndexing cL = new ColorIndexing();
        //        List<Filters.ActiveFilter> afList = cL.GetActiveFilters();
        //        Filters.ActiveFilter filter = afList.Find(x => x.JcAssign == jcAssign);
        //        return filter.FilterIndex.ToString();
        //    }
        //    else
        //        return null;
        //}

    }
}
