// La variable userId esta definida en la vista y tiene el Id del usuario de Identity
var parsedTopics = [],
    searchForm;

$(function () {
    $.ajax({
        url: '/Home/GetTopics',
        method: 'GET',
        success: configureAutocomplete
    });

    function configureAutocomplete(rawTopics) {
        parsedTopics = $.map(rawTopics, function (topic, index) {
                            return {
                                label: topic.Description,
                                id: topic.TopicId
                            };
                        });

        $('#searchInput').autocomplete({
            source: parsedTopics,
            autoFocus: true,
            select: function (event, ui) {
                var selectedTopic = ui.item;
                $('input[name="topicId"]').val(selectedTopic.id);
            }
        });
    }

    // obtener el form
    searchForm = $('#exerciseSearchForm');

    $('#searchInput').on('keyup', function (event) {
        // 13 es el ENTER, esto es para que aprentando ENTER en el autocomplete lo mande de una
        // y no haga falta apretar ENTER dos veces para mandar la búsqueda.
        if (event.keyCode == 13) {
            searchForm.submit();
        }
    });

    searchForm.submit(searchExercise);

    generateSearchTags();
});

function searchExercise() {
    // Guardarme los dos campos del formulario
    var searchBarValue = $('input#searchInput').val()
    var topicId = parseInt($('input[name="topicId"]').val());
    var lastTopicIdInput = $('input#lastTopicId');
    var lastTopicId = parseInt($('input#lastTopicId').val());
    var topicDescription = $('input#searchInput').val();

    // Si quere buscar lo mismo dos veces seguidas, no hago nada
    if (!inputIsValid(searchBarValue) || lastTopicId === topicId || isNaN(topicId)) {
        return false;
    }
    
    // Si no esta logueado solo lo mando, y no actualizo las labels de busqueda rapida.
    if (userId === '') {
        lastTopicIdInput.val(topicId);
        return;
    }

    // Sacar el registro de busquedas del usuario de local storage
    var searches = storage.get(userId);

    // Actualizar el valor de la ultima busqueda realizada
    lastTopicIdInput.val(topicId);

    // Sumarle uno a la lista de busquedas realizadas por el usuario, validar si es la primera vez que busca eso
    addOneToSearchCount(searches, topicDescription, topicId);

    // Ordernar el array y actualizar el local storage
    searches.sort(function (search1, search2) {
        return search1.searchCount < search2.searchCount
    });

    storage.set(userId, searches);
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
        var newLabel = labelStart + action + search.description + labelEnd;
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

function addOneToSearchCount(searches, topicDescription, topicId) {
    if (topicDescription === '' || isNaN(topicId)) {
        return;
    }

    var found = false;

    for (var i = 0; i < searches.length; i++) {
        if (searches[i].description === topicDescription) {
            searches[i].searchCount++;
            found = true;
            break;
        }
    }

    if (found === false) {
        searches.push({
            description: topicDescription,
            id: topicId,
            searchCount: 1
        });
    }
}

function animateResults() {
    // Animar la seccion de resultados
    $('#exerciseSearchResult').hide();
    $('#exerciseSearchResult').slideDown('slow');

    // Borrar las tags y volver a generarlas por si una pasa a tener mas busquedas
    $('#search-tags').empty();
    generateSearchTags();
}

function inputIsValid(searchValue) {
    var alertElement = $('#badSearchAlert');

    var topicLabels = $.map(parsedTopics, function (topic, index) {
                                return topic.label;
                            });

    // Verificar si lo que pusieron en la barra de busqueda es uno de los temas que tenemos.
    if (topicLabels.indexOf(searchValue) > -1) {
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