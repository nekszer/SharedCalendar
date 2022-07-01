<style>
    .card-body {
        padding-top: 0px !important;
        padding-bottom: 0px !important;
    }
</style>
<?php foreach ($css_files as $file) : ?>
    <link type="text/css" rel="stylesheet" href="<?php echo $file; ?>" />
<?php endforeach; ?>

<div class="col-12">
    <!--begin::Tables Widget 11-->

    <div class="card ">
        <div class="card-header card-header-stretch">
            <h3 class="card-title"><?= $title ?></h3>
            <div class="card-toolbar">
                <ul class="nav nav-tabs nav-line-tabs nav-stretch fs-6 border-0">
                    <li class="nav-item">
                        <a class="nav-link active" data-bs-toggle="tab" href="#crud"><i class="bi bi-table"></i>&nbsp;&nbsp;CRUD</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" data-bs-toggle="tab" href="#columns"><i class="bi bi-gear"></i>&nbsp;&nbsp;Columns</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" data-bs-toggle="tab" href="#logs"><i class="bi bi-server"></i>&nbsp;&nbsp;Logs</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" data-bs-toggle="tab" href="#help"><i class="bi bi-info-circle-fill"></i>&nbsp;&nbsp;Ayuda</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" data-bs-toggle="tab" href="#docs"><i class="bi bi-book"></i>&nbsp;&nbsp;Generar documentación</a>
                    </li>
                </ul>
            </div>
        </div>
        <div class="card-body">
            <br>
            <div class="tab-content" id="myTabContent">
                <div class="tab-pane fade show active" id="crud" role="tabpanel">
                    <?php echo $output; ?>
                </div>
                <div class="tab-pane fade" id="columns" role="tabpanel">
                    <?= $groceryCrudView; ?>
                </div>
                <div class="tab-pane fade" id="logs" role="tabpanel">
                    <?= $logView; ?>
                </div>
                <div class="tab-pane fade" id="help" role="tabpanel">
                    <div id="help-content">
                        <?= $documentation ?>
                    </div>
                    <br>
                    <br>
                </div>
                <div class="tab-pane fade" id="docs" role="tabpanel">
                    <textarea id="kt_docs_tinymce_plugins" name="kt_docs_tinymce_plugins" class="tox-target">
                        <?= $documentation ?>
                    </textarea>
                    <br>
                    <button class="btn btn-primary" id="btnSaveDocs">Guardar</button>
                    <br><br><br>
                </div>
            </div>
        </div>
    </div>
</div>
<!--end::Tables Widget 11-->
<?php foreach ($js_files as $file) : ?>
    <script src="<?php echo $file; ?>"></script>
<?php endforeach; ?>

<br><br><br>