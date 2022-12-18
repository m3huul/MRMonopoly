using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenuAggregator : MonoBehaviour
{   
    [SerializeField] private InputField[] selectionPanels = new InputField[4];
    [SerializeField] private Toggle[] aiToggles = new Toggle[4];
    [SerializeField] private GameObject selectionPanel3Parent, selectionPanel4Parent;
    [SerializeField] private Material[] playerColors;
    private int aiCount, playerCount;

    private string GetNameFromCreationPanel(int num, InputField nameField, Toggle aiToggle)
    {
        if (aiToggle.isOn)
        {
            aiCount++;
            return "AI " + aiCount;
        }
            

        if (nameField.text == "")
        {
            playerCount++;
            return "Player " + playerCount;
        }
        else
        {
            return nameField.text;
        }
    }
    
    public void StartGameClicked()
    {
        for (int i = 0; i < 4; i++)
        {
            if ((i == 2 && !selectionPanel3Parent.activeSelf) || (i == 3 && !selectionPanel4Parent.activeSelf))
            {
                Debug.Log("Skipping player " + (i + 1));
                continue;
            }
            
            Gameplay.instance.SetPlayerData(GetNameFromCreationPanel(i, selectionPanels[i], aiToggles[i]), aiToggles[i].isOn, playerColors[i]);
        }
        
        Gameplay.instance.StartGame();
        
        gameObject.SetActive(false);
    }
}
