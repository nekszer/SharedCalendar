<?php

namespace App\Database\Migrations;

use CodeIgniter\Database\Migration;

class CalendarMember extends Migration
{
    public function up()
    {
        $this->forge->addField([
            'id'                => [
                'type'           => 'INT',
                'constraint'     => 5,
                'unsigned'       => true,
                'auto_increment' => true,
            ],
            'accountId'         => [
                'type'           => 'INT',
                'constraint'     => 5,
                'unsigned'       => true
            ],
            'calendarId'        => [
                'type'           => 'INT',
                'constraint'     => 5,
                'unsigned'       => true
            ],
            'status'            => [
                'type'           => 'ENUM',
                'constraint'     => ['pending', 'accepted', 'declined'],
                'default'        => 'pending'
            ]
        ]);
        $this->forge->addKey('id', true);
        $this->forge->addForeignKey('accountId', 'account', 'id');
        $this->forge->addForeignKey('calendarId', 'calendar', 'id');
        $this->forge->createTable('calendarmember');
    }

    public function down()
    {
        //
    }
}
