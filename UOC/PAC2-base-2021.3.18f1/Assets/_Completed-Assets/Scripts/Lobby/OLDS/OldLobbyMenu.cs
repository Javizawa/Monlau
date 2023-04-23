using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;


public class OldLobbyMenu : MonoBehaviour
{
    private NetworkManager manager;
    public string serverIP = "localhost";
    void Awake()
    {
        manager = FindObjectOfType<NetworkManager>();
    }

    public void CreateGame()
    {
        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            if (!NetworkClient.active)
            {
                PlayerPrefs.SetString("Nombre", GameObject.Find("InputPlayer").GetComponent<TMP_InputField>().text);
                manager.StartHost();
            }
        }
    }

    public void RunServer()
    {
        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            if (!NetworkClient.active)
            {
                manager.StartServer();
            }
        }
    }

    public void JoinGame()
    {
        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            if (!NetworkClient.active)
            {
                PlayerPrefs.SetString("Nombre", GameObject.Find("InputPlayer").GetComponent<TMP_InputField>().text);
                manager.networkAddress = serverIP;
                manager.StartClient();
            }
        }
    }
}
