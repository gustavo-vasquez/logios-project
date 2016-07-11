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
    }); // END onDocument.ready()
})();
