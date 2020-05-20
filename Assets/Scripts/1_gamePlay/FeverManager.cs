using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverManager : MonoBehaviour
{
    public Animator anim;

    public GameObject bigBowl, feverArms, feverSucceed;
    public Transform target;
    public GameManager gameManager;
    Sprite[] bowlSprite_F;
    int eatCount_F, spriteCount_F;
    bool isLeft;
    public bool feverEnd;

    void Start()
    {
        bowlSprite_F = new Sprite[5];
        for (int i = 0; i < 5; i++)
        {
            bowlSprite_F[i] = Resources.Load<Sprite>("1_gamePlay/Bowl/Bowl" + (i + 1));
        }
        isLeft = true;
    }

    void OnEnable()
    {
        StartCoroutine("DishFall");

    }

    void Update()
    {
        if (feverEnd)
        {
            setSize();
        }
    }

    void setSize()
    {
        float step = 5f * Time.deltaTime;
        feverSucceed.transform.position = Vector3.MoveTowards(feverSucceed.transform.position, target.position, step);
        if(feverSucceed.transform.localScale.x > 0.4f) feverSucceed.transform.localScale += new Vector3(-0.08f, -0.08f, 0);
    }

    public void LeftClick_F()
    {
        if (isLeft)
        {
            anim.SetInteger("Eating", 1);
            ChangeBowlSprite_F();

            eatCount_F++;
            isLeft = !isLeft;   // to detect whether other arm is pressed or not
        }
    }

    public void RightClick_F()
    {
        if (!isLeft)
        {
            anim.SetInteger("Eating", -1);

            isLeft = !isLeft;   // to detect whether other arm is pressed or not

            if (eatCount_F == 10)   // if succeed fever time
            {
                feverEnd = true;
                StartCoroutine("endingFever");
            }
        }
    }

    public void ChangeBowlSprite_F()
    {
        if (spriteCount_F > 12) spriteCount_F = 12;
        spriteCount_F++;
        bigBowl.gameObject.GetComponent<SpriteRenderer>().sprite = bowlSprite_F[spriteCount_F/3];
    }

    IEnumerator DishFall()
    {
        float time = 0 ;
        
        while(time < 0.312f)
        {
            bigBowl.transform.position += new Vector3(0, -0.6f, 0);
            time += Time.deltaTime;
            yield return new WaitForSeconds(0.015f);            
        }
        feverArms.SetActive(true);
        anim.SetBool("Eat", true);
        yield return null;
    }

    IEnumerator endingFever()
    {
        anim.SetBool("Eat", false);
        feverSucceed.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        gameManager.ShowScore();    // show the score
    }
}
