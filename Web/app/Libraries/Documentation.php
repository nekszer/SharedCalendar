<?php

namespace App\Libraries;

class Documentation
{
    
    public function get() {
        $currentUrl = $this->getPath();
        $db = \Config\Database::connect();
        $documentation = $db->query("SELECT html FROM documentation WHERE `path` = '$currentUrl'")->getRow();
        if($documentation != null ){
            return $documentation->html;
        }
        return "";
    }

    public function getPath () {
        $router = service('router');
        $controller  = $router->controllerName();
        $controller = strtolower(str_replace('\\App\\Controllers\\', '', $controller));
        $method = $router->methodName();
        $currentUrl = "$controller/$method";
        return $currentUrl;
    }

}
