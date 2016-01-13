using System.Collections;
using MySql.Data.MySqlClient;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;



public class SaveBase : MonoBehaviour
{
	MySql.Data.MySqlClient.MySqlConnection conn;
	MySql.Data.MySqlClient.MySqlCommand cmd;
	MySql.Data.MySqlClient.MySqlDataReader myData;
	MySqlCommand cmd2;

    public InputField signup_playername;
    public InputField signup_password;  

    public InputField login_username;
    public InputField login_password;

    public Text LoggedInPlayer;
    public Button SignInOut;

    static bool loggedIn = false;
	static string LoggedInUser;
	static string LoggedInPwd;
    static int LoggedInId;

	// ConnectionString
	string mySQLconnectionString = "Server=80.60.131.231;Database=savebase;UID=userw;Pwd=Minor#8;";

	// savegame path
	string path = "Assets/saves/package.zip";

    // test code, currently on start but should move to a button
    public void Start()
    {

        //CreateNamePassword ("test2222", "test1234");
        //UploadSave ("test2222", path);
        Login();
        //Debug.Log (1);
        //Login("test", "test12345");
        //Debug.Log (2);
        // DownloadSave();


    }

    /// <summary>
    /// Opens the the connection to the remote MySQL server.
    /// </summary>
    /// 

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

    public void CreateNamePassword()
	{


        string un = signup_playername.text;
        string pwd = signup_password.text;

		// Open the Connection
		openConnection ();

		try 
		{
			cmd = conn.CreateCommand();
			cmd.CommandText = "INSERT INTO savebase.saves(username, password)VALUES(@username, @password)";
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
			cmd.CommandText = "UPDATE savebase.saves SET save=@save, filesize=@FileSize WHERE username=@Username";
			cmd.Parameters.AddWithValue("@save", rawData);
			cmd.Parameters.AddWithValue("@UserName", lun);
			cmd.Parameters.AddWithValue("@FileSize", FileSize);


			
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

	public void Login()
	{

        string username = login_username.text;
        string password = login_password.text;

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
                    LoggedInId = db_id;
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

    public void signout()
    {
        loggedIn = false;
        LoggedInUser = null;
        LoggedInPwd = null;

        LoggedInPlayer.text = "Not logged in";
        
    }

	
	/// <summary>
	/// Downloads the save.
	/// </summary>
	/// <param name="lun">Lun. Logged in User</param>
	/// <param name="path">Path. path for saving the zip</param>

	public void DownloadSave()
	{
		if (loggedIn) {

            string lun = LoggedInUser;
			
			int db_FileSize;
			byte[] rawData;
			FileStream fs;

			int db_id;
			string db_name;
			string db_password;

			openConnection ();

			try { 	


				string sql = "SELECT save, filesize FROM savebase.saves WHERE username=@username";
				cmd = new MySqlCommand (sql, conn);
				cmd.CommandText = sql;
				cmd.Parameters.AddWithValue ("@username", lun);

				myData = cmd.ExecuteReader ();

				if (! myData.HasRows){
					throw new Exception("There are no rows");
				}

				myData.Read();


				db_FileSize = myData.GetInt32("filesize");
				rawData = new byte[db_FileSize];

				myData.GetBytes(myData.GetOrdinal("save"), 0, rawData, 0, db_FileSize);

				fs = new FileStream(@path, FileMode.Create, FileAccess.Write);
				fs.Write(rawData, 0, db_FileSize);
				fs.Close();

				myData.Close();
				conn.Close();


			} catch (Exception ex) {
				Debug.Log (ex.Message.ToString ());
				if (ex is MySqlException) {				
					MySqlException ex2 = (MySqlException)ex;
					Debug.Log (ex2.Number);
				}


				Debug.Log (ex.ToString ());


				throw ex;
			} finally {
				conn.Close ();
			}
		}



	}
}

