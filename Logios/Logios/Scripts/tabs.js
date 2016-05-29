$(document).ready(function () {
    var animation = $("#loadAnimation").html();

    //$("#exerciseAdmin").html(animation);
    //$("#exerciseAdmin").load("/UsersAdmin/ExerciseTab");

    $("#tabTopicAdmin").click(function () {
        $("#topicAdmin").html(animation);
        $("#topicAdmin").load("/Administrator/Topics");
    });

    $("#tabUserAdmin").click(function () {
        $("#userAdmin").html(animation);
        $("#userAdmin").load("/UsersAdmin/UserTab");
    });    
});

function deleteUser(userId) {

    $.ajax({
        url: "/UsersAdmin/Delete/" + userId,
        type: 'GET',
        error: function (response) {
            alert("Error al procesar solicitud.");
        },
        success: function (userData) {            
            if (confirm("¿Realmente desea eliminar al usuario " + userData.UserName + "?\n\n NOTA: Esto puede deshacerse más adelante desde la base de datos.")) {
                $.ajax({
                    url: "/UsersAdmin/Delete/",
                    data: { id: userData.Id, __RequestVerificationToken: gettoken() },
                    type: 'POST',
                    error: function (response) {
                        alert("Error al procesar solicitud.");
                    },
                    success: function (response) {
                        window.location.href = response.Url;
                    }
                })
            }
        }    
    })
}