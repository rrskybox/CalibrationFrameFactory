﻿// --------------------------------------------------------------------------------
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

namespace CalFrameFactory
{
    public partial class Filters
    {
        public class ActiveFilter
        {
            //public ColorIndexing.StandardColors JcAssign { get; set; }
            public string FilterName { get; set; }
            public int FilterIndex { get; set; }
            public bool FilterActive { get; set; }
        }

          public static string[] FilterNameSet()
        {
            //Figure out the filter mapping
            //Find the filter name for the filter filter Number
            ccdsoftCamera tsxc = new ccdsoftCamera();
            try { tsxc.Connect(); }
            catch { return null; }
            int filterCount = tsxc.lNumberFilters;
            string[] TSXFilterList = new string[filterCount];
            for (int f = 0; f < filterCount; f++)
                TSXFilterList[f] = (tsxc.szFilterName(f));
            return TSXFilterList;
        }

        public static string LookUpFilterName(int filterIndex)
        {
            ccdsoftCamera tsxc = new ccdsoftCamera();
            return (tsxc.szFilterName(filterIndex));
        }

        public static int? LookUpFilterIndex(string filterName)
        {
            string[] fnl = FilterNameSet();
            if (fnl == null)
                return null;
            for (int i = 0; i < fnl.Length; i++)
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
