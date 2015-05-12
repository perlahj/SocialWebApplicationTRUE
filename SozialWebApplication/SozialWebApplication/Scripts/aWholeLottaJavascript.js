$(".my-name a").click(function () {
    event.preventDefault();
    $.ajax({
        url: "/NameCards/OwnNameCard",
    }).success(function (data) {
        debugger;
        $(".feed-body").empty();
        $('.feed-body').html(data);
    });
});

/*$("#prufa").click(function () {
    alert("veiiii");
    event.preventDefault();

    $.ajax({
        url: "/NameCards/EditNameCard",
    }).success(function (data) {
        debugger;
        $(".feed-body").empty();
        $('.feed-body').html(data);
    });
});*/

$.ajax({
    url: "/Home/NewsfeedGroups",
}).success(function (data) {
    debugger;
    $(".feed-body").empty();
    $('.feed-body').html(data);
});