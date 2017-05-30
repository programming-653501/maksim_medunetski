<?php

require_once "db_config.php";
require_once "functions.php";
require_once "DB.php";
require_once "auth.php";
require_once "models/Article.php";

$db = new DB(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

$data = get_data();

$user_id = get_user_id();

switch($_SERVER['REQUEST_METHOD'])
{
    case 'GET':
        get_post();
        break;
    case 'POST':
        add_post();
        break;
    case 'PUT':
        update_post();
        break;
    case 'DELETE':
        delete_post();
        break;
    default:
        die();
}

function get_post()
{
    global $db;
    $post = new Article();
    $post->id = $_GET['id'];
    if (!$posts = $db->find($post))
    {
        http_response_code(404);
        die('Nothing was found');
    }
    $post = $posts[0];
    $post->id += 0;
    $post->user_id += 0;
    send_object($post);
}

function auth_required()
{
    global $user_id;
    if (!$user_id)
    {
        http_response_code(401);
        die();
    }
}

function add_post()
{
    global $db, $user_id, $data;
    auth_required();
    $post = new Article();
    $post->title = $data->title;
    $post->content = $data->content;
    $post->user_id = $user_id;
    if (!isset($post->title)||($post->title === ""))
    {
        http_response_code(400);
        die();
    }
    if (!$db->add($post))
    {
        http_response_code(400);
    }
}

function update_post()
{
    global $db, $user_id, $data;
    auth_required();
    $post = new Article();
    $post->id = $data->id;
    $post->title = $data->title;
    $post->content = $data->content;
    $post->user_id = $user_id;
    if (!isset($post->title)||($post->title === ""))
    {
        http_response_code(400);
        die();
    }
    if (!$db->update($post, ['id', 'user_id']))
    {
        http_response_code(400);
    }
}

function delete_post()
{
    global $db, $user_id;
    auth_required();
    $post = new Article();
    $post->id = $_GET['id'];
    $post->user_id = $user_id;
    if (!$db->delete($post))
    {
        http_response_code(400);
    }
}