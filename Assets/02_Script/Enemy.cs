using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public int health;
    public int maxHealth;
    private int nScore = 1250;
    private int gScore = 2700;
    private int sScore = 4320;
    public float bossSpeed = 5;
    float nSpeed = 4.8f;
    float gSpeed = 5.2f;
    float sSpeed = 4.8f;
   
    public Sprite[] sprites;
    Rigidbody2D rb;
    //SpriteRenderer spriteRenderer;
    Transform targetTrm;
    Vector3 dir;
    Vector3 degree;
    Player player;





    private void Awake()
    {
        health = maxHealth;
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        targetTrm = GameObject.Find("Player").GetComponent<Transform>();
        //rEnemyPooler = GameObject.Find("EnemySpawner").GetComponent<ObjectPooler>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(gameObject.layer == 19)
        {
            dir = transform.position - targetTrm.transform.position;
            dir.Normalize();
            float rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation + 90f);
            degree = targetTrm.position - transform.position;
            if (degree.magnitude >= 0)
            {
                rb.velocity = degree.normalized * bossSpeed;
                //transform.position += degree * sSpeed * Time.deltaTime;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        if(gameObject.layer == 14)//gunENemy도 할꺼면 레이어 15로 바꾸기
        {
            EnemyRotation();
            NearEnemyMove();
            //transform.position += degree * nSpeed * Time.deltaTime;
        }
        if(gameObject.layer == 13)
        {
           
            EnemyRotation();
            SpecialEnemyMove();
            //transform.position += degree * sSpeed * Time.deltaTime;
        }
        if(gameObject.layer == 15)
        {
            
            EnemyRotation();
            GunEnemyMove();
            //transform.position += degree * nSpeed * Time.deltaTime;
        }
       
    }
    void OnHit(int dmg)
    {
        health -= 1;
       // spriteRenderer.sprite = sprites[1];
        //Invoke("ReturnSprite", 0.1f);
        if(health <= 0)
        {
            if(gameObject.layer == 14)
            {
                player.Score += nScore;
            }
            if (gameObject.layer == 13)
            {
                player.Score += sScore;
            }
            if(gameObject.layer == 15)
            {
                player.Score += gScore;
            }

            //rEnemyPooler.ReturnObject(gameObject);
            Destroy(gameObject);
        } 
    }

    void ReturnSprite()
    {
       // spriteRenderer.sprite = sprites[0];
    }

    void NearEnemyMove()
    {
        degree = targetTrm.position - transform.position;
        if (degree.magnitude >= 0)
        {
            rb.velocity = degree.normalized * nSpeed;
            //transform.position = degree * nSpeed * Time.deltaTime;
        }

    }

    void GunEnemyMove()
    {
        degree = targetTrm.position - transform.position;
        if (degree.magnitude >= 0)
        {
            rb.velocity = degree.normalized * gSpeed;
            //transform.position += degree * gSpeed * Time.deltaTime;
        }
        else
        {
            //GameObject enemyBullet = Instantiate(bullet, )
            rb.velocity = Vector2.zero;
            //transform.position += Vector3.zero;
        }
    }
    void SpecialEnemyMove()
    {
        degree = targetTrm.position - transform.position;
        if (degree.magnitude >= 0)
        {
            rb.velocity = degree.normalized * sSpeed;
            //transform.position += degree * sSpeed * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void EnemyRotation()
    {
        dir = transform.position - targetTrm.transform.position;
        dir.Normalize();
        float rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation + 90f);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Player")
        //{
        //    Destroy(gameObject);
        //    player.health -= 1;
        //    Debug.Log(player.health);
        //}
       
        if(gameObject.tag == "REnemy")
        {
            if (collision.gameObject.tag == "PBullet")
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                OnHit(bullet.damage);
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.tag == "SBullet")
            {
                health += 1;
                Destroy(collision.gameObject);
            }

        }
        if (gameObject.tag == "PEnemy")
        {
            if (collision.gameObject.tag == "SBullet")
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                Destroy(collision.gameObject);
                OnHit(bullet.damage);
            }
            else if (collision.gameObject.tag == "RBullet")
            {
                health += 1;
                Destroy(collision.gameObject);
            }
        }
        if (gameObject.tag == "SEnemy")
        {
            if (collision.gameObject.tag == "RBullet")
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                OnHit(bullet.damage);
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.tag == "PBullet")
            {
                health += 1;
                Destroy(collision.gameObject);
            }
        }
    }

}
