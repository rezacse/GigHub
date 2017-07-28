
var GigDetailsController = function (followingService) {
    var followButton;

    var done = function () {
        var text = followButton.text().trim() === "Follow" ? "Following" : "Follow";
        followButton.toggleClass("btn-info").toggleClass("btn-default").text(text);
    }

    var fail = function () {
        alert("Failed to remove attendance!");
    }

    var toogleFollowing = function (e) {
        followButton = $(e.target);
        var followeeId = followButton.attr("data-user-id");
        if (followButton.hasClass("btn-default"))
            followingService.createFollowing(followeeId, done, fail);
        else
            followingService.deleteFollowing(followeeId, done, fail);
    }

    var init = function (container) {
        $(".jsToogleFollow").click(toogleFollowing);
    };

    return {
        init: init
    }

}(FollowingService);