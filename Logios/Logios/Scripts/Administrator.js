var problemEditor;
var developmentEditor;
var solutionEditor;

$(document).ready(function () {
    problemEditor = com.wiris.jsEditor.JsEditor.newInstance({ 'language': 'es' });
    problemEditor.insertInto(document.getElementById('problemContainer'));

    developmentEditor = com.wiris.jsEditor.JsEditor.newInstance({ 'language': 'es' });
    developmentEditor.insertInto(document.getElementById('developmentContainer'));

    solutionEditor = com.wiris.jsEditor.JsEditor.newInstance({ 'language': 'es' });
    solutionEditor.insertInto(document.getElementById('solutionContainer'));

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

//function isEmptyAnyField() {
//    return ($('#Exercise_Problem').val() == "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"/>" || $('#Exercise_Development').val() == "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"/>" || $('#Exercise_Solution').val() == "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"/>");
//}