/*=========================================================================

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

using ImstkUnity;
using UnityEngine;
using UnityEditor;

namespace ImstkEditor
{
    [CustomEditor(typeof(PbdModel))]
    public class PbdModelEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            PbdModel script = target as PbdModel;
            EditorGUI.BeginChangeCheck();

            GeometryFilter visualGeomFilter = EditorUtils.GeomFilterField("Visual Geometry", script.visualGeomFilter);
            GeometryFilter physicsGeomFilter = EditorUtils.GeomFilterField("Physics Geometry", script.physicsGeomFilter);
            GeometryFilter collisionGeomFilter = EditorUtils.GeomFilterField("Collision Geometry", script.collisionGeomFilter);

            GUILayout.BeginVertical(EditorStyles.helpBox);
            bool useDistanceConstraint = EditorGUILayout.Toggle("Distance Stiffness", script.useDistanceConstraint);
            double distanceStiffness = script.distanceStiffness;
            if (useDistanceConstraint)
                distanceStiffness = EditorGUILayout.DoubleField("Stiffness", script.distanceStiffness);
            GUILayout.EndVertical();

            GUILayout.BeginVertical(EditorStyles.helpBox);
            bool useBendConstraint = EditorGUILayout.Toggle("Bend Stiffness", script.useBendConstraint);
            double bendStiffness = script.bendStiffness;
            int bendStride = script.maxBendStride;
            if (useBendConstraint)
            {
                bendStiffness = EditorGUILayout.DoubleField("Stiffness", script.bendStiffness);
                bendStride = EditorGUILayout.IntField("Stride", script.maxBendStride);
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical(EditorStyles.helpBox);
            bool useDihedralConstraint = EditorGUILayout.Toggle("Dihedral Stiffness", script.useDihedralConstraint);
            double dihedralStiffness = script.dihedralStiffness;
            if (useDihedralConstraint)
                dihedralStiffness = EditorGUILayout.DoubleField("Stiffness", script.dihedralStiffness);
            GUILayout.EndVertical();

            GUILayout.BeginVertical(EditorStyles.helpBox);
            bool useAreaConstraint = EditorGUILayout.Toggle("Area Stiffness", script.useAreaConstraint);
            double areaStiffness = script.areaStiffness;
            if (useAreaConstraint)
                areaStiffness = EditorGUILayout.DoubleField("Stiffness", script.areaStiffness);
            GUILayout.EndVertical();

            GUILayout.BeginVertical(EditorStyles.helpBox);
            bool useVolumeConstraint = EditorGUILayout.Toggle("Volume Stiffness", script.useVolumeConstraint);
            double volumeStiffness = script.volumeStiffness;
            if (useVolumeConstraint)
                volumeStiffness = EditorGUILayout.DoubleField("Stiffness", script.volumeStiffness);
            GUILayout.EndVertical();

            GUILayout.BeginVertical(EditorStyles.helpBox);
            bool useFEMConstraint = EditorGUILayout.Toggle("FEM", script.useFEMConstraint);
            double youngsModulus = script.youngsModulus;
            double possionsRatio = script.possionsRatio;
            bool useYoungsModulus = script.useYoungsModulus;
            double mu = script.mu;
            double lambda = script.lambda;
            Imstk.PbdFemConstraint.MaterialType materialType = script.materialType;
            if (useFEMConstraint)
            {
                useYoungsModulus = EditorGUILayout.Toggle("Use Youngs Modulus", script.useYoungsModulus);
                if (useYoungsModulus)
                {
                    youngsModulus = EditorGUILayout.DoubleField("Youngs Modulus", script.youngsModulus);
                    possionsRatio = EditorGUILayout.DoubleField("Possions Ratio", script.possionsRatio);
                }
                else
                {
                    mu = EditorGUILayout.DoubleField("Mu", script.mu);
                    lambda = EditorGUILayout.DoubleField("Lambda", script.lambda);
                }
                materialType = (Imstk.PbdFemConstraint.MaterialType)EditorGUILayout.EnumPopup("Material Type", script.materialType);
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical(EditorStyles.helpBox);
            bool useRealtime = EditorGUILayout.Toggle("Use Realtime", script.useRealtime);
            double dt = script.dt;
            if (!useRealtime)
                dt = EditorGUILayout.DoubleField("Timestep", script.dt);

            double uniformMassValue = EditorGUILayout.DoubleField("Uniform Mass Value", script.uniformMassValue);
            Vector3 gravityAccel = EditorGUILayout.Vector3Field("Gravity Accel", script.gravityAccel);
            int numIterations = EditorGUILayout.IntField("# Iterations", script.numIterations);
            double damping = EditorGUILayout.DoubleField("Viscous Damping Coeff", script.viscousDampingCoeff);
            double contactStiffness = EditorGUILayout.DoubleField("Contact Stiffness", script.contactStiffness);
            GUILayout.EndVertical();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RegisterCompleteObjectUndo(script, "Change of Parameters");
                script.useDistanceConstraint = useDistanceConstraint;
                script.distanceStiffness = distanceStiffness;
                script.useBendConstraint = useBendConstraint;
                script.bendStiffness = bendStiffness;
                script.maxBendStride = bendStride;
                script.useDihedralConstraint = useDihedralConstraint;
                script.dihedralStiffness = dihedralStiffness;
                script.useAreaConstraint = useAreaConstraint;
                script.areaStiffness = areaStiffness;
                script.useVolumeConstraint = useVolumeConstraint;
                script.volumeStiffness = volumeStiffness;
                script.useFEMConstraint = useFEMConstraint;
                script.youngsModulus = youngsModulus;
                script.possionsRatio = possionsRatio;
                script.mu = mu;
                script.lambda = lambda;
                script.useRealtime = useRealtime;
                script.dt = dt;
                script.uniformMassValue = uniformMassValue;
                script.gravityAccel = gravityAccel;
                script.numIterations = numIterations;
                script.useYoungsModulus = useYoungsModulus;
                script.materialType = materialType;
                script.viscousDampingCoeff = damping;
                script.contactStiffness = contactStiffness;

                script.visualGeomFilter = visualGeomFilter;
                script.physicsGeomFilter = physicsGeomFilter;
                script.collisionGeomFilter = collisionGeomFilter;
            }
        }
    }
}