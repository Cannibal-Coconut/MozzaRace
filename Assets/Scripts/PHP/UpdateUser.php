<?php

header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Headers: Origin, X-Requested-With, Content-Type, Accept");
header("Access-Control-Allow-Methods: GET, POST");
header('content-type: application/json; charset=utf-8');

$servername="localhost";
$username="id17970645_orlandmin";
$password="o_(zGa=2T!f%kZQW";
$dbname="id17970645_mozzadb";

$updateUser = $_POST["updateUser"];
$updatePass = $_POST["updatePass"];
$updateData = $_POST["updateData"];

$conn = new mysqli($servername, $username, $password, $dbname);

if($conn->connect_error){
    die("Connection failed: ");
}

$sql1 = "SELECT username FROM players WHERE username = '$updateUser'";


$result = $conn->query($sql1);

if($result->num_rows > 0){

  $sql2 = "UPDATE `players` SET `data` = '$updateData' WHERE username = '$updateUser'";

  if ($conn->query($sql2) === TRUE) 
  {
    echo "OK";
  }
  else
  {
    echo "NOK";  
  }
  

}
else 
{
  echo "Error: " . $sql1 . "<br>" . $conn->error;
}  


$conn->close();
?>
