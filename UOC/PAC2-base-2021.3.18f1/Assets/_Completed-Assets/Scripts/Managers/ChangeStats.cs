using Mirror;
using UnityEngine;
using UnityEngine.Networking;

public class ChangeStats : NetworkBehaviour
{
    private GameObject[] players;

    public void ChangeColor(Color color)
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                Debug.Log("HAY UNO");
                foreach (MeshRenderer child in player.GetComponentsInChildren<MeshRenderer>())
                {
                        child.material.color = color;
                }
            }
        }
    }

    public void ChangeName(string name)
    {

        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                player.GetComponent<PlayerNameDisplayer>().CmdSendName(name);
            }
        }
    }


}


