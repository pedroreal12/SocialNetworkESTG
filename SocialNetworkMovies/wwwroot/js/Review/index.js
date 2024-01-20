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
            if (data.reviews !== undefined && data.user !== undefined) {
                var reviews = data.reviews
                var user = data.user
                if (reviews.length > 0) {
                    reviews.forEach(function(element) {
                        $(".listReviews").append("<a href=\"/Review/Details/" + element.id + "\">" + element.text + " - Posted at " + formatDate(element.datePosted) + "</a> By <a href=\"/User/Details/" + user.idUser + "\">" + user.strUserName + "</a><br>")
                    })
                    $(".listReviews").append("<button class=\"btn btn-link\" onClick=\"loadReviews()\" id=\"loadReviews\">Load more reviews</button>")
                    Pagination += 1
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
