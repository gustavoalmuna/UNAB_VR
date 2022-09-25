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

using UnityEngine;
using System;

namespace ImstkUnity
{
    /// <summary>
    /// Like a Unity MeshFilter, except it can hold both unity mesh types
    /// and Imstk Geometry type. Use GetOutputGeometry() to get the output
    /// in Imstk.Geometry (will convert whatever it's contents as needed)
    /// </summary>
    [AddComponentMenu("iMSTK/GeometryFilter")]
    public class GeometryFilter : ImstkBehaviour
    {
        // GeometryFilter supports either type
        public Geometry inputImstkGeom = null;
        public Mesh inputUnityGeom = null;

        // It can output this type
        // Note: Imstk.Geometry cannot transition over editor and runtime
        public Imstk.Geometry outputImstkGeom = null;

        // Used to denote which should be seated, not what is currently
        public GeometryType type = GeometryType.UnityMesh;

        // Used to toggle drawing of it in the editor, ideally this would not
        // exist in runtime code, but unity's architecture doesn't allow
        public bool showHandles = true;

        // Use to write the mesh as seen by iMSTK on creation,
        // this can help with debugging mesh issues
        public bool writeMesh = false;

        private bool inGlobalSpace = false;

        public void MoveToGlobalSpace()
        {
            // Models in Unity are provided in Mesh pre transformed.
            // We move the entire model to world space for simulation. Meaning the transform
            // is applied before giving the simulation geometry. Here we store that initial
            // transform and reset the local transform

            if (inGlobalSpace) return;

            var localToWorld = gameObject.transform.localToWorldMatrix.ToMat4d();
            var filters = gameObject.GetComponents<GeometryFilter>();
            foreach (var filter in filters)
            {
                if (filter.inGlobalSpace) continue;
                // World coordinates
                var geometry = filter.GetOutputGeometry();
                geometry.transform(localToWorld, Imstk.Geometry.TransformType.ApplyToData);
                geometry.setTransform(Imstk.Mat4d.Identity());
                filter.inGlobalSpace = true;
            }

            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localScale = Vector3.one;
            gameObject.transform.localRotation = Quaternion.identity;

            // Re-parent to itself for the period of the simulation run, this avoids having 
            // to recalculate the local transforms when the object is in a hierarchy
            gameObject.transform.SetParent(null, false);
        }

        public void SetGeometry(Geometry geom)
        {
            inputImstkGeom = geom;
            type = geom.geomType;
        }
        public void SetGeometry(Mesh geom)
        {
            inputUnityGeom = geom;
            type = GeometryType.UnityMesh;
        }

        /// <summary>
        /// This will convert the input to output, allocating in native code
        /// </summary>
        public Imstk.Geometry GetOutputGeometry()
        {
            if (outputImstkGeom == null)
            {
                if (type == GeometryType.UnityMesh)
                    outputImstkGeom = inputUnityGeom.ToImstkGeometry();
                else
                    outputImstkGeom = inputImstkGeom.ToImstkGeometry();
                if (writeMesh)
                {
                    var components = gameObject.GetComponents<GeometryFilter>();
                    int i = Array.IndexOf(components, this);

                    var mesh = Imstk.Utils.CastTo<Imstk.SurfaceMesh>(outputImstkGeom);
                    if (mesh != null)
                    {
                        //mesh.flipNormals();
                        mesh.correctWindingOrder();
                        mesh.computeVertexNormals();
                        mesh.computeTrianglesNormals();

                        Imstk.MeshIO.write(mesh, gameObject.name+"_"+ i.ToString() + ".vtk");
                    }
                    
                }
            }
            return outputImstkGeom;
        }
    }
}