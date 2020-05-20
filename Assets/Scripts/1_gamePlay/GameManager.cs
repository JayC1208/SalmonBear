using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animator anim;
    public GameObject startGame;
    public AudioSource bg;

    // player arms
    public GameObject arms;

    // Bowl related
    public GameObject bowl;
    GameObject[] bowls;
    bool eat, newBowl;
    public int bowlCount;

    // fever time
    public GameObject feverTime, salmon, allBowls;
    GameObject requirements;

    // Score Board
    public GameObject scoreCherry, scoreOnly;
    public bool cherryClicked;

    // game end
    public GameObject gameSucceed, gameFail, UI_End, UI_Game;
    public AudioClip fail, succeed;

    //UI
    Text dishNum;
    public Slider timeBar;

    // Audio
    private AudioSource audiosrc;

    // Start is called before the first frame update
    void Start()
    {
        startGame.SetActive(false);

        // disable players from eating by deactivating arms collider
        arms.SetActive(false);

        // make copies of dishes and set active off
        allBowls.SetActive(true);
        bowl = GameObject.Find("Bowl0");
        Vector3 bowlPos = bowl.transform.position;
        bowls = new GameObject[4];
        bowls[0] = bowl;
        for (int i = 0; i < 3; i++)
        {
            bowls[i+1] = Instantiate(bowl, bowlPos, Quaternion.identity);
            bowls[i+1].gameObject.name = "Bowl" + (i + 1);
            bowls[i+1].transform.parent = GameObject.Find("Bowls").transform;
            bowls[i+1].SetActive(false);
        }

        // Gameend Set false
        gameSucceed.SetActive(false);
        gameFail.SetActive(false);

        // UI
        dishNum = GameObject.Find("DishNum").GetComponent<Text>();
        requirements = GameObject.Find("Requirement");

        // Audio
        audiosrc = GetComponent<AudioSource>();
        audiosrc.volume = PlayerPrefs.GetFloat("SliderVolumeLevel", audiosrc.volume);
    }

    // Update is called once per frame
    void Update()
    {
        timeBar.value -= Time.deltaTime;
        dishNum.text = bowlCount.ToString();

        newBowl = bowl.GetComponent<BowlMovement>().getNewBowl;

        if (newBowl)
        {
            bowlCount++;
            bowl = bowls[bowlCount%4];

            bowl.SetActive(true);
            bowl.GetComponent<BowlMovement>().getNewBowl = false;
        }

        if (timeBar.value <= 0)
        {
            ShowScore();
        }

        // Fever Time START
        else if (timeBar.value < 7)      // if 5 sec left till game ends, activate fever time condition (salmon)
        {
            salmon.SetActive(true);
        }  
    }

    public void FeverStart()    // if player clicks the salmon
    {
        anim.SetInteger("Eating", -1);
        anim.SetBool("Eat", false);
        arms.SetActive(false);      // deactivate arms for a moment so that player is not able to click the arms
        bowl.GetComponent<BowlMovement>().ResetBowl();  // reset the bowl state
        allBowls.SetActive(false);      // deactivate the small bowls
        requirements.SetActive(false);      // deactivate sides 
        feverTime.SetActive(true);    
    }

    public void ShowScore()
    {
        bool fever = feverTime.GetComponent<FeverManager>().feverEnd;
        
    }

    public void GameEnd()
    {
        UI_End.SetActive(true);
        UI_Game.SetActive(false);
        feverTime.SetActive(false);

        if(bowlCount >= 15) // if player eats equal to or more than 15 bowls
        {
            bg.clip = succeed;
            gameSucceed.SetActive(true);
        }
        else
        {
            bg.clip = fail;
            gameFail.SetActive(true);
        }
    }
}
