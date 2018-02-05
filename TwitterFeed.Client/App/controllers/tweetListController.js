(function () {
    'use strict';

    var twitterFeedApp = angular.module('twitterFeedApp');

    twitterFeedApp.controller('tweetListController', ['tweetService', function (tweetService) {
        var vm = this;
        vm.tweets = [];

        tweetService.getTweets().then(function (response) {
            vm.tweets = response.data;
        }, function (error) {
            console.error(error.data);
        });
    }]);
})();