(function () {
    'use strict';

    var app = angular.module('twitterFeedApp');

    app.factory('tweetService', ['$http', '$q', function ($http, $q) {
        var serviceUrl = 'http://twitterfeedapi.azurewebsites.net/api'
        return {
            getTweets: function () {
                return $http.get(serviceUrl+'/usertimeline?user=salesforce')                
            }
        }
    }]);
})();