

var GigsController = function (attendanceService) {
    var goingButton;

    var done = function () {
        var text = goingButton.text().trim() === "Going" ? "Going?" : "Going";
        goingButton.toggleClass("btn-default").toggleClass("btn-info").text(text);
    }

    var fail = function () {
        alert("Failed to remove attendance!");
    }

    var toogleAttendance = function (e) {
        goingButton = $(e.target);
        var gigId = goingButton.attr("data-gig-id");
        if (goingButton.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail);
        else
            attendanceService.deleteAttendance(gigId, done, fail);
    }

    var init = function (container) {
        $(container).on("click", ".jsToogleAttendace", toogleAttendance);
    };

    return {
        init: init
    }

}(AttendanceService);