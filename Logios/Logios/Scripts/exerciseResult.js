$(function () {
    function limitExerciseDescriptionLength() {
        var exerciseDescriptions = $('div.thumbnail div.caption p');

        exerciseDescriptions.each(function (index, element) {
            // Si el texto tiene 56 caracteres o mas, truncarlos y agregarle los tres puntos
            if (element.innerHTML.length >= 56) {
                var tooltipText = element.innerHTML;
                $(element).html(element.innerHTML.substr(0, 56) + '&hellip;'); // &hellip es la entidad HTML de ellipsis (...)
                $(element).attr('title', tooltipText);
            }
        });
    }

    limitExerciseDescriptionLength();
});