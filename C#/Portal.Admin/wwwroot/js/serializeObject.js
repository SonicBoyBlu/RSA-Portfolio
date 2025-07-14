$.fn.serializeObject = function () {
    var o = {};
    //var a = this.serializeArray();
    var a = $(".form-control, [type='hidden']", this).serializeArray();
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
    var $chk = $('input[type=checkbox]', this);
    $.each($chk, function () {
        o[this.name] = $(this).is(":checked");
        /*
        if (!o.hasOwnProperty(this.name)) {
            o[this.name] = '';
        }
        */
    });
    var $radio = $('input[type=radio]', this);
    $.each($radio, function () {
        if($(this).is(":checked"))
            o[this.name] = $(this).val();
        /*
        if (!o.hasOwnProperty(this.name)) {
            o[this.name] = '';
        }
        */
    });
    return o;
};