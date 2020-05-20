using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
   public void GameScene()
    {
        StartCoroutine(DelaySceneLoad());
       // SceneManager.LoadScene("1_gamePlay");
    }

    IEnumerator DelaySceneLoad()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("1_gamePlay");
    }

    public void MainScene()
    {
        SceneManager.LoadScene("0_start");
    }
}
