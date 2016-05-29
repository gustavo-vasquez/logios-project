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

function deleteTopic(topicId) {

    $.ajax({
        url: "/Administrator/DeleteTopic/" + topicId,
        type: 'GET',
        error: function (response) {
            alert("Error al procesar solicitud.");
        },
        success: function (topicData) {
            if (confirm("¿Realmente desea eliminar el tópico llamado " + topicData.Description + "?\n\n NOTA: Esto puede deshacerse más adelante desde la base de datos.")) {
                $.ajax({
                    url: "/Administrator/DeleteTopic/",
                    data: { id: topicData.TopicId, __RequestVerificationToken: gettoken() },
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