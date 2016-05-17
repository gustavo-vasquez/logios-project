var solutionEditor;

$(document).ready(function() {
    solutionEditor = com.wiris.jsEditor.JsEditor.newInstance({ 'language': 'es' });
    solutionEditor.insertInto(document.getElementById('solutionContainer'));

    MathJax.Hub.Config({
        jax: ["input/MathML", "output/HTML-CSS"],

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
            linebreaks: { automatic: true }
        },

        SVG: {
            scale: 50,
            font: "Gyre-Pagella",
            undefinedFamily: "STIXGeneral, 'Arial Unicode MS', serif",
            matchFontHeight: true,
            useFontCache: true,
            blacker: 10
        }
    });
});

function ViewDevelopment(UserId, ExerciseId) {
	if ($('#DevelopmentField').is(':hidden'))
	{
		$('#DevelopmentField').slideDown(500);
	}
	else
    {
	    $('#DevelopmentField').slideUp(500);
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
    var isValid = ($('#answer').val() != "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"/>");
    if (isValid)
        return true;    
    else
        alert("ERROR: Debe escribir una respuesta.");

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