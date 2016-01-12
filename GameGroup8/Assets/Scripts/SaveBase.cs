using System.Collections;
using MySql.Data.MySqlClient;
using System.IO;
using System;
using UnityEngine;


public class SaveBase : MonoBehaviour
{
	MySql.Data.MySqlClient.MySqlConnection conn;
	MySql.Data.MySqlClient.MySqlCommand cmd;
	MySql.Data.MySqlClient.MySqlDataReader myData;
	MySqlCommand cmd2;

	static bool loggedIn = false;
	string LoggedInUser;
	string LoggedInPwd;

	// ConnectionString
	string mySQLconnectionString = "Server=80.60.131.231;Database=savebase;UID=userw;Pwd=Minor#8;";

	// savegame path
	string path = "Assets/saves/package.zip";


	/// <summary>
	/// Opens the the connection to the remote MySQL server.
	/// </summary>
	public void openConnection(){
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
				Debug.Log ("MySQL Error: " + ex2.Number);
			}
		}
	}

	/// <summary>
	/// Create a new Username + Password and upload it to the server
	/// </summary>
	/// <param name="un">Un.</param>
	/// <param name="pwd">Pwd.</param>
	public void CreateNamePassword(string un, string pwd)
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
			conn.Close();
		}
		catch (Exception ex) 
		{
			Debug.Log (ex.Message.ToString ());
			if (ex is MySqlException) 
			{				
				MySqlException ex2 = (MySqlException)ex;
				Debug.Log (ex2.Number);

			}
			Debug.Log (ex.ToString());


			throw ex;


		}

		finally
		{ 	// needs fixing
			//if (conn.State ==)
			//{
				conn.Close();
			//}
		}
	}

	/// <summary>
	/// Uploads the save.
	/// </summary>
	/// <param name="un">Lun, loggedin username</param>
	/// <param name="path">Path, path where save is saved.</param>
	public void UploadSave(string lun, string path)
	{

		int FileSize;
		byte[] rawData;
		FileStream fs;

		// Open the Connection
		openConnection ();

		try
		{

			fs = new FileStream(@path, FileMode.Open, FileAccess.Read);
			FileSize = (int)fs.Length;

			rawData = new byte[FileSize];
			fs.Read(rawData, 0, FileSize);
			fs.Close();

			cmd = conn.CreateCommand();
			cmd.CommandText = "UPDATE savebase.saves SET save=@save WHERE username=@Username";
			cmd.Parameters.AddWithValue("@save", rawData);
			cmd.Parameters.AddWithValue("@UserName", lun);

			
			// Execute
			cmd.ExecuteNonQuery();
			conn.Close();
		}

		catch (Exception ex) {
			Debug.Log (ex.Message.ToString ());
			if (ex is MySqlException) {				
				MySqlException ex2 = (MySqlException)ex;
				Debug.Log (ex2.Number);

				// catch duplicate entry exception
				if (ex2.Number == 1062) {
					Debug.Log ("Same username: Pick another");
				}
			}


			Debug.Log (ex.ToString ());


			throw ex;
		}
		finally 
		{
			conn.Close ();
		}		
	}


	/// <summary>
	/// Login using the specified username and password.
	/// </summary>
	/// <param name="username">Username. the username</param>
	/// <param name="password">Password. the password</param>

	public void Login(string username, string password)
	{
		int db_id;
		string db_name;
		string db_password;

		openConnection ();

		try 
		{ 	


			string sql = "SELECT id, username, password FROM savebase.saves WHERE username=@username";
			cmd = new MySqlCommand(sql, conn);
			cmd.CommandText = sql;
			cmd.Parameters.AddWithValue("@username", username);


			myData = cmd.ExecuteReader();

			while (myData.Read())
			{
				db_id = myData.GetInt32("id");;
				db_name = myData.GetString("username");
				db_password = myData.GetString("password");


				//Debug.Log(db_id);
				//Debug.Log(db_name);
				//Debug.Log(db_password);


				// verify if password matches username
				if (password == db_password) {
					loggedIn = true;
					Debug.Log("password match");
					Debug.Log("Succesfully Logged in");
					LoggedInUser = username;
					LoggedInPwd = password;
				} 

				else 
				{
					loggedIn = false;
					Debug.Log("password doesn't match username");
					// Insert error text to unity
				}		
			
			}
			myData.Close();
			conn.Close();



		}

		catch (Exception ex) {
			Debug.Log (ex.Message.ToString ());
			if (ex is MySqlException) 
			{				
				MySqlException ex2 = (MySqlException)ex;
				Debug.Log (ex2.Number);
			}


			Debug.Log (ex.ToString ());


			throw ex;
		}
		finally 
		{
			conn.Close ();
		}	

			

		// read database username
		// read database password

		// search username  -> which id
		// save id as int, find password
		// save password as string
		// does password match?


	}

	

	/* 
	public void DownloadSave(string lun, string path)
	{
		int FileSize;
		byte[] rawData;
		FileStream fs;

		openConnection ();

		cmd.CommandText = "SELECT save FROM saves";

		try {

			myData.Close = cmd.ExecuteReader();
			if (!myData.Has

				FileSize = (int)myData.GetInt32(myData.GetOr



		

		
		
		}
	
	}
*/
	// test code, currently on start but should move to a button
	public void Start(){
	
		// CreateNamePassword ("test", "test1234");
		// UploadSave ("test", path);
		Login("test", "test1234");
		//Debug.Log (1);
		//Login("test", "test12345");
		//Debug.Log (2);

	}






}

