<?php namespace App\Controllers;

use CodeIgniter\RESTful\ResourceController;
use CodeIgniter\API\ResponseTrait;

class Api extends ResourceController
{
    use ResponseTrait;

    public function signin()
    {
        $accountModel = new \App\Models\Account();
        $email = $this->request->getVar('email');
        $password = $this->request->getVar('password');
        $status = $accountModel->signIn($email, $password, function ($userData) {  }, function () { });
        if($status) {
            $tokenModel = new \App\Models\Token();
            $token = $tokenModel->makeAuthorization($email);
            if(!empty($token))
                return $this->respond([
                    "token" => $token
                ]);
        }
        return $this->failUnauthorized();
    }

    public function signup() {
        $email = $this->request->getVar('email');
        $password = $this->request->getVar('password');
        $accountModel = new \App\Models\Account();
        $message = $accountModel->signUp($email, $password);
        if(empty($message)) {
            $tokenModel = new \App\Models\Token();
            $token = $tokenModel->makeAuthorization($email);
            if(!empty($token))
                return $this->respond([
                    "token" => $token
                ]);
        }
        return $this->failServerError($message);
    }

    private function hasAuthorization($headers) {
        $token = "";
        foreach ($headers as $key => $value) {
            if($key == 'X-Token'){
                $token = str_replace('X-Token: ', '', $value);
                break;
            }
        }

        $tokenModel = new \App\Models\Token();
        $isValidToken = $tokenModel->isValidToken($token);

        if($isValidToken){
            $tokenData = $tokenModel->where('token', $token)->findAll()[0];
            $accountId = $tokenData["accountId"];
            return $accountId;
        }

        return 0;
    }

    public function calendar ($id = null) 
    {
        $headers = $this->request->headers();
        $accountId = $this->hasAuthorization($headers);

        if($accountId < 1){
            return $this->failUnauthorized();
        }

        $calendarModel = new \App\Models\Calendar();

        $method = $this->request->getMethod();
        switch ($method) {
            case 'post':
                $name = $this->request->getGetPost('name');
                $description = $this->request->getGetPost('description');
                // $picture = $this->request->getGetPost('picture');
                $result = $calendarModel->addCalendar($name, $description, "", $accountId);
                if($result["status"])
                    return $this->respondCreated($result["data"]);
                return $this->failServerError($result["message"]);
            
            case 'get':
                $calendars = $calendarModel->getMyCalendars($accountId);
                return $this->respond($calendars);

            case 'put':
                $name = $this->request->getGetPost('name');
                $description = $this->request->getGetPost('description');
                // $picture = $this->request->getGetPost('picture');
                $result = $calendarModel->updateCalendar($id, $name, $description, "", $accountId);
                if($result["status"])
                    return $this->respondCreated($result["message"]);
                return $this->failServerError($result["message"]);
        }
    }

    public function calendarmembers ($id = null) {
        $headers = $this->request->headers();
        $accountId = $this->hasAuthorization($headers);

        if($accountId < 1){
            return $this->failUnauthorized();
        }

        $calendarMember = new \App\Models\Calendarmember();

        $method = $this->request->getMethod();
        switch ($method) {
            case 'get':
                $invitations = $calendarMember->getInvitations($accountId);
                return $this->respond($invitations);

            case 'post':
                $email = $this->request->getGetPost('email');
                $calendarId = $this->request->getGetPost('calendarId');
                $invitation = $calendarMember->sendInvitation($email, $calendarId);
                if($invitation)
                    return $this->respondNoContent();
                return $this->failServerError("Error al invitar a la persona al calendario");

            case 'put':
                $status = $calendarMember->changeInvitation($id, 'accepted');
                if($status)
                    return $this->respondUpdated();
                return $this->failServerError("No se puede aceptar la invitación al calendario");

            case 'patch':
                $status = $calendarMember->changeInvitation($id, 'declined');
                if($status)
                    return $this->respondUpdated();
                return $this->failServerError("No se puede rechazar la invitación al calendario");
        }
    }

    public function event() {
        $headers = $this->request->headers();
        $accountId = $this->hasAuthorization($headers);

        if($accountId < 1){
            return $this->failUnauthorized();
        }

        $method = $this->request->getMethod();
        $calendarId = $this->request->getGetPost('calendarId');

        $eventModel = new \App\Models\Events();

        switch ($method) {
            case 'get':
                $events = $eventModel->getEventsOfCalendar($calendarId, $accountId);
                return $this->respond($events);
                
            case 'post':
                $name = $this->request->getGetPost('name');
                $description = $this->request->getGetPost('description');
                $day = $this->request->getGetPost('day');
                $allDay = $this->request->getGetPost('allDay');
                $start = $this->request->getGetPost('start');
                $end = $this->request->getGetPost('end');
                $hasLocation = $this->request->getGetPost('hasLocation');
                $latitude = $this->request->getGetPost('latitude');
                $longitude = $this->request->getGetPost('longitude');
                $addStatus = $eventModel->addEvent(new \App\Entities\Event($calendarId, $accountId, $name, $description, $day, $allDay, $start, $end, $hasLocation, $latitude, $longitude));
                if($addStatus)
                    return $this->respondCreated();
                return $this->failServerError("No podemos agregar el evento al calendario");

            case 'put':
                // TODO: Actualizar evento del calendario
                break;
        }
    }
}