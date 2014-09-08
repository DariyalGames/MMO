using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using ModestTree;
using ModestTree.Zenject;
using Dariyal.MMO.City.Buildings;
using Dariyal.MMO.Core.Input;

namespace Dariyal.MMO.City
{
    public enum Cameras
    {
        Main,
    }

    public class CitySceneInstaller : MonoInstaller
    {
        public Settings SceneSettings;

        public override void InstallBindings()
        {
            // Install any other re-usable installers
            InstallIncludes();
            // Install the main game
            InstallBattleScene();
            InstallSettings();
            InitPriorities();
        }

        private void InstallIncludes()
        {
            // This installer is needed for all unity projects (Yes, Zenject can support non-unity projects)
            _container.Bind<IInstaller>().ToSingle<StandardUnityInstaller>();
        }

        private void InstallBattleScene()
        {
            // Root of our object graph
            _container.Bind<IDependencyRoot>().ToSingle<DependencyRootStandard>();

            //Bind the main camera.
            //_container.Bind<Camera>().ToSingle(SceneSettings.MainCamera).As(Cameras.Main);

            //Bind the CityController
            _container.Bind<IInitializable>().ToSingle<CityController>();
            _container.Bind<CityController>().ToSingle();

            //Bind the Building manager to IInitiazable, ITickable and as a singleton.
            _container.Bind<IInitializable>().ToSingle<BuildingManager>();
            _container.Bind<ITickable>().ToSingle<BuildingManager>();
            _container.Bind<BuildingManager>().ToSingle();

            //Bind the factory as a singleton.
            _container.Bind<BuildingFactory>().ToSingle();

            //Bind the hooks to the respective clases.
            _container.Bind<BuildingHooks>().ToTransientFromPrefab<BuildingHooks>(SceneSettings.BuildingSettings.Orb.Prefab).WhenInjectedInto<Orb>();
            _container.Bind<BuildingHooks>().ToTransientFromPrefab<BuildingHooks>(SceneSettings.BuildingSettings.Forger.Prefab).WhenInjectedInto<Forger>();
            _container.Bind<BuildingHooks>().ToTransientFromPrefab<BuildingHooks>(SceneSettings.BuildingSettings.GemMine.Prefab).WhenInjectedInto<GemMine>();
            _container.Bind<BuildingHooks>().ToTransientFromPrefab<BuildingHooks>(SceneSettings.BuildingSettings.CrystalMine.Prefab).WhenInjectedInto<CrystalMine>();
            _container.Bind<BuildingHooks>().ToTransientFromPrefab<BuildingHooks>(SceneSettings.BuildingSettings.Merchant.Prefab).WhenInjectedInto<Merchant>();

#if UNITY_WEB || UNITY_EDITOR || UNITY_STANDALONE
            _container.Bind<ITickable>().ToSingle<KeyboardMouseInputController>();
            _container.Bind<KeyboardMouseInputController>().ToSingle();
#else
            _container.Bind<ITickable>().ToSingle<TouchInputController>();
            _container.Bind<TouchInputController>().ToSingle();
#endif
        }

        private void InstallSettings()
        {
            //Bind all settings classes.
            _container.Bind<CityController.Settings>().ToSingle(SceneSettings.City);
            _container.Bind<BuildingManager.Settings>().ToSingle(SceneSettings.BuildingSettings);
        }

        private void InitPriorities()
        {
            _container.Bind<IInstaller>().ToSingle<InitializablePrioritiesInstaller>();
            _container.Bind<List<Type>>().To(Initializables)
                .WhenInjectedInto<InitializablePrioritiesInstaller>();

            _container.Bind<IInstaller>().ToSingle<TickablePrioritiesInstaller>();
            _container.Bind<List<Type>>().To(Tickables)
                .WhenInjectedInto<TickablePrioritiesInstaller>();
        }

        [Serializable]
        public class Settings
        {
            public CityController.Settings City;
            public BuildingManager.Settings BuildingSettings;
        }

        static List<Type> Initializables = new List<Type>()
        {
            typeof(CityController),
            typeof(BuildingManager),
        };

        static List<Type> Tickables = new List<Type>()
        {
            typeof(BuildingManager),
#if UNITY_WEB || UNITY_EDITOR || UNITY_STANDALONE
            typeof(KeyboardMouseInputController),
#else
            typeof(TouchInputController),
#endif
        };
    }
}