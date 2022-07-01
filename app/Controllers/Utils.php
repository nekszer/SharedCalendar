<?php

namespace App\Controllers;

use App\Controllers\BaseController;

class Utils extends BaseController
{
    public function savedocs() {
        $html = $this->request->getGetPost('html');
        $path = $this->request->getGetPost('path');
        $documentationModel = new \App\Models\Documentation();
        $status = $documentationModel->savedocs($path, $html);
        $this->response
                ->setStatusCode(200)
                ->setBody($status)
                ->send();
    }

    public function download($id) {
        $receipModel = new \App\Models\Receipt();
        $receipt = $receipModel->find($id);
        if($receipt == null){
            return $this->response->setStatusCode(404);
        }
        $path = $receipt["path"];
        $contents = file_get_contents(WRITEPATH. $path);
        if ($contents === false) {
            return $this->response->setStatusCode(404);
        }
        $this->response
            ->setStatusCode(200)
            ->setContentType(mime_content_type(WRITEPATH. $path))
            ->setBody($contents)
            ->send();
    }
}
