$(document).ready(function () {
    var problemEditor = com.wiris.jsEditor.JsEditor.newInstance({ 'language': 'es' });
    problemEditor.insertInto(document.getElementById('problemContainer'));

    var developmentEditor = com.wiris.jsEditor.JsEditor.newInstance({ 'language': 'es' });
    developmentEditor.insertInto(document.getElementById('developmentContainer'));

    var solutionEditor = com.wiris.jsEditor.JsEditor.newInstance({ 'language': 'es' });
    solutionEditor.insertInto(document.getElementById('solutionContainer'));
});