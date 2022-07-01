<?php

namespace App\Controllers;

use CodeIgniter\Controller;
use CodeIgniter\HTTP\CLIRequest;
use CodeIgniter\HTTP\IncomingRequest;
use CodeIgniter\HTTP\RequestInterface;
use CodeIgniter\HTTP\ResponseInterface;
use Psr\Log\LoggerInterface;

/**
 * Class BaseController
 *
 * BaseController provides a convenient place for loading components
 * and performing functions that are needed by all your controllers.
 * Extend this class in any new controllers:
 *     class Home extends BaseController
 *
 * For security be sure to declare any new methods as protected or private.
 */
class BaseController extends Controller
{
    /**
     * Instance of the main Request object.
     *
     * @var CLIRequest|IncomingRequest
     */
    protected $request;

    /**
     * An array of helpers to be loaded automatically upon
     * class instantiation. These helpers will be available
     * to all other controllers that extend BaseController.
     *
     * @var array
     */
    protected $helpers = [];

    /**
     * Email del usuario actual
     */
    protected $email;

    /**
     * Id de cliente del usuario actual
     */
    protected $clientId;

    public $scripts = [];

    /**
     * Libreria de documentacion modular
     */
    protected $documentationLibrary;

    /**
     * Constructor.
     */
    public function initController(RequestInterface $request, ResponseInterface $response, LoggerInterface $logger)
    {
        // Do Not Edit This Line
        parent::initController($request, $response, $logger);
        // Preload any models, libraries, etc, here.
        $this->documentationLibrary = new \App\Libraries\Documentation();

        $this->email = session()->get('email');
        $this->clientId = session()->get('clientId');
    }

    public function view_base($html, $option, $subtitle) {
        $html = view('dashboard/base', [
            "html" => $html,
            "option" => $option,
            "title" => "Dashboard",
            "subtitle" => $subtitle,
            "email" => $this->email
        ]);
        
        $this->addScript(base_url('assets/js/menu.js'));

        return view('theme', [
            "html" => $html,
            "scripts" => $this->scripts,
            "currentUrl" => $this->documentationLibrary->getPath()
        ]);
    }

    public function theme_base($html) {
        return view('theme', [
            "html" => $html,
            "scripts" => $this->scripts,
            "currentUrl" => $this->documentationLibrary->getPath()
        ]);
    }

    public function addScript($script_url) {
        if($this->scripts == null)
            $this->scripts = [];
        if(in_array($script_url, $this->scripts))
            return;
        array_push($this->scripts, $script_url);
    }
}
