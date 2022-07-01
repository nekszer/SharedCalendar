<?php

namespace App\Database\Migrations;

use CodeIgniter\Database\Migration;

class Log extends Migration
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
            'primaryValue' => [
                'type' => 'VARCHAR',
                'constraint' => '255'
            ],
            'json'       => [
                'type'       => 'TEXT'
            ],
            'createAt' => [
                'type' => 'DATETIME',
                'on create' => 'CURRENT_TIMESTAMP'
            ],
            'action' => [
                'type' => 'VARCHAR',
                'constraint' => '250'
            ],
            'table' => [
                'type' => 'VARCHAR',
                'constraint' => '250'
            ],
            'primarykey' => [
                'type' => 'VARCHAR',
                'constraint' => '250'
            ],
            'id' => [
                'type' => 'VARCHAR',
                'constraint' => '250'
            ],
            'iduser' => [
                'type' => 'VARCHAR',
                'constraint' => '250'
            ]
        ]);
        $this->forge->addKey('id', true);
        $this->forge->createTable('log');
    }

    public function down()
    {
        $this->forge->dropTable('log');
    }
}
