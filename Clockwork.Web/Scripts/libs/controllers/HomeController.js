var HomeController = function($scope, $http) {

    var get_timezones = 'http://127.0.0.1:5000/api/timezones';
    var get_currenttime = 'http://127.0.0.1:5000/api/currenttime/';

    var data = this;
    data.didUserRequest = false;

    $scope.currentPage = 1;
    $scope.selectedPageSize = 10;
    $scope.selectedSort = "desc";

    var onResponse = function(response) {

        if(response.data.timeRequested.currentTimeQueryId){
            data.timeRequested = response.data.timeRequested;
            data.didUserRequest = true;
        }

        data.currentTimeQueries = response.data.currentTimeQueries;

        $scope.hasNext = response.data.hasNext;
        $scope.hasPrevious = response.data.hasPrevious;

        $scope.currentPage = response.data.pageIndex;
        $scope.nextPage = $scope.currentPage + 1;
        $scope.prevPage = $scope.currentPage - 1;

        $scope.totalPages = response.data.totalPages;
    }

    var onTimezoneResponse = function(response) {
        data.timezones = response.data.timezoneInfos;
        $scope.selectedTimezone = response.data.currentTimezone;
    }

    var onError = function(error) {
        $scope.error = error.statusText === "" ? 'No error message available' : error.statusText;
    }

    $http.get(get_timezones).then(onTimezoneResponse, onError);
    $http.get(get_currenttime).then(onResponse, onError);
    
    $scope.getCurrentTime = function(didUserRequest, page, size, sort){

        $scope.currentPage = page;
        $scope.selectedSort =  sort;

        $http.get(get_currenttime + '?timezoneId=' + $scope.selectedTimezone.id + '&didUserRequest=' + didUserRequest + '&page=' + page + '&pageSize=' + size + '&sort=' + sort)
            .then(onResponse, onError);
    }

    $scope.changePageSize = function(pageSize){
        $scope.selectedPageSize = pageSize;
        $scope.getCurrentTime(false, $scope.currentPage, $scope.selectedPageSize, $scope.selectedSort)
    }

    $scope.changeSort = function(selectedSort){
        $scope.selectedSort = selectedSort;
        $scope.getCurrentTime(false, $scope.currentPage, $scope.selectedPageSize, $scope.selectedSort)
    }

    $scope.hideCheck = function(index){
        if (index === 0 && data.didUserRequest){
            return true;
        } else {
            return false;
        }
    }

}
