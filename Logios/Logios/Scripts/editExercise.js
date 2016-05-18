var problemEditor;
var developmentEditor;
var solutionEditor;
var dataModel;

function serializeModel(data) {
    dataModel = data;
}

$(document).ready(function () {    
    problemEditor = com.wiris.jsEditor.JsEditor.newInstance({ 'language': 'es' });
    problemEditor.insertInto(document.getElementById('problemContainer'));
    problemEditor.setMathML(dataModel.Problem);

    developmentEditor = com.wiris.jsEditor.JsEditor.newInstance({ 'language': 'es' });
    developmentEditor.insertInto(document.getElementById('developmentContainer'));
    developmentEditor.setMathML(dataModel.Development);

    solutionEditor = com.wiris.jsEditor.JsEditor.newInstance({ 'language': 'es' });
    solutionEditor.insertInto(document.getElementById('solutionContainer'));
    solutionEditor.setMathML(dataModel.Solution);    

    $("#stepTwo").click(function () {
        scrollToAnchor('stepTwo');
    });

    $("#stepThree").click(function () {
        scrollToAnchor('stepThree');
    });

    $("#backToStepOne").click(function () {
        $('html,body').animate({ scrollTop: '0px' }, 'slow');
    });
});

function scrollToAnchor(idToGo) {
    var aTag = $("a[name='" + idToGo + "']");
    $('html,body').animate({ scrollTop: aTag.offset().top }, 'slow');
}

function passData() {
    $('#Exercise_Problem').val(problemEditor.getMathML());
    $('#Exercise_Development').val(developmentEditor.getMathML());
    $('#Exercise_Solution').val(solutionEditor.getMathML());

    return true;
}