using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    Player player;
    float speed = 4.5f;
    float fov = 10;
    int health;
    public Transform[] balizas = new Transform[2];
    SpriteRenderer spriteRenderer;
    bool canMove = false;
    bool death = false;
    int i;
    bool canJump = true;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spriteRenderer = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        health = 3;
        i = Random.Range(0, 2);
        Debug.Log(i + " Initial balize");
        //balizas[0] = this.transform.GetChild(1).position; 
        //balizas[1] = this.transform.GetChild(2).position;

    }

    // Update is called once per frame
    void Update()
    {
        if (death != true)
        {

            IA();
        }
    }
    void IA()
    {
        var dis = ((this.transform.position.x - player.transform.position.x) > 0) ? -1 : 1;//(1) - derecha (-1) - izquierda
        var disy = Mathf.Abs(this.transform.GetChild(1).transform.position.y - player.transform.GetChild(2).transform.position.y);
        var numdis = this.transform.position.x - player.transform.position.x;
        //Debug.Log(disy);
        if (Mathf.Abs(numdis) < fov && disy < 1.2f && player.IsDeath() != true)
        {

            if (dis == 1)
            {
                spriteRenderer.flipX = true;
                if (numdis < -1.5)
                {

                    this.transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime);

                }
                else
                {
                    Debug.Log("kill");
                    player.Damage();
                }
                this.name = "derecha";
            }
            else if (dis == -1)
            {
                spriteRenderer.flipX = false;
                if (numdis > 1.5)
                {

                    this.transform.Translate(new Vector3(speed, 0, 0) * -1 * Time.deltaTime);
                }
                else
                {
                    Debug.Log("kill");
                    player.Damage();
                }
                this.name = "izquierda";
            }
            canMove = false;
        }
        else
        {
            
            
            var disp = Mathf.Abs(this.transform.position.x - balizas[i].transform.position.x);
            var deltah = this.transform.GetChild(1).transform.position.y - balizas[i].position.y;
            Debug.Log(deltah + " :delta y");
            if (Mathf.Abs(deltah) > 1 && canJump == true)
            {
                //Damage(9999);
                canJump = false;
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,11.5f),ForceMode2D.Impulse);
                StartCoroutine(JumpCooldownCoRoutine());
            }
            //Debug.Log(disp);
            if (canMove == false)
            {
                StartCoroutine(BalizeCoolDownCoRoutine(1));
            }

            if (canMove == true)
            {

                if (i == 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (i == 1)
                {
                    spriteRenderer.flipX = false;
                }
                this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(balizas[i].transform.position.x, this.transform.position.y), 0.01f);
                if (disp < 1)
                {
                    Debug.Log("arrive");
                    i++;
                    canMove = false;
                    StartCoroutine(BalizeCoolDownCoRoutine(Random.Range(1, 4)));



                    if (i >= balizas.Length)
                    {
                        i = 0;
                    }
                    Debug.Log(i + "now targeting");
                }
            }





        }



    }
    internal void Damage(int a)
    {
        health-=a;
        if (health <= 0)
        {
            death = true;


            Destroy(this.gameObject,2f);


        }
    }
    IEnumerator JumpCooldownCoRoutine()
    {
        yield return new WaitForSeconds(5);
        canJump = true;
    }
    IEnumerator BalizeCoolDownCoRoutine(int cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canMove = true;
        Debug.Log("can move again");
    }

}


