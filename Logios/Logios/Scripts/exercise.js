var solutionEditor;

$(document).ready(function () {        
    MathJax.Hub.Config({
        //jax: ["input/MathML", "output/CommonHTML"],

        MathML: {
            extensions: ["content-mathml.js"]
        },

        "HTML-CSS": {
            availableFonts: ["STIX", "TeX", "Gyre-Pagella"],
            preferredFont: "Gyre-Pagella",
            scale: 100,
            webFont: "Gyre-Pagella",
            undefinedFamily: "STIXGeneral, 'Arial Unicode MS', serif",
            matchFontHeight: false,
            linebreaks: { automatic: true }
        },

        CommonHTML: {
            scale: 100,
            //linebreaks: { automatic: true }
        },
    });

    solutionEditor = com.wiris.jsEditor.JsEditor.newInstance({
        'language': 'es',
        'fontFamily': 'Times New Roman',
        'fontSize': '24px',
        'toolbar': '<toolbar ref="general" removeLinks="true"></toolbar>',
        'autoformat': true
    });
    solutionEditor.insertInto(document.getElementById('solutionContainer'));

    $('#btnHelper').click(function () {
        window.open("/Exercise/HelperEditor", "", "width=530,height=320");
    });

    $('#joyRideTipContent').joyride({
        modal: true,
        expose: true
    });

    $('.tutorial-button').click(function (event) {
        launchTutorial(false);
    });

    // Lanzar la visita guiada automaticamente al abrir la pagina.
    launchTutorial(true);
});

function doAction(UserId, ExerciseId) {
    var jsonObject = { "UserId": UserId, "ExerciseId": ExerciseId };
    
    $.ajax({
        url: "/Exercise/ShowDevelop",
        type: 'POST',
        data: JSON.stringify(jsonObject),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (response) {
            $("#confirmDialog").modal('hide');
            $('blockquote .clearfix').wrapInner('<p class="text-danger" style="padding-bottom: 20px;"><i>' + response.responseText + '</i></p>');            
        },
        success: function (response) {
            $("#confirmDialog").modal('hide');
            $('#ViewDevelopment').hide();            
            $('#DevelopmentField').slideDown(500);
            $('blockquote .clearfix').wrapInner('<p class="text-danger" style="padding-bottom: 20px;"><i>' + response + '</i></p>');
        }
    });    
}

function copyAnswer() {
    $('#answer').val(solutionEditor.getMathML());
    if ($('#answer').val() != "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"/>") {
        $('.message').empty();
        return true;
    }        
    else {
        $('.message').wrapInner('<span class="help-block"></span><span class="text-danger">&diams; Escriba la solución</span>');
    }        

    return false;
}

function pagination(isFirst, isLast) {
    if (isFirst == "True") {        
        document.getElementById("prevEx").className += " disabled";        
    }

    if (isLast == "True") {        
        document.getElementById("nextEx").className += " disabled";
    }    
}

function launchTutorial(automatic) {
    var cookieAlreadyExists = $.cookie('logiosTutorialExercise');

    // Si es automatico y ya existe la cookie, no hacer nada.
    if (automatic && cookieAlreadyExists) {
        return;
    }

    // Si es automatico pero no existe la cookie crearla (y luego ejecutarlo)
    if (automatic) {
        $.cookie('logiosTutorialExercise', 'tutorial run', { expires: 1024 });
    }

    $('#joyRideTipContent').joyride('restart');
}