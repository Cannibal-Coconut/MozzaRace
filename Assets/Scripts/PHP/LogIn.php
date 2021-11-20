<?php

$servername="localhost";
$username="id17970645_orlandmin";
$password="o_(zGa=2T!f%kZQW";
$dbname="id17970645_mozzadb";

$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

$conn = new mysqli($servername, $username, $password, $dbname);

if($conn->connect_error){
    die("Connection failed: ");
}

$sql = "SELECT * FROM players WHERE username = '$loginUser'";

$result = $conn->query($sql);

if($result){
  if ($result->num_rows > 0) {

      // output data of each row
      while($row = $result->fetch_assoc()) {
        if($row["password"] == $loginPass){
      
          echo $row["data"];

        }
       else{

          echo "Wrong Credentials.";
        }   

      }
    }else 
    {
      echo "Username does not exist";
    }
}else 
{
  echo "Error in Syntax";
}

$conn->close();


?>