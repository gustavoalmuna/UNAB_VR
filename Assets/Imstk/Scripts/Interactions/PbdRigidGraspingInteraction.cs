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


using System;
using UnityEngine;

namespace ImstkUnity
{
    public class PbdRigidGraspingInteraction : ImstkInteractionBehaviour
    {
        public RbdModel rigidModel;
        public PbdModel pbdModel;

        public bool useVertexGrasping = true;

        Imstk.PbdRigidObjectGrasping interaction;
        private Imstk.AnalyticalGeometry graspingGeometry;
        private StandardCollisionTypes collisionDetectionType = StandardCollisionTypes.Auto;

        // For now use the Rigdbody collision geometry for the grasping test
        // private Imstk.Col
        bool OneIsA<T>(DynamicalModel a, DynamicalModel b) where T : DynamicalModel
        {
            if (b as T != null) return true;
            if (a as T != null) return true;
            return false;
        }

        public override Imstk.SceneObject GetImstkInteraction()
        {
            if (rigidModel == null || pbdModel == null)
            {
                Debug.LogError("Both models need to be assigned for the interaction to work");
            }

            collisionDetectionType = CollisionInteraction.GetCDAutoType(rigidModel, pbdModel);

            if (collisionDetectionType == StandardCollisionTypes.Auto)
            {
                Debug.LogError("Could not determine collision type for grasping in " + gameObject.name);
            }

            interaction = new Imstk.PbdRigidObjectGrasping(pbdModel.GetDynamicObject() as Imstk.PbdObject, 
                rigidModel.GetDynamicObject() as Imstk.RigidObject2);

            Imstk.Geometry geom = rigidModel.GetDynamicObject().getCollidingGeometry();
            Imstk.Capsule cap = geom as Imstk.Capsule;

            Imstk.AnalyticalGeometry analytical = cap as Imstk.AnalyticalGeometry;

            if (geom == null)
            {
                Debug.LogError("Can't convert to analytical geometry" + cap.getTypeName());
            }


            return interaction;
        }

        public void StartGrasp()
        {
            Imstk.Geometry geom = rigidModel.GetDynamicObject().getCollidingGeometry();
            Imstk.AnalyticalGeometry converted = Imstk.Utils.CastTo<Imstk.AnalyticalGeometry>(geom);

            if (converted == null)
            {
                Debug.LogError("Grasping Geometry can't be null in " + gameObject.name);
                return;
            }
            if (useVertexGrasping) interaction.beginVertexGrasp(converted);
            else interaction.beginCellGrasp(converted, collisionDetectionType.ToString());
        }

        public void EndGrasp()
        {
            interaction.endGrasp();
        }

        public bool HasConstraints()
        {
            return interaction.hasConstraints();
        }
    }

}
