$(() => {
    $("#like-button").on('click', function () {
        
        const id = $(this).data("id");
        console.log(this);
        $.post("/home/likequestion", { id }, function (numLikes) {
            if (numLikes != 0) {
                $("#likes-count").text(numLikes);
                //$("#like-button").prop("disabled", true);
                $("#like-button").prop('disabled', true);
            }
            //const count = $("like-count").val;
            //$("like-count").val(count + 1);
        });
    });
});