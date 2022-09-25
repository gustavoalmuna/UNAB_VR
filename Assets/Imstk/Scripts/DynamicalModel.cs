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
    [RequireComponent(typeof(Transform))]
    public abstract class DynamicalModel : ImstkBehaviour
    {
        // Components
        protected MeshFilter meshFilter = null;
        protected Transform transformComp = null;

        // These filters can accept either imstk or unity geometry input
        // and output imstk geometry
        public GeometryFilter visualGeomFilter = null;
        public GeometryFilter physicsGeomFilter = null;
        public GeometryFilter collisionGeomFilter = null;

        protected Imstk.CollidingObject imstkObject;

        /// <summary>
        /// Get the pointer to the object in c (not available until after initialize)
        /// </summary>
        /// <returns></returns>
        public Imstk.CollidingObject GetDynamicObject() { return imstkObject; }

        public Imstk.Geometry GetVisualGeometry()
        {
            return visualGeomFilter.GetOutputGeometry();
        }
        public Imstk.Geometry GetPhysicsGeometry()
        {
            return physicsGeomFilter.GetOutputGeometry();
        }
        public Imstk.Geometry GetCollidingGeometry()
        {
            return collisionGeomFilter.GetOutputGeometry();
        }

        protected abstract Imstk.CollidingObject InitObject();

        protected abstract void Configure();

        /// <summary>
        /// Each subclassed model may *apply* boundary conditions differently
        /// </summary>
        /// <param name="conditions">All the conditions to be processed</param>
        protected virtual void ProcessBoundaryConditions(BoundaryCondition[] conditions) { }

        protected abstract void InitGeometry();
    };
}