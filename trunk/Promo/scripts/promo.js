(function($){
    var counter = 0;
    var canSlide = true;
    var dataInfo = null;
    var numberOfSlides = 0;

    $.fn.slide = function(data, time) {
        dataInfo = data;
        numberOfSlides = data.length;
        addTempElement();
        slide(numberOfSlides);
    };

    $('ul.slideshow-buttons li').live('click', function(){
        canSlide = false;
        counter = $(this).index();
        hoverNavigationButton(counter);
        var slideShow = $("ul.slideshow");
        slideShow.animate({left: -parseInt(slideShow.children().first().css('width')) * counter + 'px'}, 1000);
    });


    $('.slideshow-wrap').mouseover(function(){
        $('.nav-play').css('opacity', '0.6');
    }).mouseout(function(){
            $('.nav-play').css('opacity', '0');
        });

    $('.nav-play').live('click', function(){
        if (canSlide) { return; }
        $('.nav-right').click();
        canSlide = true;
        setTimeout(function() { slide(7) }, 1000);
    });

    $('.nav-right').live('click', function(){
        canSlide = false;
        counter++;
        var slideShow = $("ul.slideshow");
        slideShow.animate({left: -parseInt(slideShow.children().first().css('width')) * counter + 'px'}, 1000);
        if (counter === numberOfSlides) {
            counter = 0;
            slideShow.animate({left: '0px'}, 0);
        }
        hoverNavigationButton(counter);
    });

    $('.nav-left').live('click', function(){
        canSlide = false;
        var slideShow = $("ul.slideshow");
        if (counter === 0) {
            counter = numberOfSlides;
            slideShow.animate({left: -parseInt(slideShow.css('width')) + 920 + 'px'}, 0);
        }
        counter--;
        slideShow.animate({left: -parseInt(slideShow.children().first().css('width')) * counter + 'px'}, 1000);
        hoverNavigationButton(counter);
    });

    function changeLeftImage() {
        var index = counter === 0 ? numberOfSlides : counter;
        $('.nav-left').children().last().attr('src', 'pic/' + dataInfo[index - 1].picture);
    }

    function changeRightImage() {
        var index = counter === numberOfSlides - 1 ? 0 : counter + 1;
        $('.nav-right').children().last().attr('src', 'pic/' + dataInfo[index].picture);
    }

    /**
     * adding first element to the end of collection of slides for cyclic animation
     */
    function addTempElement() {
        var slideshow = $("ul.slideshow");
        slideshow.append(slideshow.children().first().clone());
    }

    function hoverNavigationButton(index) {
        var liCollection = $('ul.slideshow-buttons li');
        liCollection.parent().find('.current').removeClass('current');
        liCollection.eq(index).addClass('current');
        changeLeftImage();
        changeRightImage();
    }

    // 4 times repeated $("ul.slideshow") may be declare a var?
    function slide(numberOfSlides) {
        hoverNavigationButton(counter);
        setTimeout(function (){
            if (!canSlide) { return; }
            counter++;
            var slideShow =  $("ul.slideshow");
            slideShow.animate({left: -parseInt(slideShow.children().first().css('width')) * counter + 'px'}, 1000); // not good, yep?
            if (counter == numberOfSlides) {
                counter = 0;
                //slideShow.animate({left: '0px'}, 0);
                $("ul.slideshow").css('left', 0); // why it doesn't work?
            }
            slide(numberOfSlides);
        }, 4000);
    }
})(jQuery);