using UnityEngine;
using Mirror;
using Mirror.Discovery;


public class LobbyMenu : MonoBehaviour
{
    private NetworkManager manager;
    public string serverIP = "localhost";
    void Awake()
    {
        manager = FindObjectOfType<NetworkManager>();
    }

    public void StartDiscovery()
    {
        manager.GetComponent<NetworkDiscovery>().StartDiscovery();
    }

    public void StopDiscovery()
    {
        manager.GetComponent<NetworkDiscovery>().StopDiscovery();
    }

    public void CreateGame()
    {
        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            if (!NetworkClient.active)
            {
                manager.StartHost();
                // Inicia el descubrimiento de red
                StartDiscovery();
            }
        }
    }

    public void JoinGame()
    {
        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            if (!NetworkClient.active)
            {
                manager.StartClient();
            }
        }
    }

    public void OnReceivedBroadcast(string fromAddress, string data)
    {
        Debug.Log("Server Found");
    }

}
