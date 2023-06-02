$(document).ready(function () {
    $('#tbDados').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/pt-BR.json',
        },
    });

    // get client height
    $('.container').css('min-height', $(window).height() - 30);
});