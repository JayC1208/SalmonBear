using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideManager : MonoBehaviour
{
    Animator anim_L, anim_R, anim;

    Sprite[] dish, dish_T;
    public GameObject arms;
    GameObject right, left;
    GameObject soySauce, tea, wasabi;

    int choice, choice2;

    float timer;

    bool soyClicked, teaClicked, wasabiClicked;
    public bool sideConfirm;

    // Start is called before the first frame update
    void Start()
    {
        sideConfirm = true;

        anim_L = GameObject.Find("LeftReq").GetComponent<Animator>();
        anim_R = GameObject.Find("RightReq").GetComponent<Animator>();
        anim = GameObject.Find("Player").GetComponent<Animator>();

        // dish idle sprite
        dish = new Sprite[3];
        dish[0] = Resources.Load<Sprite>("1_gamePlay/Sides/Wasabi");
        dish[1] = Resources.Load<Sprite>("1_gamePlay/Sides/Tea");
        dish[2] = Resources.Load<Sprite>("1_gamePlay/Sides/SoySauce");

        // dish clicked sprite
        dish_T = new Sprite[3];
        dish_T[0] = Resources.Load<Sprite>("1_gamePlay/Sides/Wasabi_T");
        dish_T[1] = Resources.Load<Sprite>("1_gamePlay/Sides/Tea_T");
        dish_T[2] = Resources.Load<Sprite>("1_gamePlay/Sides/SoySauce_T");

        right = GameObject.Find("RightReq");
        left = GameObject.Find("LeftReq");

        right.SetActive(false);
        left.SetActive(false);

        soySauce = GameObject.Find("SoySauce");
        tea = GameObject.Find("Tea");
        wasabi = GameObject.Find("Wasabi");
    }

    public void ResetSide()
    {
        sideConfirm = true;
        wasabiClicked = false;
        teaClicked = false;
        soyClicked = false;

        arms.GetComponent<PlayerMovement>().sideCount = 0;

        anim_R.SetInteger("side_R", 0);
        anim_L.SetInteger("side_L", 0);

        wasabi.GetComponent<SpriteRenderer>().sprite = dish[0];
        tea.GetComponent<SpriteRenderer>().sprite = dish[1];
        soySauce.GetComponent<SpriteRenderer>().sprite = dish[2];
    }

    public void WasabiClick()
    {
        if (!wasabiClicked) // if wasabi not clicked, change sprite to clicked version
        {
            wasabi.GetComponent<SpriteRenderer>().sprite = dish_T[0];
            wasabiClicked = !wasabiClicked;
        }
        else
        {
            wasabi.GetComponent<SpriteRenderer>().sprite = dish[0];
            wasabiClicked = !wasabiClicked;
        }
    }

    public void TeaClick()
    {
        if (!teaClicked)
        {
            tea.GetComponent<SpriteRenderer>().sprite = dish_T[1];
            teaClicked = !teaClicked;
        }
        else
        {
            tea.GetComponent<SpriteRenderer>().sprite = dish[1];
            teaClicked = !teaClicked;
        }
    }

    public void SoyClick()
    {
        if (!soyClicked)
        {
            soySauce.GetComponent<SpriteRenderer>().sprite = dish_T[2];
            soyClicked = !soyClicked;
        }
        else
        {
            soySauce.GetComponent<SpriteRenderer>().sprite = dish[2];
            soyClicked = !soyClicked;
        }
    }

    // 1:1:1의 확률로 반찬 0개:1개:2개
    public int ProbablitySide()
    {
        choice = Random.Range(1, 4);

        switch (choice)
        {
            case 1:
                arms.GetComponent<PlayerMovement>().sideCount = 0;
                break;
            case 2:
                right.SetActive(true);
                break;
            case 3:
                left.SetActive(true);
                right.SetActive(true);
                break;
        }

        return choice;
    }

    public void GetSide()
    {
        choice2 = Random.Range(1, 4);

        if (choice == 2) {
            anim_R.SetInteger("side_R", choice2);
        }
        
        else if (choice == 3)
        {
            anim_R.SetInteger("side_R", choice2);
            int second = (choice2 + 1) % 4 == 0 ? 1 : (choice2 + 1);
            anim_L.SetInteger("side_L", second);
        }
        
    }

    // if any of the sides is clicked, return true
    public bool SideClicked()
    {
        if (soyClicked || teaClicked || wasabiClicked) return true;
        else return false;
    }

    public void CorrectSide()
    {
        switch (choice2)
        {
            case 1:     // wasabi & tea
                if (choice == 2)
                {
                    if (!wasabiClicked) sideConfirm = false;
                    else if (teaClicked || soyClicked) sideConfirm = false;
                }
                else if (choice == 3)
                {
                    if (!wasabiClicked || !teaClicked) sideConfirm = false;
                    else if (soyClicked) sideConfirm = false;
                }
                break;

            case 2:     // tea & soy sauce
                if (choice == 2)
                {
                    if (!teaClicked) sideConfirm = false;
                    else if (wasabiClicked || soyClicked) sideConfirm = false;
                }
                else if (choice == 3)
                {
                    if (!teaClicked || !soyClicked) sideConfirm = false;
                    else if (wasabiClicked) sideConfirm = false;
                }
                break;

            case 3:     // soy sauce & wasabi
                if (choice == 2)
                {
                    if (!soyClicked) sideConfirm = false;
                    else if (teaClicked || wasabiClicked) sideConfirm = false;
                }
                else if (choice == 3)
                {
                    if (!wasabiClicked || !soyClicked) sideConfirm = false;
                    else if (teaClicked) sideConfirm = false;
                }
                break;
        }

        right.SetActive(false);
        left.SetActive(false);

        if(!sideConfirm)        // did not eat correctly
        {
            StartCoroutine("WrongSide");
        }

        Debug.Log("correct: " + sideConfirm);
    }

    IEnumerator WrongSide()
    {
        arms.SetActive(false);
        anim.SetTrigger("Cough");
        yield return new WaitForSeconds(1.2f);
        arms.SetActive(true);
    }
}
