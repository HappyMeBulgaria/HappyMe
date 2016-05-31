function hideElement() {
    document.getElementById("button-menu").style.display = "none";
}

function showElement() {
    document.getElementById("nav-hidden").style.display = "inline-block";
}

$(function () {
    $(".scroll").click(function (event) {
        event.preventDefault();
        var dest = 0;
        if ($(this.hash).offset().top > $(document).height() - $(window).height()) {
            dest = $(document).height() - $(window).height();
        } else {
            dest = $(this.hash).offset().top;
        }
        $('html,body').animate({
            scrollTop: dest
        }, 1000, 'swing');
    });
});

