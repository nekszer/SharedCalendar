<?php

namespace App\Database\Migrations;

use CodeIgniter\Database\Migration;

class Token extends Migration
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
            'token'       => [
                'type'       => 'VARCHAR',
                'constraint' => '250',
            ],
            'accountId' => [
                'type' => 'INT',
                'constraint' => 5,
                'unsigned' => true
            ],
            'expires' => [
                "type" => "DATETIME"
            ]
        ]);
        $this->forge->addKey('id', true);
        $this->forge->addForeignKey('accountId', 'account', 'id');
        $this->forge->createTable('token');
    }

    public function down()
    {
        $this->forge->dropTable('token');
    }
}
