function waterMark(settings) {
    var $elem,
        $result = $({ });
    
    function init() {
        if ($elem.html().toString().trim() == '')
            show();
    }

    function setCaret() {
        var helper = new caretPositionHelper();
        helper.setPosition($elem[0], 0);
    }
    
    function show() {
        $elem.empty();
        $elem.append($('<span>').addClass('placeholder').text(settings.text));

        $result.trigger('show');
    }
    
    function hide() {
        $elem.empty();
        
        if (settings.blankTemplateHtml)
            $elem.html(settings.blankTemplateHtml);
        
        setCaret();
        
        $result.trigger('hide');
    }
    
    function isVisible() {
        var childTagName = $elem.children().prop('tagName');

        return childTagName == 'SPAN';
    }
    
    $().ready(function () {

        $elem = settings.elem;
            
        $elem.focus(function () {
            if (isVisible())
                hide();
        });

        $elem.blur(function () {
            var $child = $elem.children();
            
            if ($child.length == 0 || $elem.html() == settings.blankTemplateHtml)
                show();
        });

        init();
    });

    $result.extend({
        show: show,
        hide: hide,
        isVisible: isVisible
    });

    return $result;
}