<?php

namespace App\Notifications;

class EmailNotification implements INotification {

    /**
     * Realiza el proceso para enviar una notificacion por mail al usuario de la cuenta
     */
    public function notify($accountId, $title, $message) : bool 
    {
        $email = \Config\Services::email();
        $email->SMTPHost = "smtp.hostinger.com";
        $email->SMTPUser = "contacto@aliensofttech.com";
        $email->SMTPPass = "@Nekszer01";
        $email->SMTPPort = "465";
        $userName = "Shared Calendar";
        
        $email->mailType = "html";
        $email->SMTPCrypto = "ssl";

        $accountModel = new \App\Models\Account();
        $email = $accountModel->getAccountEmailById($accountId);

        if($email == null) {
            return false;
        }

        $to = $email;
        $subject = $title;
        $body = $message;

        $email->setTo($to);
        $email->setFrom($email->SMTPUser, $userName);
        $email->setSubject($subject);
        $email->setMessage($body);

        if ($email->send()) {
            return true;
        }

        return false;
    }

}