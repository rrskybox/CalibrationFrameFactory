using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq.Expressions;
using System.Windows.Forms;
using TheSky64Lib;

namespace CalFrameFactory
{
    public class FlatMan
    {
        private string deviceId = null;
        private ASCOM.DriverAccess.CoverCalibrator device;

        public FlatMan()
        {
            CreateFlatManDevice();
            return;
        }

        public string CreateFlatManDevice()
        {
            // Create this device
            Configuration cfg = new Configuration();
            try
            {
                if (cfg.FlatPanelDeviceName == null)
                    cfg.FlatPanelDeviceName = ASCOM.DriverAccess.CoverCalibrator.Choose("");
                device = new ASCOM.DriverAccess.CoverCalibrator(cfg.FlatPanelDeviceName);
                device.Connected = true;
                return cfg.FlatPanelDeviceName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Flat panel driver error: " + ex.Message);
                return null;
            }
        }

        public bool IsConnected
        {
            get
            {
                ASCOM.DriverAccess.CoverCalibrator dvr;
                if (this.device != null)
                {
                    dvr = this.device;
                    bool cState = device.Connected;
                }
                if (this.device == null)
                    return false;
                if (device.Connected != true)
                    return false;
                return true;
            }
        }


        public bool Light
        {
            set
            {
                if (value == true)
                    device.CalibratorOn(50);
                else
                    device.CalibratorOff();
            }
        }

        public int Bright
        {
            set => device.CalibratorOn(value);
        }

        /// <summary>
        /// FMSetUp
        /// Prepares imaging for flats.  Closes dome, points telescope at MyFlat
        /// </summary>
        public bool FlatManStage()
        {
            //Console routine to set up the scope to use the FlatMan
            //  to be called from CCDAP or other apps prior to running flats
            //
            //Routine will find the "My Flat Field" location via TSX, after
            //  it has been installed in the SDB (see instructions)

            LogEvent lg = new LogEvent();
            Configuration cfg = new Configuration();

            lg.LogIt("Establishing TSX interfaces: star chart, mount, dome.");
            //Unpark (if parked), look up My Flat Field, slew to it, turn off tracking
            sky6RASCOMTele tsxm = new sky6RASCOMTele();
            tsxm.Connect();
            tsxm.Unpark();
            lg.LogIt("Looking up flat field reference position");

            Target ffTarget = FindTarget("MyFlatField");

            if (ffTarget == null)
            {
                lg.LogIt("Could not find My Flat Field");
                return false;
            }
            double altitude = ffTarget.Altitude;
            double azimuth = ffTarget.Azimuth;
            lg.LogIt("Slewing to flat field reference position");
            try
            {
                SlewRADec(ffTarget.RA, ffTarget.Dec, ffTarget.Name);
            }
            catch (Exception ex)
            {
                //If manual set up and slew error, then log and just keep going, otherwise terminate the flats session
                if (cfg.IsPortableFlatMan)
                {
                    MessageBox.Show("Slew to Flats reference position failure. " + ex.Message + " \r\n Manual setup so continuing flats session.");
                }
                else
                {
                    MessageBox.Show("Slew to Flats reference position failure. " + ex.Message + " \r\n Turn off slew limits.  Ending flats session.");
                    return false;
                }
            }
            lg.LogIt("Turning off tracking");
            TurnTrackingOff();

            //Disconnect from mount
            lg.LogIt("FlatMan positioning complete");
            if (cfg.IsPortableFlatMan)
            {
                lg.LogIt("Parking mount to wait for manual flatman set up");
                DialogResult dr = MessageBox.Show("Manual FlatMan set up selected:  Continue?", "Manual FlatMan Preparation", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.Cancel) return false;
            }
            return true;
        }


        public void FlatManClose()
        {
            //Unpark telescope, slew to park position and park
            LogEvent lg = new LogEvent();
            Configuration cfg = new Configuration();
            if (cfg.IsPortableFlatMan)
            {
                lg.LogIt("Parking mount to wait for manual flatman tear down");
                DialogResult dr = MessageBox.Show("Manual FlatMan set up selected:  Continue?", "Manual FlatMan Tear Down", MessageBoxButtons.OK);
            }
            ParkMount();
            //Turn off flatman
            device.Connected = false;
        }

