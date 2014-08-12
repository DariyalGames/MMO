using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using ModestTree;
using ModestTree.Zenject;
using Dariyal.MMO.City.Buildings;

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
            _container.Bind<Camera>().ToSingle(SceneSettings.MainCamera).As(Cameras.Main);

            //Bind the Building manager to IInitiazable, ITickable and as a singleton.
            _container.Bind<IInitializable>().ToSingle<BuildingManager>();
            _container.Bind<ITickable>().ToSingle<BuildingManager>();
            _container.Bind<BuildingManager>().ToSingle();

            //Bind the factor as a singleton.
            _container.Bind<BuildingFactory>().ToSingle();

            //Bind the hooks to the respective clases.
            _container.Bind<BarracksHooks>().ToTransientFromPrefab<BarracksHooks>(SceneSettings.Buildings.Barracks.Prefab).WhenInjectedInto<Barracks>();
            _container.Bind<GemMineHooks>().ToTransientFromPrefab<GemMineHooks>(SceneSettings.Buildings.GemMine.Prefab).WhenInjectedInto<GemMine>();
        }

        private void InstallSettings()
        {
            //Bind all settings classes.
            _container.Bind<BuildingManager.Settings>().ToSingle(SceneSettings.Buildings);
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
            public Camera MainCamera;
            public BuildingManager.Settings Buildings;
        }

        static List<Type> Initializables = new List<Type>()
        {
            
        };

        static List<Type> Tickables = new List<Type>()
        {
            
        };
    }
}