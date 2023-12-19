using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class nextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //create next level function
    public void nextLevelFunction()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //load next scene
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void quitGame()
    {
        //quit game
        Application.Quit();
    }
}
