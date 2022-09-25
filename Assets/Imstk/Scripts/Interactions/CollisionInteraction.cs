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
    public enum StandardCollisionTypes
    {
        Auto,
        BidirectionalPlaneToSphereCD,
        ImplicitGeometryToPointSetCCD,
        ImplicitGeometryToPointSetCD,
        MeshToMeshBruteForceCD,
        PointSetToCapsuleCD,
        PointSetToOrientedBoxCD,
        PointSetToPlaneCD,
        PointSetToSphereCD,
        SphereToCylinderCD,
        SphereToSphereCD,
        SurfaceMeshToCapsuleCD,
        SurfaceMeshToSphereCD,
        SurfaceMeshToSurfaceMeshCD,
        TetraToLineMeshCD,
        TetraToPointSetCD,
        UnidirectionalPlaneToSphereCD
    };

    /// <summary>
    /// Convience collision interaction, uses a lot of defaults. If specifics needed
    /// use the specific subclasses
    /// </summary>
    [AddComponentMenu("iMSTK/CollisionInteraction")]
    public class CollisionInteraction : ImstkInteractionBehaviour
    {
        public StandardCollisionTypes collisionType = StandardCollisionTypes.Auto;
        public DynamicalModel model1;
        public DynamicalModel model2;

        Imstk.CollisionInteraction interaction;

        // Create the Imstk interaction for this collision interaction
        public override Imstk.SceneObject GetImstkInteraction()
        {
            if (interaction != null) return interaction;

            if (model1 == null)
            {
                Debug.LogError("Interaction on object " + gameObject.name + " which does not have a DynamicalModel");
            }
            if (model2 == null)
            {
                Debug.LogError("Interaction on object " + gameObject.name + " which does not have a DynamicalModel");
            }

            StandardCollisionTypes cdType = collisionType;
            if (cdType == StandardCollisionTypes.Auto)
            {
                cdType = GetCDAutoType(model1, model2);

                if (cdType == StandardCollisionTypes.Auto)
                {
                    return null;
                }
            }
            string cdTypeName = cdType.ToString();

            if (model1 is RbdModel && model2 is StaticModel)
            {
                Imstk.RigidObjectCollision collision =
                    new Imstk.RigidObjectCollision(
                    model1.GetDynamicObject() as Imstk.RigidObject2,
                    model2.GetDynamicObject(),
                    cdTypeName);
                interaction = collision;
            }
            else if (model1 is StaticModel && model2 is RbdModel)
            {
                Imstk.RigidObjectCollision collision =
                    new Imstk.RigidObjectCollision(
                        model2.GetDynamicObject() as Imstk.RigidObject2,
                        model1.GetDynamicObject(),
                        cdTypeName);
                interaction = collision;
            }
            else if (model1 is RbdModel && model2 is RbdModel)
            {
                Imstk.RigidObjectCollision collision =
                    new Imstk.RigidObjectCollision(
                        model1.GetDynamicObject() as Imstk.RigidObject2,
                        model2.GetDynamicObject() as Imstk.RigidObject2,
                        cdTypeName);
                interaction = collision;
            }
            else if (model1 is StaticModel && model2 is PbdModel)
            {
                Imstk.PbdObjectCollision collision =
                    new Imstk.PbdObjectCollision(
                        model2.GetDynamicObject() as Imstk.PbdObject,
                        model1.GetDynamicObject(),
                        cdTypeName);
                interaction = collision;
            }
            else if (model1 is PbdModel && model2 is StaticModel)
            {
                Imstk.PbdObjectCollision collision =
                    new Imstk.PbdObjectCollision(
                        model1.GetDynamicObject() as Imstk.PbdObject,
                        model2.GetDynamicObject(),
                        cdTypeName);
                interaction = collision;
            }
            else if (model1 is PbdModel && model2 is PbdModel)
            {
                Imstk.PbdObjectCollision collision =
                    new Imstk.PbdObjectCollision(
                        model1.GetDynamicObject() as Imstk.PbdObject,
                        model2.GetDynamicObject() as Imstk.PbdObject,
                        cdTypeName);
                interaction = collision;
            }
            else if (model1 is PbdModel && model2 is RbdModel)
            {
                Imstk.PbdRigidObjectCollision collision =
                    new Imstk.PbdRigidObjectCollision(
                        model1.GetDynamicObject() as Imstk.PbdObject,
                        model2.GetDynamicObject() as Imstk.RigidObject2,
                        cdTypeName);
                interaction = collision;
            }
            else if (model1 is RbdModel && model2 is PbdModel)
            {
                Imstk.PbdRigidObjectCollision collision =
                    new Imstk.PbdRigidObjectCollision(
                        model2.GetDynamicObject() as Imstk.PbdObject,
                        model1.GetDynamicObject() as Imstk.RigidObject2,
                        cdTypeName);
                interaction = collision;
            }

            if (interaction == null)
            {
                Debug.LogWarning("Could not find interaction for objects " + model1.gameObject.name + " & " + 
                    model1.gameObject.name);
            }
            return interaction;
        }

        /// <summary>
        /// Attempts to automatically choose a CD type based off the the
        /// two objects it was given
        /// </summary>
        public static StandardCollisionTypes GetCDAutoType(DynamicalModel model1, DynamicalModel model2)
        {
            if (model1 == null || model2 == null)
            {
                Debug.LogWarning("Can't determine collision one of the models is null");
                return StandardCollisionTypes.Auto;
            }

            Imstk.Geometry geom1 = model1.GetCollidingGeometry();
            Imstk.Geometry geom2 = model2.GetCollidingGeometry();

            if (geom1 == null)
            {
                Debug.LogError("Could not create collision interaction, " +
                    model1.gameObject.name + "'s DynamicalModel does not contain a collision geometry");
            }
            if (geom2 == null)
            {
                Debug.LogError("Could not create collision interaction, " +
                    model2.gameObject.name + "'s DynamicalModel does not contain a collision geometry");
            }

            if ((geom1 is Imstk.Sphere && geom2 is Imstk.Plane) ||
                (geom2 is Imstk.Sphere && geom1 is Imstk.Plane))
            {
                return StandardCollisionTypes.UnidirectionalPlaneToSphereCD;
            }
            else if (geom1 is Imstk.Sphere && geom2 is Imstk.Sphere)
            {
                return StandardCollisionTypes.SphereToSphereCD;
            }
            else if ((geom1 is Imstk.Sphere && geom2 is Imstk.Cylinder) ||
                (geom2 is Imstk.Sphere && geom1 is Imstk.Cylinder))
            {
                return StandardCollisionTypes.SphereToCylinderCD;
            }
            else if ((geom1 is Imstk.Sphere && geom2 is Imstk.SurfaceMesh) ||
                (geom2 is Imstk.Sphere && geom1 is Imstk.SurfaceMesh))
            {
                return StandardCollisionTypes.SurfaceMeshToSphereCD;
            }
            else if ((geom1 is Imstk.Capsule && geom2 is Imstk.SurfaceMesh) ||
                (geom2 is Imstk.Capsule && geom1 is Imstk.SurfaceMesh))
            {
                return StandardCollisionTypes.SurfaceMeshToCapsuleCD;
            }
            else if ((geom1 is Imstk.Sphere && geom2 is Imstk.PointSet) ||
                (geom2 is Imstk.Sphere && geom1 is Imstk.PointSet))
            {
                return StandardCollisionTypes.PointSetToSphereCD;
            }
            else if ((geom1 is Imstk.Capsule && geom2 is Imstk.PointSet) ||
                (geom2 is Imstk.Capsule && geom1 is Imstk.PointSet))
            {
                return StandardCollisionTypes.PointSetToCapsuleCD;
            }
            else if ((geom1 is Imstk.Plane && geom2 is Imstk.PointSet) ||
                (geom2 is Imstk.Plane && geom1 is Imstk.PointSet))
            {
                return StandardCollisionTypes.PointSetToPlaneCD;
            }
            else if ((geom1 is Imstk.OrientedBox && geom2 is Imstk.PointSet) ||
                (geom2 is Imstk.OrientedBox && geom1 is Imstk.PointSet))
            {
                return StandardCollisionTypes.PointSetToOrientedBoxCD;
            }
            else if ((geom1 is Imstk.SurfaceMesh && geom2 is Imstk.PointSet) ||
                (geom2 is Imstk.SurfaceMesh && geom1 is Imstk.PointSet))
            {
                return StandardCollisionTypes.MeshToMeshBruteForceCD;
            }
            else
            {
                Debug.LogError("Unable to auto find collision type for interaction between " + model1.gameObject.name + " & " + model2.gameObject.name);
                return StandardCollisionTypes.Auto;
            }
        }
    }
}
