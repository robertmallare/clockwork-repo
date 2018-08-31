(
    function() {
        var app = angular.module('app', []);

        app.controller('HomeController', ['$scope', '$http', HomeController]);
    }()
);