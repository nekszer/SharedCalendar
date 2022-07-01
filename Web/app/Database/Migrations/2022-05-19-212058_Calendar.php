<?php

namespace App\Database\Migrations;

use CodeIgniter\Database\Migration;

class Calendar extends Migration
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
            'name'       => [
                'type'       => 'VARCHAR',
                'constraint' => '100',
            ],
            'description' => [
                'type'       => 'VARCHAR',
                'constraint' => '255',
            ],
            'picture' => [
                'type' => 'VARCHAR',
                'constraint' => '255',
            ],
            'accountId' => [
                'type' => 'INT',
                'constraint'     => 5,
                'unsigned'       => true,
            ]
        ]);
        $this->forge->addKey('id', true);
        $this->forge->addForeignKey('accountId', 'account', 'id');
        $this->forge->createTable('calendar');
    }

    public function down()
    {
        //
    }
}
