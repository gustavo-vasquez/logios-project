$(function () {

    // Enable autocomplete on the search input
    $.ajax({
        url: '/Home/GetTopics',
        method: 'GET',
        success: configureAutocomplete
    });

    function configureAutocomplete(rawTopics) {
        var parsedTopics = $.map(rawTopics, function (topic, index) {
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
});

function animateResults() {
    $('#exerciseSearchResult').hide();
    $('#exerciseSearchResult').slideDown('slow');
}