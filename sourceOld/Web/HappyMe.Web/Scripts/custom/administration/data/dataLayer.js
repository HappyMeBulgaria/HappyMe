var HappyMe = HappyMe || {};

HappyMe.Data = (function (httpRequester) {
    'use strict';

    var ChildrenStatisticsPersister = function (url) {
        this.url = url;
    };

    ChildrenStatisticsPersister.prototype.getAllForChild = function (childId) {
        return httpRequester.getJson(this.url + 'allForChild/' + childId);
    };

    var DataPersister = function (serviceRootUrl) {
        this.serviceRootUrl = serviceRootUrl;

        this.childrenStatistics = new ChildrenStatisticsPersister(serviceRootUrl + 'childrenStatistics/');
    };

    return {
        getAdminDataPersister: function (serviceRootUrl) {
            return new DataPersister(serviceRootUrl || '/administration/');
        },
        getDataPersister: function () { throw new Error('Not implemented.'); }
    };
}(HappyMe.HttpRequester));
