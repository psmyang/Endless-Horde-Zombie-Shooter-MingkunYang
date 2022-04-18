using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    HealthScript playerHealthScriptComp;

    private void OnHealthInitialized(HealthScript healthScriptComponent)
    {
        playerHealthScriptComp = healthScriptComponent;
    }

    private void OnEnable()
    {
        PlayerEvents.OnHealthInitialized += OnHealthInitialized;
    }

    private void OnDisable()
    {
        PlayerEvents.OnHealthInitialized -= OnHealthInitialized;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = playerHealthScriptComp.CurrentHealth.ToString() + "/" + playerHealthScriptComp.MaxHealth.ToString();

        if(playerHealthScriptComp.CurrentHealth <= 0)
        {           
            SceneManager.LoadScene("GameOver");
        }
    }
}
