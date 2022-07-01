<?php

namespace App\Models;

use CodeIgniter\Model;

class Documentation extends Model
{
    protected $DBGroup          = 'default';
    protected $table            = 'documentation';
    protected $primaryKey       = 'path';
    protected $useAutoIncrement = true;
    protected $insertID         = 0;
    protected $returnType       = 'array';
    protected $useSoftDeletes   = false;
    protected $protectFields    = true;
    protected $allowedFields    = [
        'path', 'html'
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
     * Funcion para guardar la documentacion del sitio web
     */
    public function savedocs($path, $html) : bool {
        $numRows = $this->db->query("SELECT * FROM $this->table WHERE `path` = '$path'")->getNumRows();
        if($numRows > 0){
            $update = $this->update($path, [
                'html' => $html
            ]);
            return $update;
        }
        $id = $this->insert([
            'path' => $path,
            'html' => $html
        ]);
        return $id > 0;        
    }
}
