<?php namespace App\Entities;

class Event
{
    public $id;         
    public $name;      
    public $description;
    public $day;
    public $allDay;
    public $start;
    public $end;
    public $hasLocation;
    public $latitude;
    public $longitude;
    public $calendarId;
    public $accountId;

    
    public function __construct($calendarId, $accountId, $name, $description, $day, $allDay, $start = null, $end = null, $hasLocation = "N", $latitude = null, $longitude = null)
    {
        $this->name = $name;
        $this->description = $description;
        $this->day = $day;
        $this->allDay = $allDay;

        if(empty($start))
            $this->start = date('Y-m-d H:i:s');
        else
            $this->start = $start;

        if(empty($end))
            $this->end = date('Y-m-d H:i:s');
        else
            $this->end = $end;
            
        $this->hasLocation = $hasLocation;
        
        if(empty($latitude))
            $this->latitude = 0;
        else
            $this->latitude = $latitude;

        if(empty($longitude))
            $this->longitude = 0;
        else
            $this->longitude = $longitude;
        
        $this->calendarId = $calendarId;
        $this->accountId = $accountId;
    }
    

    public function __get($key)
    {
        if (property_exists($this, $key)) {
            return $this->$key;
        }
    }

    public function __set($key, $value)
    {
        if (property_exists($this, $key)) {
            $this->$key = $value;
        }
    }
}