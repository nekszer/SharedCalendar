<?php namespace App\Controllers;

class Crudsetup extends BaseController
{

    public function save() {
        $id = $this->request->getPost("id");
        $displayAs = $this->request->getPost("displayAs");
        $order = $this->request->getPost("order");
        $visibilitySelect = $this->request->getPost("visibilitySelect");
        $visibilityAdd = $this->request->getPost("visibilityAdd");
        $visibilityUpdate = $this->request->getPost("visibilityUpdate");
        $requiredAdd = $this->request->getPost("requiredAdd");
        $requiredUpdate = $this->request->getPost("requiredUpdate");
        $join = $this->request->getPost("join");
        $size = $this->request->getPost("size");

        $data = [
            "displayAs" => $displayAs,
            "size" => $size,
            "order" => $order,
            "listshow" => $visibilitySelect ?? "false",
            "createshow" => $visibilityAdd ?? "false",
            "updateshow" => $visibilityUpdate ?? "false",
            "createrequired" => $requiredAdd ?? "false",
            "updaterequired" => $requiredUpdate ?? "false"
        ];
        
        $crudsetup = new \App\Libraries\CrudSetup();
        $data = $crudsetup->setData($id, $data, $join);    
        echo json_Encode($data, JSON_PRETTY_PRINT);
    }

}