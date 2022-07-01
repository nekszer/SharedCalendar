tinymce.init({
    selector: '#kt_docs_tinymce_plugins',
});

$(document).ready(function() {
    $(".input-maxlength").maxlength({
        threshold: $(this).attr('maxlength'),
        warningClass: "badge badge-primary",
        limitReachedClass: "badge badge-success"
    });

    $(".visibility_check").change(function() {
        let value = $(this).prop("checked");
        $(this).val(value);
    });

    $(".required_check").change(function() {
        let value = $(this).prop("checked");
        $(this).val(value);
    });

    $(".crudsetupform").submit(function(event) {
        let data = $(this).serialize();
        $.post($(this).attr("action"), data).done(function(response) {
            console.log(response);
            Swal.fire({
                text: "Se han guardado los cambios",
                icon: "success",
                buttonsStyling: false,
                confirmButtonText: "Aceptar",
                customClass: {
                    confirmButton: "btn btn-primary"
                }
            });
        }).fail(function(response) {
            console.log(response);
            Swal.fire({
                text: "No podemos guardar tus cambios",
                icon: "warning",
                buttonsStyling: false,
                confirmButtonText: "Aceptar",
                customClass: {
                    confirmButton: "btn btn-info"
                }
            });
        });
        event.preventDefault();
    });

    $("#saveall").click(function() {
        $(".crudsetupform").each(function() {
            $(this).submit();
            setTimeout(() => {
                window.location.href = "";
            }, 2000);
        });
    });

    $("#logTable").DataTable();

    $("#btnSaveDocs").click(function() {
        let content = tinymce.activeEditor.getContent();
        $.post('/utils/savedocs', {
            html: content,
            path: currentUrl
        }).done(function(response) {
            Swal.fire({
                text: "Se ha guardado la documentación",
                icon: "success",
                buttonsStyling: false,
                confirmButtonText: "Aceptar",
                customClass: {
                    confirmButton: "btn btn-primary"
                }
            });
            $("#help-content").html(content);
        });
    });
});