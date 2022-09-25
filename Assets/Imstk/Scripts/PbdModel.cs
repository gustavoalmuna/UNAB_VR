﻿/*=========================================================================

   Library: iMSTK-Unity

   Copyright (c) Kitware, Inc. 

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

      http://www.apache.org/licenses/LICENSE-2.0.txt

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.

=========================================================================*/

using System.Collections.Generic;
using UnityEngine;

namespace ImstkUnity
{
    [AddComponentMenu("iMSTK/PbdModel")]
    public class PbdModel : DeformableModel
    {
        public double distanceStiffness = 100.0;
        public bool useDistanceConstraint = true;

        public double dihedralStiffness = 10.0;
        public bool useDihedralConstraint = true;

        public double areaStiffness = 10.0;
        public bool useAreaConstraint = true;

        public double bendStiffness = 10.0;
        public int maxBendStride = 2;
        public bool useBendConstraint = false;

        public double volumeStiffness = 10.0;
        public bool useVolumeConstraint = false;

        public double youngsModulus = 5000.0;
        public double possionsRatio = 0.4;
        public double mu = 0.0;
        public double lambda = 0.0;
        public double viscousDampingCoeff = 0.01;
        public bool useYoungsModulus = true;
        public bool useFEMConstraint = false;
        public Imstk.PbdFemConstraint.MaterialType materialType = Imstk.PbdFemConstraint.MaterialType.StVK;

        public double uniformMassValue = 1.0;
        public Vector3 gravityAccel = new Vector3(0.0f, -9.81f, 0.0f);
        public int numIterations = 5;

        public double contactStiffness = 1.0;

        public bool useRealtime = false;
        public double dt = 0.01;
        public HashSet<int> fixedIndices = new HashSet<int>();

        protected override Imstk.CollidingObject InitObject()
        {
            Imstk.PbdObject pbdObject = new Imstk.PbdObject(GetFullName());
            pbdObject.setDynamicalModel(new Imstk.PbdModel());
            return pbdObject;
        }

        protected override void Configure()
        {
            Imstk.PbdModelConfig config = new Imstk.PbdModelConfig();

            if (useDistanceConstraint)
                config.enableConstraint(Imstk.PbdModelConfig.ConstraintGenType.Distance, distanceStiffness);

            if (useDihedralConstraint)
                config.enableConstraint(Imstk.PbdModelConfig.ConstraintGenType.Dihedral, dihedralStiffness);

            if (useAreaConstraint)
                config.enableConstraint(Imstk.PbdModelConfig.ConstraintGenType.Area, areaStiffness);

            if (useVolumeConstraint)
                config.enableConstraint(Imstk.PbdModelConfig.ConstraintGenType.Volume, volumeStiffness);

            if (useBendConstraint)
            {
                for (int i = 1; i <= maxBendStride; i++)
                {
                    config.enableBendConstraint(bendStiffness, i);
                }
            }

            if (useFEMConstraint)
            {
                if ((imstkObject as Imstk.DynamicObject).getPhysicsGeometry().getTypeName() != "TetrahedralMesh")
                {
                    Debug.Log("Currently only Tetrahedral mesh is supported for FEM constraints");
                }
                else
                {
                    if (useYoungsModulus)
                    {
                        config.m_femParams = new Imstk.PbdFemConstraintConfig(0.0, 0.0, youngsModulus, possionsRatio);
                    }
                    else
                    {
                        config.m_femParams = new Imstk.PbdFemConstraintConfig(mu, lambda, 0.0, 0.0);
                    }
                    config.enableFemConstraint(materialType);
                }
            }

            config.m_dt = dt;

            config.m_uniformMassValue = uniformMassValue;
            config.m_gravity = new Imstk.Vec3d(gravityAccel.x, gravityAccel.y, gravityAccel.z);
            config.m_iterations = (uint)numIterations;
            config.m_viscousDampingCoeff = viscousDampingCoeff;
            config.m_contactStiffness = contactStiffness;

            Imstk.PbdModel pbdModel = (imstkObject as Imstk.PbdObject).getPbdModel();
            pbdModel.configure(config);
            if (useRealtime)
            {
                pbdModel.setTimeStepSizeType(Imstk.TimeSteppingType.RealTime);
            }
            else
            {
                pbdModel.setTimeStepSizeType(Imstk.TimeSteppingType.Fixed);
            }

            config.m_fixedNodeIds = new Imstk.VectorSizet();
            foreach (int id in fixedIndices)
            {
                config.m_fixedNodeIds.Insert(0, (uint)id);
            }
        }

        protected void FixedUpdate()
        {
            if (useRealtime)
            {
                Imstk.PbdModel pbdModel = (imstkObject as Imstk.PbdObject).getPbdModel();
                pbdModel.setTimeStep(Time.fixedDeltaTime);
            }
        }

        protected override void ProcessBoundaryConditions(BoundaryCondition[] conditions)
        {
            // This model currently uses fixed vertices for BC, this is what we will compute
            // Clear them to start with
            fixedIndices.Clear();
            if (conditions.Length == 0)
                return;

            // For every BC test intersection to find fixed vertices
            foreach (BoundaryCondition condition in conditions)
            {
                // Create the surface mesh the points will be tested if inside of
                MeshFilter meshFilter = condition.bcObj.GetComponent<MeshFilter>();
                Transform transform = condition.bcObj.GetComponent<Transform>();
                if (meshFilter == null)
                    continue;

                // Convert unity to imstk geometry
                ImstkMesh bcGeometry = meshFilter.sharedMesh.ToImstkMesh(transform.localToWorldMatrix);
                Imstk.SurfaceMesh bcImstkGeometry = bcGeometry.ToImstkGeometry() as Imstk.SurfaceMesh;

                // Compute mask of enclosed points
                Imstk.SelectEnclosedPoints selectEnclosed = new Imstk.SelectEnclosedPoints();
                selectEnclosed.setInputMesh(bcImstkGeometry);
                Imstk.Geometry physicsGeom = (imstkObject as Imstk.PbdObject).getPhysicsGeometry();
                selectEnclosed.setInputPoints(Imstk.Utils.CastTo<Imstk.PointSet>(physicsGeom));
                selectEnclosed.setUsePruning(false);
                selectEnclosed.update();

                Imstk.DataArrayuc isInside = selectEnclosed.getIsInsideMask();
                byte[] isInsideBytes = new byte[isInside.size()];
                isInside.getValues(isInsideBytes);
                for (int i = 0; i < isInsideBytes.Length; i++)
                {
                    if (isInsideBytes[i] == 1)
                        fixedIndices.Add(i);
                }
            }
        }
    }
}