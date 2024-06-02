// Changes the size of select elements to dynamically fit
// the currently selected option
(function ($, window) {
    $(function () {

        $.fn.resizeselect = function (settings) {
            return this.each(function () {

                $(this).on("change", function () {
                    let $this = $(this);

                    // get font-weight, font-size, and font-family
                    let style = window.getComputedStyle(this)
                    let { fontWeight, fontSize, fontFamily } = style

                    // create test element
                    let text = $this.find("option:selected").text();
                    let $test = $("<span>").html(text).css({
                        "font-size": fontSize,
                        "font-weight": fontWeight,
                        "font-family": fontFamily,
                        "visibility": "hidden" // prevents FOUC
                    });

                    // add to body, get width, and get out
                    $test.appendTo($this.parent());
                    let width = $test.width();
                    $test.remove();

                    // set select width
                    $this.width(width);

                    // run on start
                }).trigger("change");

            });
        };

        // run by default
        $("select.resizeselect").resizeselect();
    })
})(jQuery, window);