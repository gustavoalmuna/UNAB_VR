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
    [AddComponentMenu("iMSTK/RbdModel")]
    public class RbdModel : DynamicalModel
    {
        public double mass = 1.0;

        public Vector3[] inertia = new Vector3[3] {
            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(0.0f, 1.0f, 0.0f),
            new Vector3(0.0f, 0.0f, 1.0f)
            };

        public Vector3 initVelocity = new Vector3();
        public Vector3 initAngularVelocity = new Vector3();

        public Vector3 initForce = new Vector3();
        public Vector3 initTorque = new Vector3();

        protected override Imstk.CollidingObject InitObject()
        {
            // All rigid bodies share a model, limiting but simple. May refactor in the future
            Imstk.RigidObject2 rbdObject = new Imstk.RigidObject2(GetFullName());
            rbdObject.setDynamicalModel(SimulationManager.rigidBodyModel);
            return rbdObject;
        }

        protected override void OnImstkInit()
        {
            // Get dependencies
            meshFilter = gameObject.GetComponentFatal<MeshFilter>();
            transformComp = gameObject.GetComponentFatal<Transform>();

            // Make sure mesh filter is read/writable
            if (!meshFilter.mesh.isReadable)
            {
                Debug.LogWarning(gameObject.name + "'s MeshFilter Mesh must be readable (check the meshes import settings)");
                return;
            }

            imstkObject = InitObject();
            SimulationManager.sceneManager.getActiveScene().addSceneObject(imstkObject);
            InitGeometry();
            ProcessBoundaryConditions(gameObject.GetComponents<BoundaryCondition>());
            Configure();
        }

        /// <summary>
        /// Initialize the geometry in the local configuration, unity transform
        /// will be used to apply transform
        /// </summary>
        protected override void InitGeometry()
        {
            // Rbd's don't need visual or physics geometry

            Transform transform = gameObject.GetComponentFatal<Transform>();

            // Setup the collision geometry
            if (collisionGeomFilter != null)
            {
                Imstk.Vec3d scaling = transform.localScale.ToImstkVec();
                Imstk.Geometry colGeom = GetCollidingGeometry();
                // Apply scaling, position & rotation applied in body
                colGeom.scale(scaling, Imstk.Geometry.TransformType.ApplyToData);
                imstkObject.setCollidingGeometry(colGeom);
                (imstkObject as Imstk.DynamicObject).setPhysicsGeometry(colGeom);
            }
            else
            {
                Debug.LogError("No collision geometry provided to DynamicalModel on object " + gameObject.name);
            }
        }

        protected override void Configure()
        {
            Transform transform = gameObject.GetComponentFatal<Transform>();

            Imstk.Mat3d inertiaTensor = Imstk.Mat3d.Identity();
            inertiaTensor.setValue(0, 0, inertia[0][0]);
            inertiaTensor.setValue(0, 1, inertia[0][1]);
            inertiaTensor.setValue(0, 2, inertia[0][1]);

            inertiaTensor.setValue(1, 0, inertia[1][0]);
            inertiaTensor.setValue(1, 1, inertia[1][1]);
            inertiaTensor.setValue(1, 2, inertia[1][2]);

            inertiaTensor.setValue(2, 0, inertia[2][0]);
            inertiaTensor.setValue(2, 1, inertia[2][1]);
            inertiaTensor.setValue(2, 2, inertia[2][2]);

            Imstk.RigidObject2 rbdObject = imstkObject as Imstk.RigidObject2;
            rbdObject.getRigidBody().setMass(mass);
            rbdObject.getRigidBody().setInertiaTensor(inertiaTensor);
            rbdObject.getRigidBody().setInitPos(transform.position.ToImstkVec());
            rbdObject.getRigidBody().setInitOrientation(transform.rotation.ToImstkQuat());
            rbdObject.getRigidBody().setInitVelocity(initVelocity.ToImstkVec());
            rbdObject.getRigidBody().setInitAngularVelocity(initAngularVelocity.ToImstkVec());
            rbdObject.getRigidBody().setInitForce(initForce.ToImstkVec());
            rbdObject.getRigidBody().setInitTorque(initTorque.ToImstkVec());
        }

        /// <summary>
        /// Visual update of the transform (only called before render)
        /// </summary>
        public void Update()
        {
            Transform transform = gameObject.GetComponentFatal<Transform>();
            transform.SetPositionAndRotation(GetBodyPosition(), GetBodyOrientation());
        }

        protected virtual Vector3 GetBodyPosition()
        {
            Imstk.RigidObject2 rbdObject = imstkObject as Imstk.RigidObject2;
            Imstk.Vec3d pos = rbdObject.getRigidBody().getPosition();
            return pos.ToUnityVec();
        }

        protected virtual Quaternion GetBodyOrientation()
        {
            Imstk.RigidObject2 rbdObject = imstkObject as Imstk.RigidObject2;
            Imstk.Quatd orientation = rbdObject.getRigidBody().getOrientation();
            return orientation.ToUnityQuat();
        }
    }
}