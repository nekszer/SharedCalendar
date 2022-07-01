<?php

namespace App\Database\Migrations;

use CodeIgniter\Database\Migration;

class GroceryConfig extends Migration
{
    public function up()
    {
        $this->forge->addField([
            'id'          => [
                'type'           => 'INT',
                'constraint'     => 5,
                'unsigned'       => true,
                'auto_increment' => true,
            ],
            'table'       => [
                'type'       => 'VARCHAR',
                'constraint' => '100',
            ],
            'column' => [
                'type' => 'VARCHAR',
                'constraint' => '250',
            ],
            'displayas' => [
                'type' => 'VARCHAR',
                'constraint' => '250',
            ],
            'size' => [
                'type' => 'VARCHAR',
                'constraint' => '250',
            ],
            'listshow' => [
                'type' => 'VARCHAR',
                'constraint' => '250',
            ],
            'createshow' => [
                'type' => 'VARCHAR',
                'constraint' => '250',
            ],
            'updateshow' => [
                'type' => 'VARCHAR',
                'constraint' => '250',
            ],
            'createrequired' => [
                'type' => 'VARCHAR',
                'constraint' => '250',
            ],
            'updaterequired' => [
                'type' => 'VARCHAR',
                'constraint' => '250',
            ],
            'relationtablename' => [
                'type' => 'VARCHAR',
                'constraint' => '250',
            ],
            'relationtablecolumnname' => [
                'type' => 'VARCHAR',
                'constraint' => '250',
            ],
            'order' => [
                'type' => 'VARCHAR',
                'constraint' => '10',
            ]
        ]);
        $this->forge->addKey('id', true);
        $this->forge->createTable('grocerycrud');
    }

    public function down()
    {
        //
    }
}
