// La variable userId esta definida en la vista y tiene el Id del usuario de Identity
var topics = [],
    searchForm,
    KEYS = {
        ENTER: 13,
        BACKSPACE: 8,
        DELETE: 46
    };

$(function () {
    var searchInput = $('#searchInput'),
        searchButton = $('#exerciseSearchButton'),
        closingAutocomplete = false;

    // Deshabilitar los elementos de busqueda hasta que se carguen los temas
    searchInput.attr('disabled', 'disabled');
    searchButton.attr('disabled', 'disabled');

    var tooltip = $('<img src="Content/images/ajax-loader.gif" alt="loading" style="height:20px;" /> <strong> Cargando la lista de temas, espera por favor&hellip; </strong>');

    searchInput.tooltipster({
        content: tooltip,
        theme: 'tooltipster-logios'
    });

    searchInput.tooltipster('show');

    $.ajax({
        url: '/Home/GetTopics',
        method: 'GET',
        success: configureAutocomplete
    });

    function configureAutocomplete(rawTopics) {
        topics = $.map(rawTopics, function (topic, index) {
                     return topic.Description;
                 });

        searchInput.autocomplete({
            source: topics,
            delay: 50,
            minLength: 0,
            close: function()
            {
                closingAutocomplete = true;
                setTimeout(function() { closingAutocomplete = false; }, 300);
            },
            select: function (event, ui) {
                event.preventDefault();

                var selectedTopic = ui.item.label;
                
                $(this).val(selectedTopic);
                searchButton.click();
            }
        });

        searchInput.on('focus', function () {
            if (!closingAutocomplete) {
                $(this).autocomplete("search");
            }                
        });


        searchInput.on('keyup', function (event) {
            if (event.keyCode === KEYS.ENTER) {
                searchForm.submit();
            }
        });

        searchInput.on('keydown', function (event) {
            var newValue = event.target.value;

            if ( (newValue.length === 0) && (event.keyCode === KEYS.BACKSPACE || event.keyCode === KEYS.DELETE) ) {
                $(this).autocomplete("search");
            }
        });

        searchButton.removeAttr('disabled');
        searchInput.removeAttr('disabled');
        searchInput.tooltipster('destroy');
        searchInput.removeAttr('title');
    }

    // obtener el form
    searchForm = $('#exerciseSearchForm');

    searchForm.submit(searchExercise);

    generateSearchTags();

    // Configuracion para ocultar y mostrar ejercicios resueltos.
    $('#showResolvedExercises').click(toggleResolvedExercises);

    function toggleResolvedExercises(event) {
        var checkboxIsTurnedOn = $(this).is(':checked'),
            resolvedExercises = $('h3.exercise-title.text-success').parents('.exercise-card'),
            lastTopicInput = $('input#lastTopic');

        if (checkboxIsTurnedOn) {
            resolvedExercises.each(function (index, element) {
                $(element).fadeIn(600);
            });
        } else {
            resolvedExercises.each(function (index, element) {
                $(element).fadeOut(600);
            });
        }

        // Resetear el valor del ultimo tema buscado para que se pueda volver a realizar una busqueda.
        lastTopicInput.val('');
    }

    $('#joyRideTipContent').joyride({
        modal: true,
        expose: true
    });

    // Configurar visita guiada para el home
    $('.tutorial-button').click(function (event) {
        launchTutorial(false);
    });

    launchTutorial(true);
});

function searchExercise() {
    // Guardarme los dos campos del formulario
    var searchInput = $('input#searchInput');
    var lastTopicInput = $('input#lastTopic');
    var showResolvedCheckbox = $('input#showResolvedExercises');
    var resultArea = $("#SearchArea");
    var searchResults = $('.exercise-card').length > 0;

    var topic = searchInput.val();
    var lastTopic = lastTopicInput.val();

    // Si quere buscar lo mismo dos veces seguidas y no hay resultados o lo que busca no es un tema válido, no hago nada
    if (!inputIsValid(topic) || (equalWords(lastTopic, topic) && searchResults)) {
        return false;
    }

    // Poner el cartel de loading
    toggleLoading(true);

    // Actualizar el valor de la ultima busqueda realizada
    lastTopicInput.val(topic);
    showResolvedCheckbox.val(showResolvedCheckbox.is(':checked'));
    
    // Si no esta logueado solo lo mando, y no actualizo las labels de busqueda rapida.
    if (userId === '') {
        resultArea.scrollToMe(50, 800);
        return;
    }

    // Sacar el registro de busquedas del usuario de local storage
    var searches = storage.get(userId);

    // Sumarle uno a la lista de busquedas realizadas por el usuario, validar si es la primera vez que busca eso
    addOneToSearchCount(searches, topic);

    // Ordernar el array y actualizar el local storage
    searches.sort(function (search1, search2) {
        return search1.searchCount < search2.searchCount
    });

    storage.set(userId, searches);

    resultArea.scrollToMe(50, 800);
}

