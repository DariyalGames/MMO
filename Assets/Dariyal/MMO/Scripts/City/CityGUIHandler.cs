using UnityEngine;
using System.Collections;
using Dariyal.Util.Messenger;
using System;

namespace Dariyal.MMO.City
{
    public class CityGUIHandler : MonoBehaviour
    {
        public GUISkin guiSkin;
        //float native_width = 1280;
        //float native_height = 800;

        private bool showOrbMenu = false;
        private Rect orbRect;
        private int unitCount;
        
        void Start()
        {
            Messenger.AddListener<Vector3>("city:orb:clicked", OnOrbClick);

            orbRect = new Rect(0, 0, 400, 200);
        }

        void OnGUI()
        {
            GUI.skin = guiSkin;
            //float rx = Screen.width / native_width;
            //float ry = Screen.height / native_height;
            //GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));

            if (showOrbMenu)
            {
                orbRect = GUI.Window(0, orbRect, CreateOrbMenu, GUIContent.none);
            }
        }

        void CreateOrbMenu(int windowID)
        {
            //AddSpikes((int)orbRect.width);
            GUILayout.BeginVertical();
            GUILayout.Label("Unit Recruitment");
            GUILayout.BeginHorizontal();
            GUILayout.Label("Stone Golem", "ShortLabel");
            Int32.TryParse(GUILayout.TextField(unitCount.ToString(), 1), out unitCount);
            if (GUILayout.Button("<", "ShortButton"))
                unitCount = Math.Max(unitCount - 1, 0);
            if (GUILayout.Button(">", "ShortButton"))
                unitCount = Math.Min(unitCount + 1, 9);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space((int)(orbRect.width / 2 - 75));
            if (GUILayout.Button("Recruit", "ShortButton"))
            {
                showOrbMenu = false;
                Messenger.Broadcast<int>("city:orb:recruited", unitCount);
                unitCount = 0;
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        void AddSpikes(int winX)
        {
            int spikeCount = (int)Mathf.Floor(winX - 152) / 22;
            GUILayout.BeginHorizontal();
            GUILayout.Label("", "SpikeLeft");
            for (int i = 0; i < spikeCount; i++)
            {
                GUILayout.Label("", "SpikeMid");
            }
            GUILayout.Label("", "SpikeRight");
            GUILayout.EndHorizontal();
        }

        void OnOrbClick(Vector3 clickPosition)
        {
            orbRect = new Rect(clickPosition.x - orbRect.width / 2, clickPosition.y - orbRect.height / 2, orbRect.width, orbRect.height);
            showOrbMenu = true;
        }
    }
}

