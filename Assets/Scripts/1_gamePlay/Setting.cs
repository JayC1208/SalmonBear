using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public GameObject totalManager;
    public GameObject setting;

    public void ReturnGame()
    {
        totalManager.SetActive(true);
        setting.SetActive(false);
    }

    public void ClickSetting()
    {
        totalManager.SetActive(false);
        setting.SetActive(true);
    }
}
