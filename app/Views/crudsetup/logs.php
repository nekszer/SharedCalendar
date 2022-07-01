<h3><i class="bi bi-gear"></i>&nbsp;&nbsp;<small><?= $tablename; ?></small></h3>
<br>
<div class="table-responsive">
    <table id="logTable" class="table table-row-bordered table-striped gy-5">
        <thead>
            <tr class="fw-bold fs-6 text-muted">
                <th>Lllave primaria</th>
                <th>Id</th>
                <th>User</th>
                <th>Action</th>
                <th>Fecha</th>
                <th>Data</th>
            </tr>
        </thead>
        <tbody>
            <?php foreach ($data as $row) : ?>
                <tr>
                    <td><?= $row->primarykey; ?></td>
                    <td><?= $row->id; ?></td>
                    <td><?= empty($row->idUser) ? 'nothing' : "[$row->nameUserRol] - $row->fullnameUser"; ?></td>
                    <td><?= $row->action; ?></td>
                    <td><?= $row->createdAt; ?></td>
                    <td><?= $row->json; ?></td>
                </tr>
            <?php endforeach; ?>
        </tbody>
        <tfoot>
            <tr>
                <th>Lllave primaria</th>
                <th>Id</th>
                <th>User</th>
                <th>Action</th>
                <th>Fecha</th>
                <th>Data</th>
            </tr>
        </tfoot>
    </table>
</div>
<br>