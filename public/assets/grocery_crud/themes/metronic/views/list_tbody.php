<?php
// Backwards compatibility for older Grocery CRUD versions
$unset_clone = isset($unset_clone) ? $unset_clone : true;

//Start counting the buttons that we have:
$buttons_counter = 0;

if (!$unset_edit) {
    $buttons_counter++;
}

if (!$unset_read) {
    $buttons_counter++;
}

if (!$unset_delete) {
    $buttons_counter++;
}

if (!$unset_clone) {
    $buttons_counter++;
}

if (!empty($list[0]) && !empty($list[0]->action_urls)) {
    $buttons_counter = $buttons_counter +  count($list[0]->action_urls);
}

$show_more_button  = $buttons_counter > 2 ? true : false;

$more_string = $this->l('list_more');
$clone_string = $this->l('list_clone');

?>

<?php foreach ($list as $num_row => $row) { ?>
    <tr>
        <td>
            <input type="checkbox" class="select-row" data-id="<?php echo $row->primary_key_value; ?>" />
        </td>
        <?php foreach ($columns as $column) { ?>
            <td>
                <?php echo $row->{$column->field_name} != '' ? $row->{$column->field_name} : '&nbsp;'; ?>
            </td>
        <?php } ?>
        <td <?php if ($unset_delete) { ?> style="border-left: none;" <?php } ?> <?php if ($buttons_counter === 0) { ?>class="hidden" <?php } ?>>
            <div style="white-space: nowrap">
                <?php if (!$unset_edit) { ?>
                    <a href="<?php echo $row->edit_url ?>" class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1" alt="<?php echo $this->l('list_edit'); ?>" title="<?php echo $this->l('list_edit'); ?>">
                        <!--begin::Svg Icon | path: icons/duotune/art/art005.svg-->
                        <span class="svg-icon svg-icon-3">
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
                                <path opacity="0.3" d="M21.4 8.35303L19.241 10.511L13.485 4.755L15.643 2.59595C16.0248 2.21423 16.5426 1.99988 17.0825 1.99988C17.6224 1.99988 18.1402 2.21423 18.522 2.59595L21.4 5.474C21.7817 5.85581 21.9962 6.37355 21.9962 6.91345C21.9962 7.45335 21.7817 7.97122 21.4 8.35303ZM3.68699 21.932L9.88699 19.865L4.13099 14.109L2.06399 20.309C1.98815 20.5354 1.97703 20.7787 2.03189 21.0111C2.08674 21.2436 2.2054 21.4561 2.37449 21.6248C2.54359 21.7934 2.75641 21.9115 2.989 21.9658C3.22158 22.0201 3.4647 22.0084 3.69099 21.932H3.68699Z" fill="black"></path>
                                <path d="M5.574 21.3L3.692 21.928C3.46591 22.0032 3.22334 22.0141 2.99144 21.9594C2.75954 21.9046 2.54744 21.7864 2.3789 21.6179C2.21036 21.4495 2.09202 21.2375 2.03711 21.0056C1.9822 20.7737 1.99289 20.5312 2.06799 20.3051L2.696 18.422L5.574 21.3ZM4.13499 14.105L9.891 19.861L19.245 10.507L13.489 4.75098L4.13499 14.105Z" fill="black"></path>
                            </svg>
                        </span>
                        <!--end::Svg Icon-->
                    </a>
                <?php } ?>
                <?php if (!$unset_delete) { ?>
                    <a data-target="<?php echo $row->delete_url?>" href="javascript:void(0)" title="<?php echo $this->l('list_delete')?>" class="delete-row btn btn-icon btn-bg-light btn-active-color-primary btn-sm">
                        <!--begin::Svg Icon | path: icons/duotune/general/gen027.svg-->
                        <span class="svg-icon svg-icon-3">
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
                                <path d="M5 9C5 8.44772 5.44772 8 6 8H18C18.5523 8 19 8.44772 19 9V18C19 19.6569 17.6569 21 16 21H8C6.34315 21 5 19.6569 5 18V9Z" fill="black"></path>
                                <path opacity="0.5" d="M5 5C5 4.44772 5.44772 4 6 4H18C18.5523 4 19 4.44772 19 5V5C19 5.55228 18.5523 6 18 6H6C5.44772 6 5 5.55228 5 5V5Z" fill="black"></path>
                                <path opacity="0.5" d="M9 4C9 3.44772 9.44772 3 10 3H14C14.5523 3 15 3.44772 15 4V4H9V4Z" fill="black"></path>
                            </svg>
                        </span>
                        <!--end::Svg Icon-->
                    </a>
                <?php } ?>
                <?php if(isset($row->action_urls) && count($row->action_urls) > 0): ?>
                <?php if (!empty($row->action_urls) || !$unset_read || !$unset_delete || !$unset_clone) { ?>
                    <?php if ($show_more_button) { ?>
                        <div class="btn-group">
                            <button type="button" class="btn btn-default btn-outline-dark gc-bootstrap-dropdown dropdown-toggle">
                                <?php echo $more_string; ?>
                                <span class="caret"></span>
                            </button>
                            <div class="dropdown-menu">
                                <?php
                                if (!empty($row->action_urls)) {
                                    foreach ($row->action_urls as $action_unique_id => $action_url) {
                                        $action = $actions[$action_unique_id];
                                        $new_tab = property_exists($action, 'new_tab') && $action->new_tab;
                                ?>
                                        <a href="<?php echo $action_url; ?>" class="dropdown-item" <?php if ($new_tab) { ?> target="_blank" <?php } ?>>
                                            <i class="fa <?php echo $action->css_class; ?>"></i> <?php echo $action->label ?>
                                        </a>
                                <?php }
                                }
                                ?>
                            </div>
                        </div>
                        <?php } else {
                        if (!empty($row->action_urls)) {
                            foreach ($row->action_urls as $action_unique_id => $action_url) {
                                $action = $actions[$action_unique_id];
                                $new_tab = property_exists($action, 'new_tab') && $action->new_tab;
                        ?>
                                <a href="<?php echo $action_url; ?>" class="btn btn-secondary" <?php if ($new_tab) { ?> target="_blank" <?php } ?>>
                                    <i class="fa <?php echo $action->css_class; ?>"></i> <?php echo $action->label ?>
                                </a>
                            <?php }
                        }

                        if (!$unset_read) { ?>
                            <a href="<?php echo $row->read_url ?>" class="btn btn-default btn-outline-dark">
                                <i class="el el-eye-open"></i> <?php echo $this->l('list_view') ?>
                            </a>
                        <?php } ?>
                    <?php } ?>

                <?php } ?>
                <?php endif; ?>
            </div>
        </td>
        
    </tr>
<?php } ?>