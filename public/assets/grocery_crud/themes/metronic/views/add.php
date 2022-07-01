<?php
    $this->set_css($this->default_theme_path.'/metronic/css/elusive-icons/css/elusive-icons.min.css');
    $this->set_css($this->default_theme_path.'/metronic/css/common.css');
    $this->set_css($this->default_theme_path.'/metronic/css/general.css');
    $this->set_css($this->default_theme_path.'/metronic/css/add-edit-form.css');
    $this->set_css($this->default_theme_path.'/metronic/css/main.css');

    $jquery_js = isset($jquery_js) ? $jquery_js : grocery_CRUD::JQUERY;

    if ($this->config->environment == 'production') {
        $this->set_js_lib($this->default_javascript_path . '/' . $jquery_js);
        $this->set_js_lib($this->default_theme_path.'/metronic/build/js/global-libs.min.js');
        $this->set_js_config($this->default_theme_path.'/metronic/js/form/add.min.js');
    } else {
        $this->set_js_lib($this->default_javascript_path . '/' . $jquery_js);
        $this->set_js_lib($this->default_theme_path.'/metronic/js/jquery-plugins/jquery.form.min.js');
        $this->set_js_lib($this->default_theme_path.'/metronic/js/common/common.min.js');
        $this->set_js_config($this->default_theme_path.'/metronic/js/form/add.js');
    }


include(__DIR__ . '/common_javascript_vars.php');
?>
<div class="crud-form" data-unique-hash="<?php echo $unique_hash; ?>">
    <div class="gc-container">
        <div class="row">
            <div class="col-md-12" style="padding: 0px; padding-right: 5px; padding-left: 5px;">
                <div class="floatL l5">
                    <span class="text-muted mt-1 fw-bold fs-7"><?php echo $this->l('form_add'); ?> <?php echo $subject?></span>
                </div>
                <div class="clear"></div>
                <div class="form-container">
                    <?php echo form_open( $insert_url, 'method="post" id="crudForm"  enctype="multipart/form-data""'); ?>
                    <div class="row">
                        <?php foreach($fields as $field) { ?>
                            <?php $size = "col-lg-6 col-12"; ?>
                            <?php foreach ($sizeOfColumns as $colSize) {
                                if($field->field_name == $colSize->column) {
                                    $size = $colSize->size . " col-12";
                                }
                            } ?>
                            <div class="form-floating <?php echo $field->field_name; ?>_form_group mb-7 <?= $size ?>">
                                <?php echo $input_fields[$field->field_name]->input; ?>
                                <label style="padding-left: 25px" id="label-<?= $field->field_name ?>" for="<?= $field->field_name ?>"><?php echo $input_fields[$field->field_name]->display_as?><?php echo ($input_fields[$field->field_name]->required)? "<span class='required'>*</span> " : ""?></label>
                            </div>
                        <?php }?>
                    </div>
                    
                    <!-- Start of hidden inputs -->
                    <?php
                    foreach ($hidden_fields as $hidden_field) {
                        echo $hidden_field->input;
                    }
                    ?>
                    <!-- End of hidden inputs -->
                    <?php if ($is_ajax) { ?><input type="hidden" name="is_ajax" value="true" /><?php }?>

                    <div class='small-loading hidden' id='FormLoading'><?php echo $this->l('form_insert_loading'); ?></div>

                    <div class="form-group gcrud-form-group">
                        <div id='report-error' class='report-div error bg-danger' style="display:none"></div>
                        <div id='report-success' class='report-div success bg-success' style="display:none"></div>
                    </div>
                    <br>
                    <div class="form-group gcrud-form-group">
                        <div class="col-offset-4 col-8 text-right btn-group">
                            <button class="btn btn-secondary btn-success b10" type="submit" id="form-button-save">
                                <i class="el el-ok"></i>
                                <?php echo $this->l('form_save'); ?>
                            </button>
                            <?php 	if(!$this->unset_back_to_list) { ?>
                                <button class="btn btn-info b10" type="button" id="save-and-go-back-button">
                                    <i class="el el-return-key"></i>
                                    <?php echo $this->l('form_save_and_go_back'); ?>
                                </button>
                                <button class="btn btn-secondary cancel-button b10" type="button" id="cancel-button">
                                    <i class="el el-warning-sign"></i>
                                    <?php echo $this->l('form_cancel'); ?>
                                </button>
                            <?php } ?>
                        </div>
                    </div>
                    <?php echo form_close(); ?>
                </div>
            </div>
        </div>
    </div>
</div>
<script>
    var validation_url = '<?php echo $validation_url?>';
    var list_url = '<?php echo $list_url?>';

    var message_alert_add_form = "<?php echo $this->l('alert_add_form')?>";
    var message_insert_error = "<?php echo $this->l('insert_error')?>";
</script>