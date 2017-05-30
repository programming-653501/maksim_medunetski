<?php

class Token
{
    public $id, $user_id, $token;
    
    public function __construct()
    {
        $this->token = $this->gen_token();
    } 

    public function gen_token()
    {
        return md5(rand(0, PHP_INT_MAX));
    }
}

