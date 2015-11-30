<?php 
	// Send variables for the MySQL database class.
	// create connection: database location, loginname, loginpassword
	// select the right database
    $db = mysql_connect('80.60.131.231', 'mysql_user', 'mysql_password') or die('Could not connect: ' . mysql_error()); 
    mysql_select_db('my_database') or die('Could not find database');
 
    // Strings must be escaped to prevent SQL injection attack. 
    $name = mysql_real_escape_string($_GET['name'], $db); 
    $score = mysql_real_escape_string($_GET['score'], $db); 
    $hash = $_GET['hash']; 
 
    $secretKey="5o&UeG97cm1A/!v"; #must match the key in unity highscores_script

    $real_hash = md5($name . $score . $secretKey); 
    if($real_hash == $hash) { 
		// Send variables for the MySQL database class. 
        $query = "insert into scores values (NULL, '$name', '$score');"; 
        $result = mysql_query($query) or die('Query failed: ' . mysql_error()); 
    } 
?>