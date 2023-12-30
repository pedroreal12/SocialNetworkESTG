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
                    $(".listReviews").append("<a href=\"/Review/Details/" + element.Id + "\">" + element.Text + " - Posted at " + formatDate(element.DatePosted) + "</a><br>")
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

function formatDate(dateObj) {
    var formattedDate = new Date(dateObj);
    var d = formattedDate.getDate();
    var m = formattedDate.getMonth();
    m += 1;  // JavaScript months are 0-11
    var y = formattedDate.getFullYear();
    var formatedDate = d + "/" + m + "/" + y

    return formatedDate
}
