using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;
using Mirror.Discovery;
using System.Collections.Generic;
using UnityEditor;


    [DisallowMultipleComponent]
    [AddComponentMenu("Network/Network Discovery HUD")]
    [HelpURL("https://mirror-networking.gitbook.io/docs/components/network-discovery")]
    [RequireComponent(typeof(NetworkDiscovery))]
    public class LobbyMenu : MonoBehaviour
    {
        public NetworkDiscovery networkDiscovery;
        public TMP_Dropdown dD;

        readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
        Vector2 scrollViewPos = Vector2.zero;
        public TextMeshProUGUI servidores;

        float buttonHeight = Screen.height * 0.1f;

        string stringToEdit = "PLAYER";
        string OldstringToEdit = "PLAYER";

#if UNITY_EDITOR
        void OnValidate()
        {
            if (networkDiscovery == null)
            {
                networkDiscovery = GetComponent<NetworkDiscovery>();
                UnityEditor.Events.UnityEventTools.AddPersistentListener(networkDiscovery.OnServerFound, OnDiscoveredServer);
                UnityEditor.Undo.RecordObjects(new Object[] { this, networkDiscovery }, "Set NetworkDiscovery");
            }
        }
    #endif

            private void Start()
            {
                PlayerPrefs.SetString("Color", ColorUtility.ToHtmlStringRGB(Color.blue));
                dD.onValueChanged.AddListener(ChangeColor);
            }

            void ChangeColor(int colorIndex)
            {
                switch (colorIndex)
                {
                
                    case 0:
                        PlayerPrefs.SetString("Color", ColorUtility.ToHtmlStringRGB(Color.blue));
                        Debug.Log(ColorUtility.ToHtmlStringRGB(Color.blue));
                        break;
                    case 1:
                        PlayerPrefs.SetString("Color", ColorUtility.ToHtmlStringRGB(Color.green));
                        Debug.Log(ColorUtility.ToHtmlStringRGB(Color.green));
                        break;
                    case 2:
                        PlayerPrefs.SetString("Color", ColorUtility.ToHtmlStringRGB(Color.magenta));
                        break;
                    case 3:
                        PlayerPrefs.SetString("Color", ColorUtility.ToHtmlStringRGB(Color.white));
                        break;
                }
            }


        private void Update()
            {
                servidores.text = discoveredServers.Count.ToString();
            }


        void OnGUI()
        {
            GUI.skin.label.fontSize = Mathf.RoundToInt(buttonHeight / 3f);
            if (NetworkManager.singleton == null)
                return;
            if (!NetworkClient.isConnected && !NetworkServer.active && !NetworkClient.active)
                {
                    servidores.text = discoveredServers.Count.ToString();
                    GUILayout.BeginArea(new Rect(Screen.width/2f, Screen.height/4f, Screen.width/5f, Screen.height / 5f));
                    GUILayout.BeginVertical();
                    foreach (ServerResponse info in discoveredServers.Values)
                        if (GUILayout.Button(info.EndPoint.Address.ToString(), GUILayout.Height(buttonHeight)))
                            Connect(info);
                GUI.skin.button.fontSize = Mathf.RoundToInt(buttonHeight / 3f);
                GUILayout.EndScrollView();
                    GUILayout.EndArea();
                }
                if (NetworkServer.active || NetworkClient.active) { 
                StopButtons();
                ChooseColor();
            }
        }


        void ChooseColor()
        {
            GUILayout.BeginArea(new Rect(Screen.width * 0.1f, 0, Screen.width*0.9f, Screen.height*0.1f));
            GUILayout.BeginHorizontal();
            GUILayout.Label("COLOR:");
            if (GUILayout.Button("AZUL")){
                GameObject.Find("GameManager").GetComponent<ChangeStats>().ChangeColor(Color.blue);
            }
            if (GUILayout.Button("VERDE"))
            {
                GameObject.Find("GameManager").GetComponent<ChangeStats>().ChangeColor(Color.green);
            }
            if (GUILayout.Button("MAGENTA"))
            {
                GameObject.Find("GameManager").GetComponent<ChangeStats>().ChangeColor(Color.magenta);
            }
            if (GUILayout.Button("BLANCO"))
            {
                GameObject.Find("GameManager").GetComponent<ChangeStats>().ChangeColor(Color.white);
            }
            GUILayout.Label("PLAYER:");
            stringToEdit = GUILayout.TextArea(stringToEdit, GUILayout.Height(buttonHeight/2f));
            if (stringToEdit != OldstringToEdit) {
                OldstringToEdit = stringToEdit;
                GameObject.Find("GameManager").GetComponent<ChangeStats>().ChangeName(stringToEdit);
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }


        void StopButtons()
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width * 0.1f, Screen.height * 0.1f));
            // stop host if host mode
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                if (GUILayout.Button("Stop Host"))
                {
                    NetworkManager.singleton.StopHost();
                    networkDiscovery.StopDiscovery();
                }
            }
            // stop client if client-only
            else if (NetworkClient.isConnected)
            {
                if (GUILayout.Button("Stop Client"))
                {
                    NetworkManager.singleton.StopClient();
                    networkDiscovery.StopDiscovery();
                }
            }
            // stop server if server-only
            else if (NetworkServer.active)
            {
                if (GUILayout.Button("Stop Server"))
                {
                    NetworkManager.singleton.StopServer();
                    networkDiscovery.StopDiscovery();
                }
            }
            
            GUILayout.EndArea();
        }



        public void CreateGame()
        {
            PlayerPrefs.SetString("Nombre", GameObject.Find("InputPlayer").GetComponent<TMP_InputField>().text);
            stringToEdit = GameObject.Find("InputPlayer").GetComponent<TMP_InputField>().text;
            OldstringToEdit = GameObject.Find("InputPlayer").GetComponent<TMP_InputField>().text;
            discoveredServers.Clear();
            NetworkManager.singleton.StartHost();
            networkDiscovery.AdvertiseServer();
        }

        public void RunServer()
            {
                //NUEVO JAVI
                PlayerPrefs.SetInt("Server", 1);
                discoveredServers.Clear();
                NetworkManager.singleton.StartServer();
                networkDiscovery.AdvertiseServer();
            }

        public void DiscoverServer()
        {
            discoveredServers.Clear();
            networkDiscovery.StartDiscovery();
            servidores.text = discoveredServers.Count.ToString();
        }

        void Connect(ServerResponse info)
        {
            networkDiscovery.StopDiscovery();
            PlayerPrefs.SetString("Nombre", GameObject.Find("InputPlayer").GetComponent<TMP_InputField>().text);
            stringToEdit = GameObject.Find("InputPlayer").GetComponent<TMP_InputField>().text;
            OldstringToEdit = GameObject.Find("InputPlayer").GetComponent<TMP_InputField>().text;
            NetworkManager.singleton.StartClient(info.uri);
        }


        public void OnDiscoveredServer(ServerResponse info)
        {
            // Note that you can check the versioning to decide if you can connect to the server or not using this method
            discoveredServers[info.serverId] = info;
        }

}
