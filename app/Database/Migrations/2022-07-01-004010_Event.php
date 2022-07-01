<?php

namespace App\Database\Migrations;

use CodeIgniter\Database\Migration;

class Event extends Migration
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
                'type'       => 'TEXT'
            ],
            'day' => [
                'type' => 'DATE'
            ],
            'allDay' => [
                'type' => 'ENUM',
                'constraint'     => ['Y', 'N'],
                'default'        => 'Y'
            ],
            'start' => [
                'type' => 'TIME'
            ],
            'end' => [
                'type' => 'TIME'
            ],
            'hasLocation' => [
                'type' => 'ENUM',
                'constraint'     => ['Y', 'N'],
                'default'        => 'N'
            ],
            'latitude' => [
                'type' => 'float'
            ],
            'longitude' => [
                'type' => 'float'
            ],
            'calendarId' => [
                'type' => 'INT',
                'constraint'     => 5,
                'unsigned'       => true
            ],
            'accountId' => [
                'type' => 'INT',
                'constraint'     => 5,
                'unsigned'       => true
            ]
        ]);
        $this->forge->addKey('id', true);
        $this->forge->addForeignKey('calendarId', 'calendar', 'id');
        $this->forge->createTable('events');
    }

    public function down()
    {
        //
    }
}
