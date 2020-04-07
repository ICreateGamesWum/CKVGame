using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public Animator menuOpenCloseAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void closeMenu()
    {
        Time.timeScale = 1;
        menuOpenCloseAnim.SetBool("Open", false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEvents>().openMenu = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        menuOpenCloseAnim.SetBool("Open", false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEvents>().openMenu = false;
    }

    public void playGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void back(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
