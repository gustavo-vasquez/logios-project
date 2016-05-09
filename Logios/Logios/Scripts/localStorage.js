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
}

var storage = new LocalStorage();