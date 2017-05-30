<?php

function get_token()
{
    $headers = apache_request_headers();
    $token = explode(' ', $headers['Authorization'])[1];
    return $token;
}

function get_data()
{
    $raw = file_get_contents('php://input');
    $data = json_decode($raw);
    return $data;
}

function send_object($object)
{
    //header("Access-Control-Allow-Origin: *");
    //header("Access-Control-Allow-Methods: GET, POST, PUT, DELETE, OPTIONS");  
    //header("Access-Control-Max-Age: 1000");     
    //header("Access-Control-Allow-Headers: Content-Type, Authorization");

    header('Content-Type: application/json;charset=utf-8');
    echo json_encode($object);
}

function hash_password($password)
{
    return md5($password);
}

?>