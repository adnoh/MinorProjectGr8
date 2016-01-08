using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System.IO;

public class SaveBase : MonoBehaviour
{
	MySql.Data.MySqlClient.MySqlConnection conn;
	MySql.Data.MySqlClient.MySqlCommand cmd;
	MySqlCommand cmd2;


	// ConnectionString
	string mySQLconnectionString = "Server=80.60.131.231;Database=savebase;UID=userw;Pwd=Minor#8;";

	// savegame path
	string path = "Assets/saves1/saves/package.zip";


	void openConnection(){
		try
		{
			conn = new MySql.Data.MySqlClient.MySqlConnection(mySQLconnectionString);
			conn.Open();
		}

		catch (MySql.Data.MySqlClient.MySqlException ex)
				{
			Debug.Log(ex.Message);
		}
	}

	void UploadSave()
	{

	}

	void UploadNamePassword(string un, string pwd)
	{
		// Open the Connection
		openConnection ();


		try 
		{
			cmd = conn.CreateCommand();
			cmd.CommandText = "INSERT INTO saves(username, password)VALUES(@username, @password)";
			cmd.Parameters.AddWithValue("@username", un);
			cmd.Parameters.AddWithValue("@password", pwd);
			// Execute
			cmd.ExecuteNonQuery();
		}
		catch (MySql.Data.MySqlClient.MySqlErrorCode e) 
		{

			// if the error is 
			if (e = MySqlErrorCode.DuplicateUnique) {

				Debug.Log ("Same username");
			}

			if (e = MySqlErrorCode.DuplicateKey) {
				Debug.Log ("Same username");
			}

			if (e = MySqlErrorCode.DuplicateKeyEntry) {
				Debug.Log ("Same username");
			}
			
		}

		finally
		{ 

			// needs fixing
			if (conn.State == "Open")
			{
				conn.Close();
			}
		}


	}

	void UploadPassword()
	{
	
	}

	void Start(){
	
		UploadNamePassword ("test", "test1234");
	}

}

