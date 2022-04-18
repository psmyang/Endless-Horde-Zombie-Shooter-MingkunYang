using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    static public int scoreAmount;

    [SerializeField] 
    TextMeshProUGUI scoreNum;

    // Start is called before the first frame update
    void Start()
    {
        scoreNum = GetComponent<TextMeshProUGUI>();
        scoreAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreNum.text = "Score: " + scoreAmount;
    }
}
