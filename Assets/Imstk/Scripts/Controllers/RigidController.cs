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
    [AddComponentMenu("iMSTK/RigidController")]
    public class RigidController : ImstkControllerBehaviour
    {
        Imstk.RigidObjectController controller = null;
        public RbdModel rbdModel = null;

        public double angularKd = 50.0;
        public double angularKs = 1000.0;
        public double linearKd = 100.0;
        public double linearKs = 10000.0;
        public bool useCriticalDamping = false;

        public double forceScale = 0.00001;
        public bool useForceSmoothing = true;
        public int forceSmoothingKernelSize = 15;

        // All these transforms below could really just be one
        public Vector3 translationalOffset = Vector3.zero;
        public Quaternion rotationalOffset = Quaternion.identity;
        public Quaternion localRotationalOffset = Quaternion.identity;
        public double translationScaling = 0.02;

        // Unity uses LHS, while imstk uses RHS, invert X positional
        public bool invertX = false;
        public bool invertY = false;
        public bool invertZ = true;

        // Unity uses LHS, while imstk uses RHS, invert Y,Z planes
        public bool invertRotX = true;
        public bool invertRotY = true;
        public bool invertRotZ = false;

        public bool debugController = false;

        public Vector3 GetPosition()
        {
            Imstk.Vec3d pos = controller.getPosition();
            return pos.ToUnityVec();
        }
        public Quaternion GetOrientation()
        {
            Imstk.Quatd quat = controller.getOrientation();
            return quat.ToUnityQuat();
        }

        public override Imstk.DeviceControl GetController()
        {
            if (device == null)
            {
                Debug.LogError("Failed to create controller, no device given");
                return null;
            }
            if (rbdModel == null)
            {
                Debug.LogError("Failed to create controller, no model given");
                return null;
            }

            controller = new Imstk.RigidObjectController(
                    rbdModel.GetDynamicObject() as Imstk.RigidObject2,
                    device.GetDevice());
            controller.setAngularKd(angularKd);
            controller.setAngularKs(angularKs);
            controller.setLinearKd(linearKd);
            controller.setLinearKs(linearKs);

            controller.setUseCritDamping(useCriticalDamping);

            controller.setForceScaling(forceScale);
            controller.setUseForceSmoothening(useForceSmoothing);
            controller.setSmoothingKernelSize(forceSmoothingKernelSize);

            controller.setTranslationOffset(translationalOffset.ToImstkVec());
            controller.setRotationOffset(rotationalOffset.ToImstkQuat());
            controller.setEffectorRotationOffset(localRotationalOffset.ToImstkQuat());
            controller.setTranslationScaling(translationScaling);

            Imstk.TrackingDeviceControl.InvertFlag invertFlag = 0x00;
            if (invertX)
            {
                invertFlag = invertFlag | Imstk.TrackingDeviceControl.InvertFlag.transX;
            }
            if (invertY)
            {
                invertFlag = invertFlag | Imstk.TrackingDeviceControl.InvertFlag.transY;
            }
            if (invertZ)
            {
                invertFlag = invertFlag | Imstk.TrackingDeviceControl.InvertFlag.transZ;
            }
            if (invertRotX)
            {
                invertFlag = invertFlag | Imstk.TrackingDeviceControl.InvertFlag.rotX;
            }
            if (invertRotY)
            {
                invertFlag = invertFlag | Imstk.TrackingDeviceControl.InvertFlag.rotY;
            }
            if (invertRotZ)
            {
                invertFlag = invertFlag | Imstk.TrackingDeviceControl.InvertFlag.rotZ;
            }

            controller.setInversionFlags((byte)invertFlag);

            return controller;
        }

        public void Update()
        {
            if (debugController)
            {
                gameObject.transform.SetPositionAndRotation(GetPosition(), GetOrientation());
            }
        }
    }
}