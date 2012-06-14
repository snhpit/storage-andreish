(function ($) {
    $.fn.zebra = function (options) {
        var options = $.extend({
            bgEven: '#FFC080',
            bgOdd: '#FFDFBF',
            fontEven: '#AA7239',
            fontOdd: '#AA7239',
            bgHover: '#FF8000',
            fontHover: '#55391C'
        }, options);

        return this.each(function () {
            $(this).find('tbody tr:even')
                .css('background-color', options.bgEven)
                .css('color', options.fontEven)
                .hover(
                function () {
                    $(this).css('background-color', options.bgHover)
                        .css('color', options.fontHover);
                },
                function () {
                    $(this).css('background-color', options.bgEven)
                        .css('color', options.fontEven);
                }
		            );

            $(this).find('tbody tr:odd')
                .css('background-color', options.bgOdd)
                .css('color', options.fontOdd)
                .hover(
                function () {
                    $(this).css('background-color', options.bgHover)
                        .css('color', options.fontHover);
                },
                function () {
                    $(this).css('background-color', options.bgOdd)
                        .css('color', options.fontOdd);
                }
                    );
        });
    };
})(jQuery);