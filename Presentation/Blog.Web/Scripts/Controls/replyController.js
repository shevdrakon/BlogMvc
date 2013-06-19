(function($) {

    $.fn.replyController = function (settings) {
        return new replyController(this.get(0), settings);
    };

    function replyController(elem, settings) {
        var $container,
            $textarea,
            $waterMark;

        $().ready(function() {
            $container = $(elem);
            $textarea = $container.find('div .textarea');

            $waterMark = new waterMark({
                text: settings.waterMarkText,
                elem: $textarea,
                blankTemplateHtml: '<p><br></p>'
            });

            $waterMark.bind('show', function() {
                $container.removeClass('focus');
            });

            $waterMark.bind('hide', function() {
                $container.addClass('expanded');
                $container.addClass('focus');
            });

            var actions = $container.find('a[data-action]');
            actions.each(function(index, a) {
                var $a = $(a);
                var action = $a.data('action');

                $a.click(function(e) {
                    e.preventDefault();

                    document.execCommand(action, false, '');
                });
            });
        });
    }
})(jQuery);