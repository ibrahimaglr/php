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


    function request($con,$ip,$limit, $interval,$count) {// Limit Kontrol
if ($count >= $limit) {
    die("Bu IP adresinden çok fazla istek geliyor. Geçici süreliğine bu IP adresinden gelen istekler askıya alındı");
}

$user_username = "";
$user_token="";
$user_role = "";
$datatype = "";

if(isset($_POST['user_username']) && !empty($_POST['user_username']) && isset($_POST['user_token']) && !empty($_POST['user_token']) && isset($_POST['user_role']) && !empty($_POST['user_role']) 
&& isset($_POST['datatype']) && !empty($_POST['datatype']))  {
  
  $user_username = mysqli_real_escape_string($con, $_POST['user_username']);
  $user_token= mysqli_real_escape_string($con, $_POST['user_token']);
  $user_role = mysqli_real_escape_string($con, $_POST['user_role']);
  $datatype = mysqli_real_escape_string($con, $_POST['datatype']);

}
elseif(isset($_GET['user_username']) && !empty($_GET['user_username']) && isset($_GET['user_token']) && !empty($_GET['user_token']) && isset($_GET['user_role']) && !empty($_GET['user_role']) 
&& isset($_GET['datatype']) && !empty($_GET['datatype'])) {
  
  $user_username = mysqli_real_escape_string($con, $_GET['user_username']);
  $user_token= mysqli_real_escape_string($con, $_GET['user_token']);
  $datatype = mysqli_real_escape_string($con, $_GET['datatype']);

}
else {
  mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
  die("nodata");
}

$query_login = $con->Query("SELECT user_banned,user_token,user_session, user_lastip FROM users WHERE user_username = '".$user_username."' LIMIT 1");
$query_result = $query_login->Fetch_assoc();

if($datatype =="GET_USERS"){
if(!$query_login) {
    mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
    die("dataerror");
  }
elseif($ip == $query_result['user_lastip']){
  if($query_result['user_session'] == 1) {
    if($query_result['user_banned'] == 1){
      mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
      die("userbanned");
    }
    elseif($user_token == $query_result['user_token']){
            if($user_role == "Admin"){
                //
                $query = "SELECT user_username,user_usergender,user_banned,user_session,session_lastrequest,user_role FROM users";
                $result = mysqli_query($con, $query);
        
                if ($result) {
                    $users = array();
                    while ($row = mysqli_fetch_assoc($result)) {
                        $users[] = $row;
                    }
                    die(json_encode($users));
                }
                else{
                    die("badgateway");
                    }
                    }
            else{
                    die("access denied");
                }
        }
    }
  }
  }
  else{
    die("badgateway1");
  }}

?>