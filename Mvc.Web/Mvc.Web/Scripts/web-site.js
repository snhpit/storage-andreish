$(function () {
    (function ($) {
        //    var cache = location.hash;
        //    //        .split(/#%0A/g).join(' ');
        //    //        .split(/\<.*\>/g).join();
        //    if (cache) {
        //        var container = $('#news-section .container');
        //        container.html('');
        //        container.append(cache);
        //    }

        $("#radio").buttonset();

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

        $('#smiles').on("click", function () {
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

        var tableZebra = function () {
            $('table.standart').zebra();

            $('table.changed-options').zebra({
                bgEven: '#CC66CC',
                bgOdd: '#E6ACE6',
                fontEven: '#662266',
                fontOdd: '#662266',
                bgHover: '#331133',
                fontHover: '#FFFFFF'
            });
        };

        var onSuccess = function (data) {
            var dataWrapper = { values: data };
            $('#finance-historical').empty();

            if (dataWrapper.values !== null) {
                $("#tableTemplate").tmpl(dataWrapper).appendTo("#finance-historical");
                $("#pager").css("display", "block");
                $("#finance-historical > table").addClass("tablesorter standart");
                $("table")
                .tablesorter({ widthFixed: false, widgets: ['zebra'], cssAsc: "headerSortUp", cssDesc: "headerSortDown", cssHeader: "header" })
                .tablesorterPager({ container: $("#pager"), size: $(".pagesize option:selected").val() });
                //tableZebra();
            }
            //            var angle = 0;
            //            setInterval(function () {
            //                angle += 3;
            //                $("div.loader").rotate(angle);
            //            }, 50);
        };

        var showLoader = function () {
            $('div.loader').show();
        };

        var hideLoader = function () {
            $('div.loader').hide();
        };

        var setCookies = function (inputInfo) {
            var data = [inputInfo.Company, inputInfo.DateFrom, inputInfo.DateTo, inputInfo.Provider].join("|");
            $.cookie("finance", data);
        };

        var ajaxGetData = function (e) {
            e.preventDefault();
            var inputInfo = {
                Company: $('#company').val(),
                DateFrom: $('#from').val(),
                DateTo: $('#to').val(),
                Provider: $('#financeForm input:radio:checked').val()
            };
            setCookies(inputInfo);
            $.ajax({
                url: '/',
                type: 'POST',
                data: JSON.stringify(inputInfo),
                dataType: 'json',
                processData: false,
                contentType: 'application/json; charset=utf-8',
                success: onSuccess
            });
        };

        var getCookies = function () {
            var financeCookies = $.cookie("finance");
            if (financeCookies) {
                financeCookies = financeCookies.split("|");
                $("#company").val(financeCookies[0]);
                $("#from").val(financeCookies[1]);
                $("#to").val(financeCookies[2]);
                var $radioButtons = $("#radio");
                $radioButtons.find("input:radio").each(function () {
                    var $radio = $(this);
                    if ($radio.val() === financeCookies[3]) {
                        var val = $radio.prop("id");
                        $radio.attr("checked", "true");
                        $radioButtons.find("label").each(function () {
                            var $label = $(this);
                            if ($label.attr("for") === val) {
                                $label.addClass("ui-state-active").attr("aria-pressed", "true");
                            }
                        });
                    };
                });
            }
        };

        $('form').submit(function (e) {
            ajaxGetData(e);
        });
        $('form').ajaxStart(function () { showLoader(); });
        $('form').ajaxStop(function () { hideLoader(); });
        $("#getCookies").on("click", getCookies());

        $("#financeForm").validate();
    } (jQuery));
});