function generateSearchTags() {
    if (userId === '') {
        return;
    }

    var topSearches = getTopThreeMostSearchedTopics();
    var labelStart = '<span class="label label-default search-tag" ';
    var labelEnd = '</span>';

    topSearches.forEach(function (search) {
        var action = 'onclick=searchFromTags(\"' + search.description + '\")>';
        var newLabel = labelStart + action + capitalizeString(search.description) + labelEnd;
        $('#search-tags').append(newLabel);
    });
}

function getTopThreeMostSearchedTopics() {
    var searches = storage.get(userId);
    
    // Devolver solo con los 3 que mas busquedas tienen
    return searches.slice(0, 3);
}

function searchFromTags(topicDescription) {
    var topic = getTopicByDescription(topicDescription);

    $('input[name="topicId"]').val(topic.id);
    $('input#searchInput').val(topic.description);

    searchForm.submit();
}

function getTopicByDescription(topicDescription) {
    var searches = storage.get(userId);

    var theSearch = searches.find(function (search) {
        return search.description === topicDescription;
    });

    return theSearch;
}

function addOneToSearchCount(searches, topicDescription) {
    if (topicDescription === '') {
        return;
    }

    var found = false;

    for (var i = 0; i < searches.length; i++) {
        if (searches[i].description.toLowerCase() === topicDescription.trim().toLowerCase()) {
            searches[i].searchCount++;
            found = true;
            break;
        }
    }

    if (found === false) {
        searches.push({
            description: capitalizeString(topicDescription),
            searchCount: 1
        });
    }
}

function animateResults() {
    // Animar la seccion de resultados
    $('#exerciseSearchResult').hide();

    toggleLoading(false);

    $('#exerciseSearchResult').slideDown('slow');

    // Borrar las tags y volver a generarlas por si una pasa a tener mas busquedas
    $('#search-tags').empty();
    generateSearchTags();
}

function inputIsValid(searchValue) {
    var alertElement = $('#badSearchAlert');
    var topicIsValid = false;

    // Verificar si lo que pusieron en la barra de busqueda es uno de los temas que tenemos.
    topics.forEach(function (topic) {
        if (equalWords(topic, searchValue)) {
            topicIsValid = true;
        }
    });

    if (topicIsValid) {
        return true;
    }

    // Si no salió es porque tengo que mostrar el alert, si ya lo estoy mostrando, no hago nada.
    if (alertElement.length > 0) {
        return false;
    }

    // Generar el HMTL del alert y convertirlo en un objeto jQuery.
    var newAlert = $('<div id="badSearchAlert" class="alert alert-danger fade in">\
                        No hay coincidencias con la temática ingresada.\
                     </div>');

    // Agregar el cartel con efecto de fadeIn.
    newAlert.hide().fadeIn().appendTo(searchForm);

    setTimeout(function () {
        $('#badSearchAlert').fadeOut('slow', function () {
            $('#badSearchAlert').remove();
        });        
    }, 9500);

    return false;
}

function toggleLoading(enable) {
    var searchButton = $('#exerciseSearchButton'),
        loadingSign = '<div id="searchLoading" class="alert-info text-center"> <strong> <img src="Content/images/loading.gif" style="width:50px; height:50px;"  alt="loading image" /> Cargando resultados </strong> </div>',
        $resultArea = $('#exerciseSearchResult');

    if (enable) {
        searchButton.attr('disabled', 'disabled');
        $resultArea.empty();
        $resultArea.append(loadingSign);
    } else {
        searchButton.removeAttr('disabled');
        $resultArea.detach('#searchLoading');
    }
}

function launchTutorial(automatic) {
    var tutorialWasPlayed = storage.isTutorialViewed('home', userId);

    // Si es automatico y ya fue reproducido, no hacer nada.
    if (automatic && tutorialWasPlayed) {
        return;
    }

    // Si es automatico pero no existe la cookie crearla (y luego ejecutarlo)
    if (automatic && tutorialWasPlayed === null) {
        storage.viewTutorial('home', userId);
    }

    $('#joyRideTipContent').joyride('restart');
}

function equalWords(word1, word2) {
    return word1.toLowerCase().trim().replace(/\s+/g, ' ') === word2.toLowerCase().trim().replace(/\s+/g, ' ');
}

function capitalizeString(words) {

    // Eliminar los espacios extras al comienzo, al final y en el medio.
    var parsedWords = words.trim().replace(/\s+/g, ' ');

    // Meter cada palabra por separado dentro de un array para trabajarlo mejor.
    var wordsInArray = parsedWords.split(' ');

    // Poner en mayuscula la primera letra de cada palabra y el resto en minuscula.
    for (var i = 0, wordCount = wordsInArray.length; i < wordCount; i++) {
        wordsInArray[i] = wordsInArray[i].substr(0, 1).toUpperCase() + wordsInArray[i].substr(1).toLowerCase();
    }

    // Devolver un string que es la union de las palabras separadas por espacios.
    return wordsInArray.join(' ');
}

//Extensión de JQuery
jQuery.fn.extend({
    scrollToMe: function (offset, speed) {
        var y = offset || 100;
        var animationSpeed = speed || 400;
        var position = jQuery(this).offset().top - y;

        jQuery('html,body').animate({ scrollTop: position }, animationSpeed);
    }
});