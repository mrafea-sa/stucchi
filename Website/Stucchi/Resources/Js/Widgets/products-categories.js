$(document).ready(function () {
    initLoadMoreEngine();
});

function initLoadMoreEngine() {

    $('.custom-solutions .button-white').on('click', function (e) {
        identifier = $(this).data("identifier");
        step = parseInt($(this).data("step")) + 1;

        rowElm = "." + identifier + " .idx" + step;
        nextRowElm = "." + identifier + " .idx" + (step + 1);

        $("" + rowElm).removeClass("hidden");
        if (!$("" + nextRowElm).length) $(this).hide();

        $(this).data("step", step);
    });

}
