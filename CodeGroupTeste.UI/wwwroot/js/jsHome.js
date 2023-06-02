function ConfirmRemove(id, local) {

    var result = confirm(`Deseja realmente remover a partida em ${local}?`);
    if (result) {
        $.ajax({
            url: '/Partida/Delete',
            type: 'POST',
            data: { id: id },
            success: function (result) {
                if (result.success)
                    window.location.href = '/';
                else
                    alert(result.message);
            },
            error: function (result) {
                console.log(result);
            },
        })
    }
}