var app = $.sammy('#main', function () {
    this.params = {};

    this.get('#/', function (context) {
        var self = this;
        app.params = self.params;
        $.get("/colaborator/list", function (data) {
            context.$element().html(data);
        });
    });

    this.get('#/colaborators/edit/:id', function (context) {
        var self = this;
        app.params = self.params;
        $.get("/colaborator/edit", function (data) {
            context.$element().html(data);
        });
    });

    this.get('#/colaborators/new', function (context) {
        var self = this;
        app.params = self.params;
        $.get("/colaborator/edit", function (data) {
            context.$element().html(data);
        });
    });

    this.get('#/colaborators/details/:id', function (context) {
        var self = this;
        app.params = self.params;
        $.get("/colaborator/details", function (data) {
            context.$element().html(data);
        });
    });
});

$(function () {
    app.run('#/');
});