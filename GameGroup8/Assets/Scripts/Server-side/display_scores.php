<?php
    // Send variables for the MySQL database class.
	// create connection: database location, loginname, loginpassword
	// select the right database
    $database = mysql_connect('localhost', '****', '********') or die('Could not connect: ' . mysql_error());
    mysql_select_db('highscores') or die('Could not select database');
 
 
	// show last 10 values (hardcoded 10);
    $query = "SELECT * FROM `scores` ORDER by `score` DESC LIMIT 10";
    $result = mysql_query($query) or die('Query failed: ' . mysql_error());
 
	// select the whole row
    $num_results = mysql_num_rows($result);  
	
	// get these results printed
    for($i = 0; $i < $num_results; $i++)
    {
         $row = mysql_fetch_array($result);
         echo $row['name'] . "\t" . $row['score'] . "\n";
    }
	// done, close connection
	mysql_close($database);
	
?>