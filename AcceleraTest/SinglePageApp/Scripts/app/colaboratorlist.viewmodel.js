function ColaboratorListViewModel() {
    var self = this;

    self.colaborators = ko.observableArray();

    self.query = ko.observable();

    self.findColaborators = function () {
        $.get("/api/colaborators", { filter: self.query() }, function (data) {
            self.colaborators([]);
            ko.utils.arrayForEach(data, function (colaborator) {
                var model = new ColaboratorModel();
                model.id(colaborator.Id);
                model.name(colaborator.Name);
                model.dateOfBirth(colaborator.DateOfBirth);
                model.registry(colaborator.Registry);
                model.phoneNumber(colaborator.PhoneNumber);
                model.estate(colaborator.Estate);
                model.city(colaborator.City);
                model.address(colaborator.Address);
                self.colaborators.push(model);
            });
        });
    };

    self.remove = function (colaborator) {
        self.colaborators.remove(colaborator);
        $.ajax('/api/colaborators/' + colaborator.id(), {
            dataType: "json",
            contentType: "application/json",
            cache: false,
            type: "DELETE",
        });
    };

    self.init = function () {
        self.findColaborators();
    };
}

var viewModel = new ColaboratorListViewModel();
viewModel.init();
var vm = ko.validatedObservable(viewModel);

ko.applyBindings(vm, $('#main')[0]);