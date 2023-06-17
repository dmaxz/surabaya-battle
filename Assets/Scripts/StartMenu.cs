using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public int nextSceneIndex;
    public GameObject loadingPanel;
    public GameObject startButton;
    public Image loadingColor;

    public float loadingTime;
    public bool started;

    public byte alphaColor;

    // Start is called before the first frame update
    void Start()
    {
        alphaColor = 0;
        started = false;
        loadingColor = loadingPanel.GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (started == true)
        {
            loadingTime -= Time.deltaTime;
            if (alphaColor < 250)
            {
                
                alphaColor = (byte)(alphaColor + 2);
            }

            if (alphaColor >= 250)
            {
                alphaColor = 255;
            }

            loadingColor.color = new Color32(0, 0, 0, alphaColor);




        }

        if (loadingTime <= 0)
        {
            startGame();
        }
    }

    public void StartBtn()
    {
        started = true;
        startButton.SetActive(false);
    }

    public void startGame()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
