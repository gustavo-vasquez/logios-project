(function () {
    $(function () {
        $('a.topic-menu').click(openExercises);
    })

    function openExercises(event) {
        var $this = $(this),
            $arrowIcon = $this.children('span:first'),
            $exerciseList = $this.next('ul'),
            exerciseCount = $exerciseList.children('li').length,
            topicName = $this.data('topic');

        if ($exerciseList.css('display') === 'none') {
            $arrowIcon.removeClass('glyphicon-chevron-right');
            $arrowIcon.addClass('glyphicon-chevron-down');
            $exerciseList.css('display', 'block');

            if (exerciseCount === 0) {
                $.ajax({
                    url: "/Manage/ExercisesTree?topicName=" + topicName,
                    type: 'GET',
                    error: function (response) {
                        alert('Error: ' + response.responseText);
                    },
                    success: function (exerciseIds) {
                        for (i = 0, arrayLength = exerciseIds.length; i < arrayLength; i++) {
                            appendExerciseElement($exerciseList, exerciseIds[i]);
                        }
                    }
                });
            }
        }
        else {
            $arrowIcon.removeClass('glyphicon glyphicon-chevron-down');
            $arrowIcon.addClass('glyphicon glyphicon-chevron-right');
            $exerciseList.css('display', 'none');
        }
    }

    function appendExerciseElement(list, exerciseId) {
        var $listItem = $('<li></li>'),
            $linkToExercise = $('<a></a>').attr('href', '/Exercise/Show/' + exerciseId),
            $fileIcon = $('<span></span>').addClass('glyphicon glyphicon-file'),
            linkText = 'Ejercicio ' + exerciseId;

        // Armar el link con el span de glyphicon y el texto adentro
        $linkToExercise.append($fileIcon);
        $linkToExercise.append(linkText);

        // Agregar el link armado adentro del elemento de LI
        $listItem.append($linkToExercise);

        // Agregar el LI a la lista de ejercicios UL
        list.append($listItem);
    }

})();
