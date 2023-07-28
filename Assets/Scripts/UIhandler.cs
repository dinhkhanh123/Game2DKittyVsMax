using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIhandel : MonoBehaviour
{
    public GameObject LevelDialog;
    public static UIhandel instance;
    public Text LevelStatus;

     void Awake()
    {
        if(instance == null)
            instance = this;
    }
   
    public void ShowLevelDialog(string status)
    {
        LevelDialog.SetActive(true);
        LevelStatus.text = status;
    }
}
