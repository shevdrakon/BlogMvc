(function ($) {

    $.fn.postContentController = function (settings) {
        return new postContentController(this.get(0), settings);
    };

    function postContentController(elem, settings) {
        var $container,
            $liMenuCollapse,
            $liMenuExpand,
            $aReply,
            $result = $({ });

        $().ready(function () {
            $container = $(elem),
            $liMenuCollapse = $container.find('li.collapse'),
            $liMenuExpand = $container.find('li.expand'),
            $aReply = $container.find('a[data-action=reply]');

            $liMenuCollapse.click(function (e) {
                e.preventDefault();

                $result.trigger('collapse');

                $liMenuCollapse.hide();
                $liMenuExpand.show();
            });

            $liMenuExpand.click(function (e) {
                e.preventDefault();

                $result.trigger('expand');
                
                $liMenuCollapse.show();
                $liMenuExpand.hide();
            });

            $aReply.click(function (e) {
                e.preventDefault();

                $result.trigger('reply');
            });
        });

        return $result;
    }
})(jQuery);