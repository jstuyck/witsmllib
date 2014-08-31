using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace witsmllib
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
        protected bool? afterFlowAnn;
        protected string squeezeObj;
        protected bool? squeezeObtained;
        protected Value mdString;
        protected Value mdTool;
        protected Value mdCoilTbg;
        protected Value volCsgIn;
        protected Value volCsgOut;
        protected bool? tailPipeUsed;
        protected Value diaTailPipe;
        protected bool? tailPipePerf;
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
        protected bool? presSqueezeHeld;
        protected Value presSqueeze;
        protected Value eTimPresHeld;
        protected Value flowrateSqueezeAv;
        protected Value flowrateSqueezeMx;
        protected Value flowratePumpStart;
        protected Value flowratePumpEnd;
        protected bool? pillBelowPlug;
        protected bool? plugCatcher;
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
        protected bool? annFlowAfter;
        protected bool? topPlug;
        protected bool? botPlug;
        protected int botPlugNumber;
        protected bool? plugBumped;
        protected Value presPriorBump;
        protected Value presBump;
        protected Value presHeld;
        protected bool? floatHeld;
        protected Value volMudLost;
        protected string fluidDisplace;
        protected Value densDisplaceFluid;
        protected Value volDisplaceFluid;

        /// <summary>
        /// Return the average displacement rate of this cement stage.
        /// </summary>
        /// <returns>Average displacement rate of this cement stage. May be null if absent or unknown.</returns>
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

        /// <summary>
        /// Return the plastic mud viscosity of this cement stage.
        /// </summary>
        /// <returns>Plastic mud viscosity of this cement stage.</returns>
        public Value getPlasticMudViscosity()
        {
            return pvMud;
        }

        /// <summary>
        /// Return the prior bump pressure of this cement stage.
        /// </summary>
        /// <returns>Prior bump pressure of this cement stage.</returns>
        public Value getPriorBumpPressure()
        {
            return presPriorBump;
        }

        /// <summary>
        /// Return the pump flowrate end of this cement stage.
        /// </summary>
        /// <returns>Pump flowrate end of this cement stage.</returns>
        public Value getPumpFlowrateEnd()
        {
            return flowratePumpEnd;
        }

        /// <summary>
        /// Return the pump flowrate start of this cement stage.
        /// </summary>
        /// <returns>Pump flowrate start of this cement stage.</returns>
        public Value getPumpFlowrateStart()
        {
            return flowratePumpStart;
        }

        /// <summary>
        /// Return the pumping time end of this cement stage.
        /// </summary>
        /// <returns>Pumping time end of this cement stage.</returns>
        public DateTime getPumpingTimeEnd()
        {
            return dTimPumpEnd;
        }

        /// <summary>
        /// Return the pumping time start of this cement stage.
        /// </summary>
        /// <returns>Pumping time start of this cement stage.</returns>
        public DateTime getPumpingTimeStart()
        {
            return dTimPumpStart;
        }

        /// <summary>
        /// Return the squeeze objective of this cement stage.
        /// </summary>
        /// <returns>Squeeze objective of this cement stage.</returns>
        public string getSqueezeObjective()
        {
            return squeezeObj;
        }

        /// <summary>
        /// Return the squeeze pressure of this cement stage.
        /// </summary>
        /// <returns>Squeeze pressure of this cement stage.</returns>
        public Value getSqueezePressure()
        {
            return presSqueeze;
        }

        /// <summary>
        /// Return the squeeze pressure end of this cement stage.
        /// </summary>
        /// <returns>Squeeze pressure end of this cement stage.</returns>
        public Value getSqueezePressureEnd()
        {
            return presSqueezeEnd;
        }

        /// <summary>
        /// Return the stage number of this cement stage.
        /// </summary>
        /// <returns>The stage number of this cement stage. May be null if absent or unknown.</returns>
        public int? getStageNumber()
        {
            return numStage;
        }

        /// <summary>
        /// Return the start tubing pressure of this cement stage.
        /// </summary>
        /// <returns>The start tubing pressure of this cement stage. May be null if absent or unknown.</returns>
        public Value getStartTubingPressure()
        {
            return presTbgStart;
        }

        /// <summary>
        /// Return the tail pipe diameter of this cement stage.
        /// </summary>
        /// <returns>The tail pipe diameter of this cement stage. May be null if absent or unknown.</returns>

        public Value getTailPipeDiameter()
        {
            return diaTailPipe;
        }

        /// <summary>
        /// Return the time pressure held of this cement stage.
        /// </summary>
        /// <returns>The time pressure held of this cement stage. May be null if absent or unknown.</returns>
        public Value getTimePressureHeld()
        {
            return eTimPresHeld;
        }

        /// <summary>
        /// Return the type of this cement stage.
        /// </summary>
        /// <returns>The type of this cement stage. May be null if absent or unknown.</returns>
        public string getType()
        {
            return typeStage;
        }

        /// <summary>
        /// Return the volume inside casing of this cement stage.
        /// </summary>
        /// <returns>The volume inside casing of this cement stage. May be null if absent or unknown.</returns>
        public Value getVolumeInsideCasing()
        {
            return volCsgIn;
        }

        /// <summary>
        /// Return the volume of returns of this cement stage.
        /// </summary>
        /// <returns>The volume of returns of this cement stage. May be null if absent or unknown.</returns>
        public Value getVolumeOfReturns()
        {
            return volReturns;
        }

        /// <summary>
        ///  Return the volume outside casing of this cement stage.
        /// </summary>
        /// <returns>The volume outside casing of this cement stage. May be null if absent or unknown.</returns>
        public Value getVolumeOutsideCasing()
        {
            return volCsgOut;
        }

        /// <summary>
        /// Return the yield point mud of this cement stage.
        /// </summary>
        /// <returns>The yield point mud of this cement stage. May be null if absent or unknown.</returns>
        public Value getYieldPointMud()
        {
            return ypMud;
        }

        /// <summary>
        /// Return if this cement stage is annular flow after.
        /// Values are "true" (or "1") and "false" (or "0"). 
        /// </summary>
        /// <returns>Is annular flow after of this cement stage? May be null if absent or unknown.</returns>

        public bool? isAnnularFlowAfter()
        {
            return annFlowAfter;
        }

        /// <summary>
        /// Return if this cement stage is annular flow at end.
        /// Values are "true" (or "1") and "false" (or "0"). 
        /// </summary>
        /// <returns>Is annular flow at end of this cement stage? May be null if absent or unknown.</returns>
        public bool? isAnnularFlowAtEnd()
        {
            return afterFlowAnn;
        }

        /// <summary>
        /// Return if this cement stage is bottom plug used.
        /// Values are "true" (or "1") and "false" (or "0"). 
        /// </summary>
        /// <returns>Is bottom plug used of this cement stage? May be null if absent or unknown.</returns>

        public bool? isBottomPlugUsed()
        {
            return botPlug;
        }

        /// <summary>
        /// Return if this cement stage is float held.
        /// Values are "true" (or "1") and "false" (or "0"). 
        /// </summary>
        /// <returns>Is float held of this cement stage? May be null if absent or unknown.</returns>
        public bool? isFloatHeld()
        {
            return floatHeld;
        }

        /// <summary>
        /// Return if this cement stage is pill below plug.
        /// Values are "true" (or "1") and "false" (or "0"). 
        /// </summary>
        /// <returns>Is pill below plug of this cement stage? May be null if absent or unknown.</returns>
        public bool? isPillBelowPlug()
        {
            return pillBelowPlug;
        }

        /// <summary>
        /// Return if this cement stage is plug bumped.
        /// Values are "true" (or "1") and "false" (or "0"). 
        /// </summary>
        /// <returns>Is plug bumped of this cement stage? May be null if absent or unknown.</returns>
        public bool? isPlugBumped()
        {
            return plugBumped;
        }

        /// <summary>
        /// Return if this cement stage is plug catcher.
        /// Values are "true" (or "1") and "false" (or "0"). 
        /// </summary>
        /// <returns>Is plug catcher of this cement stage? May be null if absent or unknown.</returns>
        public bool? isPlugCatcher()
        {
            return plugCatcher;
        }

        /// <summary>
        /// Return if this cement stage is squeeze obtained.
        /// Values are "true" (or "1") and "false" (or "0"). 
        /// </summary>
        /// <returns>Is squeeze obtained of this cement stage? May be null if absent or unknown.</returns>
        public bool? isSqueezeObtained()
        {
            return squeezeObtained;
        }

        /// <summary>
        /// Return if this cement stage is squeeze pressure held.
        /// Values are "true" (or "1") and "false" (or "0"). 
        /// </summary>
        /// <returns>Is squeeze pressure held of this cement stage? May be null if absent or unknown.</returns>
        public bool? isSqueezePressureHeld()
        {
            return presSqueezeHeld;
        }

        /// <summary>
        /// Return if this cement stage is tail pipe perforated. 
        /// Values are "true" (or "1") and "false" (or "0"). 
        /// </summary>
        /// <returns>Is tail pipe perforated of this cement stage? May be null if absent or unknown.</returns>
        public bool? isTailPipePerforated()
        {
            return tailPipePerf;
        }
        /// <summary>
        /// Return if this cement stage is tail pipe used. 
        /// Values are "true" (or "1") and "false" (or "0"). 
        /// </summary>
        /// <returns>Is tail pipe used of this cement stage? May be null if absent or unknown.</returns>
        public bool? isTailPipeUsed()
        {
            return tailPipeUsed;
        }

        /// <summary>
        /// Return if this cement stage is top plug used.
        /// Values are "true" (or "1") and "false" (or "0"). 
        /// </summary>
        /// <returns>Is top plug used of this cement stage? May be null if absent or unknown.</returns>
        public bool? isTopPlugUsed()
        {
            return topPlug;
        }



    }
}
