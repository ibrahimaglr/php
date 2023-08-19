<?php
$ip = $_SERVER['REMOTE_ADDR'];
$limit = 5; // Max istek limiti 5
$interval = 300; // saniye (5dk)

// Connect to database
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
else
{
  $user_username = "";
  $user_password = "";
  if(isset($_POST['user_username']) && !empty($_POST['user_username']) && isset($_POST['user_password']) && !empty($_POST['user_password'])) {
    $user_username = mysqli_real_escape_string($con, $_POST['user_username']);
    $user_password = mysqli_real_escape_string($con, $_POST['user_password']);
    $user_password = md5($user_password);
  }
  elseif(isset($_GET['user_username']) && !empty($_GET['user_username']) && isset($_GET['user_password']) && !empty($_GET['user_password'])) {
    $user_username = mysqli_real_escape_string($con, $_GET['user_username']);
    $user_password = mysqli_real_escape_string($con, $_GET['user_password']);
    $user_password = md5($user_password);
  } else {
    mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
    die("nodata");
  }
  $query_login = $con->Query("SELECT user_password, user_banned, user_session, user_usergender FROM users WHERE user_username = '".$user_username."' LIMIT 1");
  if(!$query_login) {
    mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
    die("dataerror");
  }
  elseif($query_login->num_rows != 1) {
    mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
    die("usernotfound");
  } else {
    $query_result = $query_login->Fetch_assoc();
    if($user_password != $query_result['user_password']) {
      mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
      die("userwrongpassword");
    }
    elseif($query_result['user_session'] == 1) {
      mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
      die("usersession");
    }
    elseif($query_result['user_banned'] == 1) {
      mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
      die("userbanned");
    } else {
// Kullanıcının rolünü veritabanından okuyoruz
$result = mysqli_query($con, "SELECT user_role, user_usergender FROM users WHERE user_username = '".$user_username."'");
$row = mysqli_fetch_assoc($result);
$user_role = $row['user_role'];
$user_usergender = $row['user_usergender'];
$token = uniqid('', true);
// Yeni tokeni veritabanına kaydediyoruz
mysqli_query($con, "UPDATE users SET user_session = 1, user_lastip = '$ip', session_lastrequest = NOW(), user_token = '$token' WHERE user_username = '".$user_username."'");
// Yanıt olarak 'success' mesajı ve tokeni gönderiyoruz
die("success|" . $user_role . "|" . $token . "|" . $user_usergender);
    }
  }
}}
?>