var viewModel = new ColaboratorModel();
var vm = ko.validatedObservable(viewModel);
viewModel.id(app.params.id);
ko.applyBindings(vm, $("#main")[0]);
