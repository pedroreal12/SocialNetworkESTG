$(document).ready(function() {
    var url = window.location.href;
    url = url.split("/");
    Id = url[5];

    imageUrl = "https://www.themoviedb.org/t/p/w220_and_h330_face"

    $.ajax({
        url: '/Movies/GetMovieId/' + Id,
        type: 'GET',
        success: function(data) {
            var content = JSON.parse(data.content)
            if (content.adult != undefined) {
                $(".movieDetails").append("<a href=\"" + content.homepage + "\"><img src=\"" + imageUrl + content.backdrop_path + "\"></img></a>")
                $("#title").html("<a href=\"" + content.homepage + "\"<h4 id=\"title\">" + content.original_title + "</h4></a>")
                $(".movieDetails").append("<p>Overview " + content.overview + "</p>")
                $(".movieDetails").append("<p>Release Date " + content.release_date + "</p>")
                if (content.adult) {
                    $(".movieDetails").append("<p>Rated R Movie</p>")
                } else {
                    $(".movieDetails").append("<p>Movie for the whole family</p>")
                }
                content.genres.forEach(function(element) {
                    $(".movieDetails").append("<p>Genres " + element.name + "</p>")
                })
                if (content.belongs_to_collection != null) {
                    $(".movieDetails").append("<p>Collections Name " + content.belongs_to_collection.name + "</p>")
                }
                content.production_companies.forEach(function(element) {
                    $(".movieDetails").append("<p>Production Companie Name " + element.name + "</p>")
                })
                content.production_countries.forEach(function(element) {
                    $(".movieDetails").append("<p>Production Country " + element.name + "</p>")
                })
                $(".movieDetails").append("<p>Budget " + content.budget + " US$</p>")
                $(".movieDetails").append("<p>Revenue " + content.revenue + " US$</p>")
                $(".movieDetails").append("<p>Vote Average " + content.vote_average + "</p>")
                $(".movieDetails").append("<p>Vote Count " + content.vote_count + "</p>")
            } else {
                alert("No movies were found. Try refreshing this page later");
            }
        },
        error: function(error) {
            alert("Error: " + error);
        }
    })

    $("#addReview").click(function() {
        var html = "<br><div class=\"row\"><textarea id=\"commentText\"></textarea></div><div class=\"btn-group\">"
        for (let i = 1; i <= 10; i++) {
            html += "<button class=\"btn btn-link\"id=\"star_" + i + "\" onClick=\"postReview(this.id)\">" + i + "*</button>"
        }
        html += "<button class=\"btn btn-danger\" id=\"cancelReview\">Cancel Review</button>"
        html += "</div>"
        $(".actions").append(html)
        $("#addReview").attr("hidden", "hidden")
        $("#cancelReview").click(function() {
            $(".btn-group").children().remove()
            $(".btn-group").remove()
            $(".actions").children("br").remove()
            $("#commentText").remove()
            $("#cancelReview").attr("hidden", "hidden")
            $("#addReview").removeAttr("hidden")
        })
    })

    $("#addToUserList").click(function() {
        $.ajax({
            url: "/UserList/GetListsByUser",
            type: "GET",
            success: function(data) {
                if (data.length > 0) {
                    $("#userList").children().remove();
                    data.forEach(function(element) {
                        $("#userList").append("<option value=\"" + element.idUserList + "\">" + element.strListName + "</option>");
                    });
                    $("#addToUserList").attr("hidden", "hidden");
                    $("#userList").removeAttr("hidden");
                    $("#cancelUserList").removeAttr("hidden");
                    $("#submitToUserList").removeAttr("hidden", "hidden")
                } else {
                    alert("No lists were found, go to Lists and create a new list to use this feature.");
                }
            },
            error: function(error) {
                alert("Error: " + error)
            }
        })
    });

    $("#cancelUserList").click(function() {
        $("#addToUserList").removeAttr("hidden")
        $("#userList").attr("hidden", "hidden")
        $("#cancelUserList").attr("hidden", "hidden")
        $("#submitToUserList").attr("hidden", "hidden")
    })

    $("#submitToUserList").click(function() {
        $.ajax({
            url: "/MovieList/AddMovieToUserList/?IdUserList=" + $("#userList").val() + "&IdMovie=" + Id,
            type: "GET",
            success: function(data) {
                var content = JSON.parse(data);
                if (content.success) {
                    alert("Added Movie to list successfully!");
                    location.reload()
                } else {
                    alert("Error on adding Movie to the list");
                }
            },
            error: function(error) {
                alert("Error: " + error)
            }
        })
    })
});

function isCommentFullfilled() {
    if ($("#commentText").val() == "") {
        return false
    }
    return true
}

function postReview(valueReview) {
    valueReview = valueReview.split("_")[1]
    if (!isCommentFullfilled()) {
        return alert("Please, provide a comment for the review")
    }
    data = {
        TextComment: $("#commentText").val()
    }
    $.ajax({
        url: "/Comment/PostComment",
        type: "POST",
        data: data,
        success: function(data) {
            var content = JSON.parse(data)
            if (content.success) {
                $.ajax({
                    url: "/Review/PostReview/?Value=" + valueReview + "&&IdMovie=" + Id + "&&FkIdComment=" + content.commentId,
                    type: "GET",
                    success: function(data) {
                        var content = JSON.parse(data)
                        if (content.success) {
                            alert("Review Posted successfully!")
                            location.reload()
                        } else {
                            alert("Error on posting review")
                        }
                    },
                    error: function(error) {
                        alert("Error: " + error)
                    }
                })
            } else {
                alert("Error on posting the Review's comment. Try again later")
            }
        },
        error: function() {
            alert("Error: " + error)
        }
    })

}

