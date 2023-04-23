using UnityEngine;
using Mirror;

public class TankController : NetworkBehaviour {

    //Se utilizan para los proyectiles de los tanques
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    void Update() {
        //SOLO APLICA EL UPDATE A LOS USUARIOS LOCALES
        if (!isLocalPlayer)
        {
            return;
        }
        //Movimiento con el INPUT viejo
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        //Disparo con el input viejo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
    }

    //Es llamada por el cliente pero ejecutada en el servidor.
    [Command]
    void CmdFire()
    {
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);
        //Crear un GameObject en el servidor y en todos los clientes conectados al servidor.
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 2.0f);
    }

    //PINTA DE AZUL EL TANQUE DEL USUARIO LOCAL
    public override void OnStartLocalPlayer()
    {
        foreach (MeshRenderer child in GetComponentsInChildren<MeshRenderer>())
        {
            child.material.color = Color.blue;
        }
    }
}