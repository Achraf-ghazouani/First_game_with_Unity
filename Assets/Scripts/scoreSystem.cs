using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public int score = 0;
    public Text textScore;
    public Canvas canvas;


    // Start is called before the first frame update
    void Start()
    {
        // Initialize the score to 0
        score = 0;

        // Update the text to show the initial score
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        // You can add any additional update logic here if needed
    }

    // Create a function that will add to the score when the player collects a key
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("key"))
        {
            Destroy(other.gameObject);
            score += 1;
            UpdateScoreText();
        }
        if (other.gameObject.CompareTag("box"))
        {
            if (score == 6)
            {
                Time.timeScale = 0;
                canvas.gameObject.SetActive(true);
            }


        }
    }

    // Update the score text on the UI
    void UpdateScoreText()
    {
        textScore.text = "Score: " + score.ToString();
    }
}