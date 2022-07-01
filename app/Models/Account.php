<?php

namespace App\Models;

use Exception;
use CodeIgniter\Model;

class Account extends Model
{
    protected $DBGroup          = 'default';
    protected $table            = 'account';
    protected $primaryKey       = 'id';
    protected $useAutoIncrement = true;
    protected $insertID         = 0;
    protected $returnType       = '\App\Entities\Account';
    protected $useSoftDeletes   = false;
    protected $protectFields    = true;
    protected $allowedFields    = [
        'id','email','password'
    ];

    // Dates
    protected $useTimestamps = false;
    protected $dateFormat    = 'datetime';
    protected $createdField  = 'created_at';
    protected $updatedField  = 'updated_at';
    protected $deletedField  = 'deleted_at';

    // Validation
    protected $validationRules      = [];
    protected $validationMessages   = [];
    protected $skipValidation       = false;
    protected $cleanValidationRules = true;

    // Callbacks
    protected $allowCallbacks = true;
    protected $beforeInsert   = [];
    protected $afterInsert    = [];
    protected $beforeUpdate   = [];
    protected $afterUpdate    = [];
    protected $beforeFind     = [];
    protected $afterFind      = [];
    protected $beforeDelete   = [];
    protected $afterDelete    = [];

    /**
     * Realiza el singin
     * @return string path
     */
    public function signIn($email, $password, $response, $error) : string {
        $account = $this->where('email', $email)->first();
        $newCipherPass = md5($password);
        if($account != null && $account->password == $newCipherPass){
            $ses_data = (array) $account;
            $response($ses_data);
            return "/dashboard";
        }
        $error();
        return "/";
    }

    /**
     * realiza la recuperacion de la contraseña
     * @return bool
     */
    public function recoveryPassword ($email) : bool {
        $account = $this->where('email', $email)->first();
        if($account != null){
            // TODO: Generar logica para crear codigo de recuperacion de contraseña
            $notificationManager = new \App\Notifications\NotificationManager();
            $code = "123456";
            $status = $notificationManager->notify($account->id, "Shared Calendar - Recuperar cuenta", "Ingresa el siguiente código $code para restablecer tu contraseña");
            return $status;
        }
        return false;
    }

    /**
     * Devuelve el id de la cuenta a traves del email
     */
    public function getAccountIdByEmail($email) {
        try{
            return $this->db->query("SELECT id FROM $this->table WHERE email = '$email'")->getRowObject()->id;
        }catch (Exception $e){ 
            return null;
        }
    }

    /**
     * Devuelve el email a traves del id de cuenta
     */
    public function getAccountEmailById ($accountId) {
        try{
            return $this->db->query("SELECT email FROM $this->table WHERE accountId = $accountId")->getRowObject()->email;
        }catch (Exception $ex){
            return null;
        }
    }
}
