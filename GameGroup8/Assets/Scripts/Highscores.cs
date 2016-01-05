using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class Highscores : MonoBehaviour
{
    private string secretKey = "5o&UeG97cm1A/!v"; // same value as server secretKey;
    public string addScoreURL = "http://80.60.131.231/groep8/Highscores/addscore.php?"; 
    public string highscoreURL = "http://80.60.131.231/groep8/Highscores/display_scores.php";
    public Text higscoretext;

    // currently an input field for both playername and score. Score must be an integer.
    public InputField playername;    
    
    // needed for posting
    string player_name;
    int hi_score;

        
    public void StartGetScores()
    {
        StartCoroutine(GetScores());
    }


    public void StartPostScores()
    {
        player_name = playername.text;
		if (player_name != null) 
		{
			hi_score = Score.score;
			StartCoroutine (PostScores (player_name, hi_score));
		}
    }





    // remember to use StartCoroutine when calling this function!
    IEnumerator PostScores(string name, int score)
    {
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        string hash = Md5Sum(name + score + secretKey);
 
        string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;
        print(post_url);
 
        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done
 
        if (hs_post.error != null)
        {
            print("There was an error posting the high score: " + hs_post.error);
        }
    }

		
 
    // Get the scores from the MySQL DB to display in a GUIText.
    // remember to use StartCoroutine when calling this function!
   IEnumerator GetScores()
    {
        //higscoretext.text = "Loading Scores";
        WWW hs_get = new WWW(highscoreURL);
        yield return hs_get;

        if (hs_get.error != null)
        {
            print("There was an error getting the high score: " + hs_get.error);
            higscoretext.text = "There was an error getting the high score: " + hs_get.error;
        }          
        higscoretext.text = hs_get.text; // this will display the text
     }
	
	public  string Md5Sum(string strToEncrypt)
{
	System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
	byte[] bytes = ue.GetBytes(strToEncrypt);
 
	// encrypt bytes
	System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
	byte[] hashBytes = md5.ComputeHash(bytes);
 
	// Convert the encrypted bytes back to a string (base 16)
	string hashString = "";
 
	for (int i = 0; i < hashBytes.Length; i++)
	{
		hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
	}
 
	return hashString.PadLeft(32, '0');
}
 
}