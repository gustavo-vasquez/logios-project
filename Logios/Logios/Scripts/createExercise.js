var problemEditor;
var developmentEditor;
var solutionEditor;

$(document).ready(function () {
    $('#CreateForm').removeData('validator').removeData('unobtrusiveValidation');
    $.validator.setDefaults({ ignore: [] });
    $.validator.unobtrusive.parse('#CreateForm');        

    $("#stepTwo").click(function () {
        scrollToAnchor('stepTwo');
    });

    $("#stepThree").click(function () {
        scrollToAnchor('stepThree');
    });

    $("#backToStepOne").click(function () {
        $('html,body').animate({ scrollTop: '0px' }, 'slow');
    });    

    problemEditor = com.wiris.jsEditor.JsEditor.newInstance({
        'language': 'es',
        'fontFamily': 'Times New Roman',
        'fontSize': '22px',
        'toolbar': '<toolbar ref="general" removeLinks="true"></toolbar>'
    });
    problemEditor.insertInto(document.getElementById('problemContainer'));

    developmentEditor = com.wiris.jsEditor.JsEditor.newInstance({
        'language': 'es',
        'fontFamily': 'Times New Roman',
        'fontSize': '22px',
        'toolbar': '<toolbar ref="general" removeLinks="true"></toolbar>'
    });
    developmentEditor.insertInto(document.getElementById('developmentContainer'));

    solutionEditor = com.wiris.jsEditor.JsEditor.newInstance({
        'language': 'es',
        'fontFamily': 'Times New Roman',
        'fontSize': '22px',
        'toolbar': '<toolbar ref="general" removeLinks="true"></toolbar>'
    });
    solutionEditor.insertInto(document.getElementById('solutionContainer'));    
});

function scrollToAnchor(idToGo) {
    var aTag = $("a[name='" + idToGo + "']");
    $('html,body').animate({ scrollTop: aTag.offset().top }, 'slow');
}

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