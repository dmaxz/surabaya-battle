using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetector : MonoBehaviour
{
    public void GoToScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        IEnumerator backToMainMenu(int scene)
        {
            //Wait for 4 seconds
            //Time.timeScale = 0;
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(scene);
            //Time.timeScale = 1;

        }
        if (other.name == "benderaBelanda")
        {
            GameObject.Find("benderaBelanda").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("benderaIndo").GetComponent<MeshRenderer>().enabled = true;
            StartCoroutine(backToMainMenu(0));
        }
        if (other.name == "planeJM")
        {
            StartCoroutine(backToMainMenu(4));
        }
        if (other.name == "planePB")
        {
            StartCoroutine(backToMainMenu(3));
        }
        if (other.name == "planeRM")
        {
            StartCoroutine(backToMainMenu(2));
        }
    }
}
