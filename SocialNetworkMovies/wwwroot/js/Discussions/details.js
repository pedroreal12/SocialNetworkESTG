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
            var user = data.user;
            var discussion = data.discussion
            if (user !== undefined && discussion !== undefined) {
                $.ajax({
                    url: "/Movies/GetMovieId/" + discussion.movieId,
                    type: "GET",
                    success: function(data) {
                        var movie = JSON.parse(data.content)
                        if (movie.adult != undefined) {
                            $(".movieInformation").append("<p>" + movie.original_title + "</p><a href=\"" + movie.homepage + "\"><img src=\"" + imageUrl + movie.backdrop_path + "\"</img></a>")
                        }
                    }
                })
                $("#commentText").html(discussion.text)
                $("#datePosted").html("Posted at: " + formatDate(discussion.datePosted))
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
        TextComment: $("#textareaComment_" + idTextArea[1]).val()
    }
    $.ajax({
        url: "/Comment/PostComment",
        type: "POST",
        data: data,
        success: function(data) {
            var content = JSON.parse(data)
            if (content.success) {
                alert("Comment posted successfully!")
                location.reload()
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
            if ((data.comments !== undefined || data.replies !== undefined) && data.user !== undefined) {
                var comments = data.comments
                var replies = data.replies
                var user = data.user
                if (comments.length > 0) {
                    comments.forEach(function(comment) {
                        var html = "<div id=\"commentSection_" + comment.idComment + "\">"
                        html += "<div class=\"row\">"
                        html += "<textarea disabled=\"disabled\">" + comment.textComment + "</textarea> At " + formatDate(comment.datePosted) + " By <a href=\"/User/Details/" + user.idUser + "\">" + user.strUserName + "</a>";
                        replies.forEach(function(reply) {
                            if (comment.idComment == reply.idCommentParent){
                                html += "<button class=\"btn btn-link\" id=\"showReplies_" + comment.idComment + "\" onClick=\"showReplies(this.id)\">Show Replies</button>"
                                html += "<button class=\"btn btn-link\" id=\"hideReplies_" + comment.idComment + "\" hidden=\"hidden\" onClick=\"hideReplies(" + reply.idComment + ", this.id)\">Hide Replies</button>"
                            }
                            PaginationReplies[comment.idComment] = 0
                        })
                        html += "<button class=\"btn btn-link\" id=\"replyComment_" + comment.idComment + "\" onClick=\"replyComment(this.id)\">Reply</button>";
                        html += "<button class=\"btn btn-link\" id=\"postReply_" + comment.idComment + "\" onClick=\"postReply(this.id)\" hidden=\"hidden\">Post reply</button>";
                        html += "</div></div>"
                        $(".discussionComments").append(html)
                    })
                    $(".discussionComments").append("<div class=\"row\"><button class=\"btn btn-secondary loadMoreComments\">Load more Comments</button></div")
                    $(".loadMoreComments").click(loadMoreComments)
                    commentsPage += 1
                } else {
                    alert("There are no more comments on this discussion. Try refreshing later")
                }
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
                location.reload()
            } else {
                alert("Error on posting the reply. Try this later")
            }
        },
        error: function(error) {
            alert("Error: " + error)
        }
    })
}

function showReplies(idReply) {
    idReply = idReply.split("_")[1]
    $("#showReplies_" + idReply).attr("hidden", "hidden")
    $("#hideReplies_" + idReply).removeAttr("hidden")
    PaginationReplies[idReply] = PaginationReplies[idReply] != NaN ? 0 : PaginationReplies[idReply]
    $.ajax({
        url: "/Comment/GetReplies/?IdDiscussion=" + Id + "&&IdReply=" + idReply + "&&Pagination=" + PaginationReplies[idReply],
        type: "GET",
        success: function(data) {
            if (data.replies !== undefined && data.user !== undefined) {
                var replies = data.replies
                var user = data.user
                if (replies.length > 0) {
                    replies.forEach(function(reply) {
                        var html = "<div id=\"commentSection_" + reply.idComment + "\"><div class=\"row\"><textarea disabled=\"disabled\">" + reply.textComment + "</textarea><p> At " + formatDate(reply.datePosted) + " By <a href=\"/User/Details/" + user.idUser + "\">" + user.strUserName + "</a></p>"
                        html += "<button class=\"btn btn-link\" id=\"replyComment_" + reply.idComment + "\" onClick=\"replyComment(this.id)\">Reply</button>";
                        html += "<button class=\"btn btn-link\" id=\"postReply_" + reply.idComment + "\" onClick=\"postReply(this.id)\" hidden=\"hidden\">Post reply</button>";
                        html += "<button class=\"btn btn-link\" id=\"showReplies_" + reply.idComment + "\" onClick=\"showReplies(this.id)\">Show Replies</button>"
                        html += "<button class=\"btn btn-link\" id=\"hideReplies_" + reply.idComment + "\" hidden=\"hidden\" onClick=\"hideReplies(" + reply.IdCommentParent + ", this.id)\">Hide Replies</button>"
                        html += "</div></div>"
                        $("#commentSection_" + reply.idCommentParent).append(html)
                    })
                    PaginationReplies[idReply] += 1
                } else {
                    alert("Error on loading these replies. Try again later")
                }
            } else {
                alert("Error on loading these replies. Try again later")
            }
        },
        error: function(error) {
            alert("Error: " + error)
        }
    })
}

function hideReplies(idReply = -1, idCommentParent) {
    idCommentParent = idCommentParent.split("_")[1]
    //PaginationReplies[idCommentParent] -= 1
    $("#hideReplies_" + idCommentParent).attr("hidden", "hidden")
    $("#showReplies_" + idCommentParent).removeAttr("hidden")

    if (idReply != -1) {
        $("#commentSection_" + idReply).children().remove()
        $("#commentSection_" + idReply).remove()
    }
}
