var problemEditor;
var developmentEditor;
var solutionEditor;
var dataModel;

function serializeModel(data) {
    dataModel = data;
}

$(document).ready(function () {
    $('#EditForm').removeData('validator').removeData('unobtrusiveValidation');
    $.validator.setDefaults({ ignore: [] });
    $.validator.unobtrusive.parse('#EditForm');

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
        'toolbar': '<toolbar ref="general" removeLinks="true"><removeTab ref="contextual" /></toolbar>'
    });
    problemEditor.insertInto(document.getElementById('problemContainer'));
    problemEditor.setMathML(dataModel.Problem);

    developmentEditor = com.wiris.jsEditor.JsEditor.newInstance({
        'language': 'es',
        'fontFamily': 'Times New Roman',
        'fontSize': '22px',
        'toolbar': '<toolbar ref="general" removeLinks="true"><removeTab ref="contextual" /></toolbar>'
    });
    developmentEditor.insertInto(document.getElementById('developmentContainer'));
    developmentEditor.setMathML(dataModel.Development);

    solutionEditor = com.wiris.jsEditor.JsEditor.newInstance({
        'language': 'es',
        'fontFamily': 'Times New Roman',
        'fontSize': '22px',
        'toolbar': '<toolbar ref="general" removeLinks="true"><removeTab ref="contextual" /></toolbar>'
    });
    solutionEditor.insertInto(document.getElementById('solutionContainer'));
    solutionEditor.setMathML(dataModel.Solution);        
});

function scrollToAnchor(idToGo) {
    var aTag = $("a[name='" + idToGo + "']");
    $('html,body').animate({ scrollTop: aTag.offset().top }, 'slow');
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