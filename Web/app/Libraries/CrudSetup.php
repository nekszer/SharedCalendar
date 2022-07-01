<?php

namespace App\Libraries;

class CrudSetup
{

    protected $tableName;

    protected $columns;

    public function setTable($tableName)
    {
        $this->tableName = $tableName;
    }

    public function getColumns($tableName = null)
    {
        $db = \Config\Database::connect();
        if ($tableName == null)
            $tableName = $this->tableName;
        return $db->query("SHOW COLUMNS FROM $tableName")->getResult();
    }

    public function getTables()
    {
        $db = \Config\Database::connect();
        $tables = $db->query("SHOW TABLES")->getResultArray();
        $tablesInfo = array();
        foreach ($tables as $index => $array) {
            foreach ($array as $key => $value) {
                $columns = $this->getColumns($value);
                $containsPrimaryKey = false;
                foreach ($columns as $col) {
                    if ($col->Key == 'PRI') {
                        $containsPrimaryKey = true;
                    }
                }
                if ($containsPrimaryKey) {
                    array_push($tablesInfo, (object) [
                        "name" => $value,
                        "columns" => $columns
                    ]);
                }
            }
        }
        return $tablesInfo;
    }

    public function initTable()
    {

        $db = \Config\Database::connect();
        $table = $this->tableName;
        $this->columns = $this->getColumns($table);
        $result = $db->query("SELECT * FROM grocerycrud WHERE grocerycrud.table = '$table'")->getResult();
        foreach ($this->columns as $column) {
            $inTable = false;
            foreach ($result as $row) {
                if ($column->Field == $row->column) {
                    $inTable = true;
                    break;
                }
            }

            if (!$inTable) {
                $db->query("INSERT INTO grocerycrud (`table`, `column`) VALUES ('$table', '$column->Field')");
            }
        }

        $tables = $this->getTables();
        $data = $db->query("SELECT * FROM grocerycrud WHERE `table` = '$table'")->getResult();

        $dataTable = [
            "tablename" => $table,
            "tables" => $tables,
            "data" => $data
        ];

        return $dataTable;
    }

    public function setData($id, array $data, $join){
        $db = \Config\Database::connect();
        $sets = [];
        foreach ($data as $key => $value) {
            array_push($sets, " `$key` = '$value' ");
        }
        
        if($join != "-- Join --"){
            $joinExplode = explode("|", $join);
            $tableName = $joinExplode[0];
            $columnName = $joinExplode[1];

            $columnsOfTable = $this->getColumns($tableName);
            $primaryKey = "";
            foreach ($columnsOfTable as $columnOfTable) {
                if($columnOfTable->Key == "PRI") {
                    $primaryKey = $columnOfTable->Field;
                }
            }

            if(!empty($primaryKey)){
                array_push($sets, "`relationtablename` = '$tableName'");
                array_push($sets, "`relationcolumnname` = '$columnName'");
            }
        }

        $set = implode(",", $sets);
        if($db->query("UPDATE grocerycrud SET $set WHERE id = $id")){
            $data = $db->query("SELECT * FROM grocerycrud WHERE id = $id")->getRow();
            return $data;
        }
        return (object) [];
    }

    public function getConfigurationOfTable($table = null){
        $db = \Config\Database::connect();
        if(empty($table))
            $table = $this->tableName;
        $rows = $db->query("SELECT * FROM grocerycrud WHERE `table` = '$table' ORDER BY `order`")->getResult();
        return $rows;
    }

    public function setToCrud(GroceryCrud $crud, $table) : GroceryCrud
    {
        $rows = $this->getConfigurationOfTable($table);
        $selectFields = [];
        $addFields = [];
        $updateFields = [];
        $requiredFields = [];
        foreach ($rows as $row) {
            
            if ($row->listshow == "true"){
                array_push($selectFields, $row->column);
            }

            if ($row->createshow == "true"){
                array_push($addFields, $row->column);
            }

            if ($row->updateshow == "true"){
                array_push($updateFields, $row->column);
            }

            if($row->listshow || $row->createshow || $row->updateshow)
                $crud->displayAs($row->column, $row->displayas);

            if($row->createrequired && $row->updaterequired)
                array_push($requiredFields, $row->column);

            if(!empty($row->relationtablename) && !empty($row->relationcolumnname)){
                $crud->setRelation($row->column, $row->relationtablename, $row->relationcolumnname);
            }
        }

        $crud->columns($selectFields);
        $crud->addFields($addFields);
        $crud->editFields($updateFields);
        $crud->requiredFields($requiredFields);
        
        log_message('debug', json_encode($requiredFields));

        return $crud;
    }

    public function getColumnsSize ($tableName) {
        $db = \Config\Database::connect();
        $rows = $db->query("SELECT `column`, `size` FROM grocerycrud WHERE `table` = '$tableName'")->getResult();
        return $rows;
    }

    public function getLogs() {
        $db = \Config\Database::connect();   
        $logs = $db->query("SELECT l.*, u.* FROM log l
            INNER JOIN account u ON l.idUser = u.id
            WHERE l.`table` = '$this->tableName'")->getResult();
        $data = [
            "tablename" => $this->tableName,
            "data" => $logs
        ];
        return $data;
    }
}
