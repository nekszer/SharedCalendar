<?php

namespace App\Models;

use CodeIgniter\Model;

class Token extends Model
{
    protected $DBGroup          = 'default';
    protected $table            = 'token';
    protected $primaryKey       = 'id';
    protected $useAutoIncrement = true;
    protected $insertID         = 0;
    protected $returnType       = 'array';
    protected $useSoftDeletes   = false;
    protected $protectFields    = false;
    protected $allowedFields    = [];

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
     * Realiza autorizacion via token para el usuario
     */
    public function makeAuthorization($email) {
        $account = (object) $this->db->query("SELECT * FROM account WHERE email = '$email'")->getRow();
        $accountId = $account->id;
        $rand_token = openssl_random_pseudo_bytes(16);
        $token = bin2hex($rand_token);
        $datetime = date('Y-m-d H:i:s', strtotime("+30 day"));
        $insertId = $this->insert([
            "accountId" => $accountId,
            "token" => $token,
            "expires" => $datetime
        ]);

        if($insertId > 0)
            return $token;

        return "";
    }

    /**
     * Permite saber si el token es valido y la sesion no ha expirado
     */
    public function isValidToken($token) : bool {
        $tokenData = $this->where('token', $token)->findAll()[0];
        $date = strtotime($tokenData["expires"]);
        if(time() < $date) {
            return true;
        }
        return false;
    }
}
