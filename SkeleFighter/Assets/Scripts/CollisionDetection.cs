using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public PlayerMovements pm;
    public GameObject hitparticle;
    public Enemy enemy;
    private float damage = 50f;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && pm.isAttacking)
        {
            other.GetComponent<Animator>().SetTrigger("Hit");
            other.GetComponent<Enemy>().TakeDamage(damage);
            enemy.TakeDamage(damage);
            Debug.Log(other.name);
            Instantiate(hitparticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
        }
    }
}
