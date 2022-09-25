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
    [CustomEditor(typeof(RbdModel))]
    class RbdModelEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            RbdModel script = target as RbdModel;
            EditorGUI.BeginChangeCheck();

            GeometryFilter collisionGeomFilter = EditorUtils.GeomFilterField("Collision Geometry", script.collisionGeomFilter);

            GUILayout.BeginVertical(EditorStyles.helpBox);
            double mass = EditorGUILayout.DoubleField("Mass", script.mass);
            if (mass < 0.0)
            {
                Debug.LogWarning("Mass cannot be negative!");
                mass = 0.0;
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label("Inertia Tensor");
            Vector3 inertiaRow1 = EditorGUILayout.Vector3Field("", script.inertia[0]);
            Vector3 inertiaRow2 = EditorGUILayout.Vector3Field("", script.inertia[1]);
            Vector3 inertiaRow3 = EditorGUILayout.Vector3Field("", script.inertia[2]);
            GUILayout.EndVertical();

            GUILayout.BeginVertical(EditorStyles.helpBox);
            Vector3 initVel = EditorGUILayout.Vector3Field("Initial Linear Velocity", script.initVelocity);
            Vector3 initAngularVel = EditorGUILayout.Vector3Field("Initial Angular Velocity", script.initAngularVelocity);
            GUILayout.EndVertical();

            GUILayout.BeginVertical(EditorStyles.helpBox);
            Vector3 initForce = EditorGUILayout.Vector3Field("Initial Force", script.initForce);
            Vector3 initTorque = EditorGUILayout.Vector3Field("Initial Torque", script.initTorque);
            GUILayout.EndVertical();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RegisterCompleteObjectUndo(script, "Change of Parameters");
                script.collisionGeomFilter = collisionGeomFilter;
                script.mass = mass;
                script.inertia[0] = inertiaRow1;
                script.inertia[1] = inertiaRow2;
                script.inertia[2] = inertiaRow3;
                script.initVelocity = initVel;
                script.initAngularVelocity = initAngularVel;
                script.initForce = initForce;
                script.initTorque = initTorque;
            }
        }
    }
}