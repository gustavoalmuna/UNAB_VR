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
    [CustomEditor(typeof(RigidController))]
    public class RigidControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            RigidController script = target as RigidController;
            EditorGUI.BeginChangeCheck();

            GUILayout.BeginVertical(EditorStyles.helpBox);
            TrackingDevice results = script.device;
            Object obj = EditorGUILayout.ObjectField("Device", results, typeof(Object), true) as Object;
            if (obj is TrackingDevice)
            {
                results = obj as TrackingDevice;
            }
            else if (obj is GameObject)
            {
                results = (obj as GameObject).GetComponent<TrackingDevice>();
            }
            else
            {
                Debug.LogWarning("Cannot set object on field, expects TrackingDevice or game object with TrackingDevice");
            }
            RbdModel model = EditorGUILayout.ObjectField("RbdModel", script.rbdModel, typeof(RbdModel), true) as RbdModel;
            GUILayout.EndVertical();


            GUILayout.BeginVertical(EditorStyles.helpBox);
            bool debugController = EditorGUILayout.Toggle("Write Controller transform", script.debugController);
            double linearKs = EditorGUILayout.DoubleField("Linear Spring Constant", script.linearKs);
            double linearKd = EditorGUILayout.DoubleField("Linear Damping", script.linearKd);
            double angularKs = EditorGUILayout.DoubleField("Angular Spring Constant", script.angularKs);
            double angularKd = EditorGUILayout.DoubleField("Angular Damping", script.angularKd);
            bool useCriticalDamping = EditorGUILayout.Toggle("Critical Damping", script.useCriticalDamping);
            GUILayout.EndVertical();

            GUILayout.BeginVertical(EditorStyles.helpBox);
            double forceScaling = EditorGUILayout.DoubleField("Force Scaling", script.forceScale);
            bool useForceSmoothing = EditorGUILayout.Toggle("Use Force Smoothing", script.useForceSmoothing);
            int forceSmoothKernelSize =
                EditorGUILayout.IntField("Force Smooth Kernel Size", script.forceSmoothingKernelSize);
            GUILayout.EndVertical();

            GUILayout.BeginVertical(EditorStyles.helpBox);
            Vector3 translationalOffset =
                EditorGUILayout.Vector3Field("Translational Offset", script.translationalOffset);
            Quaternion rotOffset =
                EditorGUILayout.Vector4Field("Rotational Offset", script.rotationalOffset.ToVector4()).ToQuat();
            Quaternion localRotOffset =
                EditorGUILayout.Vector4Field("Local Rotational Offset", script.localRotationalOffset.ToVector4()).ToQuat();
            double translationScaling =
                EditorGUILayout.DoubleField("Translation Scaling", script.translationScaling);
            GUILayout.EndVertical();

            GUILayout.BeginVertical(EditorStyles.helpBox);
            bool invertX = EditorGUILayout.Toggle("Invert X", script.invertX);
            bool invertY = EditorGUILayout.Toggle("Invert Y", script.invertY);
            bool invertZ = EditorGUILayout.Toggle("Invert Z", script.invertZ);
            bool rotInvertX = EditorGUILayout.Toggle("Rotation Invert X", script.invertRotX);
            bool rotInvertY = EditorGUILayout.Toggle("Rotation Invert Y", script.invertRotY);
            bool rotInvertZ = EditorGUILayout.Toggle("Rotation Invert Z", script.invertRotZ);
            GUILayout.EndVertical();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RegisterCompleteObjectUndo(script, "Change of Parameters");
                script.device = results;
                script.rbdModel = model;

                script.angularKd = angularKd;
                script.angularKs = angularKs;
                script.linearKd = linearKd;
                script.linearKs = linearKs;

                script.useCriticalDamping = useCriticalDamping;

                script.forceScale = forceScaling;
                script.useForceSmoothing = useForceSmoothing;
                script.forceSmoothingKernelSize = forceSmoothKernelSize;

                script.translationalOffset = translationalOffset;
                script.rotationalOffset = rotOffset;
                script.localRotationalOffset = localRotOffset;
                script.translationScaling = translationScaling;

                script.invertX = invertX;
                script.invertY = invertY;
                script.invertZ = invertZ;
                script.invertRotX = rotInvertX;
                script.invertRotY = rotInvertY;
                script.invertRotZ = rotInvertZ;

                script.debugController = debugController;
            }
        }
    }
}