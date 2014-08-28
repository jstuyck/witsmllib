/*
nwitsml Copyright 2010 Setiri LLC
Derived from the jwitsml project, Copyright 2010 Statoil ASA
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
namespace witsmllib
{

    //import java.util.Collections;
    //import java.util.Date;
    //import java.util.TreeSet;
    //import java.util.Set;

    /**
     * Java representation of a WITSML "trajectory".
     *
     * @author <a href="mailto:info@nwitsml.org">NWitsml</a>
     */
    using System;
    using System.Collections.Generic;
    public abstract class WitsmlTrajectory : WitsmlObject
    {

        /** The WITSML type name */
        private static String WITSML_TYPE = "trajectory";

        protected  SortedSet<WitsmlTrajectoryStation> stations = new SortedSet<WitsmlTrajectoryStation>();  //TreeSet

        protected Boolean? _isGrowing; // objectGrowing (1.3+)
        protected String parentTrajectoryId; // uidTrajParent (1.2)
        protected String parentTrajectoryName; // nameTrajParent (1.2)
        protected DateTime?  stationsMeasurementStart; // dTimTrajStart
        protected DateTime?  stationsMeasurementEnd; // dTimTrajEnd
        protected Value mdMin; // mdMn
        protected Value mdMax; // mdMx
        protected String serviceCompany; // serviceCompany
        protected Value magneticAngle; // magDeclUsed
        protected Value gridCorrection; // gridCorUsed
        protected Value azimuthOfVerticalSection; // aziVertSect
        protected Value originNs; // dispNsVertSectOrig
        protected Value originEw; // dispEwVertSectOrig
        protected Boolean? _isDefinitive; // definitive
        protected Boolean? _isMemoryDump; // memory
        protected Boolean? _isFinal; // finalTraj
        protected String azimuthReference; // aziRef

        /**
         * Create a trajectory object with specified ID and parent.
         *
         * @param id        ID of this trajectory.
         * @param wellbore  Parent wellbore of this trajectory.
         */
        protected WitsmlTrajectory(WitsmlServer server,
                                   String id, String name,
                                   WitsmlObject parent, String parentId)
        
            :base(server, WITSML_TYPE, id, name, parent, parentId)
        {}

        /**
         * Get stations of this trajectory. A copy is returned, so we won't
         * have to think about locking issues (i.e. deadlock).
         *
         * @return  Stations of this trajectory.
         */
        public SortedSet<WitsmlTrajectoryStation> getStations()
        {
            return stations; // Collections.unmodifiableSet(stations);
        }

        /**
         * Return the last station of this trajectory.
         *
         * @return The last station of this trajectory.
         */
        public WitsmlTrajectoryStation getFirstStation()
        {
            return stations.Count > 0? stations.Min: null; //.First(); Stations.size() > 0 ? stations.first() : null;
        }

        /**
         * Return the last station of this trajectory.
         *
         * @return The last station of this trajectory.
         */
        public WitsmlTrajectoryStation getLastStation()
        {
            return stations.Count > 0? stations.Max: null; //.size() > 0 ? stations.last() : null;
        }

        public Boolean? isGrowing()
        {
            return _isGrowing;
        }

        public String getParentTrajectoryId()
        {
            return parentTrajectoryId;
        }

        public String getParentTrajectoryName()
        {
            return parentTrajectoryName;
        }

        /**
         * Get stations measurment start of this trajectory.
         *
         * @return  Stations measurement start of this trajactory.
         */
        public DateTime?  getStationsMeasurementStart()
        {
            return stationsMeasurementStart;// != null ?
                                            //   new DateTime(stationsMeasurementStart.getTime()) : null;
        }

        /**
         * Set stations measurement start of this trajectory.
         *
         * @param stationsMeasurementStart  Stations measurement start of this trajectory.
         */
        void setStationsMeasurementStart(DateTime?  stationsMeasurementStart)
        {
            this.stationsMeasurementStart = stationsMeasurementStart ; // != null ?
                                            //new DateTime(stationsMeasurementStart.getTime()) : null;
        }

        /**
         * Get stations measurement end of this trajectory.
         *
         * @return  Station measurement end of this trajactory.
         */
        public DateTime?  getStationsMeasurementEnd()
        {
            return stationsMeasurementEnd; // stationsMeasurementEnd != null ? new DateTime(stationsMeasurementEnd.getTime()) : null;
        }

        /**
         * Set stations measurement end of this trajectory.
         *
         * @param stationsMeasurementEnd  Stations measurement end of this trajectory.
         */
        void setStationsMeasurementEnd(DateTime? stationsMeasurementEnd)
        {
            this.stationsMeasurementEnd = stationsMeasurementEnd;// != null ?
                                          //new DateTime(stationsMeasurementEnd.getTime()) : null;
        }

        public String getMdUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
            {
                if (station.getMd() != null)
                    return station.getMd().getUnit();
            }

            return null;
        }

        public String getTvdUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
                if (station.getTvd() != null)
                    return station.getTvd().getUnit();

            return null;
        }

        public String getInclinationUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
                if (station.getInclination() != null)
                    return station.getInclination().getUnit();

            return null;
        }

        public String getAzimuthUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
                if (station.getAzimuth() != null)
                    return station.getAzimuth().getUnit();

            return null;
        }

        public String getToolfaceMagneticAngleUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
                if (station.getToolfaceMagneticAngle() != null)
                    return station.getToolfaceMagneticAngle().getUnit();

            return null;
        }

        public String getToolfaceGravityAngleUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
                if (station.getToolfaceGravityAngle() != null)
                    return station.getToolfaceGravityAngle().getUnit();

            return null;
        }

        public String getNorthUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
                if (station.getNorth() != null)
                    return station.getNorth().getUnit();

            return null;
        }

        public String getEastUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
                if (station.getEast() != null)
                    return station.getEast().getUnit();

            return null;
        }

        public String getVerticalSectionDistanceUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
                if (station.getVerticalSectionDistance() != null)
                    return station.getVerticalSectionDistance().getUnit();

            return null;
        }

        public String getDlsUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
                if (station.getDls() != null)
                    return station.getDls().getUnit();

            return null;
        }

        public String getTurnRateUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
                if (station.getTurnRate() != null)
                    return station.getTurnRate().getUnit();

            return null;
        }

        public String getBuildRateUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
                if (station.getBuildRate() != null)
                    return station.getBuildRate().getUnit();

            return null;
        }

        public String getDMdUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
                if (station.getDMd() != null)
                    return station.getDMd().getUnit();

            return null;
        }

        public String getDTvdUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
                if (station.getDTvd() != null)
                    return station.getDTvd().getUnit();

            return null;
        }

        public String getGravityUncertaintyUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
                if (station.getGravityUncertainty() != null)
                    return station.getGravityUncertainty().getUnit();

            return null;
        }

        public String getDipAngleUncertaintyUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
                if (station.getDipAngleUncertainty() != null)
                    return station.getDipAngleUncertainty().getUnit();

            return null;
        }

        public String getMagneticUncertaintyUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
                if (station.getMagneticUncertainty() != null)
                    return station.getMagneticUncertainty().getUnit();

            return null;
        }

        public String getGravitationFieldReferenceUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
                if (station.getGravitationFieldReference() != null)
                    return station.getGravitationFieldReference().getUnit();

            return null;
        }

        public String getMagneticFieldReferenceUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
                if (station.getMagneticFieldReference() != null)
                    return station.getMagneticFieldReference().getUnit();

            return null;
        }

        public String getMagneticDipAngleReferenceUnit()
        {
            foreach (WitsmlTrajectoryStation station in stations)
                if (station.getMagneticDipAngleReference() != null)
                    return station.getMagneticDipAngleReference().getUnit();

            return null;
        }

        public String getAzimuthReference()
        {
            return azimuthReference;
        }

        public Value getMdMin()
        {
            return mdMin;
        }

        public Value getMdMax()
        {
            return mdMax;
        }

        /**
         * Get service company of this trajectory.
         *
         * @return  Service company of this trajactory.
         */
        public String getServiceCompany()
        {
            return serviceCompany;
        }

        /**
         * Get magnetic angle of this trajectory.
         *
         * @return  Magnetic angle of this trajactory.
         */
        public Value getMagneticAngle()
        {
            return magneticAngle;
        }

        /**
         * Get grid correction of this trajectory.
         *
         * @return  Grid correction of this trajactory.
         */
        public Value getGridCorrection()
        {
            return gridCorrection;
        }

        /**
         * Get azimuth of vertical section of this trajectory.
         *
         * @return  Azimuth of vertical section of this trajactory.
         */
        public Value getAzimuthOfVerticalSection()
        {
            return azimuthOfVerticalSection;
        }

        /**
         * Get north/south origin of this trajectory.
         *
         * @return  North/south origin of this trajactory.
         */
        public Value getOriginNs()
        {
            return originNs;
        }

        /**
         * Get east/west origin of this trajectory.
         *
         * @return  East/west origin of this trajactory.
         */
        public Value getOriginEw()
        {
            return originEw;
        }

        /**
         * Check if this trajectory is definitive.
         *
         * @return  True if this trajectory is definitive, false otherwise.
         */
        public Boolean? isDefinitive()
        {
            return _isDefinitive;
        }

        public Boolean? isMemoryDump()
        {
            return _isMemoryDump;
        }

        /**
         * Check if this trajectory is final.
         *
         * @return  True if this trajectory is final, false otherwise.
         */
        public Boolean? isFinal()
        {
            return _isFinal;
        }
    }
}