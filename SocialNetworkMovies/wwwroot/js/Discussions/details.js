var commentsPage = 0
var idTextAreas = 0
$(document).ready(function() {
    var url = window.location.href;
    url = url.split("/");
    Id = url[5];

    imageUrl = "https://www.themoviedb.org/t/p/w220_and_h330_face"
    $.ajax({
        url: "/Discussion/GetDiscussionId/" + Id,
        type: "GET",
        success: function(data) {
            var content = JSON.parse(data)
            if (content.Id != undefined) {
                $.ajax({
                    url: "/Movies/GetMovieId/" + content.MovieId,
                    type: "GET",
                    success: function(data) {
                        var movie = JSON.parse(JSON.parse(data).Content);
                        if (movie.adult != undefined) {
                            $(".movieInformation").append("<p>" + movie.original_title + "</p><a href=\"" + movie.homepage + "\"><img src=\"" + imageUrl + movie.backdrop_path + "\"</img></a>")
                        }
                    }
                })
                $("#commentText").html(content.Text)
                $("#datePosted").html("Posted at: " + formatDate(content.DatePosted))
                $(".discussionOverview").append("<button class=\"btn btn-link\" id=\"replyComment_" + content.Id + "\">Reply</button>");
                $(".discussionOverview").append("<button class=\"btn btn-link\" id=\"postReply_" + content.Id + "\" onClick=\"postReply(this.id)\">Post reply</button>");
                loadMoreComments()
            } else {
                alert("No details about this discussion were found. Try refreshing this page later")
            }
        },
        error: function(error) {
            alert("Error " + error)
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
    idTextArea = idTextArea.split("_")

    var data = {
        IdDiscussion: Id,
        Text: $("#textarea_" + idTextArea[1]).val()
    }
    console.log("Text: " + data.Text)
    $.ajax({
        url: "/Comment/PostComment",
        type: "POST",
        data: data,
        success: function(data) {
            var content = JSON.parse(data)
            if (content.data) {
                alert("Comment posted successfully!")
            } else {
                alert("Error on posting comment. Try this later")
            }
        },
        error: function(error) {
            alert("Error: " + error)
        }
    })
}

function removeComment(idTextArea) {
    idTextArea = idTextArea.split("_")
    $("#textarea_" + idTextArea[1]).remove()
    $("#postComment_" + idTextArea[1]).remove()
    $("#removeComment_" + idTextArea[1]).remove()
    $(".discussionComments").children("br").remove()
}

function loadMoreComments() {
    $.ajax({
        url: "/Comment/loadCommentsDiscussion/?Id=" + Id + "&&Pagination=" + commentsPage,
        type: "GET",
        success: function(data) {
            var comments = JSON.parse(data)
            if (comments.length > 0) {
                comments.forEach(function(element) {
                    //TODO: Add user created
                    $(".discussionComments").append("<div class=\"row\"><textarea disabled=\"disabled\">" + element.TextComment + "</textarea> at " + formatDate(element.DatePosted) + "</div>");
                    $(".discussionComments").append("<button class=\"btn btn-link\" id=\"replyComment_" + element.IdComment + "\" onClick=\"replyComment(this.id)\">Reply</button>");
                    $(".discussionComments").append("<button class=\"btn btn-link\" id=\"postReply_" + element.IdComment + "\" onClick=\"postReply(this.id)\">Post reply</button>");
                })
                $(".discussionComments").append("<div class=\"row\"><button class=\"btn btn-secondary loadMoreComments\">Load more Comments</button></div")
                $(".loadMoreComments").click(loadMoreComments)
                commentsPage += 1
            } else {
                alert("There are no more comments on this discussion. Try refreshing later")
            }
        },
        error: function(error) {
            alert("Error", error)
        }
    })
}
