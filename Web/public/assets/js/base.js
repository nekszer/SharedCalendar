$(document).ready(function() {
    $("#modal_institution").modal('show');
    $("#modal_institution").on('hidden.bs.modal', function() {
        window.location.href = "";
    });;
});