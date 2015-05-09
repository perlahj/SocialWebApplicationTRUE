$(".my-name a").click(function () {
    alert("veiiii");
    event.preventDefault();
    
    $.ajax({
        url: "test.html",
    }).success(function (data) {
        $(".namecard").html();
        $(this).addClass("done");
    });

});