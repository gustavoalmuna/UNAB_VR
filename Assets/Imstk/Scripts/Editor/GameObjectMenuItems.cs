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
    /// <summary>
    /// This class adds menu items for various parts of Unity
    /// </summary>
    public class GameObjectMenuItems : Editor
    {
        [MenuItem("GameObject/iMSTK/SimulationManager")]
        [MenuItem("CONTEXT/iMSTK/SimulationManager")]
        [MenuItem("iMSTK/GameObject/SimulationManager")]
        private static void CreateSimulationManagerGameObject()
        {
            GameObject newObj = new GameObject("SimulationManager");
            newObj.AddComponent(typeof(SimulationManager));
        }



        /// <summary>
        /// Creates a GameObject with a PbdModel and Tet cube
        /// </summary>
        [MenuItem("GameObject/iMSTK/PbdObjects/PbdVolume")]
        [MenuItem("CONTEXT/iMSTK/PbdObjects/PbdVolume")]
        [MenuItem("iMSTK/GameObject/PbdObjects/PbdVolume")]
        private static void CreatePbdVolume()
        {
            GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newObj.name = "PbdVolume";
            DestroyImmediate(newObj.GetComponent<Collider>());

            PbdModel model = newObj.AddComponent<PbdModel>();
            model.useDistanceConstraint = false;
            model.useAreaConstraint = false;
            model.useDihedralConstraint = false;
            model.useVolumeConstraint = false;
            model.useFEMConstraint = true;
            model.viscousDampingCoeff = 0.01;

            ImstkMesh tetCubeMesh = Utility.GetTetCubeMesh();
            Imstk.TetrahedralMesh imstkTetMesh = tetCubeMesh.ToImstkGeometry() as Imstk.TetrahedralMesh;
            ImstkMesh surfMesh = imstkTetMesh.extractSurfaceMesh().ToImstkMesh();
            surfMesh.name = tetCubeMesh.name + "_surface";

            MeshFilter meshFilter = newObj.GetComponent<MeshFilter>();
            meshFilter.sharedMesh = surfMesh.ToMesh();
            GeometryFilter visualGeom = newObj.AddComponent<GeometryFilter>();
            visualGeom.SetGeometry(meshFilter.sharedMesh);
            visualGeom.showHandles = false;
            GeometryFilter physicsGeom = newObj.AddComponent<GeometryFilter>();
            physicsGeom.SetGeometry(tetCubeMesh);
            physicsGeom.showHandles = false;

            model.visualGeomFilter = visualGeom;
            model.physicsGeomFilter = physicsGeom;
            model.collisionGeomFilter = visualGeom;

            DeformableMap map = newObj.AddComponent<DeformableMap>();
            map.parentGeom = physicsGeom;
            map.childGeom = visualGeom;
        }

        /// <summary>
        /// Creates a GameObject with a PbdModel and a Pbd Plane
        /// </summary>
        [MenuItem("GameObject/iMSTK/PbdObjects/PbdCloth")]
        [MenuItem("CONTEXT/iMSTK/PbdObjects/PbdCloth")]
        [MenuItem("iMSTK/GameObject/PbdObjects/PbdCloth")]
        private static void CreatePbdCloth()
        {
            GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Plane);
            newObj.name = "PbdCloth";
            DestroyImmediate(newObj.GetComponent<Collider>());

            PbdModel model = newObj.AddComponent<PbdModel>();
            model.useDistanceConstraint = true;
            model.useDihedralConstraint = true;
            model.useAreaConstraint = false;
            model.useVolumeConstraint = false;
            model.useFEMConstraint = false;
            model.dt = 0.03;
            model.distanceStiffness = 100.0;
            model.dihedralStiffness = 10.0;
            model.viscousDampingCoeff = 0.01;
            model.uniformMassValue = 0.05;
            model.numIterations = 3;

            ImstkMesh mesh = Utility.GetXYPlane(19, 19);
            MeshFilter meshFilter = newObj.GetComponent<MeshFilter>();
            meshFilter.sharedMesh = mesh.ToMesh();
            GeometryFilter visualGeom = newObj.AddComponent<GeometryFilter>();
            visualGeom.SetGeometry(meshFilter.sharedMesh);
            visualGeom.showHandles = false;

            model.visualGeomFilter = visualGeom;
            model.physicsGeomFilter = visualGeom;
            model.collisionGeomFilter = visualGeom;
        }

        /// <summary>
        /// Creates a GameObject with a PbdModel and tetrahedral grid
        /// </summary>
        [MenuItem("GameObject/iMSTK/PbdObjects/PbdGridVolume")]
        [MenuItem("CONTEXT/iMSTK/PbdObjects/PbdGridVolume")]
        [MenuItem("iMSTK/GameObject/PbdObjects/PbdGridVolume")]
        private static void CreatePbdGridVolume()
        {
            GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newObj.name = "PbdGridVolume";
            DestroyImmediate(newObj.GetComponent<Collider>());

            // Add PbdModel to the object
            PbdModel model = newObj.AddComponent<PbdModel>();
            model.useDistanceConstraint = false;
            model.useAreaConstraint = false;
            model.useDihedralConstraint = false;
            model.useVolumeConstraint = false;
            model.useFEMConstraint = true;
            model.viscousDampingCoeff = 0.01;
            model.youngsModulus = 5000.0;
            model.possionsRatio = 0.4;
            model.materialType = Imstk.PbdFemConstraint.MaterialType.StVK;

            // Create a new mesh, store the old one
            MeshFilter meshFilter = newObj.GetComponentOrCreate<MeshFilter>();
            Mesh inputMesh = meshFilter.sharedMesh;
            meshFilter.sharedMesh = new Mesh();
            meshFilter.sharedMesh.name = inputMesh.name;

            // Create a new tet geometry
            GeometryFilter physicsGeom = newObj.AddComponent<GeometryFilter>();
            ImstkMesh tetMesh = ScriptableObject.CreateInstance<ImstkMesh>();
            tetMesh.geomType = GeometryType.TetrahedralMesh;
            physicsGeom.SetGeometry(tetMesh);
            physicsGeom.showHandles = false;

            GeometryFilter visualGeom = newObj.AddComponent<GeometryFilter>();
            visualGeom.SetGeometry(meshFilter.sharedMesh);
            visualGeom.showHandles = false;

            model.visualGeomFilter = visualGeom;
            model.physicsGeomFilter = physicsGeom;
            model.collisionGeomFilter = visualGeom;

            DeformableMap map = newObj.AddComponent<DeformableMap>();
            map.parentGeom = physicsGeom;
            map.childGeom = visualGeom;

            // Use editor to add fill geometries
            TetrahedralGridEditor.Init(meshFilter.sharedMesh, physicsGeom.inputImstkGeom as ImstkMesh);
        }

        /// <summary>
        /// Creates a GameObject with a PbdModel and line mesh
        /// </summary>
        [MenuItem("GameObject/iMSTK/PbdObjects/PbdThread")]
        [MenuItem("CONTEXT/iMSTK/PbdObjects/PbdThread")]
        [MenuItem("iMSTK/GameObject/PbdObjects/PbdThread")]
        private static void CreatePbdThread()
        {
            GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newObj.name = "PbdThread";
            DestroyImmediate(newObj.GetComponent<Collider>());

            // Add PbdModel to the object
            PbdModel model = newObj.AddComponent<PbdModel>();
            model.useDistanceConstraint = true;
            model.useBendConstraint = true;
            model.useAreaConstraint = false;
            model.useDihedralConstraint = false;
            model.useVolumeConstraint = false;
            model.useFEMConstraint = false;
            model.viscousDampingCoeff = 0.01;
            model.distanceStiffness = 100.0;
            model.bendStiffness = 100.0;
            model.maxBendStride = 3;
            model.dt = 0.005;
            model.numIterations = 50;

            // Create a new mesh, store the old one
            MeshFilter meshFilter = newObj.GetComponentOrCreate<MeshFilter>();
            Mesh inputMesh = meshFilter.sharedMesh;
            inputMesh.name = "LineMesh";
            meshFilter.sharedMesh = new Mesh();
            meshFilter.sharedMesh.name = inputMesh.name;

            GeometryFilter visualGeom = newObj.AddComponent<GeometryFilter>();
            visualGeom.SetGeometry(meshFilter.sharedMesh);
            visualGeom.showHandles = false;

            model.visualGeomFilter = visualGeom;
            model.physicsGeomFilter = visualGeom;
            model.collisionGeomFilter = visualGeom;

            // Use editor to add fill geometries
            LineMeshEditor.Init(meshFilter.sharedMesh);
        }

        /// <summary>
        /// Creates a GameObject with a RbdModel and sphere
        /// </summary>
        [MenuItem("GameObject/iMSTK/RbdObjects/RbdSphere")]
        [MenuItem("CONTEXT/iMSTK/RbdObjects/RbdSphere")]
        [MenuItem("iMSTK/GameObject/RbdObjects/RbdSphere")]
        private static void CreateRbdSphere()
        {
            GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            newObj.name = "RbdSphere";
            DestroyImmediate(newObj.GetComponent<Collider>());

            // Add PbdModel to the object
            RbdModel model = newObj.AddComponent<RbdModel>();
            model.mass = 1.0;
            model.inertia = new Vector3[]
                {
                    new Vector3(1.0f, 0.0f, 0.0f),
                    new Vector3(0.0f, 1.0f, 0.0f),
                    new Vector3(0.0f, 0.0f, 1.0f)
                };

            // Create a new mesh, store the old one
            MeshFilter meshFilter = newObj.GetComponentOrCreate<MeshFilter>();
            GeometryFilter visualGeom = newObj.AddComponent<GeometryFilter>();
            visualGeom.SetGeometry(meshFilter.sharedMesh);
            visualGeom.showHandles = false;

            Sphere sphere = CreateInstance<Sphere>();
            sphere.radius = 0.5f;
            sphere.center = Vector3.zero;
            GeometryFilter collisionGeom = newObj.AddComponent<GeometryFilter>();
            collisionGeom.SetGeometry(sphere);

            model.visualGeomFilter = visualGeom;
            model.physicsGeomFilter = collisionGeom;
            model.collisionGeomFilter = collisionGeom;
        }



        /// <summary>
        /// Creates a GameObject with an OpenHapticsDevice
        /// </summary>
        [MenuItem("GameObject/iMSTK/Devices/OpenHapticsDevice")]
        [MenuItem("CONTEXT/iMSTK/Devices/OpenHapticsDevice")]
        [MenuItem("iMSTK/GameObject/Devices/OpenHapticsDevice")]
        private static void CreateOpenHapticsDevice()
        {
            GameObject newObj = new GameObject("OpenHapticsDevice");
            newObj.AddComponent(typeof(OpenHapticsDevice));
        }



        [MenuItem("GameObject/iMSTK/StaticObjects/LineObject")]
        [MenuItem("CONTEXT/iMSTK/StaticObjects/LineObject")]
        [MenuItem("iMSTK/GameObject/StaticObjects/LineObject")]
        private static void CreateLineStaticObject()
        {
            GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            newObj.name = "StaticLineObject";
            DestroyImmediate(newObj.GetComponent<Collider>());

            StaticModel model = newObj.AddComponent<StaticModel>();

            ImstkMesh mesh = ScriptableObject.CreateInstance<ImstkMesh>();
            mesh.geomType = GeometryType.LineMesh;
            mesh.vertices = new Vector3[] { new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f) };
            mesh.indices = new int[] { 0, 1 };
            MeshFilter meshFilter = newObj.GetComponent<MeshFilter>();
            meshFilter.sharedMesh = mesh.ToMesh();
            meshFilter.sharedMesh.name = "LineMesh";

            GeometryFilter collisionGeom = newObj.AddComponent<GeometryFilter>();
            collisionGeom.SetGeometry(meshFilter.sharedMesh);
            model.collisionGeomFilter = collisionGeom;
        }

        [MenuItem("GameObject/iMSTK/StaticObjects/SphereObject")]
        [MenuItem("CONTEXT/iMSTK/StaticObjects/SphereObject")]
        [MenuItem("iMSTK/GameObject/StaticObjects/SphereObject")]
        private static void CreateSphereStaticObject()
        {
            GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            newObj.name = "StaticSphereObject";
            DestroyImmediate(newObj.GetComponent<Collider>());

            StaticModel model = newObj.AddComponent<StaticModel>();

            Sphere sphere = new Sphere();

            GeometryFilter collisionGeom = newObj.AddComponent<GeometryFilter>();
            collisionGeom.SetGeometry(sphere);
            model.collisionGeomFilter = collisionGeom;
        }

        [MenuItem("GameObject/iMSTK/StaticObjects/CapsuleObject")]
        [MenuItem("CONTEXT/iMSTK/StaticObjects/CapsuleObject")]
        [MenuItem("iMSTK/GameObject/StaticObjects/CapsuleObject")]
        private static void CreateCapsuleStaticObject()
        {
            GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            newObj.name = "StaticCapsuleObject";
            DestroyImmediate(newObj.GetComponent<Collider>());

            StaticModel model = newObj.AddComponent<StaticModel>();

            Capsule capsule = new Capsule();

            GeometryFilter collisionGeom = newObj.AddComponent<GeometryFilter>();
            collisionGeom.SetGeometry(capsule);
            model.collisionGeomFilter = collisionGeom;
        }

        [MenuItem("GameObject/iMSTK/StaticObjects/OrientedBoxObject")]
        [MenuItem("CONTEXT/iMSTK/StaticObjects/OrientedBoxObject")]
        [MenuItem("iMSTK/GameObject/StaticObjects/OrientedBoxObject")]
        private static void CreateOrientedBoxStaticObject()
        {
            GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newObj.name = "StaticOrientedBoxObject";
            DestroyImmediate(newObj.GetComponent<Collider>());

            StaticModel model = newObj.AddComponent<StaticModel>();

            OrientedBox obb = new OrientedBox();

            GeometryFilter collisionGeom = newObj.AddComponent<GeometryFilter>();
            collisionGeom.SetGeometry(obb);
            model.collisionGeomFilter = collisionGeom;
        }

        [MenuItem("GameObject/iMSTK/StaticObjects/PlaneObject")]
        [MenuItem("CONTEXT/iMSTK/StaticObjects/PlaneObject")]
        [MenuItem("iMSTK/GameObject/StaticObjects/PlaneObject")]
        private static void CreatePlaneStaticObject()
        {
            GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Plane);
            newObj.name = "StaticPlaneObject";
            DestroyImmediate(newObj.GetComponent<Collider>());

            StaticModel model = newObj.AddComponent<StaticModel>();

            ImstkUnity.Plane plane = new ImstkUnity.Plane();
            plane.visualWidth = 5.1f;

            GeometryFilter collisionGeom = newObj.AddComponent<GeometryFilter>();
            collisionGeom.SetGeometry(plane);
            model.collisionGeomFilter = collisionGeom;
        }
    }
}