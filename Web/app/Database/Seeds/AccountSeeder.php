<?php

namespace App\Database\Seeds;

use CodeIgniter\Database\Seeder;

class AccountSeeder extends Seeder
{
    public function run()
    {
        $password = '123456';
        $cipherPass = md5($password);

        $data = [
            'email'     => 'admin@gmail.com',
            'password'  => $cipherPass
        ];

        $this->db->table('account')->insert($data);
    }
}
