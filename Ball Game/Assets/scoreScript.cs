using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreScript : MonoBehaviour
{
    TextMeshProUGUI score;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<TextMeshProUGUI>();
        score.text = "HighScore: "+PlayerPrefs.GetInt("score", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
