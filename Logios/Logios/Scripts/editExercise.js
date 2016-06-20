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

    problemEditor = com.wiris.jsEditor.JsEditor.newInstance({
        'language': 'es',
        'fontFamily': 'Tahoma',
        'fontStyle': 'normal',
        'fontSize': '20px',
        'toolbar': '<toolbar ref="general" removeLinks="true"></toolbar>',
        'autoformat': true
    });
    problemEditor.insertInto(document.getElementById('problemContainer'));
    problemEditor.setMathML(dataModel.Problem);

    developmentEditor = com.wiris.jsEditor.JsEditor.newInstance({
        'language': 'es',
        'fontFamily': 'Tahoma',
        'fontStyle': 'normal',
        'fontSize': '20px',
        'toolbar': '<toolbar ref="general" removeLinks="true"></toolbar>',
        'autoformat': true
    });
    developmentEditor.insertInto(document.getElementById('developmentContainer'));
    developmentEditor.setMathML(dataModel.Development);

    solutionEditor = com.wiris.jsEditor.JsEditor.newInstance({
        'language': 'es',
        'fontFamily': 'Tahoma',
        'fontStyle': 'normal',
        'fontSize': '20px',
        'toolbar': '<toolbar ref="general" removeLinks="true"></toolbar>',
        'autoformat': true
    });
    solutionEditor.insertInto(document.getElementById('solutionContainer'));
    solutionEditor.setMathML(dataModel.Solution);        
});

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