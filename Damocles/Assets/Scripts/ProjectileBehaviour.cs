using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float shotSpeed;
    public int arrowDamage = 10;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * shotSpeed;
    }

    void OnTriggerEnter2D(Collider2D stinky)
    {
        
        Debug.Log(stinky.name);

        if (stinky.gameObject.tag == "enemy")
        {
            enemy enemy = stinky.GetComponent<enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(arrowDamage);
            }
        }
        

       // Destroy(gameObject);
    }
}