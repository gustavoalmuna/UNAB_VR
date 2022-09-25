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
using System.Threading;
using UnityEngine;

namespace ImstkUnity
{
#if IMSTK_USE_VRPN
    // If this is not defined iMSTK was not built with VRPN enabled
    // to use build iMSTK with the flag iMSTK_USE_VRPN set to ON
    [AddComponentMenu("iMSTK/VrpnDeviceManager")]
    public class VrpnDeviceManager : MonoBehaviour
    {
        private static VrpnDeviceManager _instance;
        
        // Probably Refactor to Singleton base class
        public static VrpnDeviceManager Instance
        {
            get { return _instance; }
        }

        public string host = "localhost";
        public int port = 3883;

        private Imstk.VRPNDeviceManager manager;
        private Thread thread;
        private bool running = false;

        public void Awake()
        {
            // Primitive singleton 
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
            InitManager();
        }

        public void InitManager()
        {
            if (manager == null)
            {
                manager = new Imstk.VRPNDeviceManager(host, port);
                if (manager == null) Debug.LogError("Could not create VRPNDevice Manager");
                manager.setSleepDelay(20);
                manager.init();
            }
        }

        public Imstk.DeviceClient MakeDeviceClient(ImstkUnity.VrpnDevice device)
        {
            return manager.makeDeviceClient(device.Name, device.Type);
        }    

        public void StartManager()
        {
            if (running) return;

            InitManager();
            running = true;
            Debug.Log("VRPN Thread Starting");
            thread = new Thread(() =>
            {
                while (running)
                {
                    manager.update();
                }
            });
            thread.Start();
        }
        public void StopManager()
        {
            if (manager == null) return;
            if (!running) return; 
            running = false;
            thread.Join();

            Debug.Log("VRPN Thread Stopping");
            manager.uninit();
            thread = null;
        }
    }
#else
    [AddComponentMenu("iMSTK/VrpnDeviceManager")]
    public class VrpnDeviceManager : MonoBehaviour
    {
        // Probably Refactor to Singleton base class
        public static VrpnDeviceManager Instance
        {
            get { 
                Debug.LogError("VRPN is not enable in this build");
                return null;
            }
        }


        public void Awake()
        {
            var a = Instance;
        }

        public void InitManager() {}

        public Imstk.DeviceClient MakeDeviceClient(ImstkUnity.VrpnDevice device) {
            return null; 
        }

        public void StartManager() {}
        public void StopManager() {}
    }
#endif
}
