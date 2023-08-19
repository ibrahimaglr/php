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



$user_username = "";
$user_token = "";
$user_role = "";
$datatype = "";

$customer_name = "";
$customer_surname = "";
$customer_PNR = "";
$customer_progress = "";
$customer_company = "";
$customer_amount = "";
$customer_commission = "";
$customer_card = "";


if(isset($_POST['user_username']) && !empty($_POST['user_username']) && isset($_POST['user_token']) && !empty($_POST['user_token']) && isset($_POST['user_role']) && !empty($_POST['user_role']) 
&& isset($_POST['customer_name']) && !empty($_POST['customer_name']) && isset($_POST['customer_surname']) && !empty($_POST['customer_surname']) && isset($_POST['customer_PNR']) && !empty($_POST['customer_PNR']) 
&& isset($_POST['customer_progress']) && !empty($_POST['customer_progress']) && isset($_POST['customer_company']) && !empty($_POST['customer_company']) && isset($_POST['datatype']) && !empty($_POST['datatype'])
&& isset($_POST['customer_amount']) && !empty($_POST['customer_amount']) && isset($_POST['customer_commission']) && !empty($_POST['customer_commission']) && isset($_POST['customer_card']) && !empty($_POST['customer_card']))  {
  // bu satırdan dewam  
  $user_token= mysqli_real_escape_string($con, $_POST['user_token']);
  $user_role = mysqli_real_escape_string($con, $_POST['user_role']);
  $user_username = mysqli_real_escape_string($con, $_POST['user_username']);
  $datatype = mysqli_real_escape_string($con, $_POST['datatype']);

  $customer_name = mysqli_real_escape_string($con, $_POST['customer_name']);
  $customer_surname = mysqli_real_escape_string($con, $_POST['customer_surname']);
  $customer_PNR = mysqli_real_escape_string($con, $_POST['customer_PNR']);
  $customer_progress = mysqli_real_escape_string($con, $_POST['customer_progress']);
  $customer_company = mysqli_real_escape_string($con, $_POST['customer_company']);
  $customer_amount = mysqli_real_escape_string($con, $_POST['customer_amount']);
  $customer_commission = mysqli_real_escape_string($con, $_POST['customer_commission']);
  $customer_card = mysqli_real_escape_string($con, $_POST['customer_card']);

}
elseif(isset($_GET['user_username']) && !empty($_GET['user_username']) && isset($_GET['user_token']) && !empty($_GET['user_token']) && isset($_GET['user_role']) && !empty($_GET['user_role']) 
&& isset($_GET['customer_name']) && !empty($_GET['customer_name']) && isset($_GET['customer_surname']) && !empty($_GET['customer_surname']) && isset($_GET['customer_PNR']) && !empty($_GET['customer_PNR']) 
&& isset($_GET['customer_progress']) && !empty($_GET['customer_progress']) && isset($_GET['customer_company']) && !empty($_GET['customer_company']) && isset($_GET['datatype']) && !empty($_GET['datatype'])
&& isset($_GET['customer_amount']) && !empty($_GET['customer_amount']) && isset($_GET['customer_commission']) && !empty($_GET['customer_commission']) && isset($_GET['customer_card']) && !empty($_GET['customer_card'])) {
  //
  $user_token= mysqli_real_escape_string($con, $_GET['user_token']);
  $user_role = mysqli_real_escape_string($con, $_GET['user_role']);
  $user_username = mysqli_real_escape_string($con, $_GET['user_username']);

  $customer_name = mysqli_real_escape_string($con, $_GET['customer_name']);
  $customer_surname = mysqli_real_escape_string($con, $_GET['customer_surname']);
  $customer_PNR = mysqli_real_escape_string($con, $_GET['customer_PNR']);
  $customer_progress = mysqli_real_escape_string($con, $_GET['customer_progress']);
  $customer_company = mysqli_real_escape_string($con, $_GET['customer_company']);
  $customer_amount = mysqli_real_escape_string($con, $_GET['customer_amount']);
  $customer_commission = mysqli_real_escape_string($con, $_GET['customer_commission']);
  $customer_card = mysqli_real_escape_string($con, $_GET['customer_card']);
}
else {
  mysqli_query($con, "INSERT INTO requests (ip, time) VALUES ('$ip', NOW())");
  die("nodata");
}

//
$query_login = $con->Query("SELECT customer_name, customer_surname, customer_PNR, customer_progress, customer_company FROM customers WHERE customer_PNR = '".$customer_PNR."' LIMIT 1");
$query_login2 = $con->Query("SELECT user_role, user_session, user_token, user_banned, user_lastip FROM users WHERE user_username = '".$uuser_username."' LIMIT 1");
//
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