

modelComponentBodyId = '#modal_yorum_body';
var noteId = -1;
$(function () {
    $('#modal_yorum').on('show.bs.modal', function (e) {
        var btn = $(e.relatedTarget);
        noteId = btn.data("note-id");
        $('#modal_yorum_body').load('/Comment/ShowNoteComments/' + noteId)
    })
});

function doComment(btn, eventMode, commentId, spanId) {
    var button = $(btn);
    var mode = button.data("edit-mode");
    if (eventMode === "edit-clicked") {
        if (!mode) {
            button.data("edit-mode", true);
            button.removeClass("btn-warning");
            button.addClass("btn-success");

            var btnSpan = button.find("span");
            btnSpan.removeClass("glyphicon-edit");
            btnSpan.addClass("glyphicon-ok");

            $(spanId).attr("contenteditable", true);
            $(spanId).addClass("editable");
            $(spanId).focus();
        }
        else {
            button.data("edit-mode", false);
            button.removeClass("btn-success");
            button.addClass("btn-warning");

            var btnSpan = button.find("span");
            btnSpan.removeClass("glyphicon-ok");
            btnSpan.addClass("glyphicon-edit");

            $(spanId).attr("contenteditable", false);
            $(spanId).removeClass("editable");


            var txt = $(spanId).text();
            $.ajax({
                method: "POST",
                url: "/Comment/Edit/" + commentId,
                data: { text: txt }
            }).done(function (data) {
                if (data.result) {

                    $(modelComponentBodyId).load('/Comment/ShowNoteComments/' + noteId);

                } else {
                    alert("Yorum güncellenemedi.");
                }


            }).fail(function () {
                alert("Sunucuya baðlanamadý.");

            });
        }
    }

    else if (eventMode === 'delete-clicked') {
        var dialogResult = confirm("Yorum silinsin mi?")
        if (!dialogResult) return false;


        $.ajax({
            method: "GET",
            url: "/Comment/Delete/" + commentId
        }).done(function (data) {
            if (data) {
                $(modelComponentBodyId).load('/Comment/ShowNoteComments/' + noteId);
            }
            else {
                alert("Yorum silinemedi.")
            }


        }).fail(function () {
            alert("Sunucuya baðlanamadý. Silme iþlemi iptal edildi.");

        });
    }
    else if (eventMode === 'new-clicked') {

        var txt = $('#new_comment_text').val();

        $.ajax({
            method: "POST",
            url: "/Comment/Create/",
            data: { "Text": txt, "noteId": noteId }

        }).done(function (data) {


            if (data) {
                $(modelComponentBodyId).load('/Comment/ShowNoteComments/' + noteId);
            }
            else {
                alert("Yorum eklenemedi.")
            }


        }).fail(function () {
            alert("Sunucuya baðlanamadý. Yorum ekleme iþlemi iptal edildi.");

        });
    }
}
