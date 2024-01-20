var IdMovie = 0
$(document).ready(function() {
    var url = window.location.href;
    url = url.split("/");
    Id = url[5];
    imageUrl = "https://www.themoviedb.org/t/p/w220_and_h330_face"

    getDetails()
})

function getMovie() {
    $.ajax({
        url: "/Movies/GetMovieId/?Id=" + IdMovie,
        type: "GET",
        success: function(data) {
            var content = JSON.parse(data.content)
            if (content.adult != undefined) {
                var html = ("<div class=\"row\"><a href=\"/Movies/MovieDetails/" + content.id + "\">" + content.original_title + "</a>")
                html += "</div>"
                html += ("<img src=\"" + imageUrl + content.backdrop_path + "\"></img>")
                $(".movieInformation").append(html)
            } else {
                alert("Error on loading movie. Try this later")
            }
        },
        error: function(error) {
            alert("Error: " + error)
        }
    })
}

function getDetails() {
    $.ajax({
        url: "/Review/GetDetails/?Id=" + Id,
        type: "GET",
        success: function(data) {
            if (data.user !== undefined && data.review !== undefined) {
                var user = data.user
                var review = data.review
                if (review.length > 0) {
                    $(".reviewDetails").append("<div class=\"row\"><textarea disabled=\"disabled\">" + review[0].text + " " + review[0].value + "/10*</textarea> <p>- Posted at " + formatDate(review[0].datePosted) + " By <a href=\"/User/Details/" + user.idUser+ "\">" + user.strUserName + "</a></p></div>")

                    IdMovie = review[0].idMovie
                    getMovie()
                } else {
                    alert("Error on loading more comments. Try this later")
                }
            }
        },
        error: function(error) {
            alert("Error: " + error)
        }
    })
}

function formatDate(dateObj) {
    var formattedDate = new Date(dateObj);
    var d = formattedDate.getDate();
    var m = formattedDate.getMonth();
    m += 1;  // JavaScript months are 0-11
    var y = formattedDate.getFullYear();
    var formatedDate = d + "/" + m + "/" + y

    return formatedDate
}
