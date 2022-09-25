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
using UnityEditor;
using UnityEngine;

namespace ImstkEditor
{
    /// <summary>
    /// Used to display geometry components in the editor view and inspector
    /// </summary>
    [CustomEditor(typeof(GeometryFilter), true)]
    [InitializeOnLoad]
    class GeometryFilterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            OnGeomGUI();

            OnPrimitiveGeomGUI();
        }

        protected void OnGeomGUI()
        {
            GeometryFilter geomFilter = target as GeometryFilter;

            EditorGUI.BeginChangeCheck();

            bool showHandles = EditorGUILayout.Toggle("Show Handles", geomFilter.showHandles);
            bool writeMesh = EditorGUILayout.Toggle("Write Mesh", geomFilter.writeMesh);
            GeometryType newType = (GeometryType)EditorGUILayout.EnumPopup("Geom Type", geomFilter.type);
            UnityEngine.Object abstractMesh = null;

            // If unity mesh, let user slot an asset
            if (newType == GeometryType.UnityMesh)
            {
                // Accepts either a Mesh or MeshFilter. If MeshFilter, then pulls the mesh out and uses that
                // This is because users
                UnityEngine.Object obj = EditorGUILayout.ObjectField("Mesh", geomFilter.inputUnityGeom, typeof(UnityEngine.Object), true);
                if (obj as MeshFilter != null)
                {
                    abstractMesh = (obj as MeshFilter).sharedMesh;
                }
                else if (obj as Mesh != null)
                {
                    abstractMesh = obj as Mesh;
                }
                else
                {
                    if (abstractMesh != null)
                    {
                        Debug.LogWarning("Tried to set object of type " + abstractMesh.GetType().Name + " on GeometryFilter");
                    }
                }
            }
            // If an imstk mesh, let user slot an asset for it
            else if (newType == GeometryType.SurfaceMesh ||
                newType == GeometryType.LineMesh ||
                newType == GeometryType.TetrahedralMesh ||
                newType == GeometryType.HexahedralMesh ||
                newType == GeometryType.PointSet)
            {
                abstractMesh = EditorGUILayout.ObjectField("iMSTKMesh", geomFilter.inputImstkGeom, typeof(Geometry), true);
            }

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RegisterCompleteObjectUndo(geomFilter, "Change of GeomFilter");

                geomFilter.showHandles = showHandles;
                geomFilter.writeMesh = writeMesh;

                // If the type changed unslot the geometry from the component
                if (newType != geomFilter.type)
                {
                    geomFilter.inputImstkGeom = null;
                    geomFilter.inputUnityGeom = null;
                    geomFilter.type = newType;
                    abstractMesh = null;
                }

                // If the mesh changed, call the appropriate setter for it
                if (abstractMesh != null)
                {
                    if ((abstractMesh as Mesh) != null)
                        geomFilter.SetGeometry(abstractMesh as Mesh);
                    else if ((abstractMesh as Geometry) != null)
                        geomFilter.SetGeometry(abstractMesh as Geometry);
                }
                SceneView.RepaintAll();
            }
        }

        protected void OnPrimitiveGeomGUI()
        {
            GeometryFilter geomFilter = target as GeometryFilter;

            if (geomFilter.type == GeometryType.Capsule)
            {
                if (geomFilter.inputImstkGeom == null)
                {
                    geomFilter.SetGeometry(CreateInstance<Capsule>());
                }
                Capsule geom = geomFilter.inputImstkGeom as Capsule;

                EditorGUI.BeginChangeCheck();

                Vector3 center = EditorGUILayout.Vector3Field("Center", geom.center);
                float radius = Mathf.Max(EditorGUILayout.FloatField("Radius", geom.radius), float.Epsilon);
                float length = Mathf.Max(EditorGUILayout.FloatField("Length", geom.length), float.Epsilon);
                Quaternion orientation = EditorGUILayout.Vector4Field("Orientation", geom.orientation.ToVector4()).ToQuat();

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RegisterCompleteObjectUndo(geomFilter, "Change of GeomFilter Geom");

                    geom.center = center;
                    geom.radius = radius;
                    geom.length = length;
                    geom.orientation = orientation;
                    SceneView.RepaintAll();
                }
            }
            else if (geomFilter.type == GeometryType.Cylinder)
            {
                if (geomFilter.inputImstkGeom == null)
                {
                    geomFilter.SetGeometry(CreateInstance<Cylinder>());
                }
                Cylinder geom = geomFilter.inputImstkGeom as Cylinder;

                EditorGUI.BeginChangeCheck();

                Vector3 center = EditorGUILayout.Vector3Field("Center", geom.center);
                float radius = Mathf.Max(EditorGUILayout.FloatField("Radius", geom.radius), float.Epsilon);
                float length = Mathf.Max(EditorGUILayout.FloatField("Length", geom.length), float.Epsilon);
                Quaternion orientation = EditorGUILayout.Vector4Field("Orientation", geom.orientation.ToVector4()).ToQuat();

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RegisterCompleteObjectUndo(geomFilter, "Change of GeomFilter Geom");

                    geom.center = center;
                    geom.radius = radius;
                    geom.length = length;
                    geom.orientation = orientation;
                    SceneView.RepaintAll();
                }
            }
            else if (geomFilter.type == GeometryType.OrientedBox)
            {
                if (geomFilter.inputImstkGeom == null)
                {
                    geomFilter.SetGeometry(CreateInstance<OrientedBox>());
                }
                OrientedBox geom = geomFilter.inputImstkGeom as OrientedBox;

                EditorGUI.BeginChangeCheck();

                Vector3 center = EditorGUILayout.Vector3Field("Center", geom.center);
                Vector3 extents = EditorGUILayout.Vector3Field("Extents", geom.extents).cwiseMax(new Vector3(float.Epsilon, float.Epsilon, float.Epsilon)); ;
                Quaternion orientation = EditorGUILayout.Vector4Field("Orientation", geom.orientation.ToVector4()).ToQuat();

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RegisterCompleteObjectUndo(geomFilter, "Change of GeomFilter Geom");

                    geom.center = center;
                    geom.extents = extents;
                    geom.orientation = orientation;
                    SceneView.RepaintAll();
                }
            }
            else if (geomFilter.type == GeometryType.Plane)
            {
                if (geomFilter.inputImstkGeom == null)
                {
                    geomFilter.SetGeometry(CreateInstance<ImstkUnity.Plane>());
                }
                ImstkUnity.Plane geom = geomFilter.inputImstkGeom as ImstkUnity.Plane;

                EditorGUI.BeginChangeCheck();

                Vector3 center = EditorGUILayout.Vector3Field("Center", geom.center);
                Vector3 normal = EditorGUILayout.Vector3Field("Normal", geom.normal);
                float width = Mathf.Max(EditorGUILayout.FloatField("Visual Width", geom.visualWidth), float.Epsilon);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RegisterCompleteObjectUndo(geomFilter, "Change of GeomFilter Geom");

                    geom.center = center;
                    geom.normal = normal;
                    geom.visualWidth = width;
                    SceneView.RepaintAll();
                }
            }
            else if (geomFilter.type == GeometryType.Sphere)
            {
                if (geomFilter.inputImstkGeom == null)
                {
                    geomFilter.SetGeometry(CreateInstance<Sphere>());
                }
                Sphere geom = geomFilter.inputImstkGeom as Sphere;

                EditorGUI.BeginChangeCheck();

                Vector3 center = EditorGUILayout.Vector3Field("Center", geom.center);
                float radius = Mathf.Max(EditorGUILayout.FloatField("Radius", geom.radius), float.Epsilon);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RegisterCompleteObjectUndo(geomFilter, "Change of GeomFilter Geom");

                    geom.center = center;
                    geom.radius = radius;
                    SceneView.RepaintAll();
                }
            }
        }

        [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
        static void DrawHandles(GeometryFilter geomFilter, GizmoType gizmoType)
        {
            if (!geomFilter.showHandles)
                return;

            if (geomFilter.type == GeometryType.UnityMesh)
            {
                Transform transform = geomFilter.gameObject.GetComponent<Transform>();
                Mesh mesh = geomFilter.inputUnityGeom;
                if (mesh)
                {
                    //Gizmos.DrawMesh(mesh);
                    Gizmos.DrawWireMesh(mesh, transform.position, transform.rotation, transform.lossyScale);
                }
            }
            else
            {
                Transform transform = geomFilter.gameObject.GetComponent<Transform>();
                Geometry geom = geomFilter.inputImstkGeom;
                if (geom.geomType == GeometryType.Capsule)
                {
                    Capsule capsule = geom as Capsule;

                    Mesh displayMesh = capsule.GetMesh();
                    Gizmos.DrawWireMesh(displayMesh, 0, transform.position, transform.rotation, new Vector3(1.0f, 1.0f, 1.0f));
                }
                else if (geom.geomType == GeometryType.Cylinder)
                {
                    Cylinder cylinder = geom as Cylinder;

                    Mesh displayMesh = cylinder.GetMesh();
                    Gizmos.DrawWireMesh(displayMesh, 0, transform.position, transform.rotation, new Vector3(1.0f, 1.0f, 1.0f));
                }
                else if (geom.geomType == GeometryType.OrientedBox)
                {
                    OrientedBox orientedBox = geom as OrientedBox;

                    Mesh displayMesh = orientedBox.GetMesh();
                    Gizmos.DrawWireMesh(displayMesh, 0, transform.position, transform.rotation, transform.localScale);
                }
                else if (geom.geomType == GeometryType.Plane)
                {
                    // Unity's planes default config for drawing is normal along z. So we rotate from z to normal
                    ImstkUnity.Plane plane = geom as ImstkUnity.Plane;
                    Handles.RectangleHandleCap(0, plane.GetTransformedCenter(transform),
                       Quaternion.FromToRotation(Vector3.forward, plane.GetTransformedNormal(transform)), plane.visualWidth, EventType.Repaint);
                }
                else if (geom.geomType == GeometryType.Sphere)
                {
                    Sphere sphere = geom as Sphere;
                    Mesh displayMesh = sphere.GetMesh();
                    Gizmos.DrawWireMesh(displayMesh, 0, transform.position, transform.rotation, new Vector3(1.0f, 1.0f, 1.0f));
                }
                else if (geom.geomType == GeometryType.PointSet)
                {
                    ImstkMesh mesh = geom as ImstkMesh;
                    Gizmos.DrawWireMesh(mesh.ToMesh(), transform.position, transform.rotation, transform.localScale);
                }
                else if (geom.geomType == GeometryType.LineMesh)
                {
                    ImstkMesh mesh = geom as ImstkMesh;
                    Gizmos.DrawWireMesh(mesh.ToMesh(), transform.position, transform.rotation, transform.localScale);
                }
                else if (geom.geomType == GeometryType.SurfaceMesh)
                {
                    ImstkMesh mesh = geom as ImstkMesh;
                    Gizmos.DrawWireMesh(mesh.ToMesh(), transform.position, transform.rotation, transform.localScale);
                }
                else if (geom.geomType == GeometryType.TetrahedralMesh)
                {
                    ImstkMesh mesh = geom as ImstkMesh;
                }
            }
        }
    }
}