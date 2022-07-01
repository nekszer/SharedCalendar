<?php

namespace App\Controllers;

use App\Libraries\GroceryCrud;

class Home extends BaseController
{
    public function index()
    {
        $html = view('login');
        return view('theme', [
            "html" => $html,
            "currentUrl" => $this->documentationLibrary->getPath()
        ]);
    }

    public function recovery () {
        $html = view('recovery');
        return view('theme', [
            "html" => $html
        ]);
    }

    public function login() {
        $accountModel = new \App\Models\Account();
        $email = $this->request->getVar('email');
        $password = $this->request->getVar('password');
        $route = $accountModel->signIn($email, $password, function ($userData) {
            session()->set($userData);
        }, function () {
            session()->setFlashdata('msg', 'No reconocemos tus datos...');
        });
        return redirect()->to($route);
    }

    public function sendpasswordrecovery() {
        $accountModel = new \App\Models\Account();
        $email = $this->request->getVar('email');
        $status = $accountModel->recoveryPassword($email);
        if($status){
            session()->setFlashdata('msg', 'Se ha enviado el correo de recuperación');
            session()->setFlashdata('alerttype', 'success');
        }else{
            session()->setFlashdata('msg', 'No se ha enviado el correo de recuperación');
            session()->setFlashdata('alerttype', 'warning');
        }
        return redirect()->to('/recovery');
    }
    
    public function logout() {
        session()->destroy();
        return redirect()->to('/signin');
    }
}