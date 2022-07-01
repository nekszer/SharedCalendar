<?php

namespace App\Database\Migrations;

use CodeIgniter\Database\Migration;

class Documentation extends Migration
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
            'html'       => [
                'type'       => 'TEXT'
            ],
            'path' => [
                'type' => 'VARCHAR',
                'constraint' => '250'
            ],
            
        ]);
        $this->forge->addKey('id', true);
        $this->forge->createTable('documentation');
    }

    public function down()
    {
        //
    }
}
