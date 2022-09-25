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
    [AddComponentMenu("iMSTK/StaticModel")]
    public class StaticModel : DynamicalModel
    {
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
            Configure();
        }

        protected override void Configure()
        {
            //throw new NotImplementedException();
        }

        protected override void InitGeometry()
        {
            // Setup the collision geometry in its already post transform configuration
            // as it is a static geometry and will not be transformed
            if (collisionGeomFilter != null)
            {
                Imstk.Geometry colGeom = GetCollidingGeometry();
                Imstk.Mat4d m = transformComp.localToWorldMatrix.ToMat4d();
                colGeom.transform(m, Imstk.Geometry.TransformType.ApplyToData);
                imstkObject.setCollidingGeometry(colGeom);
            }
            else
            {
                Debug.LogError("No collision geometry provided to DynamicalModel on object " + gameObject.name);
            }
        }

        protected override Imstk.CollidingObject InitObject()
        {
            return new Imstk.CollidingObject(GetFullName());
        }
    }
}
