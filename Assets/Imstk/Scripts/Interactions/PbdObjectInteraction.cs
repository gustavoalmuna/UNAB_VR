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
    [AddComponentMenu("iMSTK/PbdObjectInteraction")]
    class PbdObjectInteraction : CollisionInteraction
    {

        public double friction = 0.1;
        public double restitution = 0.0;
        public int collisionIterations = 5;

        public override Imstk.SceneObject GetImstkInteraction()
        {
            if (model1 == null || model2 == null) { 
                Debug.LogError("Both models need to be set for interaction on " + gameObject.name);
                return null;
            }

            StandardCollisionTypes cdType = collisionType;
            if (cdType == StandardCollisionTypes.Auto)
            {
                cdType = CollisionInteraction.GetCDAutoType(model1, model2);
                if (cdType == StandardCollisionTypes.Auto)
                {
                    return null;
                }
            }
            string cdTypeName = cdType.ToString();
            
            PbdModel pbdModel;
            if (model1 as PbdModel != null)
            {
                pbdModel = model1 as PbdModel;
            }
            else
            {
                pbdModel = model2 as PbdModel;
                model2 = model1;
            }

            if (pbdModel == null)
            {
                Debug.LogError("One of the DynamicObjects has to be a PbdModel");
                return null;
            }

            Imstk.PbdObjectCollision collision = null;
            if (model2 is StaticModel)
            {
                collision =
                   new Imstk.PbdObjectCollision(
                       pbdModel.GetDynamicObject() as Imstk.PbdObject,
                       model2.GetDynamicObject(),
                       cdTypeName);
            }
            else if (model2 is PbdModel)
            {
                collision =
                    new Imstk.PbdObjectCollision(
                        pbdModel.GetDynamicObject() as Imstk.PbdObject,
                        model2.GetDynamicObject() as Imstk.PbdObject,
                        cdTypeName);
            }
            else if (model2 is RbdModel)
            {
                collision =
                   new Imstk.PbdObjectCollision(
                       pbdModel.GetDynamicObject() as Imstk.PbdObject,
                       model2.GetDynamicObject(),
                       cdTypeName);
            }
            else
            {
                Debug.LogWarning("Could not find interaction for objects " + pbdModel.gameObject + " & " + model2.gameObject);
                return null;
            }

            collision.setFriction(friction);
            collision.setRestitution(restitution);
            return collision;
        }
    }
}