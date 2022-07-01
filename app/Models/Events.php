<?php

namespace App\Models;

use CodeIgniter\Model;
use App\Entities\Event;

class Events extends Model
{
    protected $DBGroup          = 'default';
    protected $table            = 'events';
    protected $primaryKey       = 'id';
    protected $useAutoIncrement = true;
    protected $insertID         = 0;
    protected $returnType       = '\App\Entities\Account';
    protected $useSoftDeletes   = false;
    protected $protectFields    = true;
    protected $allowedFields    = [
        'id','name','description','day','allDay','start','end','hasLocation','latitude','longitude','calendarId','accountId'
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
     * Devuelve los eventos del calendario solicitado
     */
    public function getEventsOfCalendar($calendarId, $accountId) : array {
        $sql = "SELECT 
            * 
        FROM $this->table 
        WHERE 
            calendarId IN (
                SELECT id 
                FROM calendar c 
                WHERE 
                    c.id = $calendarId 
                        AND accountId = $accountId 
                        OR id IN (
                            SELECT calendarId 
                            FROM calendarmember cm 
                                WHERE cm.accountId = $accountId
                                    AND status = 'accepted'
                        )
            )";
        log_message('error', $sql);
        return $this->db->query($sql)->getResultObject();
    }

    /**
     * Agrega un evento al calendario
     */
    public function addEvent(Event $event) : bool {
        $calendarId = $event->calendarId;
        $accountId = $event->accountId;
        $calendarModel = new Calendar();
        if($calendarModel->isOwner($calendarId, $accountId)){
            $id = $this->insert($event);
            return $id > 0;
        }
        return false;
    }
}