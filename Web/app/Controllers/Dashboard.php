<?php

namespace App\Controllers;

use App\Libraries\CrudSetup;
use App\Libraries\GroceryCrud;
use CodeIgniter\Files\File;

class Dashboard extends BaseController
{

    public function index()
    {
        $html = view('dashboard/index');
        return $this->view_base($html, "dashboard/index", "Inicio");
    }

    /**
     * Muestra el crud para el menu del administrador
     */
    public function users()
    {
        $title = "Usuarios";
        $crud = $this->createCrud('account');
        $html = $this->returnCrud($crud, $title);
        return $this->view_base($html, "dashboard/users", $title);
    }
    
    /**
     * Metodo en comun para generar un crud
     * @param $tableName
     * @return GroceryCrud
     */
    public function createCrud($tableName): GroceryCrud
    {
        $crud = new GroceryCrud();
        $crud->setTable($tableName);
        $crudsetup = new CrudSetup();
        $crud = $crudsetup->setToCrud($crud, $tableName);
        return $crud;
    }

    /**
     * Regresa la vista del crud con la configuracion de columnas y el log
     * @return string html
     */
    public function returnCrud($crud, $title, $toolbarButtons = array())
    {
        $crud->setToolbarButtons($toolbarButtons);
        $output = (array) $crud->render();
        $output["title"] = $title;

        $tableName = $crud->get_table();

        $this->addScript(base_url("assets/js/crudsetup.js"));

        $crudSetup = new \App\Libraries\CrudSetup();
        $crudSetup->setTable($tableName);

        $dataTable = $crudSetup->initTable();
        $output["groceryCrudView"] = view('crudsetup/grocerycrud', $dataTable);

        $logData = $crudSetup->getLogs();
        $output["logView"] = view('crudsetup/logs', $logData);

        $output["documentation"] = $this->documentationLibrary->get();

        $html = view('dashboard/crud', $output);
        return $html;
    }
}
