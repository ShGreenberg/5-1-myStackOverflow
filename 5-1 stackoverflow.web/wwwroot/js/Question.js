$(() => {
    $("#like-button").on('click', function () {
        
        const id = $(this).data("id");
        $.post("/home/likequestion", { id }, function (numLikes) {
            if (numLikes != 0) {
                $("#likes-count").text(numLikes);
                $("#like-button").prop('disabled', true);
            }
        });
    });
});