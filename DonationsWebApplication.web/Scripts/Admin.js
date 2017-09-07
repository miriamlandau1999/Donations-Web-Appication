$(function () {
    var categoryId;
    filltable();    
    $(".table").on('click',".approve, .reject", function () {
        var IsAccepted;
        $(this).hasClass("approve") ? IsAccepted = true : IsAccepted = false;
        var id = $(this).data('id');
        $.post("/admin/applicationAction", { ApplicationId: id, IsAccepted: IsAccepted }, function () {
            filltable();
        });
    });
    $(".categories").change(function () {
        categoryId = $(this).val() == 0 ? null : $(this).val();
        filltable();
    });
    function filltable() {
        $("table tr:gt(0)").remove();
        $.get("/admin/GetPendingApplications", {categoryId: categoryId}, function (applications) {
            var html;
            applications.forEach(function (a) {
                html += `<tr>
                        <td><a href="/home/History?UserId=${a.userId}">${a.email}</td>
                        <td>${a.firstName}</td>
                        <td>${a.lastName}</td>
                        <td>${a.amount}</td>
                        <td>${a.category}</td>
                        <td><button class="btn btn-success approve" data-id="${a.id}">Approve</button> <button class="btn btn-danger reject" data-id="${a.id}">Reject</button></td>
                         </tr>`;
            });
            $(".table").append(html);
        });

    }
});