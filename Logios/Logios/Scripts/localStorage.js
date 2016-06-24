function LocalStorage() {
    this.get = function (name) {
        var storage = JSON.parse(window.localStorage.getItem(name));

        if (storage === null) {
            storage = [];
        }

        return storage;
    };

    this.set = function (name, value) {
        window.localStorage.setItem(name, JSON.stringify(value));
    };

    this.clear = function () {
        window.localStorage.clear();
    };

    this.isTutorialViewed = function (page, user) {
        var itemKey = page + '_' + user;

        var tutorialLogged = window.localStorage.getItem(itemKey);
        var tutorialNotLogged = window.localStorage.getItem(page + '_');

        return tutorialLogged || tutorialNotLogged;
    };

    this.viewTutorial = function (page, user) {
        var itemKey = page + '_' + user;

        window.localStorage.setItem(itemKey, true);
    };
}

var storage = new LocalStorage();