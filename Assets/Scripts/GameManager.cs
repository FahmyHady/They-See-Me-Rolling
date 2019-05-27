using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject endGamePanel;
    Text maxDistance;

    [SerializeField] Text distanceTravelled;
    [SerializeField] GameObject player;
    PlayerControl playerControl;
    GameObject tempPlayer;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject jumpPanel;
    GameObject lastSpawned;
    Light directionalLight;
    int count;
    float startTime;
    Color colorChange;
    bool increaseColor;
    [SerializeField] float delay;
    float elapsedTime;
    [SerializeField] Color targetcolor = new Color();

    void Awake()
    {
        maxDistance = endGamePanel.GetComponentsInChildren<Text>()[1];
        //    InstantiatePlayer();
        tempPlayer = FindObjectOfType<PlayerControl>().gameObject;
        playerControl = tempPlayer.GetComponent<PlayerControl>();
        directionalLight = FindObjectOfType<Light>();
        startTime = Time.time;
        InstantiateLevel();
    }
    public void GameOver()
    {
        tempPlayer.SetActive(false);
        maxDistance.text = "Distance Traveled: " + distanceTravelled.text;
        endGamePanel.SetActive(true);

    }
    void ColorLerp()
    {
        targetcolor.r = Random.Range(0.1f, 0.9f);
        targetcolor.g = Random.Range(0.1f, 0.9f);
        targetcolor.b = Random.Range(0.1f, 0.9f);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    private void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= delay)
        {
            ColorLerp();
            elapsedTime = 0;
        }
        else
        {
            colorChange = Color.Lerp(colorChange, targetcolor, 0.01f);
            directionalLight.color = colorChange;
        }
        distanceTravelled.text = "Distance: " + ((Time.time - startTime) * playerControl.speed.x).ToString();
    }
    void InstantiatePlayer()
    {
        tempPlayer = Instantiate(player, transform.position + new Vector3(4, 4, 3), Quaternion.identity);
    }
    void InstantiateLevel()
    {
        lastSpawned = Instantiate(panel, Vector3.zero, Quaternion.identity);
        for (int i = 0; i < 15; i++)
        {
            InstantiatePanel();
        }
    }

    public void InstantiatePanel()
    {
        if (count < 15)
        {
            lastSpawned = Instantiate(panel, lastSpawned.transform.GetChild(0).GetChild(0).transform.position, Quaternion.identity);
            count += 1;
        }
        else
        {
            InstantiateJumpPanel();
            count = 0;
        }
    }
    public void InstantiateJumpPanel()
    {
        lastSpawned = Instantiate(jumpPanel, lastSpawned.transform.position, Quaternion.identity);
    }
}
