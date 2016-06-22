var solutionEditor;

$(document).ready(function () {            
    solutionEditor = com.wiris.jsEditor.JsEditor.newInstance({
        'language': 'es',
        'fontFamily': 'Tahoma',
        'fontSize': '22px',
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
            $('.notice').wrapInner('<p class="text-danger" style="font-size: 18px; padding-bottom: 20px;"><b><i>' + response.responseText + '</i></b></p>');
        },
        success: function (response) {
            $("#confirmDialog").modal('hide');
            $('#ViewDevelopment').hide();            
            $('#DevelopmentField').slideDown(500);
            $('.notice').wrapInner('<p class="text-danger" style="font-size: 18px; padding-bottom: 20px;"><b><i>' + response + '</i></b></p>');
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
    if (userId === '') {
        return;
    }

    var tutorialWasPlayed = storage.isTutorialViewed('showExercise', userId);

    // Si es automatico y ya fue reproducido, no hacer nada.
    if (automatic && tutorialWasPlayed) {
        return;
    }

    // Si es automatico pero no existe la cookie crearla (y luego ejecutarlo)
    if (automatic && tutorialWasPlayed === null) {
        storage.viewTutorial('showExercise', userId);
    }

    $('#joyRideTipContent').joyride('restart');
}