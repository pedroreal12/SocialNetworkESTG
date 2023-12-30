var Pagination = 0
$(document).ready(function() {
    loadReviews()
})

function loadReviews() {
    $("#loadReviews").remove()
    $.ajax({
        url: "/Review/GetReviews/?Pagination=" + Pagination,
        type: "GET",
        success: function(data) {
            var content = JSON.parse(data)
            if (content.length > 0) {
                content.forEach(function(element) {
                    $(".listReviews").append("<a href=\"/Review/Details/" + element.Id + "\">" + element.Text + " - Posted at " + element.DatePosted + "</a><br>")
                })
                $(".listReviews").append("<button class=\"btn btn-link\" onClick=\"loadReviews()\" id=\"loadReviews\">Load more reviews</button>")
                Pagination += 1
            } else {
                alert("Error on loading more comments. Try this later")
            }
        },
        error: function(error) {
            alert("Error: " + error)
        }
    })
}
