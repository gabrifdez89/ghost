angular.module('GhostGame', [])
    .controller('GameController', ['$scope', '$http', function ($scope, $http) {
        var game = this;

        game.word = '';
        game.letter = '';
        game.finished = false;
        game.win = false;
        game.lose = false;

        game.playLetter = function () {
            if (!game.finished) {
                if ((/^[a-z]{1}$/.test(game.letter))) {
                    if (game.word === undefined) {
                        game.word = '';
                    }
                    game.word = game.word + game.letter;
                    game.letter = '';

                    $http.post('http://localhost:59359/api/play', { 'text': game.word }).then(function (res) {
                        if (res.data.lose) {
                            game.finished = true;
                            game.lose = true;
                        }

                        else if (res.data.win) {
                            game.word = res.data.text;
                            game.finished = true;
                            game.win = true;
                        }

                        else {
                            game.word = res.data.text;
                        }

                    }, function (res) {
                        alert('There was an error: ' + res);
                    });

                } else {
                    alert("Only lowercase letters are allowed");
                }
            }
        };

        game.restart = function () {
            game.word = '';
            game.letter = '';
            game.finished = false;
            game.win = false;
            game.lose = false;
        };
    }]);
