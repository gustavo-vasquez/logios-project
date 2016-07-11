(function () {
    $(function () {
        var tabNames = [
                'badgesAndPointsPanel',
                'exercisePanel',
                'favoriteExercisePanel'
            ];

        setupTabs(tabNames);
        
        function setupTabs(tabNames) {
            var tabs = {};
            
            tabNames.forEach(function (tabName) {
                var tabElement = $('a[href="#' + tabName + '"]');
                var url = tabElement.attr('data-url');
                var updateElement = $('#' + tabName);

                tabs[tabName] = {
                    tabElement: tabElement,
                    url: url,
                    updateElement: updateElement
                };

                setupAjax(tabs[tabName]);
            });
        }

        function setupAjax(tab) {
            tab.tabElement.on('click', function () {
                $.ajax({
                    url: tab.url,
                    method: 'GET',
                    success: function (partialView) {
                        tab.updateElement.html(partialView);
                    }
                });
            });
        }                
    });        
})();

function openExercises(topicName) {
    if ($('.' + topicName + '').next('ul').css('display') == 'none') {
        $('.' + topicName + ' > span:first').removeClass('glyphicon glyphicon-chevron-right');
        $('.' + topicName + ' > span:first').addClass('glyphicon glyphicon-chevron-down');
        $('.' + topicName + '').next('ul').css('display', 'block');

        if ($('ul[name=' + topicName + '] li').length == 0) {
            $.ajax({
                url: "/Manage/ExercisesTree?topicName=" + topicName,
                type: 'GET',
                error: function (response) {
                    alert('Error: ' + response.responseText);
                },
                success: function (treeObject) {                    
                    for(i = 0; i < treeObject.length; i++) {                    
                        if (treeObject[i].Value) {
                            $('ul[name=' + topicName + ']').append('<li><a href="/Exercise/Show/' + treeObject[i].Key + '"><span class="glyphicon glyphicon-file"></span> Ejercicio ' + treeObject[i].Key + ' <span class="glyphicon glyphicon-ok"></span></a></li>');
                        }
                        else {
                            $('ul[name=' + topicName + ']').append('<li><a href="/Exercise/Show/' + treeObject[i].Key + '"><span class="glyphicon glyphicon-file"></span> Ejercicio ' + treeObject[i].Key + '</a></li>');
                        }
                    }                                        
                    treeListed = true;
                }
            });
        }
    }
    else {
        $('.' + topicName + ' > span:first').removeClass('glyphicon glyphicon-chevron-down');
        $('.' + topicName + ' > span:first').addClass('glyphicon glyphicon-chevron-right');
        $('.' + topicName + '').next('ul').css('display', 'none');
    }
}