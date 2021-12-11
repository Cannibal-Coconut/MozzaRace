<?php

header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Headers: Origin, X-Requested-With, Content-Type, Accept");
header("Access-Control-Allow-Methods: GET, POST");
header('content-type: application/json; charset=utf-8');

$servername="localhost";
$username="id17970645_orlandmin";
$password="o_(zGa=2T!f%kZQW";
$dbname="id17970645_mozzadb";

$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];
$loginData = $_POST["loginData"];

$conn = new mysqli($servername, $username, $password, $dbname);

if($conn->connect_error){
    die("Connection failed: ");
}

$sql1 = "SELECT username FROM players WHERE username = '$loginUser'";


$result = $conn->query($sql1);

if($result->num_rows == 0){

  
  $sql2 = "INSERT INTO players (username, password, data) VALUES ('$loginUser', '$loginPass', '$loginData')";

  if ($conn->query($sql2) === TRUE) {
  
    echo "OK";
  } 
else 
{
  echo "Error: " . $sql1 . "<br>" . $conn->error;
}  
  
}else{
  echo "Error: " . $sql1 . "<br>" . $conn->error;
  //echo "NOK ";


}


$conn->close();
?>
