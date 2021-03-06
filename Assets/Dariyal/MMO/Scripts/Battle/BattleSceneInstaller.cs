﻿using System;
using System.Collections.Generic;

using UnityEngine;

using ModestTree.Zenject;

using Dariyal.MMO.Core.Input;
using Dariyal.MMO.Battle;
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
            InitPriorities();
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

            _container.BindFactory<Hex>();
            _container.Bind<HexHooks>().ToTransientFromPrefab<HexHooks>(SceneSettings.HexGridGenerator.HexPrefab).WhenInjectedInto<Hex>();

            _container.Bind<IHexGridGenerator>().ToSingle<RegularHeagonalGenerator>();
            _container.Bind<RegularHeagonalGenerator>().ToSingle();

            _container.Bind<IInitializable>().ToSingle<HexGridManager>();
            _container.Bind<ITickable>().ToSingle<HexGridManager>();
            _container.Bind<HexGridManager>().ToSingle();

            _container.Bind<IInitializable>().ToSingle<BattleController>();
            _container.Bind<ITickable>().ToSingle<BattleController>();
            _container.Bind<BattleController>().ToSingle();

#if UNITY_IPHONE || UNITY_ANDROID
            _container.Bind<ITickable>().ToSingle<TouchInputController>();
            _container.Bind<TouchInputController>().ToSingle();       
#else
            _container.Bind<ITickable>().ToSingle<KeyboardMouseInputController>();
            _container.Bind<KeyboardMouseInputController>().ToSingle();
#endif

        }

        private void InstallSettings()
        {
            //_container.Bind<KeyboardController.Settings>().ToSingle(SceneSettings.Keyboard);
            //_container.Bind<IsometricCameraController.Settings>().ToSingle(SceneSettings.Camera);
            _container.Bind<RegularHeagonalGenerator.Settings>().ToSingle(SceneSettings.HexGridGenerator);
            _container.Bind<BattleController.Settings>().ToSingle(SceneSettings.Battle);
            _container.Bind<Hex.Settings>().ToSingle(SceneSettings.Hex);
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
            //public Camera MainCamera;
            public RegularHeagonalGenerator.Settings HexGridGenerator;
            public BattleController.Settings Battle;
            public Hex.Settings Hex;
        }

        static List<Type> Initializables = new List<Type>()
        {
            // Re-arrange this list to control init order
            typeof(HexGridManager),
            typeof(BattleController),
            typeof(Army),
        };

        static List<Type> Tickables = new List<Type>()
        {
            // Re-arrange this list to control update order
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
            typeof(KeyboardMouseInputController),
#else
            typeof(TouchInputController),
#endif
            typeof(HexGridManager),
            typeof(BattleController),
        };
    }
}