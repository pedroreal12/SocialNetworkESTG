$(document).ready(function() {
    var idTextAreas = 0
    var url = window.location.href;
    url = url.split("/");
    Id = url[5];

    imageUrl = "https://www.themoviedb.org/t/p/w220_and_h330_face"
    $.ajax({
        url: "/Discussion/GetDiscussionId/" + Id,
        type: "GET",
        success: function(data) {
            var content = JSON.parse(data)
            console.log(content)
            if (content.Id != undefined) {
                $.ajax({
                    url: "/Movies/GetMovieId/" + content.MovieId,
                    type: "GET",
                    success: function(data) {
                        var movie = JSON.parse(JSON.parse(data).Content);
                        console.log(movie)
                        if (movie.adult != undefined) {
                            $(".movieInformation").append("<p>" + movie.original_title + "</p><a href=\"" + movie.homepage + "\"><img src=\"" + imageUrl + movie.backdrop_path + "\"</img></a>")
                        }
                    }
                })
                $("#commentText").html(content.Text)
                $("#datePosted").html("Posted at: " + formatDate(content.DatePosted))
            } else {
                alert("No details about this discussion were found. Try refreshing this page later")
            }
        },
        error: function(error) {
            alert("Error " + error)
        }
    })

    $.ajax({
        url: "Comment/loadCommentsDiscussion/" + Id,
        type: "GET",
        success: function(data) {
            var comments = JSON.parse(data)
            comments.forEach(function(element) {
                //TODO: Add user created
                $(".discussionComments").append("<p>" + element.Text + " at " + element.DatePosted + "</p>")
            })
        },
        error: function(error) {
            alert("Error", error)
        }
    })

    $("#addComment").click(function() {
        idTextAreas += 1
        $(".discussionComments").append("<div class=\"row\"><textarea id=\"textarea_" + idTextAreas + "\"></textarea><button onClick=\"postComment(this.id)\" class=\"btn btn-primary\" id=\"postComment_" + idTextAreas + "\">Post Comment</button><button class=\"btn btn-danger\" id=\"removeComment_" + idTextAreas + "\"onClick=\"removeComment(this.id)\">Remove Comment</button></div>")
    })

})

function formatDate(dateObj) {
    var formattedDate = new Date(dateObj);
    var d = formattedDate.getDate();
    var m = formattedDate.getMonth();
    m += 1;  // JavaScript months are 0-11
    var y = formattedDate.getFullYear();
    var formatedDate = d + "/" + m + "/" + y

    return formatedDate
}

function postComment(idTextArea) {

    var data = {
        IdDiscussion: Id,
        Text: $("#text_area_" + idTextArea).val()
    }
    $.ajax({
        url: "/Comment/PostComment",
        type: "POST",
        data: data,
        success: function(){
            alert("Comment posted successfully!")
        },
        error: function(error){
            alert("Error: " + error)
        }
    })
}

function removeComment(idTextArea){
    console.log(idTextArea)
    idTextArea = idTextArea.split("_")
    $("#textarea_" + idTextArea[1]).remove()
    $("#postComment_" + idTextArea[1]).remove()
    $("#removeComment_" + idTextArea[1]).remove()
    $(".discussionComments").children("br").remove()
}
