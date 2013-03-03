(function ($) {
    $.fn.Dropdown = function (options) {
        var defaults = {
        };

        var $this = $(this)
            ;

        var clearDropdown = function () {
            $(".dropdown-menu").each(function () {
                if ($(this).css('position') == 'static') return;
                $(this).slideUp('fast', function () { });
                $(this).parent().removeClass("active");
            });
        }

        var initSelectors = function (selectors) {
            selectors.on('click', function (e) {
                e.stopPropagation();
                //$("[data-role=dropdown]").removeClass("active");

                clearDropdown();
                $(this).parents("ul").css("overflow", "visible");

                var $m = $(this).children(".dropdown-menu, .sidebar-dropdown-menu");
                if ($m.css('display') == "block") {
                    $m.slideUp('fast');
                    $(this).removeClass("active");
                } else {
                    $m.slideDown('fast');
                    $(this).addClass("active");
                }
            }).on("mouseleave", function () {
                //$(this).children(".dropdown-menu").hide();
            });
            $('html').on("click", function (e) {
                clearDropdown();
            });
        }

        return this.each(function () {
            if (options) {
                $.extend(defaults, options)
            }

            initSelectors($this);
        });
    }

    $(function () {
        $('[data-role="dropdown"]').each(function () {
            $(this).Dropdown();
        })
    })
})(window.jQuery);


(function ($) {
    $.fn.PullDown = function (options) {
        var defaults = {
        };

        var $this = $(this)
            ;

        var initSelectors = function (selectors) {

            selectors.on('click', function (e) {
                e.preventDefault();
                var $m = $this.parent().children("ul");
                //console.log($m);
                if ($m.css('display') == "block") {
                    $m.slideUp('fast');
                } else {
                    $m.slideDown('fast');
                }
                //$(this).toggleClass("active");
            });
        }

        return this.each(function () {
            if (options) {
                $.extend(defaults, options)
            }

            initSelectors($this);
        });
    }

    $(function () {
        $('.pull-menu').each(function () {
            $(this).PullDown();
        })
    })
})(window.jQuery);



/**
* jQuery plugin for input elements for Metro UI CSS framework
*/
(function ($) {

    $.Input = function (element, options) {

        var defaults = {
        };

        var plugin = this;
        plugin.settings = {};
        var $element = $(element);

        plugin.init = function () {
            plugin.settings = $.extend({}, defaults, options);

            if ($element.hasClass('text')) {
                initTextInput();
            } else if ($element.hasClass('password')) {
                initPasswordInput();
            }
        };

        /**
        * initialize text input element behavior
        */
        var initTextInput = function () {
            var helper,
                $helper,
                input;
            helper = $element.children('.helper').get(0);

            if (!helper) {
                return;
            }

            $helper = $(helper);

            // clear text when clock on helper
            $helper.on('click', function () {
                input = $element.children('input');
                input.attr('value', '');
                input.focus();
            });
        };

        /**
        * initialize password input element behavior
        */
        var initPasswordInput = function () {
            var helper,
                $helper,
                password,
                text;
            helper = $element.children('.helper').get(0);
            if (!helper) {
                return;
            }

            text = $('<input type="text" />');
            password = $element.children('input');
            $helper = $(helper);

            // insert text element and hode password element when push helper
            $helper.on('mousedown', function () {
                password.hide();
                text.insertAfter(password);
                text.attr('value', password.attr('value'));
            });

            // return password and remove text element
            $helper.on('mouseup, mouseout', function () {
                text.detach();
                password.show();
                password.focus();
            });
        };

        plugin.init();

    };

    $.fn.Input = function (options) {
        return this.each(function () {
            if (undefined == $(this).data('Input')) {
                var plugin = new $.Input(this, options);
                $(this).data('Input', plugin);
            }
        });
    }

})(jQuery);

$(function () {
    var allInputs = $('.input-control');
    allInputs.each(function (index, input) {
        var params = {};
        $input = $(input);

        $input.Input(params);
    });
});



(function ($) {
    $.fn.PageControl = function (options) {
        var defaults = {
        };

        var $this = $(this)
            , $ul = $this.children("ul")
            , $selectors = $ul.find("li a")
            , $selector = $ul.find(".active a")
            , $frames = $this.find(".frames .frame")
            , $frame = $frames.children(".frame.active")
            ;

        var initSelectors = function (selectors) {
            selectors.on('click', function (e) {
                e.preventDefault();
                var $a = $(this);
                if (!$a.parent('li').hasClass('active')) {
                    $frames.hide();
                    $ul.find("li").removeClass("active");
                    var target = $($a.attr("href"));
                    target.show();
                    $(this).parent("li").addClass("active");
                }
                if ($(this).parent("li").parent("ul").parent(".page-control").find(".menu-pull-bar").is(":visible")) {
                    $(this).parent("li").parent("ul").slideUp("fast", function () {
                        $(this).css("overflow", "").css("display", "");
                    });
                }
            });

            $(".page-control .menu-pull-bar").text($(".page-control ul li.active a").text());
            $(".page-control ul li a").click(function (e) {
                e.preventDefault();
                $(this).parent("li").parent("ul").parent(".page-control").find(".menu-pull-bar").text($(this).text());
            });
        }

        return this.each(function () {
            if (options) {
                $.extend(defaults, options)
            }

            initSelectors($selectors);
        });
    }

    $(function () {
        $('[data-role="page-control"]').each(function () {
            $(this).PageControl();
        })
        $(window).resize(function () {
            if ($(window).width() >= 768) {
                $(".page-control ul").css({
                    display: "block"
                    , overflow: "visible"
                })
            }
            if ($(window).width() < 768 && $(".page-control ul").css("display") == "block") {
                $(".page-control ul").hide();
            }
        })
    })
})(window.jQuery);