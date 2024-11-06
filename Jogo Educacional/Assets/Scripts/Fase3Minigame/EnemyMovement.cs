using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int takeDamage = 1;
    [SerializeField] private float velocity;
    private void Update()
    {
        this.transform.position -= new Vector3(velocity * Time.deltaTime, 0, 0);
    }
    private void Kill()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Friend"))
        {
            health -= takeDamage;
            if (health < 0)
            {
                Kill();
            }
        }
    }
}
