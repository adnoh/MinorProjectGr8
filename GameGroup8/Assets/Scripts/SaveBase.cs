using System.Collections;
using MySql.Data.MySqlClient;
using System.IO;
using System;
using UnityEngine;


public class SaveBase
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

		catch (Exception ex)
		{
			if (ex is MySqlException) 
			{				
				MySqlException ex2 = (MySqlException)ex;
				Debug.Log (ex2.Number);
			}
			Debug.Log (ex.Message.ToString ());
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
		catch (Exception ex) 
		{
			Debug.Log (ex.Message.ToString ());
			throw ex;

		}

		finally
		{ 

			// needs fixing
			//if (conn.State ==)
			//{
				conn.Close();
			//}
		}


	}

	void UploadPassword()
	{
	
	}

	void Start(){
	
		UploadNamePassword ("test", "test1234");
	}

}

