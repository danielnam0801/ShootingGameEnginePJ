using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //[SerializeField]
    //private StageData stageData;
    //private Movement movement;

    public Image weapon1;
    public Image weapon2;
    public Image weapon3;
    public GameObject[] weapons;
    Weapon equipWeapon;
    SpriteRenderer spriteRenderer;
    PlayerAction action;
    //int equipWeaponIndex = -1;
    bool sDown1;
    bool sDown2;
    bool sDown3;
    bool fireDown;
    bool isFireReady = true;
    bool isDamage;
    bool fireDown1;
    float fireDelay;

    public int health = 7;

    int score;
    
    public int Score
    {
        get => score;
        set => score = Mathf.Max(0, value);
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        action = GameObject.Find("Player").GetComponent<PlayerAction>();
    }
    void Update()
    {
        Swap();
        Attack();
        
        GetInput();
        if (health <= 0)
        {
            
        }
    }
    void GetInput()
    {
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");
        fireDown = Input.GetButtonDown("Fire1");
        //fireDown1 = Input.GetButton("Fire1");
    }

    void Swap()
    {
        int weaponIndex = -1;
        if (sDown1)
        {
            weaponIndex = 0;
            weapon1.color = new Color(1, 1, 1, 1);
            weapon2.color = new Color(0.5f, 0.5f, 0.5f, 1);
            weapon3.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        
        if (sDown2)
        {
            weaponIndex = 1;
            weapon2.color = new Color(1,1,1,1);
            weapon1.color = new Color(0.5f, 0.5f, 0.5f, 1);
            weapon3.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        
        if (sDown3)
        {
            weaponIndex = 2;
            weapon3.color = new Color(1,1,1,1);
            weapon1.color = new Color(0.5f, 0.5f, 0.5f, 1);
            weapon2.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        

        if (sDown1 || sDown2 || sDown3)
        {
            if (equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);

            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);
        }
    }

    void Attack()
    {
        if(equipWeapon == null)
        {
            return; 
        }

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if((fireDown) && isFireReady )
        {
           equipWeapon.Use();
        }
        fireDelay = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Boss")
        {
            CircleCollider2D boss = collision.gameObject.GetComponent<CircleCollider2D>();
            if(collision == boss)
            {
                StopCoroutine("OnDamage");
                StartCoroutine(OnDamage());
            }
            
        }
        if (collision.tag == "EnemyBullet")
        {
            {
               
                StopCoroutine("OnDamage");
                StartCoroutine(OnDamage());

            }
        }
        if(collision.gameObject.layer == 13)
        {
            Destroy(collision.gameObject);
            StopCoroutine("OnDamage");
            StartCoroutine("OnDamage");
        }
        if (collision.gameObject.layer == 14)
        {
            Destroy(collision.gameObject);
            StopCoroutine("OnDamage");
            StartCoroutine("OnDamage");
        }
        if (collision.gameObject.layer == 15)
        {
            Destroy(collision.gameObject);
            StopCoroutine("OnDamage");
            StartCoroutine("OnDamage");
        }
    }
    public void DieEvent()
    {
        SceneManager.LoadScene("GameOver");
        PlayerPrefs.SetInt("Score", score);
    }

    IEnumerator OnDamage()
    {
        gameObject.layer = 10;
        action.speed = 5f;
        health -= 1;
        Debug.Log(health);
        if (health <= 0)
        {
            
        }
        for (int i=0; i < 4; i++)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            yield return new WaitForSeconds(0.2f);

            spriteRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.2f);
        }
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(1, 1, 1, 1);

        action.speed = 3f;
        gameObject.layer = 11;
        
    }

    //void Move()
    //{
    //    float x = Input.GetAxisRaw("Horizontal");
    //    float y = Input.GetAxisRaw("Vertical");

    //    movement.MoveTo(new Vector3 (x, y, 0));
    //}



    //private void LateUpdate()
    //{
    //    transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
    //                                     Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));   
    //}
}
