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
            var content = JSON.parse(data)
            if (content.length > 0) {
                $(".reviewDetails").append("<div class=\"row\"><textarea disabled=\"disabled\">" + content[0].Text + " " + content[0].Value + "/10*</textarea> - Posted at " + formatDate(content[0].DatePosted) + "</div>")

                IdMovie = content[0].IdMovie
                getMovie()
            } else {
                alert("Error on loading more comments. Try this later")
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
