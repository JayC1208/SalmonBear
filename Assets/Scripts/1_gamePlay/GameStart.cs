using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject TotalManager;
    Sprite[] start;

    // Start is called before the first frame update
    void Start()
    {
        start = new Sprite[3];
        start[0] = Resources.Load<Sprite>("1_gamePlay/2");
        start[1] = Resources.Load<Sprite>("1_gamePlay/1");
        start[2] = Resources.Load<Sprite>("1_gamePlay/Start");

        StartCoroutine("GStart");
    }

    IEnumerator GStart()
    {
        yield return new WaitForSeconds(1);
        this.GetComponent<SpriteRenderer>().sprite = start[0];
        yield return new WaitForSeconds(1);
        this.GetComponent<SpriteRenderer>().sprite = start[1];
        yield return new WaitForSeconds(1);
        this.GetComponent<SpriteRenderer>().sprite = start[2];
        yield return new WaitForSeconds(1);
        TotalManager.SetActive(true);
    }
}
