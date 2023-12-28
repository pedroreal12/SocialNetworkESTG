var PaginationReplies = []
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
        $(".discussionComments").append("<div class=\"row\"><textarea id=\"textareaComment_" + idTextAreas + "\"></textarea><button onClick=\"postComment(this.id)\" class=\"btn btn-primary\" id=\"postComment_" + idTextAreas + "\">Post Comment</button><button class=\"btn btn-danger\" id=\"removeComment_" + idTextAreas + "\"onClick=\"removeComment(this.id)\">Remove Comment</button></div>")
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

function replyComment(idReply) {
    idReply = idReply.split("_")[1]

    $("#postReply_" + idReply).removeAttr("hidden")
    $("#replyComment_" + idReply).attr("hidden", "hidden")
    $("#commentSection_" + idReply).append("<div class=\"row\"><textarea id=\"textarea_" + idReply + "\"></textarea><button id=\"removeReply_" + idReply + "\"class=\"btn btn-danger\" onClick=\"removeReply(" + idReply + ")\">Remove Reply</button></div>")
    idTextAreas += 1
}

function postComment(idTextArea) {
    idTextArea = idTextArea.split("_")

    var data = {
        IdDiscussion: Id,
        Text: $("#textareaComment_" + idTextArea[1]).val()
    }
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
    $("#textareaComment_" + idTextArea[1]).remove()
    $("#postComment_" + idTextArea[1]).remove()
    $("#removeComment_" + idTextArea[1]).remove()
    $(".discussionComments").children("br").remove()
}

function removeReply(idReply) {
    $("#textarea_" + idReply).remove()
    $("#removeReply_" + idReply).remove()
    $("#replyComment_" + idReply).removeAttr("hidden")
    $("#postReply_" + idReply).attr("hidden", "hidden")
}

function loadMoreComments() {
    $.ajax({
        url: "/Comment/loadCommentsDiscussion/?Id=" + Id + "&&Pagination=" + commentsPage,
        type: "GET",
        success: function(data) {
            var content = JSON.parse(data)
            var comments = content.Comments
            var replies = content.Replies
            if (comments.length > 0 && replies.length > 0) {
                comments.forEach(function(comment) {
                    //TODO: Add user created
                    var html = "<div id=\"commentSection_" + comment.IdComment + "\">"
                    html += "<div class=\"row\">"
                    html += "<div class=\"row\"><textarea disabled=\"disabled\">" + comment.TextComment + "</textarea> At " + formatDate(comment.DatePosted) + "</div>";
                    if (replies.length > 0) {
                        replies.forEach(function(reply) {
                            if ((reply.IdCommentParent == comment.IdComment)) {
                                html += "<button class=\"btn btn-link\" id=\"showReplies_" + comment.IdComment + "\" onClick=\"showReplies(this.id)\">Show Replies</button>"
                                html += "<button class=\"btn btn-link\" id=\"hideReplies_" + comment.IdComment + "\" hidden=\"hidden\" onClick=\"hideReplies(this.id, " + reply.IdComment + ")\">Hide Replies</button>"
                                PaginationReplies[comment.IdComment] = 0
                            }
                        })
                    }
                    html += "<button class=\"btn btn-link\" id=\"replyComment_" + comment.IdComment + "\" onClick=\"replyComment(this.id)\">Reply</button>";
                    html += "<button class=\"btn btn-link\" id=\"postReply_" + comment.IdComment + "\" onClick=\"postReply(this.id)\" hidden=\"hidden\">Post reply</button>";
                    html += "</div></div>"
                    $(".discussionComments").append(html)
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

function postReply(idReply) {
    idReply = idReply.split("_")[1]
    var data = {
        IdDiscussion: Id,
        FkIdCommentParent: idReply,
        Text: $("#textarea_" + idReply).val()
    }
    $.ajax({
        url: "/Comment/PostReply",
        type: "POST",
        data: data,
        success: function(data) {
            var content = JSON.parse(data)
            console.log(content)
            if (content.success != "false") {
                alert("Reply Posted Successfuly")
            } else {
                alert("Error on posting the reply. Try this later")
            }
        },
        error: function(error) {
            alert("Error: " + error)
        }
    })
}

function showReplies(idCommentParent) {
    idCommentParent = idCommentParent.split("_")[1]
    $.ajax({
        url: "/Comment/GetReplies/?IdDiscussion=" + Id + "&&IdCommentParent=" + idCommentParent + "&&Pagination=" + PaginationReplies[idCommentParent],
        type: "GET",
        success: function(data) {
            var replies = JSON.parse(data)
            if (replies.length > 0) {
                var map = new Map()
                replies.forEach(function(reply) {
                    map.set(reply.IdComment, reply.IdCommentParent)
                    var html = "<div id=\"commentSection_" + reply.IdComment + "\"><div class=\"row\"><textarea disabled=\"disabled\">" + reply.TextComment + "</textarea><p> At " + formatDate(reply.DatePosted) + "</p>"
                    html += "<button class=\"btn btn-link\" id=\"replyComment_" + reply.IdComment + "\" onClick=\"replyComment(this.id)\">Reply</button>";
                    html += "<button class=\"btn btn-link\" id=\"postReply_" + reply.IdComment + "\" onClick=\"postReply(this.id)\" hidden=\"hidden\">Post reply</button>";
                    html += "</div></div></div></div>"
                    $("#commentSection_" + idCommentParent).append(html)
                    $("#showReplies_" + idCommentParent).attr("hidden", "hidden")
                    $("#hideReplies_" + idCommentParent).removeAttr("hidden")
                    map.forEach(function(...data) {
                        console.log("Whole Data: " + data)
                        console.log("Data[0]" + data[0])
                        console.log("Data[1]" + data[1])
                        var htmlReplies = "<div class=\"row\"><button class=\"btn btn-link\" id=\"showReplies_" + data[0] + "\" onClick=\"showReplies(this.id)\">Show Replies</button>"
                        htmlReplies += "<button class=\"btn btn-link\" id=\"hideReplies_" + data[0] + "\" hidden=\"hidden\" onClick=\"hideReplies(this.id, " + data[1] + ")\">Hide Replies</button>"
                        htmlReplies += "</div>"
                        $("#commentSection_" + data[0]).append(htmlReplies)
                    })
                })
                //PaginationReplies[idCommentParent] += 1
            } else {
                alert("Error on loading these replies. Try again later")
            }
        },
        error: function(error) {
            alert("Error: " + error)
        }
    })
}

function hideReplies(idCommentParent, idReply = -1) {
    idCommentParent = idCommentParent.split("_")[1]
    PaginationReplies[idCommentParent] -= 1
    $("#hideReplies_" + idCommentParent).attr("hidden", "hidden")
    $("#showReplies_" + idCommentParent).removeAttr("hidden")
    if (idReply != -1) {
        $("#commentSection_" + idReply).children("").remove()
        $("#commentSection_" + idReply).remove()
    }
}
