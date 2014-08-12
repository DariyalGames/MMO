using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModestTree.Zenject;
using UnityEngine;
using Dariyal.MMO.Battle;
using Dariyal.MMO.Core.Input;
using Dariyal.MMO.Battle.HexGrid;

namespace Dariyal.MMO.Battle
{
    public enum Cameras
    {
        Main,
    }

	public class BattleSceneInstaller : MonoInstaller
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

        void InstallIncludes()
        {
            // This installer is needed for all unity projects (Yes, Zenject can support non-unity projects)
            _container.Bind<IInstaller>().ToSingle<StandardUnityInstaller>();
        }

        void InstallBattleScene()
        {
            // Root of our object graph
            _container.Bind<IDependencyRoot>().ToSingle<DependencyRootStandard>();

            _container.Bind<Camera>().ToSingle(SceneSettings.Camera.camera).As(Cameras.Main);

            //bind the input controller
            _container.Bind<IInputController>().ToSingle<KeyboardController>();

            //Bind the Camera Controller
            _container.Bind<IInitializable>().ToSingle<IsometricCameraController>();
            _container.Bind<ITickable>().ToSingle<IsometricCameraController>();
            _container.Bind<IsometricCameraController>().ToSingle();

            //Bind the hex factory
            _container.Bind<ITickable>().ToSingle<HexGridManager>();
            _container.Bind<HexGridManager>().ToSingle();
            _container.Bind<IFactory<IHex>>().To(new GameObjectFactory<IHex, Hex>(_container, SceneSettings.HexGrid.hexPrefab));

            _container.Bind<IInitializable>().ToSingle<BattleSceneController>();
            _container.Bind<ITickable>().ToSingle<BattleSceneController>();
            _container.Bind<BattleSceneController>().ToSingle();
        }

        void InstallSettings()
        {
            _container.Bind<KeyboardController.Settings>().ToSingle(SceneSettings.Keyboard);
            _container.Bind<IsometricCameraController.Settings>().ToSingle(SceneSettings.Camera);
            _container.Bind<HexGridManager.Settings>().ToSingle(SceneSettings.HexGrid);
        }

        void InitPriorities()
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
            public IsometricCameraController.Settings Camera;
            public KeyboardController.Settings Keyboard;
            public HexGridManager.Settings HexGrid;
        }

        static List<Type> Initializables = new List<Type>()
        {
            typeof(IsometricCameraController),
            typeof(BattleSceneController),
        };

        static List<Type> Tickables = new List<Type>()
        {
            typeof(IsometricCameraController),
            typeof(BattleSceneController),
            typeof(HexGridManager),
        };
    }
}
