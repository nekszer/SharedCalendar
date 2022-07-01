<!DOCTYPE html>
<!--
Author: Keenthemes
Product Name: Metronic - Bootstrap 5 HTML, VueJS, React, Angular & Laravel Admin Dashboard Theme
Purchase: https://1.envato.market/EA4JP
Website: http://www.keenthemes.com
Contact: support@keenthemes.com
Follow: www.twitter.com/keenthemes
Dribbble: www.dribbble.com/keenthemes
Like: www.facebook.com/keenthemes
License: For each use you must have a valid license purchased only from above link in order to legally use the theme for your project.
-->
<html lang="es">
<!--begin::Head-->

<head>
    <title>Metronic - the world's #1 selling Bootstrap Admin Theme Ecosystem for HTML, Vue, React, Angular &amp; Laravel by Keenthemes</title>
    <meta charset="utf-8" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta property="og:locale" content="es_MX" />
    <meta property="og:type" content="article" />
    <meta property="og:title" content="CHL" />
    <meta property="og:url" content="<?= base_url() ?>" />
    <meta property="og:site_name" content="CHL" />
    <!--begin::Fonts-->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" />
    <!--end::Fonts-->
    <!--begin::Global Stylesheets Bundle(used by all pages)-->
    <link href="/metronic/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css" />
    <link href="/metronic/css/style.bundle.css" rel="stylesheet" type="text/css" />
    <link href="/metronic/css/page.css" rel="stylesheet" type="text/css" />
    <!--end::Global Stylesheets Bundle-->

    <!-- Time Plugin -->
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.css">
</head>
<!--end::Head-->
<!--begin::Body-->

<body>
    <div id="kt_body" class="header-fixed header-tablet-and-mobile-fixed toolbar-enabled toolbar-fixed aside-enabled aside-fixed" style="--kt-toolbar-height:55px;--kt-toolbar-height-tablet-and-mobile:55px">
        <!--begin::Main-->
        <?= $html ?>
        <!--end::Main-->
        <!--begin::Javascript-->
        <script>
            var hostUrl = "metronic/";
        </script>
        <!--begin::Global Javascript Bundle(used by all pages)-->
        <script src="/metronic/plugins/global/plugins.bundle.js"></script>
        <script src="/metronic/js/scripts.bundle.js"></script>
        <!--end::Global Javascript Bundle-->
        <!--begin::Page Vendors Javascript(used by this page)-->
        <script src="/metronic/plugins/custom/fullcalendar/fullcalendar.bundle.js"></script>
        <script src="/metronic/plugins/custom/datatables/datatables.bundle.js"></script>
        <!--end::Page Vendors Javascript-->
        <!--begin::Page Custom Javascript(used by this page)-->
        <script src="/metronic/js/widgets.bundle.js"></script>
        <script src="/metronic/js/custom/widgets.js"></script>
        <script src="/metronic/js/custom/apps/chat/chat.js"></script>
        <script src="/metronic/js/custom/utilities/modals/upgrade-plan.js"></script>
        <script src="/metronic/js/custom/utilities/modals/create-app.js"></script>
        <script src="/metronic/js/custom/utilities/modals/users-search.js"></script>
        <!--end::Page Custom Javascript-->
        <!--end::Javascript-->
        <!-- Time Plugin -->
        <script src="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.js"></script>

        <!-- Tinymce -->
        <script src="https://cdnjs.cloudflare.com/ajax/libs/tinymce/6.0.1/tinymce.min.js" integrity="sha512-WVGmm/5lH0QUFrXEtY8U9ypKFDqmJM3OIB9LlyMAoEOsq+xUs46jGkvSZXpQF7dlU24KRXDsUQhQVY+InRbncA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>

        <script>
            let currentUrl = "<?= $currentUrl ?>";
        </script>

        <script src="https://unpkg.com/dropzone@5/dist/min/dropzone.min.js"></script>
        <link rel="stylesheet" href="https://unpkg.com/dropzone@5/dist/min/dropzone.min.css" type="text/css" />

        <?php if (isset($scripts)) { ?>
            <?php foreach ($scripts as $script) { ?>
                <script src="<?= $script ?>"></script>
            <?php } ?>
        <?php } ?>
    </div>
</body>

<!--end::Body-->

</html>