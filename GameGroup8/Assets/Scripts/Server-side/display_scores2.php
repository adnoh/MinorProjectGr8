<?php
$servername = 's';
$username = '';
$password = '';
$dbname = '';
$table = '';


    // Send variables for the MySQL database class.
    $database = mysql_connect($servername, $username, $password) or die('Could not connect: ' . mysql_error());
    mysql_select_db($dbname) or die('Could not select database');
 
    $query = "SELECT * FROM $table ORDER by `score` DESC LIMIT 20";
    $result = mysql_query($query) or die('Query failed: ' . mysql_error());
 
    $num_results = mysql_num_rows($result);  
    
    echo "<head>
    <title>Highscores - World War What?!</title>
    </head>
    <body background='background.png'>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>	
    <div align='center'>
    <font size='6'>
    <table border='3' style='width:20%'>
    <tr>
    <th>Name</th>
    <th>Score</th>
    </tr>";
 
    for($i = 0; $i < $num_results; $i++)
    {
        $row = mysql_fetch_array($result);
   	echo "<tr>";
	echo "<td>" . $row['name'] . "</td>";
	echo "<td>" . $row['score'] . "</td>";
	echo "</tr>";
    }
    echo "</table>
    </font>
    </div>
    </body>";
	
?>