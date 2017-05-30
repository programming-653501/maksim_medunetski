<?php

require_once "db_config.php";
require_once "functions.php";
require_once "DB.php";
require_once "models/User.php";
require_once "models/Token.php";

if ($_SERVER['REQUEST_METHOD'] === "OPTIONS")
    die();

$data = get_data();
$user = new User();
$user->login = $data->login;
$user->password = hash_password($data->password);
$db = new DB(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);
if ($user = $db->find($user))
{
    $user = $user[0];
    $token = new Token();
    $token->user_id = $user->id;
    if ($db->add($token))
    {
        send_object($token);
    }
    else
    {
        http_response_code(400);
    }
}
else
{
    http_response_code(400);
}


?>