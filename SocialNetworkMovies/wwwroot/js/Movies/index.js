$(document).ready(function() {
    imageUrl = "https://www.themoviedb.org/t/p/w220_and_h330_face"
    $.ajax({
        url: '/Movies/GetMoviesPopular',
        type: 'GET',
        success: function(data) {
            var content = JSON.parse(data.content)
            if (content.results.length > 0) {
                results = content.results
                results.forEach(function(element) {
                    $(".displayMovies").append("<a href=\"/Movies/MovieDetails/" + element.id + "\"><img src=\"" + imageUrl + element.backdrop_path + "\"></img></a>")
                })
            } else {
                alert("No movies were found. Try refreshing this page later");
            }
        },
        error: function(error) {
            alert("Error: " + error);
        }
    })

    $.ajax({
        url: "/Review/GetLastReviewsNews",
        type: "GET",
        success: function(data) {
            var content = JSON.parse(data)
            if (content.length > 0){
                var html = ""
                content.forEach(function(element) {
                    html += "<a href=\"/Review/Details/" + element.Id + "\">" + element.Text + " - Posted At " + formatDate(element.DatePosted) + "</a><br>"
                })
                $(".displayReviews").append(html);
            }
        },
        error: function(error) {
            alert("Error: " + error)
        }
    })

    $.ajax({
        url: "/Discussion/GetLastDiscussionsNews",
        type: "GET",
        success: function(data) {
            var content = JSON.parse(data)
            if (content.length > 0) {
                var html = ""
                content.forEach(function(element){
                    html += "<a href=\"/Discussion/Details/" + element.Id + "\">" + element.Text + " - Posted At " + formatDate(element.DatePosted) + "</a><br>"
                })
                $(".displayDiscussions").append(html)
            }
        },
        error: function(error) {
            alert("Error: " + error)
        }
    })
});

function formatDate(dateObj) {
    var formattedDate = new Date(dateObj);
    var d = formattedDate.getDate();
    var m = formattedDate.getMonth();
    m += 1;  // JavaScript months are 0-11
    var y = formattedDate.getFullYear();
    var formatedDate = d + "/" + m + "/" + y

    return formatedDate
}
