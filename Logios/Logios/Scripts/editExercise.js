var problemEditor;
var developmentEditor;
var solutionEditor;
var dataModel;
var params = {
    'language': 'es',
    'fontFamily': 'Tahoma',
    'fontSize': '22px',
    'toolbar': '<toolbar ref="general" removeLinks="true"></toolbar>',
    'autoformat': true,
    'reservedWords': ''
};

function serializeModel(data) {
    dataModel = data;
}

$(document).ready(function () {
    $('#EditForm').removeData('validator').removeData('unobtrusiveValidation');
    $.validator.setDefaults({ ignore: [] });
    $.validator.unobtrusive.parse('#EditForm');    

    problemEditor = com.wiris.jsEditor.JsEditor.newInstance(params);
    problemEditor.insertInto(document.getElementById('problemContainer'));
    problemEditor.setMathML(dataModel.Problem);

    developmentEditor = com.wiris.jsEditor.JsEditor.newInstance(params);
    developmentEditor.insertInto(document.getElementById('developmentContainer'));
    developmentEditor.setMathML(dataModel.Development);

    solutionEditor = com.wiris.jsEditor.JsEditor.newInstance(params);
    solutionEditor.insertInto(document.getElementById('solutionContainer'));
    solutionEditor.setMathML(dataModel.Solution);

    setTimeout(function () {
        $('.wrs_layoutFor3Rows tr td button[title="Itálica automática"]').css('display', 'none');
    }, 10);

    $('#problemContainer input').focus(function () {
        if ($('#problemContainer .wrs_layoutFor3Rows tr td button[title="Itálica automática"]').hasClass('wrs_toggled')) {
            $('#problemContainer .wrs_layoutFor3Rows tr td button[title="Itálica automática"]').click();
        }
    });

    $('#problemContainer input').keypress(function () {
        if ($('#problemContainer .wrs_layoutFor3Rows tr td button[title="Itálica automática"]').hasClass('wrs_toggled')) {
            $('#problemContainer .wrs_layoutFor3Rows tr td button[title="Itálica automática"]').click();
        }
    });

    //$('#problemContainer input').keydown(function (event) {
    //    if (event.keyCode == 8 || event.keyCode == 46) {
    //        if (problemEditor.isFormulaEmpty() && $('#problemContainer .wrs_layoutFor3Rows tr td button[title="Itálica automática"]').hasClass('wrs_toggled')) {
    //            $('#problemContainer .wrs_layoutFor3Rows tr td button[title="Itálica automática"]').click();
    //        }
    //    }
    //});

    $('#developmentContainer input').focus(function () {
        if ($('#developmentContainer .wrs_layoutFor3Rows tr td button[title="Itálica automática"]').hasClass('wrs_toggled')) {
            $('#developmentContainer .wrs_layoutFor3Rows tr td button[title="Itálica automática"]').click();
        }
    });

    $('#developmentContainer input').keypress(function () {
        if ($('#developmentContainer .wrs_layoutFor3Rows tr td button[title="Itálica automática"]').hasClass('wrs_toggled')) {
            $('#developmentContainer .wrs_layoutFor3Rows tr td button[title="Itálica automática"]').click();
        }
    });

    //$('#developmentContainer input').keydown(function (event) {
    //    if (event.keyCode == 8 || event.keyCode == 46) {
    //        if (developmentEditor.isFormulaEmpty() && $('#developmentContainer .wrs_layoutFor3Rows tr td button[title="Itálica automática"]').hasClass('wrs_toggled')) {
    //            $('#developmentContainer .wrs_layoutFor3Rows tr td button[title="Itálica automática"]').click();
    //        }
    //    }
    //});

    $('#solutionContainer input').focus(function () {
        if ($('#solutionContainer .wrs_layoutFor3Rows tr td button[title="Itálica automática"]').hasClass('wrs_toggled')) {
            $('#solutionContainer .wrs_layoutFor3Rows tr td button[title="Itálica automática"]').click();
        }
    });

    $('#solutionContainer input').keypress(function () {
        if ($('#solutionContainer .wrs_layoutFor3Rows tr td button[title="Itálica automática"]').hasClass('wrs_toggled')) {
            $('#solutionContainer .wrs_layoutFor3Rows tr td button[title="Itálica automática"]').click();
        }
    });

    //$('#solutionContainer input').keydown(function (event) {
    //    if (event.keyCode == 8 || event.keyCode == 46) {
    //        if (solutionEditor.isFormulaEmpty() && $('#solutionContainer .wrs_layoutFor3Rows tr td button[title="Itálica automática"]').hasClass('wrs_toggled')) {
    //            $('#solutionContainer .wrs_layoutFor3Rows tr td button[title="Itálica automática"]').click();
    //        }
    //    }
    //});
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