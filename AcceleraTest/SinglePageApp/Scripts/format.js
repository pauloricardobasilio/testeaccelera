Date.prototype.setISO8601 = function (string) {
    var regexp = "([0-9]{4})(-([0-9]{2})(-([0-9]{2})" +
        "(T([0-9]{2}):([0-9]{2})(:([0-9]{2})(\.([0-9]+))?)?" +
        "(Z|(([-+])([0-9]{2}):([0-9]{2})))?)?)?)?";
    var d = string.match(new RegExp(regexp));

    var offset = 0;
    var date = new Date(d[1], 0, 1);

    if (d[3]) {
        date.setMonth(d[3] - 1);
    }
    if (d[5]) {
        date.setDate(d[5]);
    }
    if (d[7]) {
        date.setHours(d[7]);
    }
    if (d[8]) {
        date.setMinutes(d[8]);
    }
    if (d[10]) {
        date.setSeconds(d[10]);
    }
    if (d[12]) {
        date.setMilliseconds(Number("0." + d[12]) * 1000);
    }
    if (d[14]) {
        offset = (Number(d[16]) * 60) + Number(d[17]);
        offset *= ((d[15] == '-') ? 1 : -1);
    }

    offset -= date.getTimezoneOffset();
    time = (Number(date) + (offset * 60 * 1000));
    this.setTime(Number(time));
};

var format = {
    money: function (value) {
        if (value === '' || isNaN(value)) value = 0;
        var toks = Number(value).toFixed(2).replace('-', '').split('.');
        var display = 'R$ ' + $.map(toks[0].split('').reverse(), function (elm, i) {
            return [(i % 3 === 0 && i > 0 ? '.' : ''), elm];
        }).reverse().join('') + ',' + toks[1];

        return value < 0 ? '-' + display : display;
    },

    formatDateISOToPt_BR: function (datetime, isdateTime) {
        if (!datetime) return null;
        var date = new Date();
        date.setISO8601(datetime);

        var day = date.getDate();
        if (day < 10)
            day = "0" + day.toString();
        var month = date.getMonth() + 1;
        if (month < 10)
            month = "0" + month.toString();
        var year = date.getFullYear();

        if (isdateTime)
            return day + "/" + month + "/" + year.toString() + " " + date.toLocaleTimeString();
        return day + "/" + month + "/" + year.toString();
    },

    dateToServer: function (clientDate) {
        if (!clientDate) return null;
        var tokens = clientDate.split("/");
        var dt = tokens[1] + "/" + tokens[0] + "/" + tokens[2];
        return dt;
    },

    dateToClient: function (serverDate) {
        return this.formatDateISOToPt_BR(serverDate, null);
    },
};