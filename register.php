<?php
$ip = $_SERVER['REMOTE_ADDR'];
$limit = 5; // Max istek limiti 5
$interval = 300; // saniye (5dk)

  $con = mysqli_connect("localhost", "root", "", "loader");
  if(!$con) {
    die("noconn");
  }

  function del_lastrequest($con, $interval)  {
    // Eski İstekleri Sil
    mysqli_query($con, "DELETE FROM requests WHERE time < (NOW() - INTERVAL $interval SECOND)");
    
    }
    
    //del_lastrequest();
    
    // İstek Sayısı
    $result = mysqli_query($con, "SELECT COUNT(*) AS count FROM requests WHERE ip = '$ip'");
    $row = mysqli_fetch_assoc($result);
    $count = $row['count'];
    
    //security mode
    $secmode = mysqli_query($con, "SELECT COUNT(*) AS securitymode FROM settings WHERE settings_mode = 'sec_secmode' AND settings_value = 1;");
    $manmode = mysqli_query($con, "SELECT COUNT(*) AS manimode FROM settings WHERE settings_mode = 'sec_manmode' AND settings_value = 1;");
    $secrow = mysqli_fetch_assoc($secmode);
    $manrow = mysqli_fetch_assoc($manmode);
    
    $secc = $secrow['securitymode'];
    $manc = $manrow['manimode'];
    
    if($secc == 1){
      request($con, $ip, $limit, $interval, $count);
    }
    elseif ($manc == 1){
      die("BAKIM MODUNDA");
    }
    else{
      del_lastrequest($con, $interval);
      request($con, $ip, $limit, $interval, $count);
    }

// Limit Kontrol
function request($con,$ip,$limit, $interval,$count) {
  if ($count >= $limit) {
    die("Bu IP adresinden çok fazla istek geliyor. Geçici süreliğine bu IP adresinden gelen istekler askıya alındı");
}
$uuser_username = "";
$user_token="";
$uuser_role = "";

$user_username = "";
$user_password = "";
$user_role = "";
$user_usergender = "";
$datatype = "";

if(isset($_POST['uuser_username']) && !empty($_POST['uuser_username']) && isset($_POST['user_token']) && !empty($_POST['user_token']) && isset($_POST['uuser_role']) && !empty($_POST['uuser_role']) 
&& isset($_POST['user_username']) && !empty($_POST['user_username']) && isset($_POST['user_password']) && !empty($_POST['user_password']) && isset($_POST['user_role']) && !empty($_POST['user_role']) 
&& isset($_POST['user_usergender']) && !empty($_POST['user_usergender']) && isset($_POST['datatype']) && !empty($_POST['datatype']))  {
  
  $uuser_username = mysqli_real_escape_string($con, $_POST['uuser_username']);
  $user_token= mysqli_real_escape_string($con, $_POST['user_token']);
  $uuser_role = mysqli_real_escape_string($con, $_POST['uuser_role']);
  $user_username = mysqli_real_escape_string($con, $_POST['user_username']);
  $user_password = mysqli_real_escape_string($con, $_POST['user_password']);
  $user_password = md5($user_password);
  $user_role = mysqli_real_escape_string($con, $_POST['user_role']);
  $user_usergender = mysqli_real_escape_string($con, $_POST['user_usergender']);
  $datatype = mysqli_real_escape_string($con, $_POST['datatype']);
}
elseif(isset($_GET['uuser_username']) && !empty($_GET['uuser_username']) && isset($_GET['user_token']) && !empty($_GET['user_token']) && isset($_GET['uuser_role']) && !empty($_GET['uuser_role']) 
&& isset($_GET['user_username']) && !empty($_GET['user_username']) && isset($_GET['user_password']) && !empty($_GET['user_password']) && isset($_GET['user_role']) && !empty($_GET['user_role']) 
&& isset($_GET['user_usergender']) && !empty($_GET['user_usergender']) && isset($_GET['datatype']) && !empty($_GET['datatype'])) {
  
  $uuser_username = mysqli_real_escape_string($con, $_GET['uuser_username']);
  $user_token= mysqli_real_escape_string($con, $_GET['user_token']);
  $uuser_role = mysqli_real_escape_string($con, $_GET['uuser_role']);
  $user_username = mysqli_real_escape_string($con, $_GET['user_username']);
  $user_password = mysqli_real_escape_string($con, $_GET['user_password']);
  $user_password = md5($user_password);
  $user_role = mysqli_real_escape_string($con, $_GET['user_role']);
  $user_usergender = mysqli_real_escape_string($con, $_GET['user_usergender']);
  $datatype = mysqli_real_escape_string($con, $_GET['datatype']);
}
else {
  mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
  die("nodata");
}

$query_login = $con->Query("SELECT user_banned FROM users WHERE user_username = '".$user_username."' LIMIT 1");
$query_login2 = $con->Query("SELECT user_role, user_session, user_token, user_banned, user_lastip FROM users WHERE user_username = '".$uuser_username."' LIMIT 1");
$query_result = $query_login->Fetch_assoc();
$query_result2 = $query_login2->Fetch_assoc();
if($datatype =="CREATE")
{
  if(!$query_login) {
    mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
    die("dataerror");
  }
  if(!$query_login2) {
    mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
    die("dataerror");
  }
  //
  elseif($query_login->num_rows == 1) {
    die("userfound");
  } 
  elseif($query_result2['user_session'] == 1) {

    if($query_result2['user_banned'] == 1){
      mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
      die("userbanned");
    }

    else{

      if($user_token == $query_result2['user_token']){ 
        if ($ip == $query_result2['user_lastip']){

if($uuser_role == "Admin"){

  $con->Query("INSERT INTO `users` (`user_username`, `user_password`, `user_banned`,`user_session`,`user_role`,`user_usergender`) VALUES('".$user_username."', '".$user_password."', 0, 0, '".$user_role."', '".$user_usergender."');");    
  die("success");
}    
else{
  mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
  die("badgateway1");
  //permaban
}
    }
    else{
      mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
      die("badgateway2");
      //permaban
    }
  }
    else{
      mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
      die("badgateway3");
      //permaban
    }
    }
    }
  }}
?>