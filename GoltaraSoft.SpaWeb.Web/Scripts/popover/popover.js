$(function () {
    $('[data-toggle="popover"]').popover();
    setTimeout(function () {
        $('[data-toggle="popover"]').popover('hide');
    }, 300)
})

$(document).ready(function () {
    $('[data-toggle="popover"]').popover('hide');
});