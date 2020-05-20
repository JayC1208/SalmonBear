using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator anim;
    public int eatCount;
    bool isLeft;

    public int sideCount;

    public GameObject requirement;
    GameObject bowl;

    // everytime the game object is set active, does the following
    void OnEnable()
    {
        bowl = GameObject.Find("Background").GetComponent<GameManager>().bowl;
        eatCount = 0;
        isLeft = true;
    }

    public void LeftClick()
    {
        if (isLeft)
        {
            anim.SetInteger("Eating", 1);
            bowl.GetComponent<BowlMovement>().ChangeBowlSprite();

            SetSides(sideCount);

            eatCount++;
            sideCount++;
            isLeft = !isLeft;   // to detect whether other arm is pressed or not
        }
    }

    public void RightClick()
    {
        if (!isLeft)
        {
            anim.SetInteger("Eating", -1);

            SetSides(sideCount);

            sideCount++;
            isLeft = !isLeft;   // to detect whether other arm is pressed or not

            if (eatCount == 4)
            {
                anim.SetBool("Eat", false);
                this.gameObject.SetActive(false);
            }
        }
    }

    void SetSides(int sideCount)
    {
        if (sideCount == 5) requirement.GetComponent<SideManager>().ProbablitySide();
        else if (sideCount == 7) requirement.GetComponent<SideManager>().GetSide();
        else if (sideCount > 7 && requirement.GetComponent<SideManager>().SideClicked())
        {
            requirement.GetComponent<SideManager>().CorrectSide();
            requirement.GetComponent<SideManager>().ResetSide();
        }
        else if (sideCount == 12)
        {
            requirement.GetComponent<SideManager>().CorrectSide();
            requirement.GetComponent<SideManager>().ResetSide();
        }
    }
}
