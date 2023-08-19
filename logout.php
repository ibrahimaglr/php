<?php
$ip = $_SERVER['REMOTE_ADDR'];
$limit = 5; // Max istek limiti 5
$interval = 300; // saniye (5dk)

// Connect to database
$con = mysqli_connect("localhost", "root", "", "loader");
  if(!$con) {
    die("noconn");
  }

// Eski İstekleri Sil
//mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
mysqli_query($con, "DELETE FROM requests WHERE time < (NOW() - INTERVAL $interval SECOND)");

// İstek Sayısı
$result = mysqli_query($con, "SELECT COUNT(*) AS count FROM requests WHERE ip = '$ip'");
$row = mysqli_fetch_assoc($result);
$count = $row['count'];

// Limit Kontrol
if ($count >= $limit) {
    die("Bu IP adresinden çok fazla istek geliyor. Geçici süreliğine bu IP adresinden gelen istekler askıya alındı");
}
else
{
  $user_username = "";
  $user_token = "";
  if(isset($_POST['user_username']) && !empty($_POST['user_username']) && isset($_POST['user_token']) && !empty($_POST['user_token'])) {
    $user_username = mysqli_real_escape_string($con, $_POST['user_username']);
    $user_token = mysqli_real_escape_string($con, $_POST['user_token']);   
  }
  elseif(isset($_GET['user_username']) && !empty($_GET['user_username']) && isset($_GET['user_token']) && !empty($_GET['user_token'])) {
    $user_username = mysqli_real_escape_string($con, $_GET['user_username']);
    $user_token = mysqli_real_escape_string($con, $_GET['user_token']);
  } else {
    mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
    die("nodata");
  }
  $query_login = $con->Query("SELECT user_token, user_session, user_username, user_lastip FROM users WHERE user_username = '".$user_username."' LIMIT 1");
  if(!$query_login) {
    mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
    die("dataerror");
  }
  elseif($query_login->num_rows != 1) {
    mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
    die("usernotfound");
  } else {
    $query_result = $query_login->Fetch_assoc();
    if($user_username != $query_result['user_username']) {
      mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
      die("wrongusername");
    }
    elseif($query_result['user_token'] != $user_token) {
      mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
      die("userwrongtoken");
    }
    elseif($query_result['user_lastip'] != $ip){
      mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
      die("userwrongip");
    } 
    else {
    mysqli_query($con, "UPDATE users SET user_session = 0, user_lastip = '$ip', session_lastrequest = NOW() WHERE user_username = '".$user_username."'");
    die("success");
    }
  }
}
?>