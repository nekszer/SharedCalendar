<?php

namespace App\Models;

use CodeIgniter\Model;

class Calendarmember extends Model
{
    protected $DBGroup          = 'default';
    protected $table            = 'calendarmember';
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
     * Invita a un usuario al calendario
     */
    public function sendInvitation($email, $calendarId) : array {
        $accountModel = new Account();
        $accountId = $accountModel->getAccountIdByEmail($email);
        if($accountId < 1){
            return [
                "status" => false,
                "message" => "No encontramos la cuenta del email"
            ];
        }

        $countInvitationForCalendar = $this->db->query("SELECT * FROM $this->table WHERE calendarId = $calendarId AND accountId = $accountId")->getNumRows();
        if($countInvitationForCalendar > 0){
            return [
                "status" => true,
                "message" => "Ya se ha invitado al usuario a este calendario"
            ];
        }

        $notificationManager = new \App\Notifications\NotificationManager();
        $notificationManager->notify($accountId, "Shared Calendar - Invitación", "Te han invitado a un calendario, accede a tu aplicación para aceptar la invitación");

        $insertId = $this->insert([
            "accountId" => $accountId,
            "calendarId" => $calendarId,
            "status" => "pending"
        ]);
        
        return $insertId > 0;
    }

    /**
     * Devuelve las invitaciones que tiene el calendario
     */
    public function getInvitations ($accountId) {
        $data = $this->db->query("SELECT id, calendarId, (SELECT name FROM calendar c WHERE c.id = cm.calendarId LIMIT 1) as calendar, status
            FROM calendarmember cm WHERE accountId = $accountId AND status = 'pending'")->getResult();
        return $data;
    }

    /**
     * Cambiar el tipo de invitacion
     */
    public function changeInvitation ($id, $status) {
        $invitation = $this->find($id);
        if($invitation == null) {
            return false;
        }
        if($invitation["status"] == 'pending'){
            return $this->update($id, [
                'status' => $status
            ]);
        }
        return false;
    }
}
