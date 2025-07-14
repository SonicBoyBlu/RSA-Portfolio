$.fn.serializeObject = function () {
    var o = {};
    //var a = this.serializeArray();
    var a = $(".form-control, .form-input, [type='hidden']", this).serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    var $radio = $('input[type=radio],input[type=checkbox]', this);
    $.each($radio, function () {
        o[this.name] = $(this).is(":checked");
        /*
        if (!o.hasOwnProperty(this.name)) {
            o[this.name] = '';
        }
        */
    });
    return o;
};