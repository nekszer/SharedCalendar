<h3><i class="bi bi-gear"></i>&nbsp;&nbsp;<small><?= $tablename; ?></small></h3>
<br>
<div class="table-responsive">


    <table id="groceryTable" class="table table-row-bordered table-striped gy-5">
        <thead>
            <tr class="fw-bold fs-6 text-muted">
                <th>Nombre</th>
                <th>Mostrar como</th>
                <th>Posición</th>
                <th>Tamaño</th>
                <th>Visible al listar</th>
                <th>Visible al crear</th>
                <th>Visible al actualizar</th>
                <th>Requerido al crear</th>
                <th>Requerido al actualizar</th>
                <th colspan="3">Join</th>
                <th>Opciones</th>
            </tr>
        </thead>
        <tbody>
            <?php foreach ($data as $row) : ?>
                <form action="<?= base_url("crudsetup/save") ?>" method="post" class="crudsetupform">
                    <input type="text" name="table" value="<?= $tablename ?>" style="display: none;">
                    <input type="text" name="id" value="<?= $row->id ?>" style="display: none;" />
                    <tr>
                        <td>
                            <?= $row->column ?>
                            <input type="text" name="column" value="<?= $row->column ?>" style="display: none" />
                        </td>
                        <td>
                            <input type="text" name="displayAs" class="form-control input-maxlength" value="<?= $row->displayas ?>" maxlength="25" />
                        </td>
                        <td>
                            <input type="number" name="order" class="form-control input-maxlength" value="<?= $row->order ?>" maxlength="3"  />
                        </td>
                        <td>
                            <input type="text" name="size" class="form-control input-maxlength" value="<?= $row->size ?>" maxlength="100" />
                        </td>
                        <td>
                            <div class="form-check form-switch form-check-custom form-check-solid me-10">
                                <input class="form-check-input h-20px w-30px visibility_check" type="checkbox" name="visibilitySelect" value="<?= $row->listshow ?>" <?= $row->listshow == "true" ? 'checked' : '' ?> id="visibility_check_<?= $row->column ?>" />
                                <label class="form-check-label" for="flexSwitch20x30"></label>
                            </div>
                        </td>
                        <td>
                            <div class="form-check form-switch form-check-custom form-check-solid me-10">
                                <input class="form-check-input h-20px w-30px visibility_check" type="checkbox" name="visibilityAdd" value="<?= $row->createshow ?>" <?= $row->createshow == "true" ? 'checked' : '' ?> id="visibility_check_<?= $row->column ?>" />
                                <label class="form-check-label" for="flexSwitch20x30"></label>
                            </div>
                        </td>
                        <td>
                            <div class="form-check form-switch form-check-custom form-check-solid me-10">
                                <input class="form-check-input h-20px w-30px visibility_check" type="checkbox" name="visibilityUpdate" value="<?= $row->updateshow ?>" <?= $row->updateshow == "true" ? 'checked' : '' ?> id="visibility_check_<?= $row->column ?>" />
                                <label class="form-check-label" for="flexSwitch20x30"></label>
                            </div>
                        </td>
                        <td>
                            <div class="form-check form-switch form-check-custom form-check-solid me-10">
                                <input class="form-check-input h-20px w-30px required_check" type="checkbox" name="requiredAdd" value="<?= $row->createrequired ?>" <?= $row->createrequired == "true" ? 'checked' : '' ?> id="required_check_<?= $row->column ?>" />
                                <label class="form-check-label" for="flexSwitch20x30"></label>
                            </div>
                        </td>
                        <td>
                            <div class="form-check form-switch form-check-custom form-check-solid me-10">
                                <input class="form-check-input h-20px w-30px required_check" type="checkbox" name="requiredUpdate" value="<?= $row->updaterequired ?>" <?= $row->updaterequired == "true" ? 'checked' : '' ?> id="required_check_<?= $row->column ?>" />
                                <label class="form-check-label" for="flexSwitch20x30"></label>
                            </div>
                        </td>
                        <td colspan="3" style="width: 300px;">
                            <select class="form-select" data-control="select2" name="join" data-placeholder="Llave primaria de la relación">
                                <option>-- Join --</option>
                                <?php foreach ($tables as $table) : ?>
                                    <optgroup label="<?= $table->name ?>">
                                        <?php foreach ($table->columns as $col) : ?>
                                            <?php if ($col->Key != 'PRI') : ?>
                                                <option value="<?= "$table->name|$col->Field" ?>" <?= $table->name == $row->relationtablename && $col->Field == $row->relationcolumnname ? 'selected' : '' ?>><?= $col->Field ?></option>
                                            <?php endif; ?>
                                        <?php endforeach; ?>
                                    </optgroup>
                                <?php endforeach; ?>
                            </select>
                        </td>
                        <td>
                            <button class="btn btn-primary">Guardar</button>
                        </td>
                    </tr>
                </form>
            <?php endforeach; ?>
        </tbody>
        <tfoot>
            <tr>
                <th>Nombre</th>
                <th>Mostrar como</th>
                <th>Posición</th>
                <th>Tamaño</th>
                <th>Visible al listar</th>
                <th>Visible al crear</th>
                <th>Visible al actualizar</th>
                <th>Requerido al crear</th>
                <th>Requerido al actualizar</th>
                <th colspan="3">Join</th>
            </tr>
        </tfoot>
    </table>
</div>
<br>
<button id="saveall" class="btn btn-primary">Guardar todo</button>
<br><br><br>