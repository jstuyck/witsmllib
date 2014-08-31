using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace witsmllib.witsmllib
{
    class WitsmlCementStage
    {

        protected int? numStage;
        protected string typeStage;
        protected DateTime dTimMixStart;
        protected DateTime dTimPumpStart;
        protected DateTime dTimPumpEnd;
        protected DateTime dTimDisplaceStart;
        protected Value mdTop;
        protected Value mdBottom;
        protected Value volExcess;
        protected Value flowrateDisplaceAv;
        protected Value flowrateDisplaceMx;
        protected Value presDisplace;
        protected Value volReturns;
        protected Value eTimMudCirculation;
        protected Value flowrateMudCirc;
        protected Value presMudCirc;
        protected Value flowrateEnd;
        protected Value cementingFluid;
        protected Value afterFlowAnn;
        protected Value squeezeObj;
        protected Value squeezeObtained;
        protected Value mdString;
        protected Value mdTool;
        protected Value mdCoilTbg;
        protected Value volCsgIn;
        protected Value volCsgOut;
        protected Value tailPipeUsed;
        protected Value diaTailPipe;
        protected Value tailPipePerf;
        protected Value presTbgStart;
        protected Value presTbgEnd;
        protected Value presCsgStart;
        protected Value presCsgEnd;
        protected Value presBackPressure;
        protected Value presCoilTbgStart;
        protected Value presCoilTbgEnd;
        protected Value presBreakDown;
        protected Value flowrateBreakDown;
        protected Value presSqueezeAv;
        protected Value presSqueezeEnd;
        protected Value presSqueezeHeld;
        protected Value presSqueeze;
        protected Value eTimPresHeld;
        protected Value flowrateSqueezeAv;
        protected Value flowrateSqueezeMx;
        protected Value flowratePumpStart;
        protected Value flowratePumpEnd;
        protected Value pillBelowPlug;
        protected Value plugCatcher;
        protected Value mdCircOut;
        protected Value volCircPrior;
        protected string typeOriginalMud;
        protected Value wtMud;
        protected Value visFunnelMud;
        protected Value pvMud;
        protected Value ypMud;
        protected Value gel10Sec;
        protected Value gel10Min;
        protected Value tempBHCT;
        protected Value tempBHST;
        protected string volExcessMethod;
        protected string mixMethod;
        protected string densMeasBy;
        protected Value annFlowAfter;
        protected Value topPlug;
        protected Value botPlug;
        protected int botPlugNumber;
        protected Value plugBumped;
        protected Value presPriorBump;
        protected Value presBump;
        protected Value presHeld;
        protected Value floatHeld;
        protected Value volMudLost;
        protected string fluidDisplace;
        protected Value densDisplaceFluid;
        protected Value volDisplaceFluid;

        /// <summary>
        /// Return the average displacement rate of this cement stage.
        /// </summary>
        /// <returns>Average displacement rate of this cement stage.</returns>
        public Value getAverageDisplacementRate()
        {
            return flowrateDisplaceAv;
        }
        /// <summary>
        /// Return the average squeeze flowrate of this cement stage.
        /// </summary>
        /// <returns>Average squeeze flowrate of this cement stage.</returns>
        public Value getAverageSqueezeFlowrate()
        {
            return flowrateSqueezeAv;
        }

        /// <summary>
        ///  Return the average squeeze pressure of this cement stage.
        /// </summary>
        /// <returns>Average squeeze pressure of this cement stage.</returns>
        public Value getAverageSqueezePressure()
        {
            return presSqueezeAv;
        }

        /// <summary>
        /// Return the back pressure of this cement stage.
        /// </summary>
        /// <returns>Back pressure of this cement stage.</returns>
        public Value getBackPressure()
        {
            return presBackPressure;
        }

        /// <summary>
        /// Return the bottom hole circulating temperature of this cement stage.
        /// </summary>
        /// <returns>Bottom hole circulating temperature of this cement stage.</returns>
        public Value getBottomHoleCirculatingTemperature()
        {
            return tempBHCT;
        }

        /// <summary>
        /// Return the bottom hole static temperature of this cement stage.
        /// </summary>
        /// <returns>Bottom hole static temperature of this cement stage.</returns>
        public Value getBottomHoleStaticTemperature()
        {
            return tempBHST;
        }

        /// <summary>
        /// Return the bottom plug number of this cement stage.
        /// </summary>
        /// <returns>Bottom plug number of this cement stage.</returns>
        public int getBottomPlugNumber()
        {
            return botPlugNumber;
        }

        /// <summary>
        /// Return the breakdown flowrate of this cement stage.
        /// </summary>
        /// <returns>Breakdown flowrate of this cement stage.</returns>
        public Value getBreakdownFlowrate()
        {
            return flowrateBreakDown;
        }

        /// <summary>
        /// Return the breakdown pressure of this cement stage.
        /// </summary>
        /// <returns>Breakdown pressure of this cement stage.</returns>
        Value getBreakdownPressure()
        {
            return presBreakDown;
        }

        /// <summary>
        /// Return the bump pressure of this cement stage.
        /// </summary>
        /// <returns>Bump pressure of this cement stage.</returns>
        public Value getBumpPressure()
        {
            return presBump;
        }

        /// <summary>
        /// Return the casing pressure end of this cement stage.
        /// </summary>
        /// <returns>Casing pressure end of this cement stage.</returns>
        public Value getCasingPressureEnd()
        {
            return presCsgEnd;
        }

        /// <summary>
        /// Return the casing pressure start of this cement stage.
        /// </summary>
        /// <returns>Casing pressure start of this cement stage.</returns>
        public Value getCasingPressureStart()
        {
            return presCsgStart;
        }

        /// <summary>
        /// Return the circulate volume start of this cement stage.
        /// </summary>
        /// <returns>Circulate volume start of this cement stage.</returns>
        public Value getCirculateVolumeStart()
        {
            return volCircPrior;
        }

        /// <summary>
        /// Return the ctu pressure end of this cement stage.
        /// </summary>
        /// <returns>Ctu pressure end of this cement stage.</returns>
        public Value getCtuPressureEnd()
        {
            return presCoilTbgEnd;
        }
        /// <summary>
        /// Return the ctu pressure start of this cement stage.
        /// </summary>
        /// <returns>Ctu pressure start of this cement stage.</returns>

        public Value getCtuPressureStart()
        {
            return presCoilTbgStart;
        }

        /// <summary>
        /// Return the density measure method of this cement stage.
        /// </summary>
        /// <returns>Density measure method of this cement stage.</returns>
        public string getDensityMeasureMethod()
        {
            return densMeasBy;
        }

        /// <summary>
        /// Return the displacement fluid of this cement stage.
        /// </summary>
        /// <returns>Displacement fluid of this cement stage.</returns>
        public string getDisplacementFluid()
        {
            return fluidDisplace;
        }
        /// <summary>
        /// Return the displacement fluid density of this cement stage.
        /// </summary>
        /// <returns>Displacement fluid density of this cement stage.</returns>
        public Value getDisplacementFluidDensity()
        {
            return densDisplaceFluid;
        }

        /// <summary>
        /// Return the displacement fluid volume of this cement stage.
        /// </summary>
        /// <returns>Displacement fluid volume of this cement stage.</returns>
        public Value getDisplacementFluidVolume()
        {
            return volDisplaceFluid;
        }
        /// <summary>
        /// Return the displacement pressure of this cement stage.
        /// </summary>
        /// <returns>Displacement pressure of this cement stage.</returns>

        public Value getDisplacementPressure()
        {
            return presDisplace;
        }

        /// <summary>
        /// Return the displacing time start of this cement stage.
        /// </summary>
        /// <returns>Displacing time start of this cement stage.</returns>
        public DateTime getDisplacingTimeStart()
        {
            return dTimDisplaceStart;
        }
        /// <summary>
        /// Return the end flow rate of this cement stage.
        /// </summary>
        /// <returns>End flow rate of this cement stage.</returns>
        public Value getEndFlowRate()
        {
            return flowrateEnd;
        }
        /// <summary>
        /// Return the end tubing pressure of this cement stage.
        /// </summary>
        /// <returns>End tubing pressure of this cement stage.</returns>
        public Value getEndTubingPressure()
        {
            return presTbgEnd;
        }
        /// <summary>
        /// Return the excess volume of this cement stage.
        /// </summary>
        /// <returns>Excess volume of this cement stage.</returns>
        public Value getExcessVolume()
        {
            return volExcess;
        }

        /// <summary>
        /// Return the excess volume estimation method of this cement stage. 
        /// </summary>
        /// <returns>Excess volume estimation method of this cement stage. </returns>
        public string getExcessVolumeEstimationMethod()
        {
            return volExcessMethod;
        }

        /// <summary>
        /// Return the funnel mud viscosity of this cement stage.
        /// </summary>
        /// <returns>Funnel mud viscosity of this cement stage.</returns>
        public Value getFunnelMudViscosity()
        {
            return visFunnelMud;
        }

        /// <summary>
        /// Return the gel10min pressure of this cement stage.
        /// </summary>
        /// <returns>Gel10min pressure of this cement stage.</returns>
        public Value getGel10minPressure()
        {
            return gel10Min;
        }

        /// <summary>
        /// Return the gel10s pressure of this cement stage.
        /// </summary>
        /// <returns>Gel10s pressure of this cement stage.</returns>
        public Value getGel10sPressure()
        {
            return gel10Sec;
        }

        /// <summary>
        /// Return the held pressure of this cement stage.
        /// </summary>
        /// <returns>Held pressure of this cement stage.</returns>
        public Value getHeldPressure()
        {
            return presHeld;
        }

        /// <summary>
        /// Return the max displacement rate of this cement stage.
        /// </summary>
        /// <returns>Max displacement rate of this cement stage.</returns>
        public Value getMaxDisplacementRate()
        {
            return flowrateDisplaceMx;
        }

        /// <summary>
        /// Return the max squeeze flowrate of this cement stage.
        /// </summary>
        /// <returns>Max squeeze flowrate of this cement stage.</returns>
        public Value getMaxSqueezeFlowrate()
        {
            return flowrateSqueezeMx;
        }

        /// <summary>
        /// Return the md bottom of this cement stage.
        /// </summary>
        /// <returns>Md bottom of this cement stage.</returns>
        public Value getMdBottom()
        {
            return mdBottom;
        }

        /// <summary>
        /// Return the md circulate out of this cement stage.
        /// </summary>
        /// <returns></returns>
        public Value getMdCirculateOut()
        {
            return mdCircOut;
        }

        /// <summary>
        /// Return the md coil tubing of this cement stage.
        /// </summary>
        /// <returns>Md coil tubing of this cement stage.</returns>
        public Value getMdCoilTubing()
        {
            return mdCoilTbg;
        }

        /// <summary>
        /// Return the md string of this cement stage.
        /// </summary>
        /// <returns>Md string of this cement stage.</returns>
        public Value getMdString()
        {
            return mdString;
        }

        /// <summary>
        /// Return the md tool of this cement stage.
        /// </summary>
        /// <returns>Md tool of this cement stage.</returns>
        public Value getMdTool()
        {
            return mdTool;
        }

        /// <summary>
        /// Return the md top of this cement stage.
        /// </summary>
        /// <returns>Md top of this cement stage.</returns>
        public Value getMdTop()
        {
            return mdTop;
        }
        
        /// <summary>
        /// Return the mixing time start of this cement stage.
        /// </summary>
        /// <returns>Mixing time start of this cement stage.</returns>
        public DateTime getMixingTimeStart()
        {
            return dTimMixStart;
        }
         
        /// <summary>
        /// Return the mix method of this cement stage.
        /// </summary>
        /// <returns>Mix method of this cement stage.</returns>
 public string getMixMethod()
 {
     return mixMethod;
 }
          /// <summary>
          /// Return the mud circulation flow rate of this cement stage.
          /// </summary>
          /// <returns>Mud circulation flow rate of this cement stage.</returns>
 public Value getMudCirculationFlowRate()
 {
     return flowrateMudCirc;
 }
          
        /// <summary>
        ///  Return the mud circulation pressure of this cement stage.
        /// </summary>
        /// <returns>Mud circulation pressure of this cement stage.</returns>
 public Value getMudCirculationPressure()
 {
    return presMudCirc;
 }
          /// <summary>
          /// Return the mud circulation time of this cement stage.
          /// </summary>
          /// <returns>Mud circulation time of this cement stage.</returns>
 public Value getMudCirculationTime()
 {
return eTimMudCirculation;
 }
          
        /// <summary>
        ///  Return the mud density of this cement stage.
        /// </summary>
        /// <returns>Mud density of this cement stage.</returns>
 public Value getMudDensity()
 {
 return wtMud;
 }
  
        /// <summary>
        /// Return the mud lost volume of this cement stage.
        /// </summary>
        /// <returns>Mud lost volume of this cement stage.</returns>
 public Value getMudLostVolume()
 {
     return volMudLost;
 }
          
        /// <summary>
        /// Return the mud type of this cement stage.
        /// </summary>
        /// <returns>Mud type of this cement stage.</returns>
 public string getMudType()
 {
     return typeOriginalMud;
 }
          


    }
}
