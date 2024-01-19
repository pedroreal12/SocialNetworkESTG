$(document).ready(function() {
    $.ajax({
        type: "GET",
        url: "/UserList/GetUserLists",
        success: function(data) {
            var content = JSON.parse(data)
            if (content.length > 0) {
                var html = ""
                content.forEach(function(element) {
                    html += "<tr>"
                    html += "<td><a href=\"/MovieList/Details/" + element.IdList + "\">" + element.StrNameList + "</td>"
                    html += "<td>" + formatDate(element.DateCreated) + "</td>"
                    html += "<td><button id=\"" + element.IdList + "\" onclick=\"editList(this.id)\" type=\"button\" class=\"btn btn-link\">Edit</button></td>"
                    html += "<td><button id=\"" + element.IdList + "\" onclick=\"viewList(this.id)\" type=\"button\" class=\"btn btn-link\">Details</button></td>"
                    html += "<td><button id=\"" + element.IdList + "\" onclick=\"deleteList(this.id)\" type=\"button\" class=\"btn btn-link\">Delete</button></td>"
                    html += "</tr>"
                })
                $("#tableLists").append(html);
            } else {
                alert("No lists were created.")
            }
        },
        error: function(error) {
            alert("Error: " + error)
        }
    })
})

function deleteList(id) {
    $.ajax({
        url: "/UserList/Delete/" + id,
        type: "GET",
        success: function(data) {
            var content = JSON.parse(data)
            if (content.success) {
                alert("List deleted successfully!");
            } else {
                alert("Error on deleting the list");
            }
        },
        error: function(error) {
            alert("Error: " + error)
        }
    })
}

function editList(id) {
    window.location = "/UserList/Edit/" + id
}

function viewList(id) {
    window.location = "/UserList/Details/" + id
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
