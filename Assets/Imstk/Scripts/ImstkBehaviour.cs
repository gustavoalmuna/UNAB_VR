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
    /// This is the base class of Imstk scripts. It exists to provide
    /// different init functions for Imstk classes. This is so that we
    /// can control initialization order in the SimulationManager
    /// </summary>
    public abstract class ImstkBehaviour : MonoBehaviour
    {
        public string GetFullName()
        {
            var stack = new System.Collections.Generic.Stack<string>();
            stack.Push(this.GetType().Name);
            stack.Push(gameObject.name);
            Transform parent = gameObject.transform.parent;
            while (parent != null)
            {
                stack.Push(parent.gameObject.name);
                parent = parent.transform.parent;
            }
            var newName = stack.Pop();
            while(stack.Count != 0)
            {
                newName = newName + "/" + stack.Pop();
            }
            return newName;
        }
        public void ImstkDestroy()
        {
            OnImstkDestroy();
        }

        public void ImstkInit() { OnImstkInit(); }

        public void ImstkStart() { OnImstkStart(); }


        /// <summary>
        /// Called before initializing the scene
        /// </summary>
        protected virtual void OnImstkInit() { }

        /// <summary>
        /// Called after scene has been initialized
        /// </summary>
        protected virtual void OnImstkStart() { }


        /// <summary>
        /// Called when done
        /// </summary>
        protected virtual void OnImstkDestroy() { }
    }
}
