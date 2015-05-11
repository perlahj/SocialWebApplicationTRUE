$(".my-name a").click(function () {
    alert("veiiii");
    event.preventDefault();
    
    $.ajax({
        url: "OwnNameCards.cshtml",
    }).success(function (data) {
        $(".namecard").html();
        $(this).addClass("done");
    });

});