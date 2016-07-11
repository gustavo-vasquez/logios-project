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
                    success: function (exercisesTree) {
                        for (i = 0, arrayLength = exercisesTree.length; i < arrayLength; i++) {
                            appendExerciseElement($exerciseList, exercisesTree[i].Key, exercisesTree[i].Value);
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

    function appendExerciseElement(list, exerciseId, resolved) {
        var $listItem = $('<li></li>'),
            $linkToExercise = $('<a></a>').attr('href', '/Exercise/Show/' + exerciseId),
            $fileIcon = $('<span></span>').addClass('glyphicon glyphicon-file'),            
            linkText = 'Ejercicio ' + exerciseId;

        // Armar el link con el span de glyphicon y el texto adentro
        $linkToExercise.append($fileIcon);
        $linkToExercise.append(linkText);

        // Agrego un check al link armado si el ejercicio esta resuelto
        if(resolved) {
            var $checkIcon = $('<span></span>').addClass('glyphicon glyphicon-ok');
            $linkToExercise.append(' ');
            $linkToExercise.append($checkIcon);
        }

        // Agregar el link armado adentro del elemento de LI
        $listItem.append($linkToExercise);

        // Agregar el LI a la lista de ejercicios UL
        list.append($listItem);
    }

})();
