$(document).ready(function () {
    var animation = $("#loadAnimation").html();

    $("#exerciseAdmin").html(animation);
    $("#exerciseAdmin").load("/Administrator/Exercises");

    $("#tabTopicAdmin").click(function () {
        $("#topicAdmin").html(animation);
        $("#topicAdmin").load("/Administrator/Topics");
    });

    $("#tabTopicAreaAdmin").click(function () {
        $("#topicAreaAdmin").html(animation);
        $("#topicAreaAdmin").load("/TopicArea/TopicAreas");
    });

    $("#tabUserAdmin").click(function () {
        $("#userAdmin").html(animation);
        $("#userAdmin").load("/UsersAdmin/UserTab");
    });

    $("#tabUserAdmin").click(function () {
        $("#userAdmin").html(animation);
        $("#userAdmin").load("/UsersAdmin/UserTab");
    });

    $("#tabRoleAdmin").click(function () {
        $("#roleAdmin").html(animation);
        $("#roleAdmin").load("/RolesAdmin/Roles");
    });

    $("#tabTrophyAdmin").click(function () {
        $("#trophyAdmin").html(animation);
        $("#trophyAdmin").load("/Trophies/TrophiesList");
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
                        if (response.Message != null) {
                            alert(response.Message);
                        }
                        else {
                            window.location.href = response.Url;
                        }
                    }
                })
            }
        }
    })
}


function deleteRole(roleId) {
    $.ajax({
        url: "/RolesAdmin/Delete/" + roleId,
        type: 'GET',
        error: function (response) {
            alert("Error al procesar solicitud.");
        },
        success: function (roleData) {
            if (confirm("¿Realmente desea eliminar el rol llamado " + roleData.Name + "?")) {
                $.ajax({
                    url: "/RolesAdmin/Delete/",
                    data: { id: roleData.Id, __RequestVerificationToken: gettoken() },
                    type: 'POST',
                    error: function (response) {
                        alert("Error al procesar solicitud.");
                    },
                    success: function (response) {
                        if (response.Message != null) {
                            alert(response.Message);
                        }
                        else {
                            window.location.href = response.Url;
                        }
                    }
                })
            }
        }
    })
}


function deleteArea(topicAreaId) {
    $.ajax({
        url: "/TopicArea/DeleteArea/" + topicAreaId,
        type: 'GET',
        error: function (response) {
            alert("Error al procesar solicitud.");
        },
        success: function (areaData) {
            if (confirm("¿Realmente desea eliminar la área temática llamada " + areaData.Description + "?\n\n NOTA: Esto puede deshacerse más adelante desde la base de datos.")) {
                $.ajax({
                    url: "/TopicArea/DeleteArea/",
                    data: { id: areaData.TopicAreaId, __RequestVerificationToken: gettoken() },
                    type: 'POST',
                    error: function (response) {
                        alert("Error al procesar solicitud.");
                    },
                    success: function (response) {
                        if (response.Message != null) {
                            alert(response.Message);
                        }
                        else {
                            window.location.href = response.Url;
                        }                        
                    }
                })
            }
        }
    })
}