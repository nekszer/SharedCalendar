<?php

namespace App\Notifications;
use Exception;

class NotificationManager
{
    private $handlers = [];

    /**
     * Permite inicializar el objeto de notificaciones
     */
    public function __construct()
    {
        array_push($this->handlers, new EmailNotification());
    }

    /**
     * Permite notificar a un usuario por diferentes vias de notificacion pre programadas
     */
    public function notify($accountId, $title, $message) : bool {
        $status = false;
        foreach ($this->handlers as $handler) {
            try{
                $result = $handler->notify($accountId, $title, $message);
                if(!$result)
                    continue;
                $status = $result;
            }catch(Exception $ex){
                
            }
        }
        return $status;
    }

}

/**
 * Interface para realizar las implementaciones especificas de un notificador
 */
interface INotification {
    public function notify($accountId, $title, $message) : bool;
}