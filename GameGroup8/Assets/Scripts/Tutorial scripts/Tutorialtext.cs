using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Tutorialtext : MonoBehaviour {

    
    public Sprite slide2;
    public Sprite slide3;
    public Sprite slide4;
    public Sprite slide5;
    public Sprite slide6;
    public Sprite slide7;


    private string str;
    Text txt;
    int count;
    public bool IsKeyEnabled_W { get; set; }
    SpriteRenderer nextslide;
    List<string> list = new List<string>();


    void Start() {
        nextslide  = GetComponentInChildren<SpriteRenderer>();
        IsKeyEnabled_W = true;
        list.Add("Welcome to the tutorial of World War What??");
        list.Add("Well done! Now, the highlighted area you see in the upper right corner are your stats.");
        list.Add("These represent your status, green for your health, if it reaches 0 you die!");
        list.Add("Yellow for your stamina, While you have stamina you can sprint, if it runs out, you cannot spirnt anymore.");
        list.Add("The grey bar represents your fatigue levels, IT drains slowly while you are outside in the world. ");
      /*6*/  list.Add("The more you lose, the slower you will become, refill the bar by sleeping in your base.");
        list.Add("The Upper bar displays more necessary information. ");
        list.Add("For example, the amount of units you have collected ");
        list.Add("It also display your current level, Whenever you level up you get to spend points to upgrade your character's skills.");
        list.Add("If you have points left to spend the upgrade button wil be blinking red.");
        list.Add("The bar also displays information about your current wave and the amount of time and enemies left.");
      /*12*/ list.Add("If your time runs out a new wave spawns.");
        list.Add("This is your weapons bar");
        list.Add("currently almost all of your weapons are disabled.");
        list.Add("You can unlock the rest with buildings from the base, for more info check your tech-tree in the pause menu");
       /*16*/ list.Add("These things are units, they are dropped by slain enemies and strewn across the world.");
        list.Add("Collect them to spend them on upgrades for your base");
        /*18*/list.Add("This is your base.");
        list.Add("When monsters get close to it, they wil start attacking it");
        list.Add("When your base reaches 0 HP you also lose");
        list.Add("But during the day monster are far less likely to attack your base");
        list.Add("And exploring the world around you might also lead to great rewards");
        list.Add("But during the night the attack intesifies so keep close to your base then!");
       /*24*/ list.Add("This is the base menu");
        list.Add("From here you can build new buildings in your base");
        list.Add("For their effects check your tech-tree");
        list.Add("Building turrets also makes them protect your base, so you dont have to be around all the time!");
        /*28*/list.Add("During the night things get dangerous!");
        list.Add("Monsters will be more aggresive and your sight will be reduced");
        list.Add("You can however buy upgrades for your lights during the night");
        list.Add("Good luck!");
        

        txt = Camera.main.GetComponentInChildren<Text>();
        txt.text = "Press Spacebar to demonstrate your abbility to read! /n/r  Press Escape to skip ";

	
        count = 0;


    }
    void Update()
    {
        
		if ( Input.GetKeyDown(KeyCode.Escape))
		{
				count = 31;
		}

		if (IsKeyEnabled_W)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if( count == list.Capacity)
                {
                    IsKeyEnabled_W = false;
                }
               
                if (count == 6)
                {
            
                    nextslide.sprite = slide2;
                }

                if (count == 12)
                {
                    nextslide.sprite = slide3;
                }

				if (count == 15)
                {
                    nextslide.sprite = slide4;
                }

                if (count == 17)
                {
                    nextslide.sprite = slide5;
                }

                if (count == 23)
                {
                    nextslide.sprite = slide6;
				}

                if (count == 27)
                {
                    nextslide.sprite = slide7;
				}

				if (count == 31)
				{
				
					SceneManager.LoadScene (1);
				}

                StartCoroutine(AnimateText(list[count]));
                count++;
            }
        }
      
    }


    IEnumerator AnimateText(string strComplete)
    {
        IsKeyEnabled_W = false;
        int i = 0;
        str = "";
        
        while (i < strComplete.Length)
        {
            str += strComplete[i++];
            
            txt.text = str;
            yield return new WaitForSeconds(0.01f);
        }
        IsKeyEnabled_W = true;
    }
}
