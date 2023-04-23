using Mirror;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerColor : NetworkBehaviour
{
    [SyncVar(hook = "OnChangeColor")]
    private Color playerColor = Color.white;

    private void OnChangeColor(Color newColor, Color oldColor)
    {
        if (isLocalPlayer) { 
            string colorEnTexto = '#' + PlayerPrefs.GetString("Color", "#FFFFFF");
            Color colorGuardado;
            ColorUtility.TryParseHtmlString(colorEnTexto, out colorGuardado);
            playerColor = colorGuardado;
            // Cambiar el color del jugador
            foreach (MeshRenderer child in GetComponentsInChildren<MeshRenderer>())
            {
                child.material.color = newColor;
            }
        }
    }

    public void ChangeColor()
    {
        OnChangeColor(playerColor, playerColor);
    }

}
