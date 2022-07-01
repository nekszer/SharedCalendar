<?php namespace App\Entities;

class Account
{
    public $id;
    public $email;
    public $password;
    public $user;
    public $clientId;

    public function __get($key)
    {
        if (property_exists($this, $key)) {
            return $this->$key;
        }
    }

    public function __set($key, $value)
    {
        if (property_exists($this, $key)) {
            $this->$key = $value;
        }
    }
}