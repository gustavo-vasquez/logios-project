var solutionEditor;

$(document).ready(function () {    
    solutionEditor = com.wiris.jsEditor.JsEditor.newInstance({
        'language': 'es',
        'fontFamily': 'Times New Roman',
        'fontSize': '24px',
        'toolbar': '<toolbar ref="general" removeLinks="true"><removeTab ref="contextual" /></toolbar>'
    });
    solutionEditor.insertInto(document.getElementById('solutionContainer'));    

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
});

function ViewDevelopment(UserId, ExerciseId) {
	if ($('#DevelopmentField').is(':hidden'))
	{
	    $('#ViewDevelopment').hide();
	    $('#DevelopmentField').slideDown(500);	    
	}
	
	var jsonObject = { "UserId": UserId, "ExerciseId": ExerciseId };
	//var urlaction = "@Url.Action(Exercise,ShowDevelop)";
	$.ajax({
	    url: "/Exercise/ShowDevelop",
	    type: 'POST',
        data: JSON.stringify(jsonObject),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (response) {
	        alert(response.responseText);
	    },
        success: function (response) {
	        alert(response);
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