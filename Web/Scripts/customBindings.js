function initBindings() {
    ko.bindingHandlers.fadeVisible = {
        update: function (element, valueAccessor) {
            // On update, fade in/out
            var shouldDisplay = valueAccessor();
            shouldDisplay ? $(element).fadeIn() : $(element).fadeOut();
        }
    };

    ko.bindingHandlers.jqButton = {
        init