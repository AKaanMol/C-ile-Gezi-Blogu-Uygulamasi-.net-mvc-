
$(function () {
    var noteids = [];  

   
    $("div[data-note-id]").each(function (i, e) {

        noteids.push($(e).data("note-id"));

    });
    $.ajax({
        method: "POST",
        url: "/Note/GetLiked/",
        data: { ids: noteids }
    }).done(function (data) {   
        if (data.result != null && data.result.length > 0) {
            for (var i = 0; i < data.result.length; i++) {
                var id = data.result[i]; 
                var likedNote = $("div[data-note-id=" + id + "]");

                var btn = likedNote.find("button[data-liked]");

                var span = btn.find("span.like-heart");

                btn.data("liked", true);
                span.removeClass("glyphicon-heart-empty");
                span.addClass("glyphicon-heart");

            }

        }

    }).fail(function () { });

    
    $("button[data-liked]").click(function () {
        
        var btn = $(this); 

        var liked = btn.data("liked"); 
        var noteid = btn.data("note-id"); 

       
        var spanHeart = btn.find("span.like-heart");
        var spanCount = btn.find("span.like-count");
       
        $.ajax({
            method: "POST",
            url: "/Note/SetNoteLike/",
            data: { "noteid": noteid, "liked": !liked }
        }).done(function (data) {
            if (data.hasError) {
                alert(data.errorMessage);
            } else {           
                liked = !liked
                btn.data("liked", liked);

                spanCount.text(data.result); 


                spanHeart.removeClass("glyphicon-heart-empty");
                spanHeart.removeClass("glyphicon-heart");
                if (liked) {
                    spanHeart.addClass("glyphicon-heart");
                }
                else {
                    spanHeart.addClass("glyphicon-heart-empty");
                }
            }
            
        }).fail(function () {
           
            alert("Gönderiyi beğenmek için sisteme giriş yapmalısınız.");
        });
    })
});

