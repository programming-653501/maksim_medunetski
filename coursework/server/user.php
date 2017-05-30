<?php

require_once "db_config.php";
require_once "functions.php";
require_once "DB.php";
require_once "auth.php";
require_once "models/User.php";
require_once "models/UserInfo.php";
require_once "models/Article.php";

$db = new DB(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

$cur_user_id = get_user_id();

switch($_SERVER['REQUEST_METHOD'])
{
    case 'GET':
        get_user_info();
        break;
    case 'POST':
        add_user_info();
        break;
    case 'PUT':
        update_user_info();
        break;
    case 'DELETE':
        delete_user_info();
        break;
    default:
        die();
}

function auth_required()
{
    global $cur_user_id;
    if (!$cur_user_id)
    {
        http_response_code(401);
        die();
    }
}

function id_from_get_query()
{
    global $db;
    if (isset($_GET['id']))
        return $_GET['id'];
    $user = new User();
    $user->login = $_GET['login'];
    if ($users = $db->find($user))
    {
        $user = $users[0];
        return $user->id;
    }
    else
    {
        http_response_code(404);
        die('Nothing was found');
    }
}

function login_from_get_query()
{
    global $db;
    if (isset($_GET['login']))
        return $_GET['login'];
    $user = new User();
    $user->id = $_GET['id'];
    if ($users = $db->find($user))
    {
        $user = $users[0];
        return $user->login;
    }
    else
    {
        http_response_code(404);
        die('Nothing was found');
    }
}

function get_user_info()
{
    global $db;
    $user_id = id_from_get_query($db);
    $user_login = login_from_get_query($db);

    $userinf = new UserInfo();
    $userinf->user_id = $user_id;
    $infs = $db->find($userinf);
    if (!$infs)
        $infs = [];

    $post = new Article();
    $post->user_id = $user_id;
    $posts = $db->find($post);
    if (!$posts)
        $posts = [];

    $data = new stdClass();
    $data->id = $user_id;
    $data->login = $user_login;
    $data->articles_id = [];
    $data->additional_info = [];
    foreach ($posts as $post)
    {
        $data->articles_id[] = $post->id + 0;
    }
    foreach ($infs as $info)
    {
        $cl = new stdClass();
        $cl->id = $info->id + 0;
        $cl->type = $info->type;
        $cl->value = $info->value;
        $data->additional_info[] = $cl;
    }
    send_object($data);
}

function add_user_info()
{
    auth_required();
    global $db, $cur_user_id;
    
    $data = get_data();

    $userinfo = new UserInfo();
    $userinfo->user_id = $cur_user_id;
    $userinfo->type = $data->type;
    $userinfo->value = $data->value;

    if (!$db->add($userinfo))
    {
        http_response_code(400);
    }
}

function update_user_info()
{
    auth_required();
    global $db, $cur_user_id;
    
    $data = get_data();

    $userinfo = new UserInfo();
    $userinfo->user_id = $cur_user_id;
    $userinfo->id = $data->id;
    $userinfo->type = $data->type;
    $userinfo->value = $data->value;

    if (!$db->update($userinfo, ['user_id', 'id']))
    {
        http_response_code(400);
    }
}

function delete_user_info()
{
    auth_required();
    global $db, $cur_user_id;
    
    $data = get_data();

    $userinfo = new UserInfo();
    $userinfo->user_id = $cur_user_id;
    $userinfo->id = $_GET['id'];

    if (!$db->delete($userinfo))
    {
        http_response_code(400);
    }
}

?>