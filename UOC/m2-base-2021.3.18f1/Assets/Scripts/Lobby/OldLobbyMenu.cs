using UnityEngine;
using Mirror;
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
                manager.StartHost();
            }
        }
    }
    public void JoinGame()
    {
        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            if (!NetworkClient.active)
            {
                manager.networkAddress = serverIP;
                manager.StartClient();
            }
        }
    }
}
