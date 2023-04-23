using UnityEngine;
public class Bullet : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * 6;
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Tank" || collision.gameObject.tag == "TankEnemy")
        {
            collision.gameObject.GetComponentInChildren<TankHealth>().TakeDamage(10, collision.gameObject.tag);
        }
        
        Destroy(gameObject);
    }
}