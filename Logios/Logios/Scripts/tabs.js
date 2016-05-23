$(document).ready(function () {
    var animation = $("#loadAnimation").html();

    //$("#exerciseAdmin").html(animation);
    //$("#exerciseAdmin").load("/UsersAdmin/Index");

    $("#tabUserAdmin").click(function () {
        $("#userAdmin").html(animation);
        $("#userAdmin").load("/UsersAdmin/Index");
    });

    //$("#galeriaDBLink").click(function () {
    //    $("#galeriaDB").html(animation);
    //    $("#galeriaDB").load("/Tab/Galeria");
    //});

    //$("#galeriaSVLink").click(function () {
    //    $("#galeriaSV").html(animation);
    //    $("#galeriaSV").load("/Tab/GaleriaSV");
    //});

    //$("#contactoLink").click(function () {
    //    $("#contacto").html(animation);
    //    $("#contacto").load("/Tab/Contacto");
    //});

    //$("#trailerLink").click(function () {
    //    $("#trailer").html(animation);
    //    $("#trailer").load("/Tab/Trailer");
    //});
});