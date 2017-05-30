<?php

require_once "db_config.php";
require_once "functions.php";
require_once "DB.php";
require_once "models/Token.php";

function get_user_id()
{
    $_token = get_token();
    if ($_token === null)
        return false;
    $db = new DB(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);
    $token = new Token();
    $token->token = $_token;
    if ($token = $db->find($token))
    {
        $token = $token[0];
        return $token->user_id;
    }
    else
    {
        return false;
    }
}

?>