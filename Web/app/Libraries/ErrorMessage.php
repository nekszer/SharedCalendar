<?php namespace App\Libraries;

class ErrorMessage {

    public $errorMessage;

    public function setMessage(string $errorMessage){
        $this->errorMessage = $errorMessage;
    }

    public function getMessage() : string {
        return $this->errorMEssage;
    }

}