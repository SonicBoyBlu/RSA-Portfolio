app.controller('ActionItemsController', ['$scope', '$http', 'Upload', '$timeout', '$uibModal', function ($scope, $http, Upload, $timeout, $uibModal) {

    var ctl = this;
    ctl.actionItems = [];
    ctl.actionItemMessages = [];
    ctl.actionItemAttachments = [];
    ctl.loading = false;
    ctl.viewMode = 'list';
    ctl.selectedActionItem = {};
    ctl.reply = '';
    ctl.createMessage = '';
    ctl.createSubject = '';
    ctl.showClosedItems = false;

    ctl.loadActionItems = function () {
        ctl.loading = true;
        $http.get("/ActionItems/GetActionItems").
            then(function (response) {
                ctl.actionItems = response.data.ActionItems;
                setStatusIcons(ctl.actionItems);
                ctl.loading = false;
            })
            .catch(function (response) {
                handleJsonError(response);
                ctl.loading = false;
            });
    };

    ctl.getActionItemMessages = function (actionItem) {
        ctl.loadingMessages = true;
        ctl.selectedActionItem = actionItem;
        ctl.actionItemMessages = [];
        $http.get("/ActionItems/GetActionItemMessages?ticketId=" + actionItem.TicketId).
            then(function (response) {
                ctl.actionItemMessages = response.data.Messages;
                ctl.loadingMessages = false;
            })
            .catch(function (response) {
                handleJsonError(response);
                ctl.loadingMessages = false;
            });

        ctl.getActionItemAttachments(actionItem);
    };

    ctl.getActionItemAttachments = function (actionItem) {
        ctl.actionItemAttachments = [];
        $http.get("/ActionItems/GetActionItemAttachments?ticketId=" + actionItem.TicketId).
            then(function (response) {
                ctl.actionItemAttachments = response.data.Attachments;
            })
            .catch(function (response) {
                handleJsonError(response);
            });
    };

    ctl.createActionItem = function () {
        var modalInstance = $uibModal.open({
            templateUrl: "/ActionItems/NewActionItemModal",
            controller: "CreateActionItemController",
            size: 'lg'
        });

        modalInstance.result.then(function () {

        });
    };

    ctl.replyToActionItem = function (message) {

        var lastMessage = ctl.actionItemMessages[0];

        var replyMessage = {
            TicketId: ctl.selectedActionItem.TicketId,
            TicketNumber: ctl.selectedActionItem.TicketNumber,
            FromAddress: '',
            ToAddress: lastMessage.Recipient,
            CCAddress: '',
            Subject: lastMessage.Subject,
            Body: message,
            IsHtml: false,
            ToCustomer: false,
            SendEmail: false
        };

        Upload.upload({
            url: "/ActionItems/ReplyToActionItem",
            data: {
                files: ctl.selectedFiles,
                reply: replyMessage
            }
        }).then(function (response) {
            $timeout(function () {
                ctl.reply = '';
                ctl.selectedFiles = [];
                ctl.selectedActionItem.Active = response.data.ActionItem.Active;
                ctl.selectedActionItem.IsOpen = response.data.ActionItem.IsOpen;
                ctl.selectedActionItem.IsLocked = response.data.ActionItem.IsLocked;
                setStatusIcons(ctl.actionItems);
                ctl.viewMode = 'list';
            });
        }, function (response) {
                handleJsonError(response);
        });

    };

    ctl.showList = function (actionItem) {
        ctl.viewMode = 'list';
    };

    ctl.showMessage = function (actionItem) {
        ctl.getActionItemMessages(actionItem);
        ctl.viewMode = 'message';
    };

    ctl.selectFiles = function (files) {
        ctl.selectedFiles = files;
    };

    ctl.removeFile = function (file) {
        for (var i = 0; i < ctl.selectedFiles.length; i++) {
            if (ctl.selectedFiles[i].name === file.name) {
                ctl.selectedFiles.splice(i, 1);
            }
        }
    };

    var setStatusIcons = function (actionItems) {

        for (var i = 0; i < actionItems.length; i++) {
            var actionItem = actionItems[i];

            if (actionItem.IsLocked) {
                actionItem.Icon = "fas fa-user-lock fa-lg text-danger";
                actionItem.Title = "Closed and Locked";
                actionItem.Info = "Closed and Locked";
                actionItem.StatusClass = "closed";
            }
            else if (!actionItem.IsOpen) {
                actionItem.Icon = "fas fa-user-slash fa-lg text-danger";
                actionItem.Title = "Closed";
                actionItem.Info = "Closed";
                actionItem.StatusClass = "closed";
            }
            else if (actionItem.Active) {
                actionItem.Icon = "fas fa-user fa-lg text-success";
                actionItem.Title = "Active";
                actionItem.Info = actionItem.IsAgentOwner ? 'Active - assigned to you' : 'Waiting on agent - Active';
                actionItem.StatusClass = "active";
            }
            else {
                actionItem.Icon = "fas fa-user-clock fa-lg text-warning";
                actionItem.Title = "Waiting for you";
                actionItem.Info = actionItem.IsAgentOwner ? 'Waiting on customer' : 'Waiting on you';             
                actionItem.StatusClass = "waiting";
            }
        }
    };

 
    $scope.$on('actionitem-created', function (event, args) {
        ctl.actionItems.push(args.actionItem);
        setStatusIcons(ctl.actionItems);
    });



    ctl.loadActionItems();

}]);
