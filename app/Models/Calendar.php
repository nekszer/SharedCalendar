<?php

namespace App\Models;

use CodeIgniter\Model;

class Calendar extends Model
{
    protected $DBGroup          = 'default';
    protected $table            = 'calendar';
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
     * Agrega un calendario
     */
    public function addCalendar($name, $description, $picture, $accountId) : array {
        $totalCalendars = $this->db->query("SELECT * FROM calendar WHERE accountId = $accountId")->getNumRows();
        if($totalCalendars < 1) {
            $calendar = [
                'name' => $name,
                'description' => $description,
                // 'picture' => $picture,
                'accountId' => $accountId
            ];
            $calendarId = $this->insert($calendar);
            $calendar["calendarId"];
            return [
                "status" => $calendarId > 0,
                "data" => $calendar
            ];
        }
        return [
            "status" => false,
            "message" => "Se han excedido el limite de calendarios..."
        ];
    }

    /**
     * Devuelve los calendarios de la cuenta solicitada
     */
    public function getMyCalendars($accountId) {
        $data = $this->db->query("SELECT id, name, description, accountId,
            IF(c.accountId = $accountId, 'Admin', 'User') as role
        FROM calendar c WHERE accountId = $accountId OR id IN(SELECT calendarId FROM calendarmember cm WHERE cm.accountId = $accountId AND status = 'accepted')")->getResult();
        return $data;
    }

    /**
     * Realiza la accion de actualizar un calendario validando que sea el dueño quien puede actualizar
     */
    public function updateCalendar ($id, $name, $description, $picture, $accountId) : array {

        $calendar = (object) $this->find($id);
        if($calendar == null) 
            return [
                "status" => false,
                "message" => "No encontramos el calendario..."
            ];
        
        if($calendar->accountId != $accountId) 
            return [
                "status" => false,
                "message" => "No eres dueño del calendario..."
            ];

        $status = $this->update($id, [
            "name" => $name,
            "description" => $description
        ]);

        return [
            "status" => $status,
            "message" => $status ? "Se ha actualizado el calendario" : "No podemos actualizar el calendario"
        ];
    }

    /**
     * Permite saber si el usuario es dueño del calendario en cuestion
     */
    public function isOwner ($calendarId, $accountId) : bool {
        $sql = "SELECT id, name, description, accountId,
            IF(c.accountId = $accountId, 'Admin', 'User') as role
        FROM calendar c WHERE c.id = $calendarId AND accountId = $accountId OR id IN(SELECT calendarId FROM calendarmember cm WHERE cm.accountId = $accountId AND status = 'accepted')";
        $rows = $this->db->query($sql)->getNumRows();
        return $rows > 0;
    }
}
