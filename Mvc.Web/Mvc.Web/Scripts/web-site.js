$(function ($) {
    //    var cache = location.hash;
    //    //        .split(/#%0A/g).join(' ');
    //    //        .split(/\<.*\>/g).join();
    //    if (cache) {
    //        var container = $('#news-section .container');
    //        container.html('');
    //        container.append(cache);
    //    }

    var dates = $("#from, #to").datepicker({
        defaultDate: "+1w",
        changeMonth: true,
        numberOfMonths: 3,
        onSelect: function (selectedDate) {
            var option = this.id == "from" ? "minDate" : "maxDate",
                        instance = $(this).data("datepicker"),
                        date = $.datepicker.parseDate(
                            instance.settings.dateFormat ||
                                $.datepicker._defaults.dateFormat,
                            selectedDate, instance.settings);
            dates.not(this).datepicker("option", option, date);
        }
    });

    $('table.standart').zebra();

    $('table.changed-options').zebra({
        bgEven: '#CC66CC',
        bgOdd: '#E6ACE6',
        fontEven: '#662266',
        fontOdd: '#662266',
        bgHover: '#331133',
        fontHover: '#FFFFFF'
    });

    $('#smiles').click(function () {
        $('#news-section .container').load('smiles.htm', function (response, status, xhr) {
            if (status == "error") {
                var msg = "Sorry but there was an error: ";
                alert(msg + xhr.status + " " + xhr.statusText + '. Please check your connection or wait a little bit.');
            }
            if (status == "success") {
                setTimeout(function () {
                    //location.hash = $('#news').html();
                }, 100);
            }
        });
    });
    $('.close').live("click", function () {
        $(this).parent().parent().parent().remove();
        //location.hash = $('#news').html();
    });
    $(window).unload(function () {
        //location.hash = $('#news').html();
    });
})(jQuery);