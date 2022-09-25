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

namespace ImstkUnity
{
    /// <summary>
    /// Used for models with deformable vertices. That is the vertices are changing
    /// per update of the model in global configuration.
    /// </summary>
    public abstract class DeformableModel : DynamicalModel
    {
        protected override void OnImstkInit()
        {
            // Get dependencies
            meshFilter = visualGeomFilter.gameObject.GetComponentFatal<MeshFilter>();
            transformComp = gameObject.GetComponentFatal<Transform>();

            // Make sure mesh filter is read/writable
            meshFilter.mesh.MarkDynamic();
            if (!meshFilter.mesh.isReadable)
            {
                Debug.LogWarning(gameObject.name + "'s MeshFilter Mesh must be readable (check the meshes import settings)");
                return;
            }

            imstkObject = InitObject();
            SimulationManager.sceneManager.getActiveScene().addSceneObject(imstkObject);
            InitGeometry();
            InitGeometryMaps();
            ProcessBoundaryConditions(gameObject.GetComponents<BoundaryCondition>());
            Configure();

        }

        protected override void InitGeometry()
        {
            // Copy all the geometries over to imstk, set the transform and
            // apply later. (to avoid applying transform twice *since two
            // geometries could point to the same one*)

            // Setup the visual geometry
            {
                Imstk.Geometry visualGeom = GetVisualGeometry();
                visualGeomFilter.MoveToGlobalSpace();
                imstkObject.setVisualGeometry(visualGeom);
            }

            // Setup the collision geometry
            if (collisionGeomFilter != null)
            {
                Imstk.Geometry colGeom = GetCollidingGeometry();
                collisionGeomFilter.MoveToGlobalSpace();
                imstkObject.setCollidingGeometry(colGeom);
            }
            else
            {
                Debug.LogError("No collision geometry provided to DynamicalModel on object " + gameObject.name);
            }

            // Setup the physics geometry
            if (physicsGeomFilter != null)
            {
                Imstk.Geometry physicsGeom = GetPhysicsGeometry();
                physicsGeomFilter.MoveToGlobalSpace();
                (imstkObject as Imstk.DynamicObject).setPhysicsGeometry(physicsGeom);
                (imstkObject as Imstk.DynamicObject).getDynamicalModel().setModelGeometry(physicsGeom);
            }
            else
            {
                Debug.LogError("No physics geometry provided to DynamicalModel on object " + gameObject.name);
            }
        }

        protected void InitGeometryMaps()
        {
            // Setup any geometry maps on the object
            // \todo: Generalize geometry maps in imstk, currently
            // well test geometry types to figure out maps
            GeometryMap[] geomMaps = gameObject.GetComponents<GeometryMap>();

            // \todo: Currently imstk only supports physicstovisual and physicstocollision
            // this needs to be generalized. For now we will only support two maps. There are
            // some other minute but tricky details to be worked out here.
            for (int i = 0; i < geomMaps.Length; i++)
            {
                GeometryMap map = geomMaps[i];
                // Test if map contains physics or visual
                if (map.parentGeom == physicsGeomFilter &&
                    map.childGeom == collisionGeomFilter)
                {
                    Imstk.DynamicObject dynObj = imstkObject as Imstk.DynamicObject;
                    Imstk.GeometryMap geomMap = map.GetMap();
                    if (geomMap != null)
                    {
                        dynObj.setPhysicsToCollidingMap(geomMap);
                        Debug.Log("Set up Physics to Collision Map");
                        geomMap.compute();
                    }
                }
                else if (map.parentGeom == physicsGeomFilter &&
                    map.childGeom == visualGeomFilter)
                {
                    Imstk.DynamicObject dynObj = imstkObject as Imstk.DynamicObject;
                    Imstk.GeometryMap geomMap = map.GetMap();
                    if (geomMap != null)
                    {
                        dynObj.setPhysicsToVisualMap(geomMap);
                        Debug.Log("Set up Physics to Visual Map");
                        geomMap.compute();
                    }
                }
                else if (map.parentGeom == collisionGeomFilter &&
                    map.childGeom == visualGeomFilter)
                {
                    Imstk.GeometryMap geomMap = map.GetMap();
                    if (geomMap != null)
                    {
                        imstkObject.setCollidingToVisualMap(geomMap);
                        Debug.Log("Set up Physics to Visual Map");
                        geomMap.compute();
                    }
                }
            }
        }

        /// <summary>
        /// Visual update of the geometry (only needs to call before render)
        /// </summary>
        public void Update()
        {
            if (SimulationManager.sceneManager == null)
            {
                Debug.LogWarning("Failed to update dynamical model on " + gameObject.name + " no SimulationManager");
                return;
            }

            //System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

            Imstk.PointSet visualGeom = Imstk.Utils.CastTo<Imstk.PointSet>(imstkObject.getVisualGeometry());
            meshFilter.mesh.vertices = MathUtil.ToVector3Array(visualGeom.getVertexPositions());

            //stopwatch.Stop();
            //Debug.Log("time (ms): " + stopwatch.ElapsedMilliseconds.ToString());

            if (meshFilter.mesh.GetTopology(0) == MeshTopology.Triangles ||
                meshFilter.mesh.GetTopology(0) == MeshTopology.Quads)
            {
                meshFilter.mesh.RecalculateNormals();
            }
            meshFilter.mesh.RecalculateBounds();
        }
    }
}