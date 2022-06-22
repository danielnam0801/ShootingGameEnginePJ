using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameCamera;
    public GameObject startCamera;
    public Player player;
    public bool isBattle;
    public GameObject gamePanel;
    public GameObject spawn1;
    public GameObject[] spawn;
    float time = 0f;
    float ftime = 1f;
    public Image Panel;
    public GameObject player_1;
    Image Button;
    Image image;
    Text text;


    public void Update()
    {

    }
    public void Fade()
    {
        StartCoroutine(FadeFlow());

    }

    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        Button = GameObject.Find("Button").GetComponent<Image>();
        Button.enabled = false;
        image = GameObject.Find("Image").GetComponent<Image>();
        image.enabled = false;
        text = GameObject.Find("Text").GetComponent<Text>();
        text.enabled = false;
        Color alpha = Panel.color;
        while (alpha.a < 1f)

        {

            time += Time.deltaTime / ftime;
            alpha.a = Mathf.Lerp(0.3f, 1, time);
            Panel.color = alpha;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        gamePanel.SetActive(true);
        player_1.SetActive(true);
        startCamera.SetActive(false);
        
        

        time = 0f;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / ftime;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }

        Panel.gameObject.SetActive(false);
        
        for(int i = 0; i < spawn.Length; i++)
        {
            spawn[i].SetActive(true);
        }
        spawn1.SetActive(true);


    }
}