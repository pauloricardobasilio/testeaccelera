function ColaboratorModel() {
    var self = this;

    self.id = ko.observable();
    self.name = ko.observable().extend({ required: { message: "O nome do colaborador é obrigatório" } });
    self.dateOfBirth = ko.observable();
    self.phoneNumber = ko.observable();
    self.registry = ko.observable().extend({ required: { message: "O registro do colaborador é obrigatório" } });
    self.address = ko.observable();
    self.estate = ko.observable().extend({ required: { message: "O estado é obrigatório" } });
    self.city = ko.observable().extend({ required: { message: "A cidade é obrigatória" } });

    self.errorsList = ko.observableArray();
    self.messagesList = ko.observableArray();

    self.editMode = ko.observable(false);

    self.fullAddress = ko.computed(function () {
        return self.address() + " - " + self.city() + "/" + self.estate();
    });

    self.legend = ko.computed(function () {
        return self.editMode() ? "Editar Colaborador" : "Novo Colaborador";
    });

    self.id.subscribe(function () {
        self.editMode(true);
        self.findColaborator(self.id());
    });

    self.findColaborator = function (id) {
        $.getJSON('/api/colaborators/' + id)
            .success(function (response) {
                self.name(response.Name);
                self.registry(response.Registry);
                self.dateOfBirth(format.dateToClient(response.DateOfBirth));
                self.phoneNumber(response.PhoneNumber);
                self.address(response.Address);
                self.estate(response.Estate);
                self.city(response.City);
            }).error(function () {
                window.location = "#/";
            });
    };

    self.saveColaborator = function () {
        clearMessages();
        if (vm.isValid()) {
            var colaborator = {
                Id: self.id(),
                Name: self.name(),
                Registry: self.registry(),
                DateOfBirth: format.dateToServer(self.dateOfBirth()),
                PhoneNumber: self.phoneNumber(),
                Address: self.address(),
                Estate: self.estate(),
                City: self.city()
            };

            if (self.editMode()) {
                ajaxRequest("put", "/api/colaborators", colaborator)
                .done(function () {
                    self.messagesList.push("Registro alterado com sucesso");
                })
                .fail(function (result) {
                    var errors = JSON.parse(result.responseText).ModelState;
                    $.each(errors, function (index, item) {
                        self.errorsList.push(item);
                    });
                });
            } else {
                ajaxRequest("post", "/api/colaborators", colaborator)
                .done(function (result) {
                    self.id(result.Id);
                    self.messagesList.push("Colaborador adicionado com sucesso");
                })
                .fail(function (result) {
                    var errors = JSON.parse(result.responseText).ModelState;
                    $.each(errors, function (index, item) {
                        self.errorsList.push(item);
                    });
                });
            }
        } else {
            vm.errors.showAllMessages();
        }
    };

    function clearMessages() {
        self.errorsList([]);
        self.messagesList([]);
    }

    function ajaxRequest(type, url, data, dataType) {
        var options = {
            dataType: dataType || "json",
            contentType: "application/json",
            cache: false,
            type: type,
            data: data ? ko.toJSON(data) : null
        };
        return $.ajax(url, options);
    }
}