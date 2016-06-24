var problemEditor;
var developmentEditor;
var solutionEditor;

$(document).ready(function () {    
    $('#CreateForm').removeData('validator').removeData('unobtrusiveValidation');
    $.validator.setDefaults({ ignore: [] });
    $.validator.unobtrusive.parse('#CreateForm');            

    problemEditor = com.wiris.jsEditor.JsEditor.newInstance({
        'language': 'es',
        'fontFamily': 'Tahoma',
        'fontSize': '22px',        
        'toolbar': '<toolbar ref="general" removeLinks="true"></toolbar>',
        'autoformat': true
    });
    problemEditor.insertInto(document.getElementById('problemContainer'));

    developmentEditor = com.wiris.jsEditor.JsEditor.newInstance({
        'language': 'es',
        'fontFamily': 'Tahoma',        
        'fontSize': '22px',
        'toolbar': '<toolbar ref="general" removeLinks="true"></toolbar>',
        'autoformat': true
    });
    developmentEditor.insertInto(document.getElementById('developmentContainer'));

    solutionEditor = com.wiris.jsEditor.JsEditor.newInstance({
        'language': 'es',
        'fontFamily': 'Tahoma',        
        'fontSize': '22px',
        'toolbar': '<toolbar ref="general" removeLinks="true"></toolbar>',
        'autoformat': true
    });
    solutionEditor.insertInto(document.getElementById('solutionContainer'));

    //setTimeout(function () {
    //    $('.wrs_layoutFor3Rows tr td button[title="Itálica automática"]').click();
    //    $('.wrs_layoutFor3Rows tr td button[title="Itálica automática"]').css('display', 'none');
    //}, 10);
});

function isNotEmpty(data) {
    return (data != "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"/>");
}

function passData() {    
    $('#Exercise_Problem').val(problemEditor.getMathML());
    if ($('#Exercise_Problem').val() == "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"/>") {
        $('#Exercise_Problem').val(undefined);
    }        

    $('#Exercise_Development').val(developmentEditor.getMathML());
    if ($('#Exercise_Development').val() == "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"/>") {
        $('#Exercise_Development').val(undefined);
    }

    $('#Exercise_Solution').val(solutionEditor.getMathML());
    if ($('#Exercise_Solution').val() == "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"/>") {
        $('#Exercise_Solution').val(undefined);
    }
}