$(document).ready(function () {
    $("#btnRemove").hide();

    $('.trJogador').click(function (e) {
        var trClicked = $(e.target).parent();
        $("#nome").focus();

        $(".jogadorId").val(trClicked.attr('id'));
        $("#btnRemove").val(trClicked.attr('id'));
        $("#nome").val(trClicked.attr('nome'));
        $("#nivel").val(trClicked.attr('nivel'));
        $("#isGoleiro").val(trClicked.attr('isGoleiro'));
        $("#isConfirmado").val(trClicked.attr('isConfirmado'));
        $("#observacao").val(trClicked.attr('obs'));

        $("#btnAddOrEdit").html("Editar <i class='fas fa-pencil ms-1'></i>");
        $("#btnAddOrEdit").removeClass("btn-success");
        $("#btnAddOrEdit").addClass("btn-warning");
        $("#btnRemove").show();
    });

    $("#cleanForm").click(() => {
        $("#frmAddOrEdit")[0].reset();
        $("#nome").focus();
        $("#btnAddOrEdit").html("Adicionar <i class='fas fa-plus ms-1'></i>");
        $("#btnAddOrEdit").removeClass("btn-warning");
        $("#btnAddOrEdit").addClass("btn-success");
        $("#btnRemove").hide();
    })

    $("#btnRemove").click(() => {
        $.ajax({
            url: "/Jogador/Delete",
            type: "POST",
            data: { jogadorId: document.querySelector("#jogadorIdDelete").value },
            success: function (data) {
                window.location.reload();
            },
            error: function (data) {
                console.log(data);
            }
        });
    });
});