        public static Target FindTarget(string targetName)
        {
            //Encapsulates TSX method to perform a find and return target information in
            //  a target object, and to center the star chart on the target

            sky6StarChart tsxs = new sky6StarChart();
            if (targetName != null)
            {
                //If targetName contains an underscore ("_") then convert to a slash
                string stargetName = targetName.Replace('_', '/');
                try
                { tsxs.Find(targetName.Replace('_', '/')); }
                catch
                {
                    return (null);
                }
            }
            else
            {
                return (null);
            }

            Target tgt = new Target
            {
                Name = targetName
            };
            //Get target position
            sky6ObjectInformation tsxo = new sky6ObjectInformation();
            tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_RA_2000);
            tgt.RA = tsxo.ObjInfoPropOut;
            tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_DEC_2000);
            tgt.Dec = tsxo.ObjInfoPropOut;
            tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_ALT);
            tgt.Altitude = tsxo.ObjInfoPropOut;
            tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_AZM);
            tgt.Azimuth = tsxo.ObjInfoPropOut;

            tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_RA_RATE_ASPERSEC);
            tgt.DeltaRARate = tsxo.ObjInfoPropOut;
            tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_DEC_RATE_ASPERSEC);
            tgt.DeltaDecRate = tsxo.ObjInfoPropOut;

            //Get target events
            tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_RISE_TIME);
            tgt.Rise = TimeSpan.FromHours(tsxo.ObjInfoPropOut);
            tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_SET_TIME);
            tgt.Set = TimeSpan.FromHours(tsxo.ObjInfoPropOut);
            tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_TRANSIT_TIME);
            tgt.Transit = TimeSpan.FromHours(tsxo.ObjInfoPropOut);
            tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_HA_HOURS);
            tgt.HA = TimeSpan.FromHours((double)tsxo.ObjInfoPropOut);
            tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_TWIL_ASTRON_START);
            try
            { tgt.Dusk = TimeSpan.FromHours(tsxo.ObjInfoPropOut); }
            catch { tgt.Dusk = TimeSpan.FromHours(0); }
            tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_TWIL_ASTRON_END);
            try
            { tgt.Dawn = TimeSpan.FromHours(tsxo.ObjInfoPropOut); }
            catch { tgt.Dawn = TimeSpan.FromHours(0); }
            //Get get observation location information
            tsxs.DocumentProperty(Sk6DocumentProperty.sk6DocProp_Latitude);
            tgt.Lat = tsxs.DocPropOut;
            tsxs.DocumentProperty(Sk6DocumentProperty.sk6DocProp_Longitude);
            tgt.Long = tsxs.DocPropOut;

            CenterStarChart(tgt);
            return (tgt);
        }

        public static void CenterStarChart(Target target)
        {
            //Centers the star chart on a target coordinates
            sky6StarChart tsxc = new sky6StarChart
            {
                RightAscension = target.RA,
                Declination = target.Dec
            };
            return;
        }

        public static void SlewRADec(double ra, double dec, string targetName)
        {
            //Make sure this is synchronous wait.
            sky6RASCOMTele tsxm = new sky6RASCOMTele { Asynchronous = 0 };
            try
            {
                tsxm.SlewToRaDec(ra, dec, targetName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Slew Faiure: " + ex.Message);
                return;
            }
            TurnTrackingOn();
            return;
        }

        public static void TurnTrackingOff()
        {
            sky6RASCOMTele tsxm = new sky6RASCOMTele();
            try { tsxm.SetTracking(0, 1, 0, 0); }
            catch { }
            return;
        }

        public static void TurnTrackingOn()
        {
            sky6RASCOMTele tsxm = new sky6RASCOMTele();
            try { tsxm.SetTracking(1, 1, 0, 0); }
            catch { }
        }

        public static void ParkMount()
        {
            sky6RASCOMTele tsxm = new sky6RASCOMTele();
            TurnTrackingOn();
            tsxm.Park();
            TurnTrackingOff(); //Although the park should automatically turn off tracking
        }

        public class Target
        {
            //class containing definition of a target -- name, ra, dec
            private string pName;
            private double pRA;
            private double pDec;
            private double pAlt;
            private double pAzm;
            private TimeSpan pHA;
            private TimeSpan pTransit;
            private double pLat;
            private double pLong;
            private TimeSpan pRise;
            private TimeSpan pSet;
            private TimeSpan pDusk;
            private TimeSpan pDawn;

            public Target() //Empty target object
            {
                pName = "";
                pRA = 0;
                pDec = 0;
                return;
            }

            public Target(string Name, double RA, double Dec)
            {
                pName = Name;
                pRA = RA;
                pDec = Dec;
                return;
            }

            public string Name
            {
                get => pName;
                set => pName = value;
            }

            public double RA
            {
                get => pRA;
                set => pRA = value;
            }

            public double Dec
            {
                get => pDec;
                set => pDec = value;
            }

            public double Lat
            {
                get => pLat;
                set => pLat = value;
            }

            public double Long
            {
                get => pLong;
                set => pLong = value;
            }

            public double Altitude
            {
                get => pAlt;
                set => pAlt = value;
            }

            public double Azimuth
            {
                get => pAzm;
                set => pAzm = value;
            }

            public TimeSpan Rise
            {
                get => pRise;
                set => pRise = value;
            }

            public TimeSpan Set
            {
                get => pSet;
                set => pSet = value;
            }

            public TimeSpan Transit
            {
                get => pTransit;
                set => pTransit = value;
            }

            public TimeSpan HA
            {
                get => pHA;
                set => pHA = value;
            }

            public TimeSpan Dusk
            {
                get => pDusk;
                set => pDusk = value;
            }

            public TimeSpan Dawn
            {
                get => pDawn;
                set => pDawn = value;
            }

            public double DeltaRARate { get; set; }
            public double DeltaDecRate { get; set; }

        }
    }
}
