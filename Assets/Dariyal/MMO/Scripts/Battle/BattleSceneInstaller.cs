using System;
using UnityEngine;
using System.Collections;
using ModestTree.Zenject;
using Dariyal.MMO.Battle.HexGrid;

namespace Dariyal.MMO.Battle
{
    public class BattleSceneInstaller : MonoInstaller
    {
        public Settings SceneSettings;

        public override void InstallBindings()
        {
            InstallIncludes();
            InstallScene();
            InstallSettings();
        }

        private void InstallIncludes()
        {
            //prerequisite for all unity zenject projects
            _container.Bind<IInstaller>().ToSingle<StandardUnityInstaller>();
        }

        private void InstallScene()
        {
            //installing scene dependent classes.

            //prerequisite for all zenject projects
            _container.Bind<IDependencyRoot>().ToSingle<DependencyRootStandard>();

            //install the main camera
            _container.Bind<Camera>().ToSingle(SceneSettings.MainCamera);

            //bind the input to mouse n keyboard controller
            //_container.Bind<IInputController>().ToSingle<KeyboardController>();

            //Bind the Camera Controller
            //_container.Bind<IInitializable>().ToSingle<IsometricCameraController>();
            //_container.Bind<ITickable>().ToSingle<IsometricCameraController>();
            //_container.Bind<IsometricCameraController>().ToSingle();

            _container.BindFactory<Hex>();
            _container.Bind<HexHooks>().ToTransientFromPrefab<HexHooks>(SceneSettings.HexGridGenerator.HexPrefab).WhenInjectedInto<Hex>();

            _container.Bind<IHexGridGenerator>().ToSingle<RegularHeagonalGenerator>();
            _container.Bind<RegularHeagonalGenerator>().ToSingle();

            _container.Bind<IInitializable>().ToSingle<HexGridManager>();
            _container.Bind<ITickable>().ToSingle<HexGridManager>();
            _container.Bind<HexGridManager>().ToSingle();
        }

        private void InstallSettings()
        {
            //_container.Bind<KeyboardController.Settings>().ToSingle(SceneSettings.Keyboard);
            //_container.Bind<IsometricCameraController.Settings>().ToSingle(SceneSettings.Camera);
            _container.Bind<RegularHeagonalGenerator.Settings>().ToSingle(SceneSettings.HexGridGenerator);
        }

        [Serializable]
        public class Settings
        {
            public Camera MainCamera;
            public RegularHeagonalGenerator.Settings HexGridGenerator;
            //public IsometricCameraController.Settings Camera;
            //public KeyboardController.Settings Keyboard;
            //[Serializable]
            //public class HexGridSettings
            //{
            //    public RegularHeagonalGenerator.Settings Generator;
            //}
        }
    }
}