(function($) {

    $.fn.postController = function (settings) {
        return new postController(this.get(0), settings);
    };

    function postController(elem, settings) {
        var $liPostContainer,
            $ulChildrenContainer,
            $replyContainer,
            content;
        
        $().ready(function () {
            $liPostContainer = $(elem);

            $ulChildrenContainer = $liPostContainer.children('ul.children');

            var $postContentContainer = $liPostContainer.children('.post-content');
            content = $postContentContainer.postContentController({ id: settings.id });

            content.bind('expand', function () {
                $liPostContainer.removeClass('collapsed');
            });
            
            content.bind('collapse', function () {
                $liPostContainer.addClass('collapsed');
            });

            content.bind('reply', function () {
                if (!$replyContainer) {
                    var url = 'http://' + document.location.host + '/en/Blog/ReplyTemplate/' + settings.id;

                    $.get(url, function (html) {
                        $replyContainer = $(html);
                        $ulChildrenContainer.prepend($replyContainer);

                        $replyContainer.replyController({ waterMarkText: 'Leave a message...' });
                    });
                }
                else {
                    $replyContainer.toggle();
                }
            });
        });
    }
})(jQuery);