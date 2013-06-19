function caretPositionHelper() {
    var me = caretPositionHelper.instance = caretPositionHelper.instance || {
        getPosition: function(el, focus) {
            //For chrome/safari/ie9/modern browsers
            if (typeof el.selectionStart != 'undefined')
                return el.selectionStart;

            //For ie7,ie8, etc
            if (document.selection) {

                if (focus)
                    el.focus();

                var r = document.selection.createRange();
                if (r == null) return 0;

                var re = el.createTextRange(),
                    rc = re.duplicate();
                re.moveToBookmark(r.getBookmark());
                rc.setEndPoint('EndToStart', re);

                return rc.text.replace(/\n/g, "").length; //do not count line breaks in the length
            }
        },

        setPosition: function(el, pos) {
            if (el.setSelectionRange) {
                el.focus();
                el.setSelectionRange(pos, pos);
            } else if (el.createTextRange) {
                var r = el.createTextRange();
                r.collapse(true);
                r.move('character', pos);
                r.select();
            } else {
                var range = document.createRange();
                var sel = window.getSelection();
                
                range.setStart(el.childNodes[0], pos);
                range.collapse(true);
                sel.removeAllRanges();
                sel.addRange(range);
                el.focus();
            }
        }
    };
    
    return me;
};