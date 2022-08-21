using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 3;
    public bool flipX = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (flipX == true)
        {
            this.transform.Translate(-5 * speed * Time.deltaTime, 0, 0);

        }
        else if (flipX == false)
        {
            this.transform.Translate(5 * speed * Time.deltaTime, 0, 0);

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {

            if (collision.CompareTag("Enemy"))
            {
                Debug.Log("hit");
                collision.GetComponent<EnemyIA>().Damage(1);
            }
            Destroy(this.gameObject);
        }
    }
}
