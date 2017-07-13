$(function () {
    $('.menuDiv').prepend('<div class="bar"></div>');
    $('.modern-menu li ul').parent().children('a').addClass('sub-menu');
    $('.modern-menu > li > ul').parent().children('a').addClass('down');

    $('.modern-menu li').hover(function () {
        if ($(this).children('ul').length > 0) {
            var parent = $(this);
            var submenu = $(this).children('ul')

            if (submenu.css('width') == 'auto')
                submenu.children('li').css({ 'width': submenu.width() + 10 + 'px' });

            if (submenu.parents('ul').length < 2)
                submenu.css({
                    'margin-top': '-20px', 'left': parent.position().left + 'px', 'max-width': parent.width() + 'px'
                }).animate({ 'opacity': 'toggle', 'margin-top': '0' }, 100);

            else
                submenu.css({
                    'left': parent.position().left + parent.width() - 15 + 'px', 'margin-top': -parent.height() + 'px', 'max-width': parent.width() + 'px'
                }).animate({
                    'opacity': 'toggle', 'left': parent.position().left + parent.width() - 1 + 'px'
                }, 200);
        }
    }, function () {
        $(this).children('ul').hide();
    });
});