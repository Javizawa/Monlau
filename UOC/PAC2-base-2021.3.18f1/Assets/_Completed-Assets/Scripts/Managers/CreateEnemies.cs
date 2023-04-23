using UnityEngine;
using System.Collections;
using Mirror;

public class CreateEnemies : NetworkBehaviour
{

    public GameObject m_TankPrefabNPC;
    public int enemyNumber;
    public Vector3 minPosition;
    public Vector3 maxPosition;
    public Vector3 minRotation;
    public Vector3 maxRotation;
    public float checkRadius = 1.5f;
    // Use this for initialization
    /*

    void Start()
    {


        //Generación de los tanques
        for (int i = 0; i < enemyNumber; ++i)
        {
            //HAY QUE ASEGURARSE QUE COLOCA EL TANQUE EN EL LUGAR CORRECTO. YA LO HARÉ
            //MARCAMOS LAS POSICIONES Y LA DIRECCIÓN ALEATORIAS
            Vector3 position = new Vector3(Random.Range(minPosition.x, maxPosition.x),
                                           Random.Range(minPosition.y, maxPosition.y),
                                           Random.Range(minPosition.z, maxPosition.z));



            Quaternion rotation = Quaternion.Euler(new Vector3(Random.Range(minRotation.x, maxRotation.x),
                                                                Random.Range(minRotation.y, maxRotation.y),
                                                                Random.Range(minRotation.z, maxRotation.z)));

            //Crear un GameObject en el servidor y en todos los clientes conectados al servidor.
            NetworkServer.Spawn(Instantiate(m_TankPrefabNPC,
                                                position,
                                                rotation) as GameObject);
        }
    }


    */


    void Start()
    {
        int generatedEnemyCount = 0;

        while (generatedEnemyCount < enemyNumber)
        {
            Vector3 position = new Vector3(Random.Range(minPosition.x, maxPosition.x),
                                           Random.Range(minPosition.y, maxPosition.y),
                                           Random.Range(minPosition.z, maxPosition.z));

            Collider[] colliders = Physics.OverlapSphere(position, checkRadius);

            // Comprobar si hay objetos en la posición, y si tienen la etiqueta "Obstacle"
            bool isObstacle = false;
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Obstacle"))
                {
                    isObstacle = true;
                    break;
                }
            }

            // Si no hay obstáculos, crear el tanque en la posición
            if (!isObstacle)
            {
                Quaternion rotation = Quaternion.Euler(new Vector3(Random.Range(minRotation.x, maxRotation.x),
                                                                    Random.Range(minRotation.y, maxRotation.y),
                                                                    Random.Range(minRotation.z, maxRotation.z)));

                NetworkServer.Spawn(Instantiate(m_TankPrefabNPC, position, rotation) as GameObject);

                generatedEnemyCount++;
            }
        }
    }
}
