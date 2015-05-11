$(".my-name a").click(function () {
    alert("veiiii");
    event.preventDefault();

    $.ajax({
        url: "OwnNameCards.cshtml",
    }).success(function (data) {
        $(".feed-body").empty();
        $('.feed-body').load('OwnNameCards.cshtml');



        $(".namecard").html();
        $('.feed-body').load('OwnNameCards.cshtml');
        $(this).addClass("done");
    });

    
});