using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlMovement : MonoBehaviour
{
    Animator anim;
    public GameObject arms;

    bool eat;
    Vector3 bowlPos;
    public bool getNewBowl;
    Sprite[] bowlSprite;

    int spriteCount;

    // Start is called before the first frame update
    void Start()
    {
        getNewBowl = false;

        anim = GameObject.Find("Player").GetComponent<Animator>();
        bowlPos = this.transform.position;

        // get bowl Sprites
        bowlSprite = new Sprite[5];
        for(int i = 0; i < 5; i++)
        {
            bowlSprite[i] = Resources.Load<Sprite>("1_gamePlay/Bowl/Bowl" + (i+1));
        }
    }

    // Update is called once per frame
    void Update()
    {
        eat = anim.GetBool("Eat");

        if (!eat)
        {
            this.transform.position += new Vector3(0.3f, 0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Table")
        {
            anim.SetBool("Eat", true);
            arms.SetActive(true);
        }

        if (other.gameObject.name == "OffScreen")
        {
            ResetBowl();
        }
    }

    // bring new bowl into screen
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Table")
        {
            getNewBowl = true;
        }
    }

    // the amount of food inside the bowl changes as user clicks arms of bear
    public void ChangeBowlSprite()
    {
        if (spriteCount > 3) spriteCount = 3;
        spriteCount++;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = bowlSprite[spriteCount];
    }

    // set the bowl at its original place and deactivate
    public void ResetBowl()
    {
        this.transform.position = bowlPos;
        spriteCount = 0;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = bowlSprite[0];
        this.gameObject.SetActive(false);
    }
}
