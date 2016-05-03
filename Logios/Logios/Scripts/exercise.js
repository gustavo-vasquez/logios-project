var solutionEditor;

window.onload = function () {
    solutionEditor = com.wiris.jsEditor.JsEditor.newInstance({ 'language': 'es' });
    solutionEditor.insertInto(document.getElementById('solutionContainer'));
}

function ViewDevelopment() {
    if ($('#DevelopmentField').is(':hidden'))
        $('#DevelopmentField').slideDown(500);
    else
        $('#DevelopmentField').slideUp(500);
}

function copyAnswer() {
    $('#answer').val(solutionEditor.getMathML());
}