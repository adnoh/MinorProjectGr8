using System.Collections;
using MySql.Data.MySqlClient;
using System.IO;
using System;
using UnityEngine;


public class SaveBase : MonoBehaviour
{
	MySql.Data.MySqlClient.MySqlConnection conn;
	MySql.Data.MySqlClient.MySqlCommand cmd;
	MySqlCommand cmd2;

	string LoggedInUser;
	string LoggedInPwd;

	// ConnectionString
	string mySQLconnectionString = "Server=80.60.131.231;Database=savebase;UID=userw;Pwd=Minor#8;";

	// savegame path
	string path = "Assets/saves/package.zip";
	int FileSize;
	byte[] rawData;
	FileStream fs;

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

			// This works :)
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
		}
		catch (Exception ex) 
		{
			Debug.Log (ex.Message.ToString ());
			if (ex is MySqlException) 
			{				
				MySqlException ex2 = (MySqlException)ex;
				Debug.Log (ex2.Number);

				if (ex2.Number == 1062) {
					Debug.Log ("Same username: Pick another");
				}
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
		}

		catch (Exception ex) {
			Debug.Log (ex.Message.ToString ());
			if (ex is MySqlException) {				
				MySqlException ex2 = (MySqlException)ex;
				Debug.Log (ex2.Number);

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



	// test code
	public void Start(){
	
		CreateNamePassword ("test", "test1234");
		UploadSave ("test", path);
		Debug.Log (1);
	}






}

