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

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImstkUnity
{
    [AddComponentMenu("iMSTK/SimulationManager")]
    [DefaultExecutionOrder(-99)]
    public class SimulationManager : MonoBehaviour
    {
        public static Imstk.SceneManager sceneManager = null;

        // This overwrites the Project Settings->Fixed Timestep as that one is per project not scene
        // This could be a point of pain when integrating with other Unity plugins
        public float sceneFixedTimestep = 0.01f;

        /// <summary>
        /// The imstk wrapper uses a global rigid body model, this is limiting
        /// but also simplifies things. May need to refactor in the future but the UI/UX
        /// is a bit tricky/bad for that
        /// </summary>
        public static Imstk.RigidBodyModel2 rigidBodyModel = null;
        public bool rbdUseRealtime = false;
        public Vector3 rigidBodyGravity = new Vector3(0.0f, -9.81f, 0.0f);
        public int rigidBodyMaxNumIterations = 10;
        public double rigidBodyDt = 0.01;

        public bool writeTaskGraph = false;

        private Imstk.CacheOutput output;

        /// <summary>
        /// Returns all the ImstkBehaviours
        /// </summary>
        /// <returns></returns>
        public List<ImstkBehaviour> GetAllBehaviours()
        {
            List<ImstkBehaviour> behaviours = new List<ImstkBehaviour>();
            List<GameObject> objects = FindObjectsOfType<GameObject>().ToList();
            foreach (GameObject obj in objects)
            {
                behaviours.AddRange(obj.GetComponents<ImstkBehaviour>());
            }
            return behaviours;
        }

        /// <summary>
        /// Returns all the ImstkInteractionBehaviours
        /// </summary>
        /// <returns></returns>
        public List<ImstkInteractionBehaviour> GetAllInteractionBehaviours()
        {
            List<ImstkInteractionBehaviour> behaviours = new List<ImstkInteractionBehaviour>();
            List<GameObject> objects = FindObjectsOfType<GameObject>().ToList();
            foreach (GameObject obj in objects)
            {
                behaviours.AddRange(obj.GetComponents<ImstkInteractionBehaviour>());
            }
            return behaviours;
        }

        /// <summary>
        /// Returns all the ImstkControlelrBehaviours
        /// </summary>
        /// <returns></returns>
        public List<ImstkControllerBehaviour> GetAllControllers()
        {
            List<ImstkControllerBehaviour> behaviours = new List<ImstkControllerBehaviour>();
            List<GameObject> objects = FindObjectsOfType<GameObject>().ToList();
            foreach (GameObject obj in objects)
            {
                behaviours.AddRange(obj.GetComponents<ImstkControllerBehaviour>());
            }
            return behaviours;
        }

        private void Awake()
        {
            // Get the settings
            ImstkSettings settings = ImstkSettings.Instance();
            //if (settings.useOptimalNumberOfThreads)
            //    settings.numThreads = 0;

            Imstk.Logger.startLogger();
            output = Imstk.Logger.instance().getCacheOutput();

            // Create the simulation manager
            sceneManager = new Imstk.SceneManager();
            sceneManager.setActiveScene(new Imstk.Scene("DefaultScene"));
            sceneManager.getActiveScene().getConfig().writeTaskGraph = writeTaskGraph;

            // Create A single RigidBodyModel to share for all rigid bodies used in the scene
            rigidBodyModel = new Imstk.RigidBodyModel2();
            Imstk.RigidBodyModel2Config rbdConfig = rigidBodyModel.getConfig();
            rbdConfig.m_gravity = rigidBodyGravity.ToImstkVec();
            rbdConfig.m_maxNumIterations = (uint)rigidBodyMaxNumIterations;
            rbdConfig.m_dt = rigidBodyDt;

            // Override the Unity fixed delta time
            Time.fixedDeltaTime = sceneFixedTimestep;

        }

        private void Start()
        {

            // It seems that InitManager needs to come AFTER the call that creates the
            // device inside of the OpenHapticsDevice
            IntializeImstkStructures();

#if IMSTK_USE_OPENHAPTICS
            OpenHapticsDevice.InitManager();
#endif

            sceneManager.init();

            // Start order
            {
                List<ImstkBehaviour> behaviours = GetAllBehaviours();
                foreach (ImstkBehaviour behaviour in behaviours)
                {
                    behaviour.ImstkStart();
                }
            }

            // #refactor should follow the same pattern as all
            // i.e. Get all Managers in the scene and start them
            // use same pattern for both managers
#if IMSTK_USE_VRPN
            if (VrpnDeviceManager.Instance != null) VrpnDeviceManager.Instance.StartManager();
#endif
#if IMSTK_USE_OPENHAPTICS
            OpenHapticsDevice.StartManager();
#endif
        }

        // Update the imstk scene within the fixed update of Unity
        public void FixedUpdate()
        {
            double dt = Time.fixedDeltaTime;

            if (rbdUseRealtime)
            {
                rigidBodyModel.getConfig().m_dt = dt;
            }

            sceneManager.getActiveScene().advance(dt);
        }

        public void OnApplicationQuit()
        {
            // Destroy order
            {
                List<ImstkBehaviour> behaviours = GetAllBehaviours();
                foreach (ImstkBehaviour behaviour in behaviours)
                {
                    behaviour.ImstkDestroy();
                }
            }

            sceneManager.uninit();

#if IMSTK_USE_OPENHAPTICS
            OpenHapticsDevice.StopManager(); // Stops in async
#endif
#if IMSTK_USE_VRPN
            if (VrpnDeviceManager.Instance != null) VrpnDeviceManager.Instance.StopManager();
#endif

            // These are static so we need to make sure to set them to null on quit
            sceneManager = null;
            rigidBodyModel = null;
        }

        private void Update()
        {
            LogToUnity();
        }

        private void IntializeImstkStructures()
        {
            // SimulationManager initializes objects in a particular order
            List<ImstkBehaviour> behaviours = GetAllBehaviours();
            foreach (ImstkBehaviour behaviour in behaviours)
            {
                behaviour.ImstkInit();
            }

            // We need to ensure all objects are created before interactions and controllers
            // are setup using them
            List<ImstkInteractionBehaviour> interactions = GetAllInteractionBehaviours();
            foreach (ImstkInteractionBehaviour behaviour in interactions)
            {
                Imstk.SceneObject interaction = behaviour.GetImstkInteraction();
                if (interaction != null)
                {
                    sceneManager.getActiveScene().addInteraction(interaction);
                }
            }

            List<ImstkControllerBehaviour> controllers = GetAllControllers();
            foreach (ImstkControllerBehaviour behaviour in controllers)
            {
                // Currently only support tracking device controls
                Imstk.TrackingDeviceControl control =
                    behaviour.GetController() as Imstk.TrackingDeviceControl;
                if (control != null)
                {
                    sceneManager.getActiveScene().addController(control);
                }
            }
        }

        private void LogToUnity()
        {
            while (output.hasMessages())
                Debug.Log(output.popLastMessage());
        }
    }
}