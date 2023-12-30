$(document).ready(function() {
    var url = window.location.href;
    url = url.split("/");
    Id = url[5];

    getDetails()
})

function getDetails() {
    $.ajax({
        url: "/Review/GetDetails/?Id=" + Id,
        type: "GET",
        success: function(data) {
            var content = JSON.parse(data)
            if (content.length > 0) {
                $(".reviewDetails").append("<div class=\"row\"><textarea disabled=\"disabled\">" + content[0].Text + " " + content[0].Value  + "/10*</textarea> - Posted at " + formatDate(content[0].DatePosted) + "</div>")
            } else {
                alert("Error on loading more comments. Try this later")
            }
        },
        error: function(error) {
            alert("Error: " + error)
        }
    })
}

function formatDate(dateObj){
    var formattedDate = new Date(dateObj);
    var d = formattedDate.getDate();
    var m =  formattedDate.getMonth();
    m += 1;  // JavaScript months are 0-11
    var y = formattedDate.getFullYear();
    var formatedDate = d + "/" + m  + "/" + y

    return formatedDate
}
