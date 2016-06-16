$(function () {
    var favoriteStars = $('.favorite-star'),
        removeFavoriteButtons = $('.remove-favorite-button');

    // Configurar a las estrellas de favoritos para que hagan lo que tienen que hacer.
    favoriteStars.click(function (event) {
        var $this = $(this),
            isToggled = $this.hasClass('glyphicon-star');        

        if (isToggled) {
            $this.removeClass('glyphicon-star');
            $this.addClass('glyphicon-star-empty');
            $this.tooltip('hide').attr('data-original-title', 'Marcar como favorito').tooltip('fixTitle').tooltip('show');
        } else {
            $this.removeClass('glyphicon-star-empty');
            $this.addClass('glyphicon-star');
            $this.tooltip('hide').attr('data-original-title', 'Quitar de favoritos').tooltip('fixTitle').tooltip('show');            
        }

        var exerciseId = $this.data('exerciseId');

        $.ajax({
            url: '/Exercise/ToggleFavorite',
            type: 'POST',
            contentType: 'application/x-www-form-urlencoded',
            data: {
                exerciseId: exerciseId
            }
        });
    });

    removeFavoriteButtons.click(function (event) {
        var $this = $(this),
            exerciseId = $this.data('exerciseId');

        $this.attr('disabled', 'disabled');

        $.ajax({
            url: '/Exercise/ToggleFavorite',
            type: 'POST',
            contentType: 'application/x-www-form-urlencoded',
            data: {
                exerciseId: exerciseId
            },
            success: function (response) {
                var $row = $this.parents('tr');
                $row.fadeOut('slow', function () { $row.remove() });
            }
        });
    });
});