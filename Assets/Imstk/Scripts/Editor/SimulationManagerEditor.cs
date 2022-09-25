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
    [CustomEditor(typeof(SimulationManager))]
    public class SimulationManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Define a style for centering text
            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperCenter;

            // Begin the script modification!
            SimulationManager script = target as SimulationManager;
            EditorGUI.BeginChangeCheck();

            GUILayout.Label("Scene Settings", centeredStyle);
            GUILayout.BeginVertical(EditorStyles.helpBox);
            bool writeTaskGraph = EditorGUILayout.Toggle("Write Task Graph", script.writeTaskGraph);
            EditorGUILayout.HelpBox(
               "The scene timestep here is linked to the Unity Fixed Timestep found in Project Settings->Time. Imstk" +
               " renders that one unusable. This is because it is set per project whereas it should be per scene. Users" +
               " can still set it in their own code if needbe. This parameter balances the Update vs FixedUpdate rate, or" +
               " render vs simulation update.",
               MessageType.Warning);
            float sceneTimestep = EditorGUILayout.FloatField("Unity Fixed Timestep", script.sceneFixedTimestep);
            GUILayout.EndVertical();

            GUILayout.Label("Rigid Body Settings", centeredStyle);
            GUILayout.BeginVertical(EditorStyles.helpBox);
            Vector3 rbdGravity = EditorGUILayout.Vector3Field("Gravity", script.rigidBodyGravity);
            int rbdIterations = EditorGUILayout.IntField("Max # Iterations", script.rigidBodyMaxNumIterations);
            bool rbdRealtime = EditorGUILayout.Toggle("Use Realtime", script.rbdUseRealtime);
            double rbdDt = script.rigidBodyDt;
            if (!rbdRealtime)
            {
                rbdDt = EditorGUILayout.DoubleField("Delta Time", script.rigidBodyDt);
            }
            GUILayout.EndVertical();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RegisterCompleteObjectUndo(script, "Change of Parameters");
                script.writeTaskGraph = writeTaskGraph;
                script.rigidBodyGravity = rbdGravity;
                script.rigidBodyMaxNumIterations = rbdIterations;
                script.rbdUseRealtime = rbdRealtime;
                script.rigidBodyDt = rbdDt;
                Time.fixedDeltaTime = script.sceneFixedTimestep = sceneTimestep;
            }
        }
    }
}