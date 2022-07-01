<?php

namespace App\Database\Migrations;

use CodeIgniter\Database\Migration;

class CalendarColor extends Migration
{
    public function up()
    {
        $fields = [
            'color'          => [
                'type'           => 'VARCHAR(7)'
            ]
        ];
        $this->forge->addColumn('calendar', $fields);
    }

    public function down()
    {
        //
    }
}
